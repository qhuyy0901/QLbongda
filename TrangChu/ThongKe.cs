using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using BUS;

namespace TrangChu
{
    public partial class ThongKe : Form
    {
        private ThongKeBUS busThongKe = new ThongKeBUS();

        public ThongKe()
        {
            InitializeComponent();
            this.Load += ThongKe_Load;
        }

        private void ThongKe_Load(object sender, EventArgs e)
        {
            try
            {
                // ===== CẤU HÌNH COMBOBOX NĂM =====
                LoadYearComboBox();

                // ===== CẤU HÌNH COMBOBOX LOẠI THỐNG KÊ =====
                if (cboLoaiThongKe.Items.Count == 0)
                {
                    cboLoaiThongKe.Items.Add("Theo tháng");
                    cboLoaiThongKe.Items.Add("Theo năm");
                }
                cboLoaiThongKe.SelectedIndex = 0;

                // ===== CẤU HÌNH CHART =====
                ConfigureChart();

                // ===== THÊM SỰ KIỆN =====
                btnThongKe.Click += BtnThongKe_Click;

                // ===== LOAD DỮ LIỆU MẶC ĐỊNH =====
                LoadChartData();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"❌ Lỗi khởi tạo form: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ===== LOAD CÁC NĂM VÀO COMBOBOX =====
        private void LoadYearComboBox()
        {
            try
            {
                cboNam.Items.Clear();
                List<int> years = busThongKe.GetListYears();

                foreach (int year in years)
                {
                    cboNam.Items.Add(year);
                }

                if (cboNam.Items.Count > 0)
                {
                    cboNam.SelectedIndex = 0;
                }
                else
                {
                    cboNam.Items.Add(DateTime.Now.Year);
                    cboNam.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"❌ Lỗi tải năm: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ===== CẤU HÌNH BIỂU ĐỒ (CÓ 2 SERIES: LỊCH ĐẶT + DỊCH VỤ) =====
        private void ConfigureChart()
        {
            try
            {
                // ===== XÓA SỐ LIỆU CŨ =====
                chartDoanhThu.Series.Clear();
                chartDoanhThu.ChartAreas.Clear();
                chartDoanhThu.Legends.Clear();

                // ===== TẠO CHART AREA =====
                ChartArea chartArea = new ChartArea("ChartArea1");
                chartArea.AxisX.Title = "Tháng";
                chartArea.AxisY.Title = "Doanh thu (VNĐ)";
                chartArea.AxisY.LabelStyle.Format = "#,##0";
                chartDoanhThu.ChartAreas.Add(chartArea);

                // ===== TẠO LEGEND =====
                Legend legend = new Legend("Legend1");
                legend.Docking = Docking.Top;
                chartDoanhThu.Legends.Add(legend);

                // ===== TẠO SERIES 1: DOANH THU LỊCH ĐẶT =====
                Series seriesLichDat = new Series("Doanh thu Lịch đặt");
                seriesLichDat.ChartType = SeriesChartType.Column;
                seriesLichDat.ChartArea = "ChartArea1";
                seriesLichDat.Legend = "Legend1";
                seriesLichDat.LabelFormat = "#,##0";
                seriesLichDat.IsVisibleInLegend = true;
                seriesLichDat.Color = Color.SteelBlue;  // 🔵 Màu xanh dương
                chartDoanhThu.Series.Add(seriesLichDat);

                // ===== TẠO SERIES 2: DOANH THU DỊCH VỤ =====
                Series seriesDichVu = new Series("Doanh thu Dịch vụ");
                seriesDichVu.ChartType = SeriesChartType.Column;
                seriesDichVu.ChartArea = "ChartArea1";
                seriesDichVu.Legend = "Legend1";
                seriesDichVu.LabelFormat = "#,##0";
                seriesDichVu.IsVisibleInLegend = true;
                seriesDichVu.Color = Color.OrangeRed;  // 🟠 Màu cam đỏ
                chartDoanhThu.Series.Add(seriesDichVu);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"❌ Lỗi cấu hình biểu đồ: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ===== LOAD DỮ LIỆU VÀO BIỂU ĐỒ =====
        private void LoadChartData()
        {
            try
            {
                if (cboNam.SelectedItem == null)
                {
                    return;
                }

                int selectedYear = Convert.ToInt32(cboNam.SelectedItem);
                string loaiThongKe = cboLoaiThongKe.SelectedItem?.ToString() ?? "Theo tháng";

                // ===== XÓA DỮ LIỆU CŨ =====
                foreach (Series series in chartDoanhThu.Series)
                {
                    series.Points.Clear();
                }

                if (loaiThongKe == "Theo tháng")
                {
                    LoadChartByMonth(selectedYear);
                }
                else if (loaiThongKe == "Theo năm")
                {
                    LoadChartByYear();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"❌ Lỗi tải dữ liệu biểu đồ: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ===== LOAD BIỂU ĐỒ THEO THÁNG (2 SERIES) =====
        private void LoadChartByMonth(int year)
        {
            try
            {
                Dictionary<int, decimal> revenueByMonthLichDat = busThongKe.GetRevenueByMonthFromLichDat(year);
                Dictionary<int, decimal> revenueByMonthDichVu = busThongKe.GetRevenueByMonthFromDichVu(year);

                if (chartDoanhThu.ChartAreas.Count > 0)
                {
                    chartDoanhThu.ChartAreas[0].AxisX.Title = $"Tháng (Năm {year})";
                }

                if (chartDoanhThu.Series.Count >= 2)
                {
                    Series seriesLichDat = chartDoanhThu.Series[0];
                    Series seriesDichVu = chartDoanhThu.Series[1];

                    for (int month = 1; month <= 12; month++)
                    {
                        decimal revenueLichDat = revenueByMonthLichDat.ContainsKey(month) ? revenueByMonthLichDat[month] : 0;
                        decimal revenueDichVu = revenueByMonthDichVu.ContainsKey(month) ? revenueByMonthDichVu[month] : 0;

                        // ===== THÊM LỊCH ĐẶT =====
                        DataPoint pointLichDat = new DataPoint();
                        pointLichDat.XValue = month;
                        pointLichDat.YValues = new double[] { Convert.ToDouble(revenueLichDat) };
                        pointLichDat.AxisLabel = $"T{month}";
                        seriesLichDat.Points.Add(pointLichDat);

                        // ===== THÊM DỊCH VỤ =====
                        DataPoint pointDichVu = new DataPoint();
                        pointDichVu.XValue = month + 0.25;  // ⬅️ Offset để không trùng với series trước
                        pointDichVu.YValues = new double[] { Convert.ToDouble(revenueDichVu) };
                        seriesDichVu.Points.Add(pointDichVu);
                    }

                    // ===== ĐỊNH DẠNG CHỈ SỐ =====
                    if (chartDoanhThu.ChartAreas.Count > 0)
                    {
                        chartDoanhThu.ChartAreas[0].AxisX.Minimum = 0;
                        chartDoanhThu.ChartAreas[0].AxisX.Maximum = 13;
                        chartDoanhThu.ChartAreas[0].AxisX.Interval = 1;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"❌ Lỗi tải biểu đồ theo tháng: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ===== LOAD BIỂU ĐỒ THEO NĂM (2 SERIES) =====
        private void LoadChartByYear()
        {
            try
            {
                Dictionary<int, decimal> revenueByYearLichDat = busThongKe.GetRevenueByYearFromLichDat();
                Dictionary<int, decimal> revenueByYearDichVu = busThongKe.GetRevenueByYearFromDichVu();

                if (chartDoanhThu.ChartAreas.Count > 0)
                {
                    chartDoanhThu.ChartAreas[0].AxisX.Title = "Năm";
                }

                if (chartDoanhThu.Series.Count >= 2)
                {
                    Series seriesLichDat = chartDoanhThu.Series[0];
                    Series seriesDichVu = chartDoanhThu.Series[1];

                    int index = 0;
                    foreach (var yearData in revenueByYearLichDat.OrderBy(x => x.Key))
                    {
                        decimal revenueLichDat = yearData.Value;
                        decimal revenueDichVu = revenueByYearDichVu.ContainsKey(yearData.Key) ? revenueByYearDichVu[yearData.Key] : 0;

                        // ===== THÊM LỊCH ĐẶT =====
                        DataPoint pointLichDat = new DataPoint();
                        pointLichDat.XValue = index;
                        pointLichDat.YValues = new double[] { Convert.ToDouble(revenueLichDat) };
                        pointLichDat.AxisLabel = yearData.Key.ToString();
                        seriesLichDat.Points.Add(pointLichDat);

                        // ===== THÊM DỊCH VỤ =====
                        DataPoint pointDichVu = new DataPoint();
                        pointDichVu.XValue = index + 0.25;  // ⬅️ Offset
                        pointDichVu.YValues = new double[] { Convert.ToDouble(revenueDichVu) };
                        seriesDichVu.Points.Add(pointDichVu);

                        index++;
                    }

                    // ===== ĐỊNH DẠNG CHỈ SỐ =====
                    if (chartDoanhThu.ChartAreas.Count > 0)
                    {
                        chartDoanhThu.ChartAreas[0].AxisX.Minimum = -1;
                        chartDoanhThu.ChartAreas[0].AxisX.Maximum = revenueByYearLichDat.Count;
                        chartDoanhThu.ChartAreas[0].AxisX.Interval = 1;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"❌ Lỗi tải biểu đồ theo năm: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ===== SỰ KIỆN NÚT THỐNG KÊ =====
        private void BtnThongKe_Click(object sender, EventArgs e)
        {
            try
            {
                LoadChartData();
                MessageBox.Show("✔ Tải dữ liệu thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"❌ Lỗi: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void thốngKêSânToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                ThongKeSan frmThongKeSan = new ThongKeSan();
                frmThongKeSan.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"❌ Lỗi mở form thống kê sân: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}