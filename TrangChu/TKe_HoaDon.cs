using BUS;
using DAL;
using Microsoft.Reporting.WinForms;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection; // Dùng Reflection để xử lý dynamic
using System.Windows.Forms;

namespace TrangChu
{
    public partial class TKe_HoaDon : Form
    {
        private HoaDonBUS busHoaDon = new HoaDonBUS();

        // Lưu trữ dữ liệu hiện tại đang hiển thị
        private List<object> currentData = new List<object>();

        public TKe_HoaDon()
        {
            InitializeComponent();
        }

        private void TKe_HoaDon_Load(object sender, EventArgs e)
        {
            try
            {
                // 1. Khởi tạo Combobox Năm (5 năm gần nhất)
                int currentYear = DateTime.Now.Year;
                cboNam.Items.Clear();
                for (int i = currentYear; i >= currentYear - 5; i--)
                {
                    cboNam.Items.Add(i);
                }
                cboNam.SelectedIndex = 0; // Mặc định chọn năm nay

                // 2. Khởi tạo Combobox Tháng
                cboThang.Items.Clear();
                cboThang.Items.Add("Tất cả"); // Index 0
                for (int i = 1; i <= 12; i++)
                {
                    cboThang.Items.Add(i);
                }
                cboThang.SelectedIndex = DateTime.Now.Month; // Mặc định chọn tháng hiện tại

                // 3. Tải dữ liệu ban đầu
                LoadDataAndRefreshReport();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khởi động form: " + ex.Message);
            }
        }

