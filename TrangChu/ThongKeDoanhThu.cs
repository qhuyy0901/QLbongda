using BUS;
using DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace TrangChu
{
    public partial class ThongKeDoanhThu : Form
    {
        private HoaDonBUS busHoaDon = new HoaDonBUS();

        // Biến toàn cục lưu danh sách hóa đơn để không phải query DB nhiều lần
        private List<dynamic> rawDataHoaDon;

        public ThongKeDoanhThu()
        {
            InitializeComponent();
            LoadCombobox();
        }

        private void LoadCombobox()
        {
            // 1. Load Năm
            int currentYear = DateTime.Now.Year;
            cbNam.Items.Clear();
            cbNam.Items.Add("Tất cả");
            for (int i = currentYear; i >= currentYear - 4; i--) // Lấy 5 năm gần nhất
            {
                cbNam.Items.Add(i);
            }
            cbNam.SelectedIndex = 1; // Chọn năm hiện tại

            // 2. Load Tháng
            cbThang.Items.Clear();
            cbThang.Items.Add("Tất cả");
            for (int i = 1; i <= 12; i++)
            {
                cbThang.Items.Add("Tháng " + i);
            }
            cbThang.SelectedIndex = DateTime.Now.Month; // Chọn tháng hiện tại
        }

        private void btnThongKe_Click(object sender, EventArgs e)
        {
            try
            {
                if (cbNam.SelectedItem == null) return;

                // 1. Lấy tham số lọc
                string selectedYear = cbNam.SelectedItem.ToString();
                int nam = (selectedYear == "Tất cả") ? 0 : int.Parse(selectedYear);
                int thang = cbThang.SelectedIndex; // 0 = Tất cả

                // 2. Tải dữ liệu thô một lần duy nhất
                rawDataHoaDon = busHoaDon.GetHoaDonWithCustomerInfo();

                // 3. Xử lý hiển thị theo kịch bản
                chartDoanhThu.Series["Doanh Thu Sân"].Points.Clear();
                chartDoanhThu.Series["Doanh Thu Dịch Vụ"].Points.Clear();

                decimal grandTotalSan = 0;
                decimal grandTotalDV = 0;

                // === TRƯỜNG HỢP 1: TẤT CẢ NĂM (Vẽ biểu đồ theo cột NĂM) ===
                if (nam == 0)
                {
                    // Lấy danh sách các năm có trong dữ liệu hoặc 5 năm gần nhất
                    int currentYear = DateTime.Now.Year;
                    for (int y = currentYear - 4; y <= currentYear; y++)
                    {
                        var result = CalculateRevenue(y, 0); // Tính tổng năm y, tất cả tháng
                        AddChartPoint(y.ToString(), result.TienSan, result.TienDV);

                        grandTotalSan += result.TienSan;
                        grandTotalDV += result.TienDV;
                    }
                }
                // === TRƯỜNG HỢP 2: 1 NĂM CỤ THỂ + TẤT CẢ THÁNG (Vẽ biểu đồ theo cột THÁNG) ===
                else if (thang == 0)
                {
                    for (int m = 1; m <= 12; m++)
                    {
                        var result = CalculateRevenue(nam, m); // Tính tổng năm nam, tháng m
                        AddChartPoint("T" + m, result.TienSan, result.TienDV);

                        grandTotalSan += result.TienSan;
                        grandTotalDV += result.TienDV;
                    }
                }
                // === TRƯỜNG HỢP 3: 1 NĂM + 1 THÁNG CỤ THỂ (Vẽ 1 cột duy nhất) ===
                else
                {
                    var result = CalculateRevenue(nam, thang);
                    AddChartPoint($"T{thang}/{nam}", result.TienSan, result.TienDV);

                    grandTotalSan = result.TienSan;
                    grandTotalDV = result.TienDV;
                }

                // 4. Hiển thị tổng số liệu lên Label
                decimal grandTotal = grandTotalSan + grandTotalDV;
                lblTongSan.Text = grandTotalSan.ToString("N0") + " VNĐ";
                lblTongDichVu.Text = grandTotalDV.ToString("N0") + " VNĐ";
                lblTongCong.Text = grandTotal.ToString("N0") + " VNĐ";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tính toán: " + ex.Message);
            }
        }

        // --- HÀM TÍNH TOÁN DOANH THU CHO 1 KHOẢNG THỜI GIAN ---
        // Trả về: (Tiền Sân, Tiền Dịch Vụ)
        private (decimal TienSan, decimal TienDV) CalculateRevenue(int year, int month)
        {
            decimal tongHD = 0;

            // 1. Tính Tổng Hóa Đơn từ list rawData (đã load từ DB)
            if (rawDataHoaDon != null)
            {
                foreach (var item in rawDataHoaDon)
                {
                    DateTime? ngayTT = GetDateValue(item, "ThoiGianThanhToan");
                    if (ngayTT.HasValue)
                    {
                        // Kiểm tra Năm
                        if (year != 0 && ngayTT.Value.Year != year) continue;
                        // Kiểm tra Tháng
                        if (month != 0 && ngayTT.Value.Month != month) continue;

                        tongHD += GetDecimalValue(item, "TongTien");
                    }
                }
            }

            // 2. Tính Tiền Dịch Vụ (Gọi BUS để query chính xác từ bảng chi tiết)
            decimal tienDV = busHoaDon.GetTongDoanhThuDichVu(year, month);

            // 3. Tính Tiền Sân = Tổng Hóa Đơn - Tiền Dịch Vụ
            decimal tienSan = tongHD - tienDV;
            if (tienSan < 0) tienSan = 0;

            return (tienSan, tienDV);
        }

        // --- HÀM VẼ ĐIỂM LÊN BIỂU ĐỒ ---
        private void AddChartPoint(string label, decimal san, decimal dichVu)
        {
            // --- CỘT DOANH THU SÂN ---
            // Sử dụng AddXY: Tham số 1 là Nhãn (Trục X), Tham số 2 là Giá trị (Trục Y)
            // Hàm này trả về vị trí (index) của điểm vừa thêm
            int p1 = chartDoanhThu.Series["Doanh Thu Sân"].Points.AddXY(label, Convert.ToDouble(san));

            // Thiết lập hiển thị số tiền trên đầu cột
            if (san > 0)
            {
                chartDoanhThu.Series["Doanh Thu Sân"].Points[p1].Label = string.Format("{0:N0}", san);
                chartDoanhThu.Series["Doanh Thu Sân"].Points[p1].LabelForeColor = System.Drawing.Color.Black; // Màu chữ
            }

            // --- CỘT DOANH THU DỊCH VỤ ---
            int p2 = chartDoanhThu.Series["Doanh Thu Dịch Vụ"].Points.AddXY(label, Convert.ToDouble(dichVu));

            // Thiết lập hiển thị số tiền trên đầu cột
            if (dichVu > 0)
            {
                chartDoanhThu.Series["Doanh Thu Dịch Vụ"].Points[p2].Label = string.Format("{0:N0}", dichVu);
                chartDoanhThu.Series["Doanh Thu Dịch Vụ"].Points[p2].LabelForeColor = System.Drawing.Color.Red; // Màu chữ
            }
        }

        // --- REFLECTION HELPERS ---
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
    }
}