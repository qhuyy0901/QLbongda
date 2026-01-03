namespace TrangChu
{
    partial class TKe_HoaDon
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
            this.panelFooter = new System.Windows.Forms.Panel();
            this.btnTroVe = new System.Windows.Forms.Button();
            this.lblTongDoanhThu = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.btnXemChiTiet = new System.Windows.Forms.Button();
            this.btnXuatExcel = new System.Windows.Forms.Button();
            this.panelReport = new System.Windows.Forms.Panel();
            this.reportViewer1 = new Microsoft.Reporting.WinForms.ReportViewer();
            this.btnTaiLai = new System.Windows.Forms.Button();
            this.btnLoc = new System.Windows.Forms.Button();
            this.cboThang = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cboNam = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.thToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.thongkesanToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.label4 = new System.Windows.Forms.Label();
            this.txtTimKiemHoaDon = new System.Windows.Forms.TextBox();
            this.panelFooter.SuspendLayout();
            this.panelReport.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelFooter
            // 
            this.panelFooter.BackColor = System.Drawing.Color.WhiteSmoke;
            this.panelFooter.Controls.Add(this.btnTroVe);
            this.panelFooter.Controls.Add(this.lblTongDoanhThu);
            this.panelFooter.Controls.Add(this.label3);
            this.panelFooter.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelFooter.Location = new System.Drawing.Point(0, 501);
            this.panelFooter.Name = "panelFooter";
            this.panelFooter.Size = new System.Drawing.Size(1000, 60);
            this.panelFooter.TabIndex = 1;
            // 
            // btnTroVe
            // 
            this.btnTroVe.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnTroVe.BackColor = System.Drawing.Color.DimGray;
            this.btnTroVe.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnTroVe.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTroVe.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnTroVe.ForeColor = System.Drawing.Color.White;
            this.btnTroVe.Location = new System.Drawing.Point(3, 13);
            this.btnTroVe.Name = "btnTroVe";
            this.btnTroVe.Size = new System.Drawing.Size(100, 35);
            this.btnTroVe.TabIndex = 5;
            this.btnTroVe.Text = "🔙 Thoát";
            this.btnTroVe.UseVisualStyleBackColor = false;
            this.btnTroVe.Click += new System.EventHandler(this.btnTroVe_Click);
            // 
            // lblTongDoanhThu
            // 
            this.lblTongDoanhThu.AutoSize = true;
            this.lblTongDoanhThu.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.lblTongDoanhThu.ForeColor = System.Drawing.Color.Red;
            this.lblTongDoanhThu.Location = new System.Drawing.Point(353, 18);
            this.lblTongDoanhThu.Name = "lblTongDoanhThu";
            this.lblTongDoanhThu.Size = new System.Drawing.Size(70, 25);
            this.lblTongDoanhThu.TabIndex = 1;
            this.lblTongDoanhThu.Text = "0 VNĐ";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.label3.Location = new System.Drawing.Point(161, 18);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(187, 21);
            this.label3.TabIndex = 0;
            this.label3.Text = "💰 TỔNG DOANH THU:";
            // 
            // btnXemChiTiet
            // 
            this.btnXemChiTiet.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnXemChiTiet.BackColor = System.Drawing.Color.Orange;
            this.btnXemChiTiet.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnXemChiTiet.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnXemChiTiet.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnXemChiTiet.ForeColor = System.Drawing.Color.White;
            this.btnXemChiTiet.Location = new System.Drawing.Point(870, 37);
            this.btnXemChiTiet.Name = "btnXemChiTiet";
            this.btnXemChiTiet.Size = new System.Drawing.Size(101, 32);
            this.btnXemChiTiet.TabIndex = 4;
            this.btnXemChiTiet.Text = "🔍 Xem CT";
            this.btnXemChiTiet.UseVisualStyleBackColor = false;
            this.btnXemChiTiet.Click += new System.EventHandler(this.btnXemChiTiet_Click);
            // 
            // btnXuatExcel
            // 
            this.btnXuatExcel.BackColor = System.Drawing.Color.ForestGreen;
            this.btnXuatExcel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnXuatExcel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnXuatExcel.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnXuatExcel.ForeColor = System.Drawing.Color.White;
            this.btnXuatExcel.Location = new System.Drawing.Point(751, 37);
            this.btnXuatExcel.Name = "btnXuatExcel";
            this.btnXuatExcel.Size = new System.Drawing.Size(100, 32);
            this.btnXuatExcel.TabIndex = 3;
            this.btnXuatExcel.Text = "📁  Xuất Excel";
            this.btnXuatExcel.UseVisualStyleBackColor = false;
            this.btnXuatExcel.Click += new System.EventHandler(this.btnXuatExcel_Click);
            // 
            // panelReport
            // 
            this.panelReport.Controls.Add(this.reportViewer1);
            this.panelReport.Location = new System.Drawing.Point(0, 80);
            this.panelReport.Name = "panelReport";
            this.panelReport.Size = new System.Drawing.Size(1000, 421);
            this.panelReport.TabIndex = 2;
            // 
            // reportViewer1
            // 
            this.reportViewer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.reportViewer1.Location = new System.Drawing.Point(0, 0);
            this.reportViewer1.Name = "reportViewer1";
            this.reportViewer1.ServerReport.BearerToken = null;
            this.reportViewer1.Size = new System.Drawing.Size(1000, 421);
            this.reportViewer1.TabIndex = 0;
            // 
            // btnTaiLai
            // 
            this.btnTaiLai.BackColor = System.Drawing.Color.Gray;
            this.btnTaiLai.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnTaiLai.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTaiLai.ForeColor = System.Drawing.Color.White;
            this.btnTaiLai.Location = new System.Drawing.Point(632, 37);
            this.btnTaiLai.Name = "btnTaiLai";
            this.btnTaiLai.Size = new System.Drawing.Size(100, 32);
            this.btnTaiLai.TabIndex = 11;
            this.btnTaiLai.Text = "🔄 Tải Lại";
            this.btnTaiLai.UseVisualStyleBackColor = false;
            this.btnTaiLai.Click += new System.EventHandler(this.btnTaiLai_Click_1);
            // 
            // btnLoc
            // 
            this.btnLoc.BackColor = System.Drawing.Color.DodgerBlue;
            this.btnLoc.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnLoc.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLoc.ForeColor = System.Drawing.Color.White;
            this.btnLoc.Location = new System.Drawing.Point(514, 37);
            this.btnLoc.Name = "btnLoc";
            this.btnLoc.Size = new System.Drawing.Size(100, 32);
            this.btnLoc.TabIndex = 10;
            this.btnLoc.Text = "🔍 Lọc";
            this.btnLoc.UseVisualStyleBackColor = false;
            this.btnLoc.Click += new System.EventHandler(this.btnLoc_Click_1);
            // 
            // cboThang
            // 
            this.cboThang.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboThang.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cboThang.FormattingEnabled = true;
            this.cboThang.Location = new System.Drawing.Point(192, 41);
            this.cboThang.Name = "cboThang";
            this.cboThang.Size = new System.Drawing.Size(78, 25);
            this.cboThang.TabIndex = 9;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.label2.Location = new System.Drawing.Point(136, 44);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(50, 19);
            this.label2.TabIndex = 8;
            this.label2.Text = "Tháng:";
            // 
            // cboNam
            // 
            this.cboNam.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboNam.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cboNam.FormattingEnabled = true;
            this.cboNam.Location = new System.Drawing.Point(50, 41);
            this.cboNam.Name = "cboNam";
            this.cboNam.Size = new System.Drawing.Size(77, 25);
            this.cboNam.TabIndex = 7;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.label1.Location = new System.Drawing.Point(3, 46);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 19);
            this.label1.TabIndex = 6;
            this.label1.Text = "Năm:";
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.Color.Green;
            this.menuStrip1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.thToolStripMenuItem,
            this.thongkesanToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1000, 24);
            this.menuStrip1.TabIndex = 12;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // thToolStripMenuItem
            // 
            this.thToolStripMenuItem.ForeColor = System.Drawing.Color.White;
            this.thToolStripMenuItem.Name = "thToolStripMenuItem";
            this.thToolStripMenuItem.Size = new System.Drawing.Size(151, 20);
            this.thToolStripMenuItem.Text = "📈 Thống Kê Doanh Thu";
            this.thToolStripMenuItem.Click += new System.EventHandler(this.thToolStripMenuItem_Click);
            // 
            // thongkesanToolStripMenuItem
            // 
            this.thongkesanToolStripMenuItem.ForeColor = System.Drawing.Color.White;
            this.thongkesanToolStripMenuItem.Name = "thongkesanToolStripMenuItem";
            this.thongkesanToolStripMenuItem.Size = new System.Drawing.Size(111, 20);
            this.thongkesanToolStripMenuItem.Text = "📅 Thống Kê Sân";
            this.thongkesanToolStripMenuItem.Click += new System.EventHandler(this.thongkesanToolStripMenuItem_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.label4.Location = new System.Drawing.Point(284, 44);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(68, 19);
            this.label4.TabIndex = 13;
            this.label4.Text = "Tìm Kiếm:";
            // 
            // txtTimKiemHoaDon
            // 
            this.txtTimKiemHoaDon.Location = new System.Drawing.Point(358, 41);
            this.txtTimKiemHoaDon.Multiline = true;
            this.txtTimKiemHoaDon.Name = "txtTimKiemHoaDon";
            this.txtTimKiemHoaDon.Size = new System.Drawing.Size(100, 25);
            this.txtTimKiemHoaDon.TabIndex = 14;
            this.txtTimKiemHoaDon.TextChanged += new System.EventHandler(this.txtTimKiemHoaDon_TextChanged);
            // 
            // TKe_HoaDon
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1000, 561);
            this.Controls.Add(this.txtTimKiemHoaDon);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.btnTaiLai);
            this.Controls.Add(this.btnXemChiTiet);
            this.Controls.Add(this.btnLoc);
            this.Controls.Add(this.btnXuatExcel);
            this.Controls.Add(this.cboThang);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cboNam);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.panelReport);
            this.Controls.Add(this.panelFooter);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "TKe_HoaDon";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "BÁO CÁO THỐNG KÊ HÓA ĐƠN";
            this.Load += new System.EventHandler(this.TKe_HoaDon_Load);
            this.panelFooter.ResumeLayout(false);
            this.panelFooter.PerformLayout();
            this.panelReport.ResumeLayout(false);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Panel panelFooter;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblTongDoanhThu;
        private System.Windows.Forms.Button btnXuatExcel;
        private System.Windows.Forms.Button btnXemChiTiet;
        private System.Windows.Forms.Button btnTroVe;
        private System.Windows.Forms.Panel panelReport;
        private Microsoft.Reporting.WinForms.ReportViewer reportViewer1;
        private System.Windows.Forms.Button btnTaiLai;
        private System.Windows.Forms.Button btnLoc;
        private System.Windows.Forms.ComboBox cboThang;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cboNam;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem thToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem thongkesanToolStripMenuItem;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtTimKiemHoaDon;
    }
}