        // ==========================================================
        // 1. HÀM TẢI DỮ LIỆU VÀ LỌC (LOGIC CHÍNH)
        // ==========================================================
        private void LoadDataAndRefreshReport()
        {
            try
            {
                int nam = int.Parse(cboNam.SelectedItem.ToString());
                int thang = cboThang.SelectedIndex; // 0 là Tất cả

                // Lấy toàn bộ dữ liệu từ BUS
                var rawData = busHoaDon.GetHoaDonWithCustomerInfo();

                currentData = new List<object>();

                if (rawData != null && rawData.Count > 0)
                {
                    foreach (var item in rawData)
                    {
                        DateTime? ngayTT = GetDateValue(item, "ThoiGianThanhToan");

                        // Logic lọc: Năm phải trùng, Tháng nếu chọn "Tất cả" thì bỏ qua
                        if (ngayTT.HasValue && ngayTT.Value.Year == nam)
                        {
                            // Nếu tháng = 0 (Tất cả) thì bỏ qua điều kiện tháng
                            if (thang == 0 || ngayTT.Value.Month == thang)
                            {
                                currentData.Add(item);
                            }
                        }
                    }
                }

                DisplayReport();
                CalculateTotalRevenue();

                if (currentData.Count == 0)
                {
                    string thangText = thang == 0 ? $"năm {nam}" : $"tháng {thang}/{nam}";
                    MessageBox.Show($"⚠️ Không tìm thấy hóa đơn nào trong {thangText}!", "Thông báo");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("❌ Lỗi lọc dữ liệu:\n" + ex.Message, "Lỗi");
            }
        }

        // ==========================================================
        // 2. HÀM HIỂN THỊ REPORT (QUAN TRỌNG)
        // ==========================================================
        private void DisplayReport()
        {
            try
            {
                string reportPath = System.IO.Path.Combine(
                    System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location),
                    "Report_HoaDon.rdlc");

                if (!System.IO.File.Exists(reportPath))
                {
                    MessageBox.Show($"❌ File báo cáo không tìm thấy:\n{reportPath}", 
                        "Lỗi File", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                reportViewer1.LocalReport.ReportPath = reportPath;
                reportViewer1.LocalReport.DataSources.Clear();

                // Chuyển đổi dữ liệu sang anonymous type (PHẢI khớp với Fields trong RDLC)
                var reportDataSource = currentData.Select(x => new
                {
                    MaHD = GetStringValue(x, "MaHD") ?? "",
                    MaLich = GetStringValue(x, "MaLich") ?? "",
                    HinhThucTT = GetStringValue(x, "HinhThucTT") ?? "",
                    ThoiGianThanhToan = GetDateValue(x, "ThoiGianThanhToan"),
                    TongTien = GetDecimalValue(x, "TongTien")
                }).ToList();

                // Tên "DataSet1" PHẢI trùng với DataSet name trong RDLC
                ReportDataSource rds = new ReportDataSource("DataSet1", reportDataSource);
                reportViewer1.LocalReport.DataSources.Add(rds);

                reportViewer1.RefreshReport();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"❌ Lỗi hiển thị Report:\n{ex.Message}\n\n📍 StackTrace:\n{ex.StackTrace}", 
                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ==========================================================
        // 3. CÁC HÀM SỰ KIỆN (BUTTON CLICK)
        // ==========================================================

        private void btnLoc_Click(object sender, EventArgs e)
        {
            LoadDataAndRefreshReport();
        }

        private void btnTaiLai_Click(object sender, EventArgs e)
        {
            // Reset bộ lọc về hiện tại
            cboNam.SelectedIndex = 0;
            cboThang.SelectedIndex = DateTime.Now.Month;
            LoadDataAndRefreshReport();
        }

        private void btnXuatPDF_Click(object sender, EventArgs e)
        {
            ExportReport("PDF");
        }

        private void btnXuatExcel_Click(object sender, EventArgs e)
        {
            ExportReport("Excel");
        }

        private void ExportReport(string format)
        {
            try
            {
                SaveFileDialog saveDialog = new SaveFileDialog();
                saveDialog.Filter = format == "PDF" ? "PDF Files (*.pdf)|*.pdf" : "Excel Files (*.xls)|*.xls";
                saveDialog.FileName = $"BaoCao_HoaDon_{DateTime.Now:ddMMyyyy}";

                if (saveDialog.ShowDialog() == DialogResult.OK)
                {
                    Warning[] warnings;
                    string[] streamIds;
                    string mimeType, encoding, extension;

                    byte[] bytes = reportViewer1.LocalReport.Render(
                        format, null, out mimeType, out encoding, out extension,
                        out streamIds, out warnings);

                    System.IO.File.WriteAllBytes(saveDialog.FileName, bytes);

                    if (MessageBox.Show("✅ Xuất file thành công! Bạn có muốn mở file ngay không?", "Thông báo",
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

        private void btnTroVe_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnXemChiTiet_Click(object sender, EventArgs e)
        {
            string maHD = PromptForMaHD();
            if (string.IsNullOrWhiteSpace(maHD)) return;

            var chiTietList = busHoaDon.GetChiTietHoaDon(maHD);
            if (chiTietList == null || chiTietList.Count == 0)
            {
                MessageBox.Show("⚠️ Không tìm thấy chi tiết hóa đơn này!", "Thông báo");
                return;
            }

            string info = $"🧾 CHI TIẾT HÓA ĐƠN: {maHD}\n══════════════════════════\n";
            decimal tongDV = 0;
            foreach (var ct in chiTietList)
            {
                string tenDV = ct.DichVu?.TenDV ?? ct.MaDV;
                info += $"📦 {tenDV} | SL: {ct.SoLuong} | {ct.ThanhTien:N0} VNĐ\n";
                if (ct.ThanhTien.HasValue) tongDV += ct.ThanhTien.Value;
            }
            info += $"══════════════════════════\n💎 TỔNG DỊCH VỤ: {tongDV:N0} VNĐ";

            MessageBox.Show(info, "Chi Tiết", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        // ==========================================================
        // 4. CÁC HÀM HỖ TRỢ (HELPER)
        // ==========================================================

        private void CalculateTotalRevenue()
        {
            try
            {
                decimal total = 0;
                if (currentData != null)
                {
                    foreach (var item in currentData)
                    {
                        total += GetDecimalValue(item, "TongTien");
                    }
                }
                lblTongDoanhThu.Text = total.ToString("N0") + " VNĐ";
            }
            catch
            {
                lblTongDoanhThu.Text = "0 VNĐ";
            }
        }

        // Reflection Helper
        private string GetStringValue(object obj, string propName)
        {
            if (obj == null) return "";
            var val = obj.GetType().GetProperty(propName)?.GetValue(obj, null);
            return val?.ToString() ?? "";
        }

        private decimal GetDecimalValue(object obj, string propName)
        {
            if (obj == null) return 0;
            var val = obj.GetType().GetProperty(propName)?.GetValue(obj, null);
            return Convert.ToDecimal(val ?? 0);
        }

        private DateTime? GetDateValue(object obj, string propName)
        {
            if (obj == null) return null;
            var val = obj.GetType().GetProperty(propName)?.GetValue(obj, null);
            return (DateTime?)val;
        }

        // Hộp thoại nhập mã (Thay thế InputBox VB)
        private string PromptForMaHD()
        {
            using (Form prompt = new Form())
            {
                prompt.Width = 350; prompt.Height = 160;
                prompt.Text = "Tra Cứu Chi Tiết";
                prompt.StartPosition = FormStartPosition.CenterScreen;
                prompt.FormBorderStyle = FormBorderStyle.FixedDialog;
                prompt.MaximizeBox = false; prompt.MinimizeBox = false;

                Label lbl = new Label() { Left = 20, Top = 20, Text = "Nhập mã hóa đơn:", AutoSize = true, Font = new System.Drawing.Font("Segoe UI", 10) };
                TextBox txt = new TextBox() { Left = 20, Top = 50, Width = 290, Font = new System.Drawing.Font("Segoe UI", 10) };
                Button btnOK = new Button() { Text = "Xem", Left = 120, Top = 85, Width = 80, DialogResult = DialogResult.OK, BackColor = System.Drawing.Color.DodgerBlue, ForeColor = System.Drawing.Color.White };

                prompt.Controls.Add(lbl); prompt.Controls.Add(txt); prompt.Controls.Add(btnOK);
                prompt.AcceptButton = btnOK;

                return prompt.ShowDialog() == DialogResult.OK ? txt.Text.Trim() : "";
            }
        }
    }
}