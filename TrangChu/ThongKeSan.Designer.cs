namespace TrangChu
{
    partial class ThongKeSan
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Title title1 = new System.Windows.Forms.DataVisualization.Charting.Title();

            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend2 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Title title2 = new System.Windows.Forms.DataVisualization.Charting.Title();

            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea3 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend3 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series3 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series4 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Title title3 = new System.Windows.Forms.DataVisualization.Charting.Title();

            this.panelFilter = new System.Windows.Forms.Panel();
            this.groupBoxFilter = new System.Windows.Forms.GroupBox();
            this.btnThongKe = new System.Windows.Forms.Button();
            this.cbxSan = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.dtpDenNgay = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.dtpTuNgay = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();

            this.layoutCharts = new System.Windows.Forms.TableLayoutPanel();
            this.chartGioHoatDong = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.chartDoanhThu = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.chartTrangThai = new System.Windows.Forms.DataVisualization.Charting.Chart();

            this.panelFilter.SuspendLayout();
            this.groupBoxFilter.SuspendLayout();
            this.layoutCharts.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chartGioHoatDong)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartDoanhThu)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartTrangThai)).BeginInit();
            this.SuspendLayout();

            // 
            // panelFilter (Khu vực lọc dữ liệu)
            // 
            this.panelFilter.Controls.Add(this.groupBoxFilter);
            this.panelFilter.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelFilter.Location = new System.Drawing.Point(0, 0);
            this.panelFilter.Name = "panelFilter";
            this.panelFilter.Size = new System.Drawing.Size(1100, 80);
            this.panelFilter.TabIndex = 0;

            // 
            // groupBoxFilter
            // 
            this.groupBoxFilter.Controls.Add(this.btnThongKe);
            this.groupBoxFilter.Controls.Add(this.cbxSan);
            this.groupBoxFilter.Controls.Add(this.label3);
            this.groupBoxFilter.Controls.Add(this.dtpDenNgay);
            this.groupBoxFilter.Controls.Add(this.label2);
            this.groupBoxFilter.Controls.Add(this.dtpTuNgay);
            this.groupBoxFilter.Controls.Add(this.label1);
            this.groupBoxFilter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBoxFilter.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBoxFilter.Location = new System.Drawing.Point(0, 0);
            this.groupBoxFilter.Name = "groupBoxFilter";
            this.groupBoxFilter.Size = new System.Drawing.Size(1100, 80);
            this.groupBoxFilter.TabIndex = 0;
            this.groupBoxFilter.TabStop = false;
            this.groupBoxFilter.Text = "Bộ Lọc Thống Kê";

            // 
            // label1 (Từ ngày)
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(30, 35);
            this.label1.Text = "Từ ngày:";

            // dtpTuNgay
            this.dtpTuNgay.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpTuNgay.Location = new System.Drawing.Point(100, 32);
            this.dtpTuNgay.Size = new System.Drawing.Size(120, 25);

            // 
            // label2 (Đến ngày)
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(240, 35);
            this.label2.Text = "Đến ngày:";

            // dtpDenNgay
            this.dtpDenNgay.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpDenNgay.Location = new System.Drawing.Point(320, 32);
            this.dtpDenNgay.Size = new System.Drawing.Size(120, 25);

            // 
            // label3 (Chọn sân)
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(470, 35);
            this.label3.Text = "Chọn sân:";

            // cbxSan
            this.cbxSan.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxSan.FormattingEnabled = true;
            this.cbxSan.Location = new System.Drawing.Point(550, 32);
            this.cbxSan.Size = new System.Drawing.Size(150, 25);
            // Lưu ý: Code xử lý sự kiện Load sẽ thêm item "Tất cả", "San1"...

            // btnThongKe
            this.btnThongKe.BackColor = System.Drawing.Color.ForestGreen;
            this.btnThongKe.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnThongKe.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnThongKe.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnThongKe.ForeColor = System.Drawing.Color.White;
            this.btnThongKe.Location = new System.Drawing.Point(730, 28);
            this.btnThongKe.Size = new System.Drawing.Size(120, 32);
            this.btnThongKe.Text = "Thống Kê";
            this.btnThongKe.UseVisualStyleBackColor = false;
            this.btnThongKe.Click += new System.EventHandler(this.btnThongKe_Click);

            // 
            // layoutCharts (Bố cục chứa 3 biểu đồ)
            // 
            this.layoutCharts.ColumnCount = 2;
            this.layoutCharts.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F)); // Cột trái 50%
            this.layoutCharts.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F)); // Cột phải 50%
            this.layoutCharts.Controls.Add(this.chartGioHoatDong, 0, 0); // Hàng 0, Cột 0
            this.layoutCharts.Controls.Add(this.chartDoanhThu, 1, 0);    // Hàng 0, Cột 1
            this.layoutCharts.Controls.Add(this.chartTrangThai, 0, 1);   // Hàng 1, Cột 0 (Sẽ Span 2 cột)
            this.layoutCharts.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutCharts.Location = new System.Drawing.Point(0, 80);
            this.layoutCharts.Name = "layoutCharts";
            this.layoutCharts.RowCount = 2;
            this.layoutCharts.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F)); // Hàng trên 50%
            this.layoutCharts.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F)); // Hàng dưới 50%
            this.layoutCharts.Size = new System.Drawing.Size(1100, 620);
            this.layoutCharts.TabIndex = 1;

            // 
            // 1. Biểu đồ Giờ Hoạt Động
            // 
            chartArea1.Name = "ChartArea1";
            this.chartGioHoatDong.ChartAreas.Add(chartArea1);
            this.chartGioHoatDong.Dock = System.Windows.Forms.DockStyle.Fill;
            legend1.Name = "Legend1";
            this.chartGioHoatDong.Legends.Add(legend1);
            this.chartGioHoatDong.Location = new System.Drawing.Point(3, 3);
            this.chartGioHoatDong.Name = "chartGioHoatDong";
            series1.ChartArea = "ChartArea1";
            series1.Legend = "Legend1";
            series1.Name = "Số Giờ";
            series1.Color = System.Drawing.Color.DodgerBlue;
            this.chartGioHoatDong.Series.Add(series1);
            title1.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            title1.Name = "Title1";
            title1.Text = "TỔNG GIỜ HOẠT ĐỘNG TỪNG SÂN";
            this.chartGioHoatDong.Titles.Add(title1);

            // 
            // 2. Biểu đồ Doanh Thu
            // 
            chartArea2.Name = "ChartArea1";
            this.chartDoanhThu.ChartAreas.Add(chartArea2);
            this.chartDoanhThu.Dock = System.Windows.Forms.DockStyle.Fill;
            legend2.Name = "Legend1";
            this.chartDoanhThu.Legends.Add(legend2);
            this.chartDoanhThu.Location = new System.Drawing.Point(553, 3);
            this.chartDoanhThu.Name = "chartDoanhThu";
            series2.ChartArea = "ChartArea1";
            series2.Legend = "Legend1";
            series2.Name = "Doanh Thu (VNĐ)";
            series2.Color = System.Drawing.Color.OrangeRed;
            series2.LabelFormat = "N0"; // Định dạng số tiền
            this.chartDoanhThu.Series.Add(series2);
            title2.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            title2.Name = "Title1";
            title2.Text = "DOANH THU TỪNG SÂN";
            this.chartDoanhThu.Titles.Add(title2);

            // 
            // 3. Biểu đồ Trạng Thái (Thanh toán / Hủy) - Chiếm toàn bộ hàng dưới
            // 
            chartArea3.Name = "ChartArea1";
            this.chartTrangThai.ChartAreas.Add(chartArea3);
            this.layoutCharts.SetColumnSpan(this.chartTrangThai, 2); // Span 2 cột
            this.chartTrangThai.Dock = System.Windows.Forms.DockStyle.Fill;
            legend3.Name = "Legend1";
            this.chartTrangThai.Legends.Add(legend3);
            this.chartTrangThai.Location = new System.Drawing.Point(3, 313);
            this.chartTrangThai.Name = "chartTrangThai";

            // Series 1: Đã thanh toán
            series3.ChartArea = "ChartArea1";
            series3.Legend = "Legend1";
            series3.Name = "Đã Thanh Toán";
            series3.Color = System.Drawing.Color.ForestGreen;

            // Series 2: Đã hủy
            series4.ChartArea = "ChartArea1";
            series4.Legend = "Legend1";
            series4.Name = "Đã Hủy";
            series4.Color = System.Drawing.Color.Gray;

            this.chartTrangThai.Series.Add(series3);
            this.chartTrangThai.Series.Add(series4);

            title3.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            title3.Name = "Title1";
            title3.Text = "TỶ LỆ THANH TOÁN VS HỦY LỊCH";
            this.chartTrangThai.Titles.Add(title3);

            // 
            // ThongKeSan Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1100, 700);
            this.Controls.Add(this.layoutCharts);
            this.Controls.Add(this.panelFilter);
            this.Name = "ThongKeSan";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "BÁO CÁO THỐNG KÊ HOẠT ĐỘNG SÂN";

            this.panelFilter.ResumeLayout(false);
            this.groupBoxFilter.ResumeLayout(false);
            this.groupBoxFilter.PerformLayout();
            this.layoutCharts.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chartGioHoatDong)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartDoanhThu)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartTrangThai)).EndInit();
            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Panel panelFilter;
        private System.Windows.Forms.GroupBox groupBoxFilter;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker dtpTuNgay;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker dtpDenNgay;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cbxSan;
        private System.Windows.Forms.Button btnThongKe;
        private System.Windows.Forms.TableLayoutPanel layoutCharts;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartGioHoatDong;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartDoanhThu;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartTrangThai;
    }
}