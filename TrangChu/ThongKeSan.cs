using BUS;
using DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace TrangChu
{
    public partial class ThongKeSan : Form
    {
        private readonly LichDatBUS busLichDat = new LichDatBUS();
        private readonly SanBongBUS busSanBong = new SanBongBUS();

        public ThongKeSan()
        {
            InitializeComponent();
        }

        private void ThongKeSan_Load(object sender, EventArgs e)
        {
            LoadComboboxSan();
            SetDefaultDate();
        }

        private void SetDefaultDate()
        {
            DateTime now = DateTime.Now;
            dtpTuNgay.Value = new DateTime(now.Year, now.Month, 1);
            dtpDenNgay.Value = now;
        }

        private void LoadComboboxSan()
        {
            try
            {
                var listSan = busSanBong.GetListSanBong();
                cbxSan.Items.Clear();
                cbxSan.Items.Add("Tất cả");

                foreach (var san in listSan)
                {
                    cbxSan.Items.Add(san.MaSan);
                }
                cbxSan.SelectedIndex = 0;
            }
            catch
            {
                cbxSan.Items.Add("Tất cả");
                cbxSan.Items.Add("San1");
                cbxSan.Items.Add("San2");
                cbxSan.Items.Add("San3");
                cbxSan.Items.Add("San4");
                cbxSan.Items.Add("San5");
                cbxSan.Items.Add("San6");
                cbxSan.SelectedIndex = 0;
            }
        }

        private void btnThongKe_Click(object sender, EventArgs e)
        {
            try
            {
                DateTime fromDate = dtpTuNgay.Value.Date;
                DateTime toDate = dtpDenNgay.Value.Date.AddDays(1).AddSeconds(-1);
                string selectedSan = cbxSan.SelectedItem?.ToString() ?? "Tất cả";

                var allLichDat = busLichDat.GetAll();

                var filteredQuery = allLichDat.Where(x => x.NgayDat.HasValue && x.NgayDat.Value >= fromDate && x.NgayDat.Value <= toDate);

                if (selectedSan != "Tất cả")
                {
                    filteredQuery = filteredQuery.Where(x => x.MaSan == selectedSan);
                }

                var dataToList = filteredQuery.ToList();

                if (dataToList.Count == 0) {}

                // Vẽ biểu đồ
                DrawChartGioHoatDong(dataToList);
                DrawChartDoanhThu(dataToList);
                DrawChartTrangThai(dataToList);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message + "\n\nStack Trace:\n" + ex.StackTrace);
            }
        }

        private void DrawChartGioHoatDong(List<DAL.LichDat> data)
        {
            try
            {
                var chartData = data
                    .Where(x => x.TrangThai == "Đã thanh toán" || x.TrangThai == "Đã đặt")
                    .GroupBy(x => x.MaSan)
                    .Select(g => new
                    {
                        San = g.Key,
                        TongGio = g.Sum(x => Convert.ToDouble(x.GioKT ?? 0) - Convert.ToDouble(x.GioBD ?? 0))
                    })
                    .OrderBy(x => x.San)
                    .ToList();

                chartGioHoatDong.Series["Số Giờ"].Points.Clear();
                foreach (var item in chartData)
                {
                    chartGioHoatDong.Series["Số Giờ"].Points.AddXY(item.San, item.TongGio);
                }
            }
            catch (Exception ex){}
        }

        private void DrawChartDoanhThu(List<DAL.LichDat> data)
        {
            try
            {
                var chartData = data
                    .Where(x => x.TrangThai == "Đã thanh toán")
                    .GroupBy(x => x.MaSan)
                    .Select(g => new
                    {
                        San = g.Key,
                        DoanhThu = g.Sum(x => x.DonGiaThucTe ?? 0)
                    })
                    .OrderBy(x => x.San)
                    .ToList();

                chartDoanhThu.Series["Doanh Thu (VNĐ)"].Points.Clear();
                foreach (var item in chartData)
                {
                    chartDoanhThu.Series["Doanh Thu (VNĐ)"].Points.AddXY(item.San, item.DoanhThu);
                }
            }
            catch (Exception ex) {}
        }

        private void DrawChartTrangThai(List<DAL.LichDat> data)
        {
            try
            {
                var chartData = data
                    .GroupBy(x => x.MaSan)
                    .Select(g => new
                    {
                        San = g.Key,
                        DaThanhToan = g.Count(x => x.TrangThai == "Đã thanh toán"),
                        DaHuy = g.Count(x => x.TrangThai == "Đã hủy" || x.TrangThai == "Hủy")
                    })
                    .OrderBy(x => x.San)
                    .ToList();

                chartTrangThai.Series["Đã Thanh Toán"].Points.Clear();
                chartTrangThai.Series["Đã Hủy"].Points.Clear();

                foreach (var item in chartData)
                {
                    chartTrangThai.Series["Đã Thanh Toán"].Points.AddXY(item.San, item.DaThanhToan);
                    chartTrangThai.Series["Đã Hủy"].Points.AddXY(item.San, item.DaHuy);
                }
            }
            catch (Exception ex) { }
        }

        private void ClearCharts()
        {
            chartGioHoatDong.Series["Số Giờ"].Points.Clear();
            chartDoanhThu.Series["Doanh Thu (VNĐ)"].Points.Clear();
            chartTrangThai.Series["Đã Thanh Toán"].Points.Clear();
            chartTrangThai.Series["Đã Hủy"].Points.Clear();
        }
    }
}