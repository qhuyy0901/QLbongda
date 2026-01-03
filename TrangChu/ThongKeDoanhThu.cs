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
            int currentYear = DateTime.Now.Year;
            cbNam.Items.Clear();
            cbNam.Items.Add("Tất cả");
            for (int i = currentYear; i >= currentYear - 4; i--) 
            {
                cbNam.Items.Add(i);
            }
            cbNam.SelectedIndex = 1; 


            cbThang.Items.Clear();
            cbThang.Items.Add("Tất cả");
            for (int i = 1; i <= 12; i++)
            {
                cbThang.Items.Add("Tháng " + i);
            }
            cbThang.SelectedIndex = DateTime.Now.Month; 
        }

        private void btnThongKe_Click(object sender, EventArgs e)
        {
            try
            {
                if (cbNam.SelectedItem == null) return;

                string selectedYear = cbNam.SelectedItem.ToString();
                int nam = (selectedYear == "Tất cả") ? 0 : int.Parse(selectedYear);
                int thang = cbThang.SelectedIndex; 

                rawDataHoaDon = busHoaDon.GetHoaDonWithCustomerInfo();

                chartDoanhThu.Series["Doanh Thu Sân"].Points.Clear();
                chartDoanhThu.Series["Doanh Thu Dịch Vụ"].Points.Clear();

                decimal grandTotalSan = 0;
                decimal grandTotalDV = 0;

                if (nam == 0)
                {
                    int currentYear = DateTime.Now.Year;
                    for (int y = currentYear - 4; y <= currentYear; y++)
                    {
                        var result = CalculateRevenue(y, 0); 
                        AddChartPoint(y.ToString(), result.TienSan, result.TienDV);

                        grandTotalSan += result.TienSan;
                        grandTotalDV += result.TienDV;
                    }
                }
                else if (thang == 0)
                {
                    for (int m = 1; m <= 12; m++)
                    {
                        var result = CalculateRevenue(nam, m); 
                        AddChartPoint("T" + m, result.TienSan, result.TienDV);

                        grandTotalSan += result.TienSan;
                        grandTotalDV += result.TienDV;
                    }
                }
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
            catch (Exception ex){}
        }

        // HÀM TÍNH TOÁN DOANH THU CHO 1 KHOẢNG THỜI GIAN 
        // Trả về: Tiền Sân, Tiền Dịch Vụ
        private (decimal TienSan, decimal TienDV) CalculateRevenue(int year, int month)
        {
            decimal tongHD = 0;

            if (rawDataHoaDon != null)
            {
                foreach (var item in rawDataHoaDon)
                {
                    DateTime? ngayTT = GetDateValue(item, "ThoiGianThanhToan");
                    if (ngayTT.HasValue)
                    {
                        if (year != 0 && ngayTT.Value.Year != year) continue;
                        if (month != 0 && ngayTT.Value.Month != month) continue;

                        tongHD += GetDecimalValue(item, "TongTien");
                    }
                }
            }

            // 2. Tính Tiền Dịch Vụ Gọi BUS để query chính xác từ bảng chi tiết
            decimal tienDV = busHoaDon.GetTongDoanhThuDichVu(year, month);

            // 3. Tính Tiền Sân = Tổng Hóa Đơn - Tiền Dịch Vụ
            decimal tienSan = tongHD - tienDV;
            if (tienSan < 0) tienSan = 0;

            return (tienSan, tienDV);
        }

        // --- HÀM VẼ ĐIỂM LÊN BIỂU ĐỒ ---
        private void AddChartPoint(string label, decimal san, decimal dichVu)
        {

            int p1 = chartDoanhThu.Series["Doanh Thu Sân"].Points.AddXY(label, Convert.ToDouble(san));

            if (san > 0)
            {
                chartDoanhThu.Series["Doanh Thu Sân"].Points[p1].Label = string.Format("{0:N0}", san);
                chartDoanhThu.Series["Doanh Thu Sân"].Points[p1].LabelForeColor = System.Drawing.Color.Black; // Màu chữ
            }

            int p2 = chartDoanhThu.Series["Doanh Thu Dịch Vụ"].Points.AddXY(label, Convert.ToDouble(dichVu));

            if (dichVu > 0)
            {
                chartDoanhThu.Series["Doanh Thu Dịch Vụ"].Points[p2].Label = string.Format("{0:N0}", dichVu);
                chartDoanhThu.Series["Doanh Thu Dịch Vụ"].Points[p2].LabelForeColor = System.Drawing.Color.Red; // Màu chữ
            }
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

        private void btnTroVe_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}