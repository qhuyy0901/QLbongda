using System;
using System.Linq;
using System.Windows.Forms;
using BUS; // Gọi BUS của bạn

namespace TrangChu
{
    public partial class ThongKeSan : Form
    {
        private LichDatBUS busLichDat = new LichDatBUS(); // Giả sử bạn lấy dữ liệu từ Lịch Đặt

        public ThongKeSan()
        {
            InitializeComponent();
            LoadCombobox();
            dtpTuNgay.Value = DateTime.Now.AddDays(-30); // Mặc định 30 ngày
            dtpDenNgay.Value = DateTime.Now;
        }

        private void LoadCombobox()
        {
            cbxSan.Items.Add("Tất cả");
            cbxSan.Items.Add("San1");
            cbxSan.Items.Add("San2");
            // ... load từ CSDL thì tốt hơn
            cbxSan.SelectedIndex = 0;
        }

        private void btnThongKe_Click(object sender, EventArgs e)
        {
            DateTime from = dtpTuNgay.Value.Date;
            DateTime to = dtpDenNgay.Value.Date.AddDays(1).AddSeconds(-1);
            string selectedSan = cbxSan.SelectedItem.ToString();

            // 1. Lấy dữ liệu thô từ BUS
            var listData = busLichDat.GetAll()
                .Where(x => x.NgayDat >= from && x.NgayDat <= to);

            if (selectedSan != "Tất cả")
            {
                listData = listData.Where(x => x.MaSan == selectedSan);
            }

            var data = listData.ToList();

            // 2. Xử lý Biểu đồ 1: Tổng Giờ Hoạt Động (Nhóm theo Sân)
            var chart1Data = data
                .Where(x => x.TrangThai == "Đã thanh toán" || x.TrangThai == "Đã đặt")
                .GroupBy(x => x.MaSan)
                .Select(g => new { San = g.Key, TongGio = g.Sum(x => x.GioKT - x.GioBD) })
                .ToList();

            chartGioHoatDong.Series["Số Giờ"].Points.Clear();
            foreach (var item in chart1Data)
            {
                chartGioHoatDong.Series["Số Giờ"].Points.AddXY(item.San, item.TongGio);
            }

            // 3. Xử lý Biểu đồ 2: Doanh Thu (Nhóm theo Sân)
            var chart2Data = data
                .Where(x => x.TrangThai == "Đã thanh toán") // Chỉ tính tiền đã thanh toán
                .GroupBy(x => x.MaSan)
                .Select(g => new { San = g.Key, DoanhThu = g.Sum(x => x.DonGiaThucTe ?? 0) })
                .ToList();

            chartDoanhThu.Series["Doanh Thu (VNĐ)"].Points.Clear();
            foreach (var item in chart2Data)
            {
                chartDoanhThu.Series["Doanh Thu (VNĐ)"].Points.AddXY(item.San, item.DoanhThu);
            }

            // 4. Xử lý Biểu đồ 3: Trạng thái (Đếm số lần)
            var chart3Data = data
                .GroupBy(x => x.MaSan)
                .Select(g => new
                {
                    San = g.Key,
                    DaThanhToan = g.Count(x => x.TrangThai == "Đã thanh toán"),
                    DaHuy = g.Count(x => x.TrangThai == "Đã hủy")
                })
                .ToList();

            chartTrangThai.Series["Đã Thanh Toán"].Points.Clear();
            chartTrangThai.Series["Đã Hủy"].Points.Clear();

            foreach (var item in chart3Data)
            {
                chartTrangThai.Series["Đã Thanh Toán"].Points.AddXY(item.San, item.DaThanhToan);
                chartTrangThai.Series["Đã Hủy"].Points.AddXY(item.San, item.DaHuy);
            }
        }
    }
}