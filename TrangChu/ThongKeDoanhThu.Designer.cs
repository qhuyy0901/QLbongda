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
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Title title1 = new System.Windows.Forms.DataVisualization.Charting.Title();
            this.panelFilter = new System.Windows.Forms.Panel();
            this.groupBoxFilter = new System.Windows.Forms.GroupBox();
            this.btnTroVe = new System.Windows.Forms.Button();
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
            // panelFilter
            // 
            this.panelFilter.Controls.Add(this.groupBoxFilter);
            this.panelFilter.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelFilter.Location = new System.Drawing.Point(0, 0);
            this.panelFilter.Name = "panelFilter";
            this.panelFilter.Size = new System.Drawing.Size(900, 80);
            this.panelFilter.TabIndex = 0;
            // 
            // groupBoxFilter
            // 
            this.groupBoxFilter.Controls.Add(this.btnTroVe);
            this.groupBoxFilter.Controls.Add(this.btnThongKe);
            this.groupBoxFilter.Controls.Add(this.cbThang);
            this.groupBoxFilter.Controls.Add(this.label2);
            this.groupBoxFilter.Controls.Add(this.cbNam);
            this.groupBoxFilter.Controls.Add(this.label1);
            this.groupBoxFilter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBoxFilter.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBoxFilter.Location = new System.Drawing.Point(0, 0);
            this.groupBoxFilter.Name = "groupBoxFilter";
            this.groupBoxFilter.Size = new System.Drawing.Size(900, 80);
            this.groupBoxFilter.TabIndex = 0;
            this.groupBoxFilter.TabStop = false;
            this.groupBoxFilter.Text = "Bộ Lọc Thời Gian";
            // 
            // btnTroVe
            // 
            this.btnTroVe.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnTroVe.BackColor = System.Drawing.Color.DimGray;
            this.btnTroVe.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnTroVe.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTroVe.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnTroVe.ForeColor = System.Drawing.Color.White;
            this.btnTroVe.Location = new System.Drawing.Point(649, 29);
            this.btnTroVe.Name = "btnTroVe";
            this.btnTroVe.Size = new System.Drawing.Size(100, 32);
            this.btnTroVe.TabIndex = 6;
            this.btnTroVe.Text = "🔙 Thoát";
            this.btnTroVe.UseVisualStyleBackColor = false;
            this.btnTroVe.Click += new System.EventHandler(this.btnTroVe_Click);
            // 
            // btnThongKe
            // 
            this.btnThongKe.BackColor = System.Drawing.Color.DodgerBlue;
            this.btnThongKe.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnThongKe.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnThongKe.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnThongKe.ForeColor = System.Drawing.Color.White;
            this.btnThongKe.Location = new System.Drawing.Point(509, 29);
            this.btnThongKe.Name = "btnThongKe";
            this.btnThongKe.Size = new System.Drawing.Size(100, 32);
            this.btnThongKe.TabIndex = 0;
            this.btnThongKe.Text = "📊 Xem";
            this.btnThongKe.UseVisualStyleBackColor = false;
            this.btnThongKe.Click += new System.EventHandler(this.btnThongKe_Click);
            // 
            // cbThang
            // 
            this.cbThang.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbThang.FormattingEnabled = true;
            this.cbThang.Location = new System.Drawing.Point(339, 33);
            this.cbThang.Name = "cbThang";
            this.cbThang.Size = new System.Drawing.Size(120, 25);
            this.cbThang.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(279, 36);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(50, 19);
            this.label2.TabIndex = 2;
            this.label2.Text = "Tháng:";
            // 
            // cbNam
            // 
            this.cbNam.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbNam.FormattingEnabled = true;
            this.cbNam.Location = new System.Drawing.Point(139, 33);
            this.cbNam.Name = "cbNam";
            this.cbNam.Size = new System.Drawing.Size(100, 25);
            this.cbNam.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(89, 36);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 19);
            this.label1.TabIndex = 4;
            this.label1.Text = "Năm:";
            // 
            // panelChart
            // 
            this.panelChart.Controls.Add(this.chartDoanhThu);
            this.panelChart.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelChart.Location = new System.Drawing.Point(0, 80);
            this.panelChart.Name = "panelChart";
            this.panelChart.Padding = new System.Windows.Forms.Padding(10);
            this.panelChart.Size = new System.Drawing.Size(900, 420);
            this.panelChart.TabIndex = 1;
            // 
            // chartDoanhThu
            // 
            chartArea1.Name = "ChartArea1";
            this.chartDoanhThu.ChartAreas.Add(chartArea1);
            this.chartDoanhThu.Dock = System.Windows.Forms.DockStyle.Fill;
            legend1.Docking = System.Windows.Forms.DataVisualization.Charting.Docking.Top;
            legend1.Name = "Legend1";
            this.chartDoanhThu.Legends.Add(legend1);
            this.chartDoanhThu.Location = new System.Drawing.Point(10, 10);
            this.chartDoanhThu.Name = "chartDoanhThu";
            series1.ChartArea = "ChartArea1";
            series1.Color = System.Drawing.Color.DodgerBlue;
            series1.IsValueShownAsLabel = true;
            series1.LabelFormat = "N0";
            series1.Legend = "Legend1";
            series1.Name = "Doanh Thu Sân";
            series2.ChartArea = "ChartArea1";
            series2.Color = System.Drawing.Color.Orange;
            series2.IsValueShownAsLabel = true;
            series2.LabelFormat = "N0";
            series2.Legend = "Legend1";
            series2.Name = "Doanh Thu Dịch Vụ";
            this.chartDoanhThu.Series.Add(series1);
            this.chartDoanhThu.Series.Add(series2);
            this.chartDoanhThu.Size = new System.Drawing.Size(880, 400);
            this.chartDoanhThu.TabIndex = 0;
            title1.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            title1.Name = "Title1";
            title1.Text = "BIỂU ĐỒ DOANH THU SÂN VÀ DỊCH VỤ";
            this.chartDoanhThu.Titles.Add(title1);
            // 
            // panelTotal
            // 
            this.panelTotal.Controls.Add(this.groupBoxTotal);
            this.panelTotal.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelTotal.Location = new System.Drawing.Point(0, 500);
            this.panelTotal.Name = "panelTotal";
            this.panelTotal.Size = new System.Drawing.Size(900, 100);
            this.panelTotal.TabIndex = 2;
            // 
            // groupBoxTotal
            // 
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
            this.groupBoxTotal.Name = "groupBoxTotal";
            this.groupBoxTotal.Size = new System.Drawing.Size(900, 100);
            this.groupBoxTotal.TabIndex = 0;
            this.groupBoxTotal.TabStop = false;
            this.groupBoxTotal.Text = "Chi Tiết Doanh Thu";
            // 
            // lblTongCong
            // 
            this.lblTongCong.AutoSize = true;
            this.lblTongCong.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.lblTongCong.ForeColor = System.Drawing.Color.Red;
            this.lblTongCong.Location = new System.Drawing.Point(570, 38);
            this.lblTongCong.Name = "lblTongCong";
            this.lblTongCong.Size = new System.Drawing.Size(70, 25);
            this.lblTongCong.TabIndex = 0;
            this.lblTongCong.Text = "0 VNĐ";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.label6.Location = new System.Drawing.Point(450, 40);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(109, 21);
            this.label6.TabIndex = 1;
            this.label6.Text = "TỔNG CỘNG:";
            // 
            // panelLine
            // 
            this.panelLine.BackColor = System.Drawing.Color.Silver;
            this.panelLine.Location = new System.Drawing.Point(400, 20);
            this.panelLine.Name = "panelLine";
            this.panelLine.Size = new System.Drawing.Size(2, 70);
            this.panelLine.TabIndex = 2;
            // 
            // lblTongDichVu
            // 
            this.lblTongDichVu.AutoSize = true;
            this.lblTongDichVu.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblTongDichVu.ForeColor = System.Drawing.Color.Orange;
            this.lblTongDichVu.Location = new System.Drawing.Point(220, 60);
            this.lblTongDichVu.Name = "lblTongDichVu";
            this.lblTongDichVu.Size = new System.Drawing.Size(55, 20);
            this.lblTongDichVu.TabIndex = 3;
            this.lblTongDichVu.Text = "0 VNĐ";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(50, 60);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(171, 19);
            this.label4.TabIndex = 4;
            this.label4.Text = "Tổng doanh thu DỊCH VỤ:";
            // 
            // lblTongSan
            // 
            this.lblTongSan.AutoSize = true;
            this.lblTongSan.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblTongSan.ForeColor = System.Drawing.Color.DodgerBlue;
            this.lblTongSan.Location = new System.Drawing.Point(220, 30);
            this.lblTongSan.Name = "lblTongSan";
            this.lblTongSan.Size = new System.Drawing.Size(55, 20);
            this.lblTongSan.TabIndex = 5;
            this.lblTongSan.Text = "0 VNĐ";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(50, 30);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(141, 19);
            this.label3.TabIndex = 6;
            this.label3.Text = "Tổng doanh thu SÂN:";
            // 
            // ThongKeDoanhThu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(900, 600);
            this.Controls.Add(this.panelChart);
            this.Controls.Add(this.panelTotal);
            this.Controls.Add(this.panelFilter);
            this.Name = "ThongKeDoanhThu";
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

        private System.Windows.Forms.Panel panelTotal;
        private System.Windows.Forms.GroupBox groupBoxTotal;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblTongSan;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lblTongDichVu;
        private System.Windows.Forms.Panel panelLine;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label lblTongCong;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartDoanhThu;
        private System.Windows.Forms.Button btnTroVe;
    }
}