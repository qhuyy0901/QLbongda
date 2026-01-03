using BUS;
using DAL;
using Microsoft.Reporting.WinForms;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO; 
using System.Linq;
using System.Reflection; 
using System.Windows.Forms;

namespace TrangChu
{
    public partial class TKe_HoaDon : Form
    {
        private HoaDonBUS busHoaDon = new HoaDonBUS();

        // CLASS DTO ĐỂ HỨNG DỮ LIỆU (QUAN TRỌNG NHẤT)
        
        public class ReportDataStruct
        {
            public string MaHD { get; set; }
            public string MaLich { get; set; }
            public string HinhThucTT { get; set; }
            public DateTime ThoiGianThanhToan { get; set; }
            public decimal TongTien { get; set; }
        }

        // List chứa dữ liệu chuẩn
        private List<ReportDataStruct> listReportData = new List<ReportDataStruct>();

        public TKe_HoaDon()
        {
            InitializeComponent();
        }

        private void TKe_HoaDon_Load(object sender, EventArgs e)
        {
            try
            {
                int currentYear = DateTime.Now.Year;
                cboNam.Items.Clear();
                cboNam.Items.Add("Tất cả");
                for (int i = currentYear; i >= currentYear - 2; i--) cboNam.Items.Add(i);
                cboNam.SelectedIndex = 1;

                cboThang.Items.Clear();
                cboThang.Items.Add("Tất cả");
                for (int i = 1; i <= 12; i++) cboThang.Items.Add(i);
                cboThang.SelectedIndex = DateTime.Now.Month;

                LoadDataAndRefreshReport();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khởi động: " + ex.Message);
            }
        }



        // HÀM TẢI DỮ LIỆU & LỌC
        private void LoadDataAndRefreshReport()
        {
            try
            {
                var selectedYear = cboNam.SelectedItem?.ToString();
                bool allYears = selectedYear == "Tất cả";
                int nam = allYears ? DateTime.Now.Year : int.Parse(selectedYear);

                int thang = cboThang.SelectedIndex; 

                // Lấy dữ liệu thô từ BUS
                var rawData = busHoaDon.GetHoaDonWithCustomerInfo();

                listReportData.Clear(); 

                if (rawData != null)
                {
                    foreach (var item in rawData)
                    {
                        DateTime? ngayTT = GetPropValue<DateTime?>(item, "ThoiGianThanhToan");

                        if (ngayTT.HasValue && (allYears || ngayTT.Value.Year == nam))
                        {
                            if (thang == 0 || ngayTT.Value.Month == thang)
                            {
                                listReportData.Add(new ReportDataStruct
                                {
                                    MaHD = GetPropValue<string>(item, "MaHD") ?? "",
                                    MaLich = GetPropValue<string>(item, "MaLich") ?? "",
                                    HinhThucTT = GetPropValue<string>(item, "HinhThucTT") ?? "",
                                    ThoiGianThanhToan = ngayTT.Value,
                                    TongTien = GetPropValue<decimal>(item, "TongTien")
                                });
                            }
                        }
                    }
                }

                DisplayReport();

                // Tính tổng tiền
                decimal total = listReportData.Sum(x => x.TongTien);
                lblTongDoanhThu.Text = total.ToString("N0") + " VNĐ";

                if (listReportData.Count == 0)
                {
                    MessageBox.Show($"⚠️ Không có hóa đơn nào trong tháng {thang}/{(allYears ? "Tất cả" : nam.ToString())}!", "Thông báo");
                }
            }
            catch (Exception ex) {}
        }



        //  HÀM HIỂN THỊ REPORT 
        private void DisplayReport()
        {
            try
            {
                reportViewer1.LocalReport.DataSources.Clear();

                string exeFolder = Path.GetDirectoryName(Application.ExecutablePath);
                string reportPath = Path.Combine(exeFolder, "Report_HoaDon.rdlc");

                if (!File.Exists(reportPath))
                {
                    // Thử tìm ở thư mục Project
                    string devPath = Path.GetFullPath(Path.Combine(exeFolder, @"..\..\Report_HoaDon.rdlc"));
                    if (File.Exists(devPath))
                        reportPath = devPath;

                }

                reportViewer1.LocalReport.ReportPath = reportPath;

                // 🔹 Đẩy dữ liệu vào DataSet1 (Tên bắt buộc trùng trong RDLC)
                ReportDataSource rds = new ReportDataSource("DataSet1", listReportData);
                reportViewer1.LocalReport.DataSources.Add(rds);

                reportViewer1.RefreshReport();
            }
            catch (Exception ex) {}
        }



        //  HÀM HỖ TRỢ REFLECTION
 
