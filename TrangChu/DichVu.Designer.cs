namespace TrangChu
{
    partial class DichVu
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.grpThongTinHoaDon = new System.Windows.Forms.GroupBox();
            this.btnThanhToan = new System.Windows.Forms.Button();
            this.lblLoaiHoaDon = new System.Windows.Forms.Label();
            this.rdoKhachLe = new System.Windows.Forms.RadioButton();
            this.rdoKhachDatSan = new System.Windows.Forms.RadioButton();
            this.lblMaLich = new System.Windows.Forms.Label();
            this.cbxLichDat = new System.Windows.Forms.ComboBox();
            this.lblTenKH = new System.Windows.Forms.Label();
            this.txtTenKH = new System.Windows.Forms.TextBox();
            this.lblSDT = new System.Windows.Forms.Label();
            this.txtSDT = new System.Windows.Forms.TextBox();
            this.grpDichVu = new System.Windows.Forms.GroupBox();
            this.dgvDichVu = new System.Windows.Forms.DataGridView();
            this.grpGioDichVu = new System.Windows.Forms.GroupBox();
            this.dgvGioHang = new System.Windows.Forms.DataGridView();
            this.btnXoaDV = new System.Windows.Forms.Button();
            this.btnThemDV = new System.Windows.Forms.Button();
            this.btnThemSP = new System.Windows.Forms.Button();
            this.btnSuaSP = new System.Windows.Forms.Button();
            this.btnXoaSP = new System.Windows.Forms.Button();
            this.btnQuayLai = new System.Windows.Forms.Button();
            this.grpThongTinHoaDon.SuspendLayout();
            this.grpDichVu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDichVu)).BeginInit();
            this.grpGioDichVu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvGioHang)).BeginInit();
            this.SuspendLayout();
            // 
            // grpThongTinHoaDon
            // 
            this.grpThongTinHoaDon.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.grpThongTinHoaDon.Controls.Add(this.btnThanhToan);
            this.grpThongTinHoaDon.Controls.Add(this.lblLoaiHoaDon);
            this.grpThongTinHoaDon.Controls.Add(this.rdoKhachLe);
            this.grpThongTinHoaDon.Controls.Add(this.rdoKhachDatSan);
            this.grpThongTinHoaDon.Controls.Add(this.lblMaLich);
            this.grpThongTinHoaDon.Controls.Add(this.cbxLichDat);
            this.grpThongTinHoaDon.Controls.Add(this.lblTenKH);
            this.grpThongTinHoaDon.Controls.Add(this.txtTenKH);
            this.grpThongTinHoaDon.Controls.Add(this.lblSDT);
            this.grpThongTinHoaDon.Controls.Add(this.txtSDT);
            this.grpThongTinHoaDon.Dock = System.Windows.Forms.DockStyle.Top;
            this.grpThongTinHoaDon.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpThongTinHoaDon.Location = new System.Drawing.Point(0, 0);
            this.grpThongTinHoaDon.Name = "grpThongTinHoaDon";
            this.grpThongTinHoaDon.Size = new System.Drawing.Size(923, 110);
            this.grpThongTinHoaDon.TabIndex = 0;
            this.grpThongTinHoaDon.TabStop = false;
            this.grpThongTinHoaDon.Text = "THÔNG TIN KHÁCH HÀNG & HÓA ĐƠN";
            // 
            // btnThanhToan
            // 
            this.btnThanhToan.BackColor = System.Drawing.Color.DodgerBlue;
            this.btnThanhToan.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnThanhToan.FlatAppearance.BorderSize = 0;
            this.btnThanhToan.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnThanhToan.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnThanhToan.ForeColor = System.Drawing.Color.White;
            this.btnThanhToan.Location = new System.Drawing.Point(770, 45);
            this.btnThanhToan.Name = "btnThanhToan";
            this.btnThanhToan.Size = new System.Drawing.Size(120, 40);
            this.btnThanhToan.TabIndex = 8;
            this.btnThanhToan.Text = "THANH TOÁN";
            this.btnThanhToan.UseVisualStyleBackColor = false;
            this.btnThanhToan.Click += new System.EventHandler(this.btnThanhToan_Click);
            // 
            // lblLoaiHoaDon
            // 
            this.lblLoaiHoaDon.AutoSize = true;
            this.lblLoaiHoaDon.Location = new System.Drawing.Point(30, 30);
            this.lblLoaiHoaDon.Name = "lblLoaiHoaDon";
            this.lblLoaiHoaDon.Size = new System.Drawing.Size(77, 19);
            this.lblLoaiHoaDon.TabIndex = 0;
            this.lblLoaiHoaDon.Text = "Loại khách:";
            // 
            // rdoKhachLe
            // 
            this.rdoKhachLe.AutoSize = true;
            this.rdoKhachLe.Checked = true;
            this.rdoKhachLe.Location = new System.Drawing.Point(120, 28);
            this.rdoKhachLe.Name = "rdoKhachLe";
            this.rdoKhachLe.Size = new System.Drawing.Size(115, 23);
            this.rdoKhachLe.TabIndex = 1;
            this.rdoKhachLe.TabStop = true;
            this.rdoKhachLe.Text = "Khách vãng lai";
            this.rdoKhachLe.UseVisualStyleBackColor = true;
            // 
            // rdoKhachDatSan
            // 
            this.rdoKhachDatSan.AutoSize = true;
            this.rdoKhachDatSan.Location = new System.Drawing.Point(240, 28);
            this.rdoKhachDatSan.Name = "rdoKhachDatSan";
            this.rdoKhachDatSan.Size = new System.Drawing.Size(113, 23);
            this.rdoKhachDatSan.TabIndex = 2;
            this.rdoKhachDatSan.Text = "Khách đặt sân";
            this.rdoKhachDatSan.UseVisualStyleBackColor = true;
            // 
            // lblMaLich
            // 
            this.lblMaLich.AutoSize = true;
            this.lblMaLich.Location = new System.Drawing.Point(30, 68);
            this.lblMaLich.Name = "lblMaLich";
            this.lblMaLich.Size = new System.Drawing.Size(73, 19);
            this.lblMaLich.TabIndex = 3;
            this.lblMaLich.Text = "Chọn Lịch:";
            // 
            // cbxLichDat
            // 
            this.cbxLichDat.Enabled = false;
            this.cbxLichDat.FormattingEnabled = true;
            this.cbxLichDat.Location = new System.Drawing.Point(103, 65);
            this.cbxLichDat.Name = "cbxLichDat";
            this.cbxLichDat.Size = new System.Drawing.Size(324, 25);
            this.cbxLichDat.TabIndex = 4;
            // 
            // lblTenKH
            // 
            this.lblTenKH.AutoSize = true;
            this.lblTenKH.Location = new System.Drawing.Point(457, 33);
            this.lblTenKH.Name = "lblTenKH";
            this.lblTenKH.Size = new System.Drawing.Size(55, 19);
            this.lblTenKH.TabIndex = 5;
            this.lblTenKH.Text = "Tên KH:";
            // 
            // txtTenKH
            // 
            this.txtTenKH.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtTenKH.Enabled = false;
            this.txtTenKH.Location = new System.Drawing.Point(527, 30);
            this.txtTenKH.Name = "txtTenKH";
            this.txtTenKH.Size = new System.Drawing.Size(200, 25);
            this.txtTenKH.TabIndex = 6;
            // 
            // lblSDT
            // 
            this.lblSDT.AutoSize = true;
            this.lblSDT.Location = new System.Drawing.Point(457, 71);
            this.lblSDT.Name = "lblSDT";
            this.lblSDT.Size = new System.Drawing.Size(36, 19);
            this.lblSDT.TabIndex = 7;
            this.lblSDT.Text = "SĐT:";
            // 
            // txtSDT
            // 
            this.txtSDT.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtSDT.Enabled = false;
            this.txtSDT.Location = new System.Drawing.Point(527, 68);
            this.txtSDT.Name = "txtSDT";
            this.txtSDT.Size = new System.Drawing.Size(200, 25);
            this.txtSDT.TabIndex = 8;
            // 
            // grpDichVu
            // 
            this.grpDichVu.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.grpDichVu.BackColor = System.Drawing.Color.White;
            this.grpDichVu.Controls.Add(this.dgvDichVu);
            this.grpDichVu.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpDichVu.Location = new System.Drawing.Point(12, 120);
            this.grpDichVu.Name = "grpDichVu";
            this.grpDichVu.Size = new System.Drawing.Size(327, 260);
            this.grpDichVu.TabIndex = 1;
            this.grpDichVu.TabStop = false;
            this.grpDichVu.Text = "DANH SÁCH DỊCH VỤ";
            // 
            // dgvDichVu
            // 
            this.dgvDichVu.AllowUserToOrderColumns = true;
            this.dgvDichVu.BackgroundColor = System.Drawing.Color.White;
            this.dgvDichVu.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.ForestGreen;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvDichVu.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvDichVu.ColumnHeadersHeight = 30;
            this.dgvDichVu.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvDichVu.EnableHeadersVisualStyles = false;
            this.dgvDichVu.Location = new System.Drawing.Point(3, 21);
            this.dgvDichVu.Name = "dgvDichVu";
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.WhiteSmoke;
            this.dgvDichVu.RowsDefaultCellStyle = dataGridViewCellStyle2;
            this.dgvDichVu.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvDichVu.Size = new System.Drawing.Size(321, 236);
            this.dgvDichVu.TabIndex = 0;
            // 
            // grpGioDichVu
            // 
            this.grpGioDichVu.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpGioDichVu.BackColor = System.Drawing.Color.White;
            this.grpGioDichVu.Controls.Add(this.dgvGioHang);
            this.grpGioDichVu.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpGioDichVu.Location = new System.Drawing.Point(433, 120);
            this.grpGioDichVu.Name = "grpGioDichVu";
            this.grpGioDichVu.Size = new System.Drawing.Size(478, 260);
            this.grpGioDichVu.TabIndex = 3;
            this.grpGioDichVu.TabStop = false;
            this.grpGioDichVu.Text = "GIỎ DỊCH VỤ ĐÃ CHỌN";
            // 
            // dgvGioHang
            // 
            this.dgvGioHang.AllowUserToAddRows = false;
            this.dgvGioHang.BackgroundColor = System.Drawing.Color.White;
            this.dgvGioHang.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.Orange;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvGioHang.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dgvGioHang.ColumnHeadersHeight = 30;
            this.dgvGioHang.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvGioHang.EnableHeadersVisualStyles = false;
            this.dgvGioHang.Location = new System.Drawing.Point(3, 21);
            this.dgvGioHang.Name = "dgvGioHang";
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.WhiteSmoke;
            this.dgvGioHang.RowsDefaultCellStyle = dataGridViewCellStyle4;
            this.dgvGioHang.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvGioHang.Size = new System.Drawing.Size(472, 236);
            this.dgvGioHang.TabIndex = 0;
            // 
            // btnXoaDV
            // 
            this.btnXoaDV.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnXoaDV.BackColor = System.Drawing.Color.White;
            this.btnXoaDV.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnXoaDV.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnXoaDV.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnXoaDV.ForeColor = System.Drawing.Color.Crimson;
            this.btnXoaDV.Location = new System.Drawing.Point(678, 391);
            this.btnXoaDV.Name = "btnXoaDV";
            this.btnXoaDV.Size = new System.Drawing.Size(100, 30);
            this.btnXoaDV.TabIndex = 2;
            this.btnXoaDV.Text = "Xóa dòng";
            this.btnXoaDV.UseVisualStyleBackColor = false;
            this.btnXoaDV.Click += new System.EventHandler(this.btnXoaDV_Click);
            // 
            // btnThemDV
            // 
            this.btnThemDV.BackColor = System.Drawing.Color.ForestGreen;
            this.btnThemDV.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnThemDV.FlatAppearance.BorderSize = 0;
            this.btnThemDV.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnThemDV.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnThemDV.ForeColor = System.Drawing.Color.White;
            this.btnThemDV.Location = new System.Drawing.Point(351, 244);
            this.btnThemDV.Name = "btnThemDV";
            this.btnThemDV.Size = new System.Drawing.Size(56, 73);
            this.btnThemDV.TabIndex = 2;
            this.btnThemDV.Text = ">>";
            this.btnThemDV.UseVisualStyleBackColor = false;
            this.btnThemDV.Click += new System.EventHandler(this.btnThemDV_Click);
            // 
            // btnThemSP
            // 
            this.btnThemSP.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnThemSP.BackColor = System.Drawing.Color.ForestGreen;
            this.btnThemSP.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnThemSP.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnThemSP.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnThemSP.ForeColor = System.Drawing.Color.White;
            this.btnThemSP.Location = new System.Drawing.Point(15, 386);
            this.btnThemSP.Name = "btnThemSP";
            this.btnThemSP.Size = new System.Drawing.Size(92, 35);
            this.btnThemSP.TabIndex = 4;
            this.btnThemSP.Text = "Thêm Mới SP";
            this.btnThemSP.UseVisualStyleBackColor = false;
            // 
            // btnSuaSP
            // 
            this.btnSuaSP.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnSuaSP.BackColor = System.Drawing.Color.Goldenrod;
            this.btnSuaSP.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSuaSP.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSuaSP.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnSuaSP.ForeColor = System.Drawing.Color.White;
            this.btnSuaSP.Location = new System.Drawing.Point(136, 389);
            this.btnSuaSP.Name = "btnSuaSP";
            this.btnSuaSP.Size = new System.Drawing.Size(87, 35);
            this.btnSuaSP.TabIndex = 5;
            this.btnSuaSP.Text = "Cập nhật SP";
            this.btnSuaSP.UseVisualStyleBackColor = false;
            // 
            // btnXoaSP
            // 
            this.btnXoaSP.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnXoaSP.BackColor = System.Drawing.Color.Crimson;
            this.btnXoaSP.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnXoaSP.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnXoaSP.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnXoaSP.ForeColor = System.Drawing.Color.White;
            this.btnXoaSP.Location = new System.Drawing.Point(251, 386);
            this.btnXoaSP.Name = "btnXoaSP";
            this.btnXoaSP.Size = new System.Drawing.Size(88, 35);
            this.btnXoaSP.TabIndex = 6;
            this.btnXoaSP.Text = "Xóa SP";
            this.btnXoaSP.UseVisualStyleBackColor = false;
            // 
            // btnQuayLai
            // 
            this.btnQuayLai.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnQuayLai.BackColor = System.Drawing.Color.Gray;
            this.btnQuayLai.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnQuayLai.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnQuayLai.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnQuayLai.ForeColor = System.Drawing.Color.White;
            this.btnQuayLai.Location = new System.Drawing.Point(15, 427);
            this.btnQuayLai.Name = "btnQuayLai";
            this.btnQuayLai.Size = new System.Drawing.Size(92, 30);
            this.btnQuayLai.TabIndex = 7;
            this.btnQuayLai.Text = "Quay Lại";
            this.btnQuayLai.UseVisualStyleBackColor = false;
            this.btnQuayLai.Click += new System.EventHandler(this.btnQuayLai_Click);
            // 
            // DichVu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(923, 464);
            this.Controls.Add(this.btnQuayLai);
            this.Controls.Add(this.btnXoaDV);
            this.Controls.Add(this.btnXoaSP);
            this.Controls.Add(this.btnSuaSP);
            this.Controls.Add(this.btnThemSP);
            this.Controls.Add(this.grpThongTinHoaDon);
            this.Controls.Add(this.grpDichVu);
            this.Controls.Add(this.btnThemDV);
            this.Controls.Add(this.grpGioDichVu);
            this.Name = "DichVu";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "QUẢN LÝ DỊCH VỤ & BÁN HÀNG";
            this.Load += new System.EventHandler(this.DichVu_Load);
            this.grpThongTinHoaDon.ResumeLayout(false);
            this.grpThongTinHoaDon.PerformLayout();
            this.grpDichVu.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvDichVu)).EndInit();
            this.grpGioDichVu.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvGioHang)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grpThongTinHoaDon;
        private System.Windows.Forms.Label lblLoaiHoaDon;
        private System.Windows.Forms.RadioButton rdoKhachLe;
        private System.Windows.Forms.RadioButton rdoKhachDatSan;
        private System.Windows.Forms.Label lblMaLich;
        private System.Windows.Forms.ComboBox cbxLichDat;
        private System.Windows.Forms.Label lblTenKH;
        private System.Windows.Forms.TextBox txtTenKH;
        private System.Windows.Forms.Label lblSDT;
        private System.Windows.Forms.TextBox txtSDT;
        private System.Windows.Forms.GroupBox grpDichVu;
        private System.Windows.Forms.DataGridView dgvDichVu;
        private System.Windows.Forms.GroupBox grpGioDichVu;
        private System.Windows.Forms.DataGridView dgvGioHang;
        private System.Windows.Forms.Button btnThemDV;
        private System.Windows.Forms.Button btnXoaDV;
        private System.Windows.Forms.Button btnThemSP;
        private System.Windows.Forms.Button btnSuaSP;
        private System.Windows.Forms.Button btnXoaSP;
        private System.Windows.Forms.Button btnQuayLai;
        private System.Windows.Forms.Button btnThanhToan;
    }
}