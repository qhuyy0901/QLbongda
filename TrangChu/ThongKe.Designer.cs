namespace TrangChu
{
    partial class ThongKe
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend2 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.label1 = new System.Windows.Forms.Label();
            this.cboNam = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cboLoaiThongKe = new System.Windows.Forms.ComboBox();
            this.btnThongKe = new System.Windows.Forms.Button();
            this.chartDoanhThu = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.thốngToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.thốngKêSânToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.chartDoanhThu)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.label1.Location = new System.Drawing.Point(33, 42);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 19);
            this.label1.TabIndex = 0;
            this.label1.Text = "Năm:";
            // 
            // cboNam
            // 
            this.cboNam.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboNam.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cboNam.FormattingEnabled = true;
            this.cboNam.Location = new System.Drawing.Point(76, 39);
            this.cboNam.Name = "cboNam";
            this.cboNam.Size = new System.Drawing.Size(103, 25);
            this.cboNam.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.label2.Location = new System.Drawing.Point(204, 42);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(96, 19);
            this.label2.TabIndex = 2;
            this.label2.Text = "Loại thống kê:";
            // 
            // cboLoaiThongKe
            // 
            this.cboLoaiThongKe.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboLoaiThongKe.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cboLoaiThongKe.FormattingEnabled = true;
            this.cboLoaiThongKe.Items.AddRange(new object[] {
            "Theo tháng",
            "Theo năm"});
            this.cboLoaiThongKe.Location = new System.Drawing.Point(306, 39);
            this.cboLoaiThongKe.Name = "cboLoaiThongKe";
            this.cboLoaiThongKe.Size = new System.Drawing.Size(129, 25);
            this.cboLoaiThongKe.TabIndex = 3;
            // 
            // btnThongKe
            // 
            this.btnThongKe.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnThongKe.Location = new System.Drawing.Point(453, 37);
            this.btnThongKe.Name = "btnThongKe";
            this.btnThongKe.Size = new System.Drawing.Size(103, 26);
            this.btnThongKe.TabIndex = 4;
            this.btnThongKe.Text = "Thống kê";
            this.btnThongKe.UseVisualStyleBackColor = true;
            // 
            // chartDoanhThu
            // 
            chartArea2.Name = "ChartArea1";
            this.chartDoanhThu.ChartAreas.Add(chartArea2);
            legend2.Name = "Legend1";
            this.chartDoanhThu.Legends.Add(legend2);
            this.chartDoanhThu.Location = new System.Drawing.Point(26, 80);
            this.chartDoanhThu.Name = "chartDoanhThu";
            series2.ChartArea = "ChartArea1";
            series2.Legend = "Legend1";
            series2.Name = "Doanh thu";
            this.chartDoanhThu.Series.Add(series2);
            this.chartDoanhThu.Size = new System.Drawing.Size(771, 371);
            this.chartDoanhThu.TabIndex = 5;
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.thốngToolStripMenuItem,
            this.thốngKêSânToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(840, 24);
            this.menuStrip1.TabIndex = 6;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // thốngToolStripMenuItem
            // 
            this.thốngToolStripMenuItem.Name = "thốngToolStripMenuItem";
            this.thốngToolStripMenuItem.Size = new System.Drawing.Size(70, 20);
            this.thốngToolStripMenuItem.Text = "Thống Kê";
            // 
            // thốngKêSânToolStripMenuItem
            // 
            this.thốngKêSânToolStripMenuItem.Name = "thốngKêSânToolStripMenuItem";
            this.thốngKêSânToolStripMenuItem.Size = new System.Drawing.Size(92, 20);
            this.thốngKêSânToolStripMenuItem.Text = "Thống Kê Sân";
            this.thốngKêSânToolStripMenuItem.Click += new System.EventHandler(this.thốngKêSânToolStripMenuItem_Click);
            // 
            // ThongKe
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(840, 477);
            this.Controls.Add(this.chartDoanhThu);
            this.Controls.Add(this.btnThongKe);
            this.Controls.Add(this.cboLoaiThongKe);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cboNam);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "ThongKe";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "BÁO CÁO - THỐNG KÊ DOANH THU";
            ((System.ComponentModel.ISupportInitialize)(this.chartDoanhThu)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cboNam;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cboLoaiThongKe;
        private System.Windows.Forms.Button btnThongKe;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartDoanhThu;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem thốngToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem thốngKêSânToolStripMenuItem;
    }
}