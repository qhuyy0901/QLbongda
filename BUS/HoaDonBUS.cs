using BUS;
using DAL;
using Microsoft.Reporting.WinForms;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection; // Thư viện quan trọng để xử lý dữ liệu động
using System.Reflection.Emit;
using System.Windows.Forms;

namespace TrangChu
{
    public partial class TKe_HoaDon : Form
    {
        private HoaDonBUS busHoaDon = new HoaDonBUS();

        // Dùng List<object> để lưu dữ liệu tạm thời
        private List<object> currentData;

        public TKe_HoaDon()
        {
            InitializeComponent();
        }

        private void HoaDon_Load(object sender, EventArgs e)
        {
            try
            {
                // Mặc định xem 30 ngày gần nhất
                dtpFromDate.Value = DateTime.Now.AddDays(-30);
                dtpToDate.Value = DateTime.Now;
                LoadData();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khởi động form: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ===== HÀM TẢI DỮ LIỆU CHÍNH =====
        private void LoadData()
        {
            try
            {
                // Lấy dữ liệu gốc từ BUS (Trả về List<dynamic>)
                var data = busHoaDon.GetHoaDonWithCustomerInfo();

                if (data == null || data.Count == 0)
                {
                    currentData = new List<object>();
                    MessageBox.Show("⚠️ Không có dữ liệu hóa đơn!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    // Chuyển đổi sang List<object> để lưu trữ
                    currentData = data.Cast<object>().ToList();
                }

                LoadReport(); // Đẩy dữ liệu lên báo cáo
                TinhTongTien(); // Tính tổng tiền hiển thị lên Label
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải dữ liệu: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ===== CẤU HÌNH REPORT VIEWER =====
        private void LoadReport()
        {
            try
            {
                // 1. Đường dẫn file báo cáo .rdlc
                string reportPath = System.IO.Path.Combine(
                    System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location),
                    "Report_HoaDon.rdlc");

                reportViewer1.LocalReport.ReportPath = reportPath;
                reportViewer1.LocalReport.DataSources.Clear();

                // 2. Chuyển đổi dữ liệu Dynamic sang cấu trúc Phẳng (Anonymous Type)
                // Bước này QUAN TRỌNG để ReportViewer hiểu được dữ liệu
                var reportData = currentData.Select(x => new
                {
                    MaHD = GetPropValue(x, "MaHD"),
                    MaLich = GetPropValue(x, "MaLich"),
                    HinhThucTT = GetPropValue(x, "HinhThucTT"),
                    ThoiGianThanhToan = (DateTime?)x.GetType().GetProperty("ThoiGianThanhToan")?.GetValue(x, null),
                    TongTien = Convert.ToDecimal(x.GetType().GetProperty("TongTien")?.GetValue(x, null) ?? 0)
                }).ToList();

                // 3. Gán vào DataSet1 (Tên này phải trùng với tên Dataset trong file thiết kế RDLC)
                ReportDataSource rds = new ReportDataSource("DataSet1", reportData);
                reportViewer1.LocalReport.DataSources.Add(rds);

                // 4. Làm mới báo cáo
                reportViewer1.RefreshReport();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi hiển thị báo cáo: " + ex.Message + "\n\n💡 Gợi ý: Kiểm tra xem file Report_HoaDon.rdlc có trong thư mục bin/Debug chưa.",
                    "Lỗi Report", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ===== HÀM HỖ TRỢ LẤY GIÁ TRỊ TỪ OBJECT ĐỘNG (REFLECTION) =====
        private string GetPropValue(object src, string propName)
        {
            if (src == null) return "";
            var val = src.GetType().GetProperty(propName)?.GetValue(src, null);
            return val?.ToString() ?? "";
        }

        // ===== SỰ KIỆN LỌC THEO NGÀY =====
        private void btnLoc_Click(object sender, EventArgs e)
        {
            try
            {
                DateTime tuNgay = dtpFromDate.Value.Date;
                DateTime denNgay = dtpToDate.Value.Date.AddDays(1).AddSeconds(-1); // 23:59:59

                // Lấy tất cả dữ liệu rồi lọc
                var allData = busHoaDon.GetHoaDonWithCustomerInfo();

                currentData = new List<object>();

                foreach (var item in allData)
                {
                    // Lấy ngày thanh toán an toàn
                    DateTime? ngayTT = (DateTime?)item.GetType().GetProperty("ThoiGianThanhToan")?.GetValue(item, null);

                    if (ngayTT.HasValue && ngayTT.Value >= tuNgay && ngayTT.Value <= denNgay)
                    {
                        currentData.Add(item);
                    }
                }

                if (currentData.Count == 0)
                {
                    MessageBox.Show($"⚠️ Không tìm thấy hóa đơn từ {tuNgay:dd/MM} đến {denNgay:dd/MM}",
                        "Kết quả lọc", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                LoadReport();
                TinhTongTien();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi lọc: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ===== SỰ KIỆN TÌM KIẾM =====
        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            try
            {
                string keyword = txtSearch.Text.Trim();

                if (string.IsNullOrWhiteSpace(keyword))
                {
                    MessageBox.Show("❌ Vui lòng nhập từ khóa tìm kiếm!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtSearch.Focus();
                    return;
                }

                var allData = busHoaDon.GetHoaDonWithCustomerInfo();
                currentData = new List<object>();

                foreach (var item in allData)
                {
                    string maHD = GetPropValue(item, "MaHD");
                    string maLich = GetPropValue(item, "MaLich");

                    // Tìm theo Mã HĐ hoặc Mã Lịch (Không phân biệt hoa thường)
                    if (maHD.IndexOf(keyword, StringComparison.OrdinalIgnoreCase) >= 0 ||
                        maLich.IndexOf(keyword, StringComparison.OrdinalIgnoreCase) >= 0)
                    {
                        currentData.Add(item);
                    }
                }

                if (currentData.Count == 0)
                {
                    MessageBox.Show($"⚠️ Không tìm thấy kết quả nào chứa '{keyword}'",
                        "Kết quả tìm kiếm", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                LoadReport();
                TinhTongTien();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tìm kiếm: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ===== NÚT TẢI LẠI (RESET) =====
        private void btnTaiLai_Click(object sender, EventArgs e)
        {
            try
            {
                txtSearch.Clear();
                dtpFromDate.Value = DateTime.Now.AddDays(-30);
                dtpToDate.Value = DateTime.Now;
                LoadData();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải lại: " + ex.Message);
            }
        }

        // ===== TÍNH TỔNG TIỀN HIỂN THỊ =====
        private void TinhTongTien()
        {
            try
            {
                decimal tong = 0;
                if (currentData != null)
                {
                    foreach (var item in currentData)
                    {
                        var val = item.GetType().GetProperty("TongTien")?.GetValue(item, null);
                        tong += Convert.ToDecimal(val ?? 0);
                    }
                }
                lblTongDoanhThu.Text = tong.ToString("N0") + " VNĐ";
            }
            catch
            {
                lblTongDoanhThu.Text = "0 VNĐ";
            }
        }

        // ===== XEM CHI TIẾT HÓA ĐƠN =====
        private void btnXemChiTiet_Click(object sender, EventArgs e)
        {
            try
            {
                if (currentData == null || currentData.Count == 0)
                {
                    MessageBox.Show("❌ Không có dữ liệu để xem!", "Cảnh báo");
                    return;
                }

                // Gọi hàm tự viết PromptForMaHD để lấy mã hóa đơn
                string maHD = PromptForMaHD();

                if (string.IsNullOrWhiteSpace(maHD)) return;

                var chiTietList = busHoaDon.GetChiTietHoaDon(maHD);

                if (chiTietList == null || chiTietList.Count == 0)
                {
                    MessageBox.Show($"⚠️ Hóa đơn [{maHD}] không tìm thấy hoặc không có dịch vụ nào!", "Thông báo");
                    return;
                }

                // Tạo chuỗi thông tin hiển thị
                string info = $"🧾 CHI TIẾT HÓA ĐƠN: {maHD}\n";
                info += "══════════════════════════\n";

                decimal tongTien = 0;
                foreach (var ct in chiTietList)
                {
                    info += $"📦 DV: {ct.MaDV} | SL: {ct.SoLuong}\n";
                    info += $"💰 Thành tiền: {ct.ThanhTien:N0} VNĐ\n";
                    info += "--------------------------\n";

                    if (ct.ThanhTien.HasValue) tongTien += ct.ThanhTien.Value;
                }

                info += $"💎 TỔNG DỊCH VỤ: {tongTien:N0} VNĐ";

                MessageBox.Show(info, "Chi Tiết Hóa Đơn", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi xem chi tiết: " + ex.Message);
            }
        }

        // ===== XUẤT EXCEL / PDF =====
        private void btnInHoaDon_Click(object sender, EventArgs e)
        {
            try
            {
                SaveFileDialog saveDialog = new SaveFileDialog();
                saveDialog.Filter = "PDF (*.pdf)|*.pdf|Excel (*.xls)|*.xls";
                saveDialog.Title = "Xuất Báo Cáo Doanh Thu";

                if (saveDialog.ShowDialog() == DialogResult.OK)
                {
                    string mimeType, encoding, extension;
                    string[] streamIds;
                    Warning[] warnings;

                    string format = saveDialog.FilterIndex == 1 ? "PDF" : "Excel";

                    byte[] bytes = reportViewer1.LocalReport.Render(
                        format, null, out mimeType, out encoding, out extension,
                        out streamIds, out warnings);

                    System.IO.File.WriteAllBytes(saveDialog.FileName, bytes);

                    if (MessageBox.Show("✅ Xuất thành công! Bạn có muốn mở file ngay không?", "Thành công",
                        MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        System.Diagnostics.Process.Start(saveDialog.FileName);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi xuất file: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ===== HÀM TỰ VIẾT ĐỂ NHẬP MÃ HÓA ĐƠN (Thay thế Interaction.InputBox) =====
        private string PromptForMaHD()
        {
            using (Form prompt = new Form())
            {
                prompt.Width = 400;
                prompt.Height = 180;
                prompt.Text = "Tra Cứu Chi Tiết";
                prompt.StartPosition = FormStartPosition.CenterScreen;
                prompt.FormBorderStyle = FormBorderStyle.FixedDialog;
                prompt.MaximizeBox = false;
                prompt.MinimizeBox = false;

                Label textLabel = new Label() { Left = 20, Top = 20, Text = "Nhập mã hóa đơn cần xem:", AutoSize = true, Font = new System.Drawing.Font("Segoe UI", 10) };
                TextBox inputBox = new TextBox() { Left = 20, Top = 50, Width = 340, Font = new System.Drawing.Font("Segoe UI", 10) };

                Button confirmation = new Button() { Text = "Tra Cứu", Left = 190, Width = 80, Top = 90, DialogResult = DialogResult.OK, BackColor = System.Drawing.Color.DodgerBlue, ForeColor = System.Drawing.Color.White, FlatStyle = FlatStyle.Flat };
                Button cancel = new Button() { Text = "Hủy", Left = 280, Width = 80, Top = 90, DialogResult = DialogResult.Cancel, BackColor = System.Drawing.Color.Gray, ForeColor = System.Drawing.Color.White, FlatStyle = FlatStyle.Flat };

                prompt.Controls.Add(textLabel);
                prompt.Controls.Add(inputBox);
                prompt.Controls.Add(confirmation);
                prompt.Controls.Add(cancel);
                prompt.AcceptButton = confirmation; // Nhấn Enter để xác nhận
                prompt.CancelButton = cancel;       // Nhấn Esc để hủy

                return prompt.ShowDialog() == DialogResult.OK ? inputBox.Text.Trim() : "";
            }
        }

        // ===== CHUYỂN FORM =====
        private void thốngKêSânToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ThongKeSan frm = new ThongKeSan();
            this.Hide();
            frm.ShowDialog();
            this.Show();
        }

        private void thốngKêToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ThongKeDoanhThu frm = new ThongKeDoanhThu();
            this.Hide();
            frm.ShowDialog();
            this.Show();
        }
    }
}