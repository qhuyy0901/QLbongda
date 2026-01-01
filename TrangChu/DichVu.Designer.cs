using System.Windows.Forms;

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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle13 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle14 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle15 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle16 = new System.Windows.Forms.DataGridViewCellStyle();
            this.grpThongTinHoaDon = new System.Windows.Forms.GroupBox();
            this.txtTimKiemDVu = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.lblMaLich = new System.Windows.Forms.Label();
            this.cbxLichDat = new System.Windows.Forms.ComboBox();
            this.lblTenKH = new System.Windows.Forms.Label();
            this.txtTenKH = new System.Windows.Forms.TextBox();
            this.lblSDT = new System.Windows.Forms.Label();
            this.txtSDT = new System.Windows.Forms.TextBox();
            this.btnThanhToan = new System.Windows.Forms.Button();
            this.grpDichVu = new System.Windows.Forms.GroupBox();
            this.dgvDichVu = new System.Windows.Forms.DataGridView();
            this.grpGioDichVu = new System.Windows.Forms.GroupBox();
            this.dgvGioHang = new System.Windows.Forms.DataGridView();
            this.btnXoaDV = new System.Windows.Forms.Button();
            this.btnThemDV = new System.Windows.Forms.Button();
            this.btnThemDVu = new System.Windows.Forms.Button();
            this.btnSuaDVu = new System.Windows.Forms.Button();
            this.btnXoaDVu = new System.Windows.Forms.Button();
            this.btnQuayLai = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txtMaDVu = new System.Windows.Forms.TextBox();
            this.txtTenDVu = new System.Windows.Forms.TextBox();
            this.txtDonGiaDVu = new System.Windows.Forms.TextBox();
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
            this.grpThongTinHoaDon.Controls.Add(this.txtDonGiaDVu);
            this.grpThongTinHoaDon.Controls.Add(this.txtTenDVu);
            this.grpThongTinHoaDon.Controls.Add(this.txtMaDVu);
            this.grpThongTinHoaDon.Controls.Add(this.label4);
            this.grpThongTinHoaDon.Controls.Add(this.label3);
            this.grpThongTinHoaDon.Controls.Add(this.label2);
            this.grpThongTinHoaDon.Controls.Add(this.txtTimKiemDVu);
            this.grpThongTinHoaDon.Controls.Add(this.label1);
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
            this.grpThongTinHoaDon.Size = new System.Drawing.Size(981, 114);
            this.grpThongTinHoaDon.TabIndex = 0;
            this.grpThongTinHoaDon.TabStop = false;
            this.grpThongTinHoaDon.Text = "THÔNG TIN KHÁCH HÀNG & HÓA ĐƠN";
            // 
            // txtTimKiemDVu
            // 
            this.txtTimKiemDVu.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtTimKiemDVu.Location = new System.Drawing.Point(255, 90);
            this.txtTimKiemDVu.Multiline = true;
            this.txtTimKiemDVu.Name = "txtTimKiemDVu";
            this.txtTimKiemDVu.Size = new System.Drawing.Size(114, 22);
            this.txtTimKiemDVu.TabIndex = 10;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(251, 64);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(120, 19);
            this.label1.TabIndex = 9;
            this.label1.Text = "Tìm Kiếm Dịch Vụ:";
            // 
            // lblMaLich
            // 
            this.lblMaLich.AutoSize = true;
            this.lblMaLich.Location = new System.Drawing.Point(510, 21);
            this.lblMaLich.Name = "lblMaLich";
            this.lblMaLich.Size = new System.Drawing.Size(73, 19);
            this.lblMaLich.TabIndex = 3;
            this.lblMaLich.Text = "Chọn Lịch:";
            // 
            // cbxLichDat
            // 
            this.cbxLichDat.Enabled = false;
            this.cbxLichDat.FormattingEnabled = true;
            this.cbxLichDat.Location = new System.Drawing.Point(596, 18);
            this.cbxLichDat.Name = "cbxLichDat";
            this.cbxLichDat.Size = new System.Drawing.Size(339, 25);
            this.cbxLichDat.TabIndex = 4;
            // 
            // lblTenKH
            // 
            this.lblTenKH.AutoSize = true;
            this.lblTenKH.Location = new System.Drawing.Point(526, 52);
            this.lblTenKH.Name = "lblTenKH";
            this.lblTenKH.Size = new System.Drawing.Size(55, 19);
            this.lblTenKH.TabIndex = 5;
            this.lblTenKH.Text = "Tên KH:";
            // 
            // txtTenKH
            // 
            this.txtTenKH.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtTenKH.Enabled = false;
            this.txtTenKH.Location = new System.Drawing.Point(596, 49);
            this.txtTenKH.Name = "txtTenKH";
            this.txtTenKH.Size = new System.Drawing.Size(200, 25);
            this.txtTenKH.TabIndex = 6;
            // 
            // lblSDT
            // 
            this.lblSDT.AutoSize = true;
            this.lblSDT.Location = new System.Drawing.Point(526, 83);
            this.lblSDT.Name = "lblSDT";
            this.lblSDT.Size = new System.Drawing.Size(36, 19);
            this.lblSDT.TabIndex = 7;
            this.lblSDT.Text = "SĐT:";
            // 
            // txtSDT
            // 
            this.txtSDT.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtSDT.Enabled = false;
            this.txtSDT.Location = new System.Drawing.Point(596, 80);
            this.txtSDT.Name = "txtSDT";
            this.txtSDT.Size = new System.Drawing.Size(200, 25);
            this.txtSDT.TabIndex = 8;
            // 
            // btnThanhToan
            // 
            this.btnThanhToan.BackColor = System.Drawing.Color.DodgerBlue;
            this.btnThanhToan.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnThanhToan.FlatAppearance.BorderSize = 0;
            this.btnThanhToan.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnThanhToan.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnThanhToan.ForeColor = System.Drawing.Color.White;
            this.btnThanhToan.Location = new System.Drawing.Point(753, 391);
            this.btnThanhToan.Name = "btnThanhToan";
            this.btnThanhToan.Size = new System.Drawing.Size(120, 38);
            this.btnThanhToan.TabIndex = 8;
            this.btnThanhToan.Text = "THANH TOÁN";
            this.btnThanhToan.UseVisualStyleBackColor = false;
            this.btnThanhToan.Click += new System.EventHandler(this.btnThanhToan_Click);
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
            this.dgvDichVu.BackgroundColor = System.Drawing.Color.Gainsboro;
            this.dgvDichVu.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle13.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle13.BackColor = System.Drawing.Color.ForestGreen;
            dataGridViewCellStyle13.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle13.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle13.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle13.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle13.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvDichVu.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle13;
            this.dgvDichVu.ColumnHeadersHeight = 30;
            this.dgvDichVu.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvDichVu.EnableHeadersVisualStyles = false;
            this.dgvDichVu.Location = new System.Drawing.Point(3, 21);
            this.dgvDichVu.Name = "dgvDichVu";
            dataGridViewCellStyle14.BackColor = System.Drawing.Color.WhiteSmoke;
            this.dgvDichVu.RowsDefaultCellStyle = dataGridViewCellStyle14;
            this.dgvDichVu.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvDichVu.Size = new System.Drawing.Size(321, 236);
            this.dgvDichVu.TabIndex = 0;
            dgvDichVu.ReadOnly = true;
            dgvDichVu.AllowUserToAddRows = false;
            dgvDichVu.AllowUserToDeleteRows = false;
            dgvDichVu.AllowUserToResizeRows = false;
            dgvDichVu.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            dgvGioHang.ReadOnly = false;
            dgvGioHang.AllowUserToAddRows = false;
            dgvGioHang.AllowUserToDeleteRows = false;
            dgvGioHang.AllowUserToResizeRows = false;
            dgvGioHang.SelectionMode = DataGridViewSelectionMode.CellSelect;
            // 
            // grpGioDichVu
            // 
            this.grpGioDichVu.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpGioDichVu.BackColor = System.Drawing.Color.White;
            this.grpGioDichVu.Controls.Add(this.dgvGioHang);
            this.grpGioDichVu.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpGioDichVu.Location = new System.Drawing.Point(425, 120);
            this.grpGioDichVu.Name = "grpGioDichVu";
            this.grpGioDichVu.Size = new System.Drawing.Size(556, 260);
            this.grpGioDichVu.TabIndex = 3;
            this.grpGioDichVu.TabStop = false;
            this.grpGioDichVu.Text = "GIỎ DỊCH VỤ ĐÃ CHỌN";
            // 
            // dgvGioHang
            // 
            this.dgvGioHang.AllowUserToAddRows = false;
            this.dgvGioHang.BackgroundColor = System.Drawing.Color.Gainsboro;
            this.dgvGioHang.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle15.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle15.BackColor = System.Drawing.Color.Orange;
            dataGridViewCellStyle15.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle15.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle15.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle15.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle15.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvGioHang.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle15;
            this.dgvGioHang.ColumnHeadersHeight = 30;
            this.dgvGioHang.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvGioHang.EnableHeadersVisualStyles = false;
            this.dgvGioHang.Location = new System.Drawing.Point(3, 21);
            this.dgvGioHang.Name = "dgvGioHang";
            dataGridViewCellStyle16.BackColor = System.Drawing.Color.WhiteSmoke;
            this.dgvGioHang.RowsDefaultCellStyle = dataGridViewCellStyle16;
            this.dgvGioHang.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvGioHang.Size = new System.Drawing.Size(550, 236);
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
            this.btnXoaDV.Location = new System.Drawing.Point(596, 391);
            this.btnXoaDV.Name = "btnXoaDV";
            this.btnXoaDV.Size = new System.Drawing.Size(116, 38);
            this.btnXoaDV.TabIndex = 2;
            this.btnXoaDV.Text = "Xóa Lựa Chọn";
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
            this.btnThemDV.Location = new System.Drawing.Point(354, 214);
            this.btnThemDV.Name = "btnThemDV";
            this.btnThemDV.Size = new System.Drawing.Size(56, 55);
            this.btnThemDV.TabIndex = 2;
            this.btnThemDV.Text = ">>";
            this.btnThemDV.UseVisualStyleBackColor = false;
            this.btnThemDV.Click += new System.EventHandler(this.btnThemDV_Click);
            // 
            // btnThemDVu
            // 
            this.btnThemDVu.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnThemDVu.BackColor = System.Drawing.Color.ForestGreen;
            this.btnThemDVu.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnThemDVu.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnThemDVu.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnThemDVu.ForeColor = System.Drawing.Color.White;
            this.btnThemDVu.Location = new System.Drawing.Point(12, 386);
            this.btnThemDVu.Name = "btnThemDVu";
            this.btnThemDVu.Size = new System.Drawing.Size(133, 35);
            this.btnThemDVu.TabIndex = 4;
            this.btnThemDVu.Text = "Dịch Vụ Mới";
            this.btnThemDVu.UseVisualStyleBackColor = false;
            this.btnThemDVu.Click += new System.EventHandler(this.btnThemDVu_Click);
            // 
            // btnSuaDVu
            // 
            this.btnSuaDVu.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnSuaDVu.BackColor = System.Drawing.Color.Goldenrod;
            this.btnSuaDVu.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSuaDVu.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSuaDVu.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnSuaDVu.ForeColor = System.Drawing.Color.White;
            this.btnSuaDVu.Location = new System.Drawing.Point(208, 386);
            this.btnSuaDVu.Name = "btnSuaDVu";
            this.btnSuaDVu.Size = new System.Drawing.Size(131, 35);
            this.btnSuaDVu.TabIndex = 5;
            this.btnSuaDVu.Text = "Cập Nhật Dịch Vụ ";
            this.btnSuaDVu.UseVisualStyleBackColor = false;
            this.btnSuaDVu.Click += new System.EventHandler(this.btnSuaDVu_Click);
            // 
            // btnXoaDVu
            // 
            this.btnXoaDVu.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnXoaDVu.BackColor = System.Drawing.Color.Crimson;
            this.btnXoaDVu.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnXoaDVu.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnXoaDVu.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnXoaDVu.ForeColor = System.Drawing.Color.White;
            this.btnXoaDVu.Location = new System.Drawing.Point(208, 430);
            this.btnXoaDVu.Name = "btnXoaDVu";
            this.btnXoaDVu.Size = new System.Drawing.Size(131, 35);
            this.btnXoaDVu.TabIndex = 6;
            this.btnXoaDVu.Text = "Xóa Dịch Vụ ";
            this.btnXoaDVu.UseVisualStyleBackColor = false;
            this.btnXoaDVu.Click += new System.EventHandler(this.btnXoaDVu_Click);
            // 
            // btnQuayLai
            // 
            this.btnQuayLai.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnQuayLai.BackColor = System.Drawing.Color.Gray;
            this.btnQuayLai.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnQuayLai.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnQuayLai.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnQuayLai.ForeColor = System.Drawing.Color.White;
            this.btnQuayLai.Location = new System.Drawing.Point(12, 427);
            this.btnQuayLai.Name = "btnQuayLai";
            this.btnQuayLai.Size = new System.Drawing.Size(133, 35);
            this.btnQuayLai.TabIndex = 7;
            this.btnQuayLai.Text = "Quay Lại";
            this.btnQuayLai.UseVisualStyleBackColor = false;
            this.btnQuayLai.Click += new System.EventHandler(this.btnQuayLai_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(46, 23);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(84, 19);
            this.label2.TabIndex = 11;
            this.label2.Text = "Mã Dịch Vụ:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(251, 21);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(85, 19);
            this.label3.TabIndex = 12;
            this.label3.Text = "Tên Dịch Vụ:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(46, 70);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(114, 19);
            this.label4.TabIndex = 13;
            this.label4.Text = "Đơn Giá Dịch Vụ:";
            // 
            // txtMaDVu
            // 
            this.txtMaDVu.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtMaDVu.Location = new System.Drawing.Point(50, 45);
            this.txtMaDVu.Multiline = true;
            this.txtMaDVu.Name = "txtMaDVu";
            this.txtMaDVu.Size = new System.Drawing.Size(110, 22);
            this.txtMaDVu.TabIndex = 14;
            // 
            // txtTenDVu
            // 
            this.txtTenDVu.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtTenDVu.Location = new System.Drawing.Point(255, 43);
            this.txtTenDVu.Multiline = true;
            this.txtTenDVu.Name = "txtTenDVu";
            this.txtTenDVu.Size = new System.Drawing.Size(114, 22);
            this.txtTenDVu.TabIndex = 15;
            // 
            // txtDonGiaDVu
            // 
            this.txtDonGiaDVu.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtDonGiaDVu.Location = new System.Drawing.Point(49, 92);
            this.txtDonGiaDVu.Multiline = true;
            this.txtDonGiaDVu.Name = "txtDonGiaDVu";
            this.txtDonGiaDVu.Size = new System.Drawing.Size(111, 22);
            this.txtDonGiaDVu.TabIndex = 16;
            // 
            // DichVu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(981, 470);
            this.Controls.Add(this.btnThanhToan);
            this.Controls.Add(this.btnQuayLai);
            this.Controls.Add(this.btnXoaDV);
            this.Controls.Add(this.btnXoaDVu);
            this.Controls.Add(this.btnSuaDVu);
            this.Controls.Add(this.btnThemDVu);
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
        private System.Windows.Forms.Label lblMaLich;
        private System.Windows.Forms.ComboBox cbxLichDat;
        private System.Windows.Forms.GroupBox grpDichVu;
        private System.Windows.Forms.DataGridView dgvDichVu;
        private System.Windows.Forms.GroupBox grpGioDichVu;
        private System.Windows.Forms.DataGridView dgvGioHang;
        private System.Windows.Forms.Button btnThemDV;
        private System.Windows.Forms.Button btnXoaDV;
        private System.Windows.Forms.Button btnThemDVu;
        private System.Windows.Forms.Button btnSuaDVu;
        private System.Windows.Forms.Button btnXoaDVu;
        private System.Windows.Forms.Button btnQuayLai;
        private System.Windows.Forms.Button btnThanhToan;
        private System.Windows.Forms.Label lblTenKH;
        private System.Windows.Forms.TextBox txtTenKH;
        private System.Windows.Forms.Label lblSDT;
        private System.Windows.Forms.TextBox txtSDT;
        private System.Windows.Forms.TextBox txtTimKiemDVu;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtDonGiaDVu;
        private System.Windows.Forms.TextBox txtTenDVu;
        private System.Windows.Forms.TextBox txtMaDVu;
    }
}