        private T GetPropValue<T>(object src, string propName)
        {
            try
            {
                if (src == null) return default(T);
                var prop = src.GetType().GetProperty(propName);
                if (prop == null) return default(T);

                var val = prop.GetValue(src, null);
                if (val == null) return default(T);

                Type targetType = typeof(T);
                if (Nullable.GetUnderlyingType(targetType) != null)
                    targetType = Nullable.GetUnderlyingType(targetType);

                return (T)Convert.ChangeType(val, targetType);
            }
            catch { return default(T); }
        }

   
        private void btnLoc_Click(object sender, EventArgs e) => LoadDataAndRefreshReport();

        private void btnTroVe_Click(object sender, EventArgs e) => this.Close();


        private void btnXuatExcel_Click(object sender, EventArgs e) => ExportFile("Excel");

        private void ExportFile(string format)
        {
            try
            {
                string ext = format == "PDF" ? "pdf" : "xls";
                SaveFileDialog sfd = new SaveFileDialog() { Filter = $"{format}|*.{ext}", FileName = $"HoaDon_{DateTime.Now:ddMMyyyy}" };

                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    byte[] bytes = reportViewer1.LocalReport.Render(format);
                    File.WriteAllBytes(sfd.FileName, bytes);
                    if (MessageBox.Show("Xuất thành công! Mở file ngay?", "Thông báo", MessageBoxButtons.YesNo) == DialogResult.Yes)
                        System.Diagnostics.Process.Start(sfd.FileName);
                }
            }
            catch (Exception ex) { MessageBox.Show("Lỗi xuất file: " + ex.Message); }
        }

        private void btnXemChiTiet_Click(object sender, EventArgs e)
        {
            // Hộp thoại nhập mã đơn giản
            Form prompt = new Form() { Width = 300, Height = 150, Text = "Nhập mã HĐ", StartPosition = FormStartPosition.CenterScreen };
            TextBox txt = new TextBox() { Left = 20, Top = 20, Width = 240 };
            Button btn = new Button() { Text = "Tra cứu", Left = 180, Top = 60, DialogResult = DialogResult.OK };
            prompt.Controls.Add(txt); prompt.Controls.Add(btn); prompt.AcceptButton = btn;

            if (prompt.ShowDialog() == DialogResult.OK && !string.IsNullOrWhiteSpace(txt.Text))
            {
                var details = busHoaDon.GetChiTietHoaDon(txt.Text.Trim());
                if (details != null && details.Count > 0)
                {
                    string msg = "";
                    foreach (var d in details) msg += $"{d.DichVu?.TenDV ?? d.MaDV} - SL: {d.SoLuong} - {d.ThanhTien:N0} VNĐ\n";
                    MessageBox.Show(msg, $"Chi tiết {txt.Text}");
                }
                else MessageBox.Show("Không tìm thấy chi tiết!");
            }
        }

        private void ApplySearchFilter()
        {
            try
            {
                var keyword = txtTimKiemHoaDon.Text.Trim();
                IEnumerable<ReportDataStruct> query = listReportData;

                if (!string.IsNullOrEmpty(keyword))
                {
                    var kw = keyword.ToLower();
                    query = query.Where(x =>
                        (!string.IsNullOrEmpty(x.MaHD) && x.MaHD.ToLower().Contains(kw)) ||
                        (!string.IsNullOrEmpty(x.MaLich) && x.MaLich.ToLower().Contains(kw)) ||
                        (!string.IsNullOrEmpty(x.HinhThucTT) && x.HinhThucTT.ToLower().Contains(kw)));
                }

                var filtered = query.ToList();

                // Cập nhật ReportViewer với dữ liệu đã lọc
                reportViewer1.LocalReport.DataSources.Clear();
                reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", filtered));
                reportViewer1.RefreshReport();

                // Cập nhật tổng doanh thu theo kết quả lọc
                lblTongDoanhThu.Text = filtered.Sum(x => x.TongTien).ToString("N0") + " VNĐ";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tìm kiếm: " + ex.Message);
            }
        }

        private void txtTimKiemHoaDon_TextChanged(object sender, EventArgs e)
        {
            ApplySearchFilter();
        }

        private void btnTaiLai_Click_1(object sender, EventArgs e)
        {
            if (cboNam.Items.Count > 0)
                cboNam.SelectedIndex = 0;

            if (cboThang.Items.Count > DateTime.Now.Month)
                cboThang.SelectedIndex = DateTime.Now.Month;
            else if (cboThang.Items.Count > 0)
                cboThang.SelectedIndex = 0;
            txtTimKiemHoaDon.Text = string.Empty;

            LoadDataAndRefreshReport();
        }

        private void btnLoc_Click_1(object sender, EventArgs e)
        {
            LoadDataAndRefreshReport();
            ApplySearchFilter();

        }

        private void thToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Hide();

            ThongKeDoanhThu frmDichVu = new ThongKeDoanhThu();
            frmDichVu.ShowDialog();

            this.Show();
        }

        private void thongkesanToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Hide();

            ThongKeSan frmDichVu = new ThongKeSan();
            frmDichVu.ShowDialog();

            this.Show();
        }
    }
}