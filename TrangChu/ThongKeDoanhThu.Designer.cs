namespace TrangChu
{
    partial class ThongKeDoanhThu
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
            System.Windows.Forms.DataVisualization.Charting.Series seriesSan = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series seriesDV = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Title title1 = new System.Windows.Forms.DataVisualization.Charting.Title();

            this.panelFilter = new System.Windows.Forms.Panel();
            this.groupBoxFilter = new System.Windows.Forms.GroupBox();
            this.btnThongKe = new System.Windows.Forms.Button();
            this.cbThang = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cbNam = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();

            this.panelChart = new System.Windows.Forms.Panel();
            this.chartDoanhThu = new System.Windows.Forms.DataVisualization.Charting.Chart();

            this.panelTotal = new System.Windows.Forms.Panel();
            this.groupBoxTotal = new System.Windows.Forms.GroupBox();
            this.lblTongCong = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.panelLine = new System.Windows.Forms.Panel();
            this.lblTongDichVu = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.lblTongSan = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();

            this.panelFilter.SuspendLayout();
            this.groupBoxFilter.SuspendLayout();
            this.panelChart.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chartDoanhThu)).BeginInit();
            this.panelTotal.SuspendLayout();
            this.groupBoxTotal.SuspendLayout();
            this.SuspendLayout();

            // 
            // panelFilter (Khu vực chọn Năm/Tháng)
            // 
            this.panelFilter.Controls.Add(this.groupBoxFilter);
            this.panelFilter.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelFilter.Location = new System.Drawing.Point(0, 0);
            this.panelFilter.Name = "panelFilter";
            this.panelFilter.Size = new System.Drawing.Size(900, 80);
            this.panelFilter.TabIndex = 0;

            // groupBoxFilter
            this.groupBoxFilter.Controls.Add(this.btnThongKe);
            this.groupBoxFilter.Controls.Add(this.cbThang);
            this.groupBoxFilter.Controls.Add(this.label2);
            this.groupBoxFilter.Controls.Add(this.cbNam);
            this.groupBoxFilter.Controls.Add(this.label1);
            this.groupBoxFilter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBoxFilter.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBoxFilter.Location = new System.Drawing.Point(0, 0);
            this.groupBoxFilter.Text = "Bộ Lọc Thời Gian";

            // label1: Năm
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(30, 35);
            this.label1.Text = "Năm:";

            // cbNam
            this.cbNam.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbNam.FormattingEnabled = true;
            this.cbNam.Location = new System.Drawing.Point(80, 32);
            this.cbNam.Size = new System.Drawing.Size(100, 25);

            // label2: Tháng
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(220, 35);
            this.label2.Text = "Tháng:";

            // cbThang
            this.cbThang.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbThang.FormattingEnabled = true;
            this.cbThang.Location = new System.Drawing.Point(280, 32);
            this.cbThang.Size = new System.Drawing.Size(120, 25);

            // btnThongKe (Nút Xem)
            this.btnThongKe.BackColor = System.Drawing.Color.DodgerBlue;
            this.btnThongKe.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnThongKe.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnThongKe.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnThongKe.ForeColor = System.Drawing.Color.White;
            this.btnThongKe.Location = new System.Drawing.Point(450, 28);
            this.btnThongKe.Size = new System.Drawing.Size(100, 32);
            this.btnThongKe.Text = "Xem Báo Cáo";
            this.btnThongKe.UseVisualStyleBackColor = false;
            this.btnThongKe.Click += new System.EventHandler(this.btnThongKe_Click);

            // 
            // panelTotal (Khu vực hiển thị tổng tiền dưới cùng)
            // 
            this.panelTotal.Controls.Add(this.groupBoxTotal);
            this.panelTotal.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelTotal.Location = new System.Drawing.Point(0, 500);
            this.panelTotal.Name = "panelTotal";
            this.panelTotal.Size = new System.Drawing.Size(900, 100);
            this.panelTotal.TabIndex = 2;

            // groupBoxTotal
            this.groupBoxTotal.Controls.Add(this.lblTongCong);
            this.groupBoxTotal.Controls.Add(this.label6);
            this.groupBoxTotal.Controls.Add(this.panelLine);
            this.groupBoxTotal.Controls.Add(this.lblTongDichVu);
            this.groupBoxTotal.Controls.Add(this.label4);
            this.groupBoxTotal.Controls.Add(this.lblTongSan);
            this.groupBoxTotal.Controls.Add(this.label3);
            this.groupBoxTotal.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBoxTotal.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBoxTotal.Location = new System.Drawing.Point(0, 0);
            this.groupBoxTotal.Text = "Chi Tiết Doanh Thu";

            // Labels for Field Revenue
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(50, 30);
            this.label3.Text = "Tổng doanh thu SÂN:";

            this.lblTongSan.AutoSize = true;
            this.lblTongSan.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblTongSan.ForeColor = System.Drawing.Color.DodgerBlue;
            this.lblTongSan.Location = new System.Drawing.Point(220, 30);
            this.lblTongSan.Text = "0 VNĐ";

            // Labels for Service Revenue
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(50, 60);
            this.label4.Text = "Tổng doanh thu DỊCH VỤ:";

            this.lblTongDichVu.AutoSize = true;
            this.lblTongDichVu.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblTongDichVu.ForeColor = System.Drawing.Color.Orange;
            this.lblTongDichVu.Location = new System.Drawing.Point(220, 60);
            this.lblTongDichVu.Text = "0 VNĐ";

            // Separator Line
            this.panelLine.BackColor = System.Drawing.Color.Silver;
            this.panelLine.Location = new System.Drawing.Point(400, 20);
            this.panelLine.Size = new System.Drawing.Size(2, 70);

            // Labels for Total Revenue
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.label6.Location = new System.Drawing.Point(450, 40);
            this.label6.Text = "TỔNG CỘNG:";

            this.lblTongCong.AutoSize = true;
            this.lblTongCong.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.lblTongCong.ForeColor = System.Drawing.Color.Red;
            this.lblTongCong.Location = new System.Drawing.Point(570, 38);
            this.lblTongCong.Text = "0 VNĐ";

            // 
            // panelChart (Khu vực biểu đồ)
            // 
            this.panelChart.Controls.Add(this.chartDoanhThu);
            this.panelChart.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelChart.Location = new System.Drawing.Point(0, 80);
            this.panelChart.Name = "panelChart";
            this.panelChart.Size = new System.Drawing.Size(900, 420);
            this.panelChart.TabIndex = 1;
            this.panelChart.Padding = new System.Windows.Forms.Padding(10);

            // chartDoanhThu
            chartArea1.Name = "ChartArea1";
            this.chartDoanhThu.ChartAreas.Add(chartArea1);
            this.chartDoanhThu.Dock = System.Windows.Forms.DockStyle.Fill;
            legend1.Name = "Legend1";
            legend1.Docking = System.Windows.Forms.DataVisualization.Charting.Docking.Top;
            this.chartDoanhThu.Legends.Add(legend1);
            this.chartDoanhThu.Location = new System.Drawing.Point(10, 10);
            this.chartDoanhThu.Name = "chartDoanhThu";

            // Series 1: Sân
            seriesSan.ChartArea = "ChartArea1";
            seriesSan.Legend = "Legend1";
            seriesSan.Name = "Doanh Thu Sân";
            seriesSan.Color = System.Drawing.Color.DodgerBlue;
            seriesSan.IsValueShownAsLabel = true;
            seriesSan.LabelFormat = "N0";

            // Series 2: Dịch Vụ
            seriesDV.ChartArea = "ChartArea1";
            seriesDV.Legend = "Legend1";
            seriesDV.Name = "Doanh Thu Dịch Vụ";
            seriesDV.Color = System.Drawing.Color.Orange;
            seriesDV.IsValueShownAsLabel = true;
            seriesDV.LabelFormat = "N0";

            this.chartDoanhThu.Series.Add(seriesSan);
            this.chartDoanhThu.Series.Add(seriesDV);

            title1.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            title1.Name = "Title1";
            title1.Text = "BIỂU ĐỒ DOANH THU SÂN VÀ DỊCH VỤ";
            this.chartDoanhThu.Titles.Add(title1);

            // 
            // frmThongKeDoanhThu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(900, 600);
            this.Controls.Add(this.panelChart);
            this.Controls.Add(this.panelTotal);
            this.Controls.Add(this.panelFilter);
            this.Name = "frmThongKeDoanhThu";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Báo Cáo Doanh Thu Tổng Hợp";

            this.panelFilter.ResumeLayout(false);
            this.groupBoxFilter.ResumeLayout(false);
            this.groupBoxFilter.PerformLayout();
            this.panelChart.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chartDoanhThu)).EndInit();
            this.panelTotal.ResumeLayout(false);
            this.groupBoxTotal.ResumeLayout(false);
            this.groupBoxTotal.PerformLayout();
            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Panel panelFilter;
        private System.Windows.Forms.GroupBox groupBoxFilter;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbNam;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cbThang;
        private System.Windows.Forms.Button btnThongKe;

        private System.Windows.Forms.Panel panelChart;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartDoanhThu;

        private System.Windows.Forms.Panel panelTotal;
        private System.Windows.Forms.GroupBox groupBoxTotal;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblTongSan;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lblTongDichVu;
        private System.Windows.Forms.Panel panelLine;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label lblTongCong;
    }
}