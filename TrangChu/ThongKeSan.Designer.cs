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

            // panelFilter
            this.panelFilter.Controls.Add(this.groupBoxFilter);
            this.panelFilter.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelFilter.Location = new System.Drawing.Point(0, 0);
            this.panelFilter.Name = "panelFilter";
            this.panelFilter.Size = new System.Drawing.Size(1100, 80);
            this.panelFilter.TabIndex = 0;

            // groupBoxFilter
            this.groupBoxFilter.Controls.Add(this.btnThongKe);
            this.groupBoxFilter.Controls.Add(this.cbxSan);
            this.groupBoxFilter.Controls.Add(this.label3);
            this.groupBoxFilter.Controls.Add(this.dtpDenNgay);
            this.groupBoxFilter.Controls.Add(this.label2);
            this.groupBoxFilter.Controls.Add(this.dtpTuNgay);
            this.groupBoxFilter.Controls.Add(this.label1);
            this.groupBoxFilter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBoxFilter.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.groupBoxFilter.Location = new System.Drawing.Point(0, 0);
            this.groupBoxFilter.Name = "groupBoxFilter";
            this.groupBoxFilter.Size = new System.Drawing.Size(1100, 80);
            this.groupBoxFilter.TabIndex = 0;
            this.groupBoxFilter.TabStop = false;
            this.groupBoxFilter.Text = "Bộ Lọc Thống Kê Sân";

            // label1
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(30, 35);
            this.label1.Name = "label1";
            this.label1.Text = "Từ ngày:";

            // dtpTuNgay
            this.dtpTuNgay.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpTuNgay.Location = new System.Drawing.Point(100, 32);
            this.dtpTuNgay.Name = "dtpTuNgay";
            this.dtpTuNgay.Size = new System.Drawing.Size(120, 25);

            // label2
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(240, 35);
            this.label2.Name = "label2";
            this.label2.Text = "Đến ngày:";

            // dtpDenNgay
            this.dtpDenNgay.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpDenNgay.Location = new System.Drawing.Point(320, 32);
            this.dtpDenNgay.Name = "dtpDenNgay";
            this.dtpDenNgay.Size = new System.Drawing.Size(120, 25);

            // label3
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(470, 35);
            this.label3.Name = "label3";
            this.label3.Text = "Chọn sân:";

            // cbxSan
            this.cbxSan.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxSan.FormattingEnabled = true;
            this.cbxSan.Location = new System.Drawing.Point(550, 32);
            this.cbxSan.Name = "cbxSan";
            this.cbxSan.Size = new System.Drawing.Size(150, 25);

            // btnThongKe
            this.btnThongKe.BackColor = System.Drawing.Color.ForestGreen;
            this.btnThongKe.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnThongKe.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnThongKe.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnThongKe.ForeColor = System.Drawing.Color.White;
            this.btnThongKe.Location = new System.Drawing.Point(730, 28);
            this.btnThongKe.Name = "btnThongKe";
            this.btnThongKe.Size = new System.Drawing.Size(120, 32);
            this.btnThongKe.Text = "Thống Kê";
            this.btnThongKe.UseVisualStyleBackColor = false;
            this.btnThongKe.Click += new System.EventHandler(this.btnThongKe_Click);

            // layoutCharts
            this.layoutCharts.ColumnCount = 2;
            this.layoutCharts.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.layoutCharts.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.layoutCharts.Controls.Add(this.chartGioHoatDong, 0, 0);
            this.layoutCharts.Controls.Add(this.chartDoanhThu, 1, 0);
            this.layoutCharts.Controls.Add(this.chartTrangThai, 0, 1);
            this.layoutCharts.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutCharts.Location = new System.Drawing.Point(0, 80);
            this.layoutCharts.Name = "layoutCharts";
            this.layoutCharts.RowCount = 2;
            this.layoutCharts.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.layoutCharts.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.layoutCharts.Size = new System.Drawing.Size(1100, 620);
            this.layoutCharts.TabIndex = 1;

            // Chart 1: Giờ Hoạt Động
            chartArea1.Name = "ChartArea1";
            this.chartGioHoatDong.ChartAreas.Add(chartArea1);
            this.chartGioHoatDong.Dock = System.Windows.Forms.DockStyle.Fill;
            legend1.Name = "Legend1";
            this.chartGioHoatDong.Legends.Add(legend1);
            this.chartGioHoatDong.Name = "chartGioHoatDong";
            series1.ChartArea = "ChartArea1";
            series1.Legend = "Legend1";
            series1.Name = "Số Giờ";
            series1.Color = System.Drawing.Color.DodgerBlue;
            series1.IsValueShownAsLabel = true;
            series1.LabelFormat = "N1";
            this.chartGioHoatDong.Series.Add(series1);
            title1.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            title1.Text = "TỔNG GIỜ HOẠT ĐỘNG";
            this.chartGioHoatDong.Titles.Add(title1);

            // Chart 2: Doanh Thu
            chartArea2.Name = "ChartArea1";
            this.chartDoanhThu.ChartAreas.Add(chartArea2);
            this.chartDoanhThu.Dock = System.Windows.Forms.DockStyle.Fill;
            legend2.Name = "Legend1";
            this.chartDoanhThu.Legends.Add(legend2);
            this.chartDoanhThu.Name = "chartDoanhThu";
            series2.ChartArea = "ChartArea1";
            series2.Legend = "Legend1";
            series2.Name = "Doanh Thu (VNĐ)";
            series2.Color = System.Drawing.Color.OrangeRed;
            series2.IsValueShownAsLabel = true;
            series2.LabelFormat = "N0";
            this.chartDoanhThu.Series.Add(series2);
            title2.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            title2.Text = "DOANH THU SÂN";
            this.chartDoanhThu.Titles.Add(title2);

            // Chart 3: Trạng Thái
            chartArea3.Name = "ChartArea1";
            this.chartTrangThai.ChartAreas.Add(chartArea3);
            this.layoutCharts.SetColumnSpan(this.chartTrangThai, 2);
            this.chartTrangThai.Dock = System.Windows.Forms.DockStyle.Fill;
            legend3.Name = "Legend1";
            this.chartTrangThai.Legends.Add(legend3);
            this.chartTrangThai.Name = "chartTrangThai";

            series3.ChartArea = "ChartArea1";
            series3.Legend = "Legend1";
            series3.Name = "Đã Thanh Toán";
            series3.Color = System.Drawing.Color.ForestGreen;
            series3.IsValueShownAsLabel = true;

            series4.ChartArea = "ChartArea1";
            series4.Legend = "Legend1";
            series4.Name = "Đã Hủy";
            series4.Color = System.Drawing.Color.Gray;
            series4.IsValueShownAsLabel = true;

            this.chartTrangThai.Series.Add(series3);
            this.chartTrangThai.Series.Add(series4);
            title3.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            title3.Text = "TỶ LỆ THANH TOÁN / HỦY";
            this.chartTrangThai.Titles.Add(title3);

            // Form Main
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1100, 700);
            this.Controls.Add(this.layoutCharts);
            this.Controls.Add(this.panelFilter);
            this.Name = "ThongKeSan";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "BÁO CÁO THỐNG KÊ HOẠT ĐỘNG SÂN";
            this.Load += new System.EventHandler(this.ThongKeSan_Load);

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