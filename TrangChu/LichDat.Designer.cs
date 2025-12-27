namespace TrangChu
{
    partial class LichDat
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LichDat));
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.cbxMaSan = new System.Windows.Forms.ComboBox();
            this.txtTenKhachHang = new System.Windows.Forms.TextBox();
            this.dtpGioBatDau = new System.Windows.Forms.DateTimePicker();
            this.dtpGioKetThuc = new System.Windows.Forms.DateTimePicker();
            this.btnDatSAn = new System.Windows.Forms.Button();
            this.btnHuySan = new System.Windows.Forms.Button();
            this.btnXoa = new System.Windows.Forms.Button();
            this.btnSua = new System.Windows.Forms.Button();
            this.lblSDT_KH = new System.Windows.Forms.Label();
            this.txtSDT = new System.Windows.Forms.TextBox();
            this.dtpNgayDat = new System.Windows.Forms.DateTimePicker();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnThanhToan = new System.Windows.Forms.Button();
            this.btnThemDivhVu = new System.Windows.Forms.Button();
            this.btnQuayLai = new System.Windows.Forms.Button();
            this.txtDonGia = new System.Windows.Forms.TextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.dgvDatSan = new System.Windows.Forms.DataGridView();
            this.clMaLich = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clMaSan = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clSDT_KH = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clTenKH = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clNgayDat = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clGioBatDau = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clGioKetThuc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clTrangThai = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clDonGiaThucTe = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.btnSan1 = new System.Windows.Forms.Button();
            this.btnSan2 = new System.Windows.Forms.Button();
            this.btnSan3 = new System.Windows.Forms.Button();
            this.btnSan4 = new System.Windows.Forms.Button();
            this.btnSan5 = new System.Windows.Forms.Button();
            this.btnSan6 = new System.Windows.Forms.Button();
            this.label12 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.btnTimKiem = new System.Windows.Forms.Button();
            this.txtTimKiem = new System.Windows.Forms.TextBox();
            this.btnTaiLai = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDatSan)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(10, 140);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(58, 19);
            this.label7.TabIndex = 22;
            this.label7.Text = "Mã Sân:";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(10, 60);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(55, 19);
            this.label8.TabIndex = 21;
            this.label8.Text = "Tên KH:";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(10, 180);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(70, 19);
            this.label9.TabIndex = 20;
            this.label9.Text = "Ngày Đặt:";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(10, 220);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(86, 19);
            this.label10.TabIndex = 19;
            this.label10.Text = "Giờ Bắt Đầu:";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(10, 260);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(90, 19);
            this.label11.TabIndex = 18;
            this.label11.Text = "Giờ Kết Thúc:";
            // 
            // cbxMaSan
            // 
            this.cbxMaSan.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxMaSan.FormattingEnabled = true;
            this.cbxMaSan.Items.AddRange(new object[] {
            "San1",
            "San2",
            "San3",
            "San4",
            "San5",
            "San6",
            "San7"});
            this.cbxMaSan.Location = new System.Drawing.Point(110, 137);
            this.cbxMaSan.Name = "cbxMaSan";
            this.cbxMaSan.Size = new System.Drawing.Size(160, 25);
            this.cbxMaSan.TabIndex = 4;
            // 
            // txtTenKhachHang
            // 
            this.txtTenKhachHang.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtTenKhachHang.Location = new System.Drawing.Point(110, 57);
            this.txtTenKhachHang.Name = "txtTenKhachHang";
            this.txtTenKhachHang.Size = new System.Drawing.Size(160, 25);
            this.txtTenKhachHang.TabIndex = 2;
            // 
            // dtpGioBatDau
            // 
            this.dtpGioBatDau.Format = System.Windows.Forms.DateTimePickerFormat.Time;
            this.dtpGioBatDau.Location = new System.Drawing.Point(110, 217);
            this.dtpGioBatDau.Name = "dtpGioBatDau";
            this.dtpGioBatDau.ShowUpDown = true;
            this.dtpGioBatDau.Size = new System.Drawing.Size(160, 25);
            this.dtpGioBatDau.TabIndex = 6;
            // 
            // dtpGioKetThuc
            // 
            this.dtpGioKetThuc.Format = System.Windows.Forms.DateTimePickerFormat.Time;
            this.dtpGioKetThuc.Location = new System.Drawing.Point(110, 257);
            this.dtpGioKetThuc.Name = "dtpGioKetThuc";
            this.dtpGioKetThuc.ShowUpDown = true;
            this.dtpGioKetThuc.Size = new System.Drawing.Size(160, 25);
            this.dtpGioKetThuc.TabIndex = 7;
            // 
            // btnDatSAn
            // 
            this.btnDatSAn.BackColor = System.Drawing.Color.ForestGreen;
            this.btnDatSAn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnDatSAn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDatSAn.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDatSAn.ForeColor = System.Drawing.Color.White;
            this.btnDatSAn.Location = new System.Drawing.Point(20, 360);
            this.btnDatSAn.Name = "btnDatSAn";
            this.btnDatSAn.Size = new System.Drawing.Size(110, 35);
            this.btnDatSAn.TabIndex = 9;
            this.btnDatSAn.Text = "ĐẶT SÂN";
            this.btnDatSAn.UseVisualStyleBackColor = false;
            this.btnDatSAn.Click += new System.EventHandler(this.btnDatSAn_Click);
            // 
            // btnHuySan
            // 
            this.btnHuySan.BackColor = System.Drawing.Color.Gray;
            this.btnHuySan.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnHuySan.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnHuySan.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnHuySan.ForeColor = System.Drawing.Color.White;
            this.btnHuySan.Location = new System.Drawing.Point(150, 360);
            this.btnHuySan.Name = "btnHuySan";
            this.btnHuySan.Size = new System.Drawing.Size(110, 35);
            this.btnHuySan.TabIndex = 10;
            this.btnHuySan.Text = "HỦY";
            this.btnHuySan.UseVisualStyleBackColor = false;
            this.btnHuySan.Click += new System.EventHandler(this.btnHuySan_Click);
            // 
            // btnXoa
            // 
            this.btnXoa.BackColor = System.Drawing.Color.Crimson;
            this.btnXoa.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnXoa.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnXoa.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnXoa.ForeColor = System.Drawing.Color.White;
            this.btnXoa.Location = new System.Drawing.Point(150, 419);
            this.btnXoa.Name = "btnXoa";
            this.btnXoa.Size = new System.Drawing.Size(110, 35);
            this.btnXoa.TabIndex = 12;
            this.btnXoa.Text = "XÓA";
            this.btnXoa.UseVisualStyleBackColor = false;
            this.btnXoa.Click += new System.EventHandler(this.btnXoa_Click);
            // 
            // btnSua
            // 
            this.btnSua.BackColor = System.Drawing.Color.Goldenrod;
            this.btnSua.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSua.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSua.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSua.ForeColor = System.Drawing.Color.White;
            this.btnSua.Location = new System.Drawing.Point(20, 419);
            this.btnSua.Name = "btnSua";
            this.btnSua.Size = new System.Drawing.Size(110, 35);
            this.btnSua.TabIndex = 11;
            this.btnSua.Text = "SỬA";
            this.btnSua.UseVisualStyleBackColor = false;
            this.btnSua.Click += new System.EventHandler(this.btnSua_Click);
            // 
            // lblSDT_KH
            // 
            this.lblSDT_KH.AutoSize = true;
            this.lblSDT_KH.Location = new System.Drawing.Point(10, 100);
            this.lblSDT_KH.Name = "lblSDT_KH";
            this.lblSDT_KH.Size = new System.Drawing.Size(58, 19);
            this.lblSDT_KH.TabIndex = 23;
            this.lblSDT_KH.Text = "SĐT KH:";
            // 
            // txtSDT
            // 
            this.txtSDT.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtSDT.Location = new System.Drawing.Point(110, 97);
            this.txtSDT.Name = "txtSDT";
            this.txtSDT.Size = new System.Drawing.Size(160, 25);
            this.txtSDT.TabIndex = 3;
            // 
            // dtpNgayDat
            // 
            this.dtpNgayDat.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpNgayDat.Location = new System.Drawing.Point(110, 177);
            this.dtpNgayDat.Name = "dtpNgayDat";
            this.dtpNgayDat.Size = new System.Drawing.Size(160, 25);
            this.dtpNgayDat.TabIndex = 5;
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.WhiteSmoke;
            this.groupBox1.Controls.Add(this.btnThanhToan);
            this.groupBox1.Controls.Add(this.btnThemDivhVu);
            this.groupBox1.Controls.Add(this.btnQuayLai);
            this.groupBox1.Controls.Add(this.txtDonGia);
            this.groupBox1.Controls.Add(this.label15);
            this.groupBox1.Controls.Add(this.txtSDT);
            this.groupBox1.Controls.Add(this.btnXoa);
            this.groupBox1.Controls.Add(this.btnSua);
            this.groupBox1.Controls.Add(this.dtpNgayDat);
            this.groupBox1.Controls.Add(this.btnHuySan);
            this.groupBox1.Controls.Add(this.lblSDT_KH);
            this.groupBox1.Controls.Add(this.btnDatSAn);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.txtTenKhachHang);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Controls.Add(this.cbxMaSan);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.label11);
            this.groupBox1.Controls.Add(this.dtpGioBatDau);
            this.groupBox1.Controls.Add(this.dtpGioKetThuc);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Left;
            this.groupBox1.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(306, 611);
            this.groupBox1.TabIndex = 26;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "THÔNG TIN ĐẶT SÂN";
            // 
            // btnThanhToan
            // 
            this.btnThanhToan.BackColor = System.Drawing.Color.DodgerBlue;
            this.btnThanhToan.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnThanhToan.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnThanhToan.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnThanhToan.ForeColor = System.Drawing.Color.Black;
            this.btnThanhToan.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnThanhToan.Location = new System.Drawing.Point(20, 526);
            this.btnThanhToan.Name = "btnThanhToan";
            this.btnThanhToan.Padding = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.btnThanhToan.Size = new System.Drawing.Size(240, 35);
            this.btnThanhToan.TabIndex = 39;
            this.btnThanhToan.Text = "Thanh Toán";
            this.btnThanhToan.UseVisualStyleBackColor = false;
            this.btnThanhToan.Click += new System.EventHandler(this.btnThanhToan_Click);
            // 
            // btnThemDivhVu
            // 
            this.btnThemDivhVu.BackColor = System.Drawing.Color.Cyan;
            this.btnThemDivhVu.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnThemDivhVu.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnThemDivhVu.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnThemDivhVu.ForeColor = System.Drawing.Color.Black;
            this.btnThemDivhVu.Location = new System.Drawing.Point(20, 478);
            this.btnThemDivhVu.Name = "btnThemDivhVu";
            this.btnThemDivhVu.Size = new System.Drawing.Size(240, 35);
            this.btnThemDivhVu.TabIndex = 35;
            this.btnThemDivhVu.Text = "Thêm Dịch Vụ";
            this.btnThemDivhVu.UseVisualStyleBackColor = false;
            this.btnThemDivhVu.Click += new System.EventHandler(this.btnThemDivhVu_Click);
            // 
            // btnQuayLai
            // 
            this.btnQuayLai.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnQuayLai.BackColor = System.Drawing.Color.White;
            this.btnQuayLai.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnQuayLai.FlatAppearance.BorderColor = System.Drawing.Color.Silver;
            this.btnQuayLai.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnQuayLai.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnQuayLai.Image = global::TrangChu.Properties.Resources.Screenshot_2025_12_14_150219_removebg_preview;
            this.btnQuayLai.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnQuayLai.Location = new System.Drawing.Point(0, 581);
            this.btnQuayLai.Name = "btnQuayLai";
            this.btnQuayLai.Size = new System.Drawing.Size(100, 30);
            this.btnQuayLai.TabIndex = 26;
            this.btnQuayLai.Text = "Quay Lại";
            this.btnQuayLai.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnQuayLai.UseVisualStyleBackColor = false;
            this.btnQuayLai.Click += new System.EventHandler(this.btnQuayLai_Click);
            // 
            // txtDonGia
            // 
            this.txtDonGia.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtDonGia.Location = new System.Drawing.Point(110, 297);
            this.txtDonGia.Name = "txtDonGia";
            this.txtDonGia.Size = new System.Drawing.Size(160, 25);
            this.txtDonGia.TabIndex = 8;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(10, 300);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(62, 19);
            this.label15.TabIndex = 34;
            this.label15.Text = "Đơn Giá:";
            // 
            // dgvDatSan
            // 
            this.dgvDatSan.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvDatSan.BackgroundColor = System.Drawing.Color.White;
            this.dgvDatSan.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvDatSan.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.ForestGreen;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvDatSan.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvDatSan.ColumnHeadersHeight = 35;
            this.dgvDatSan.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvDatSan.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.clMaLich,
            this.clMaSan,
            this.clSDT_KH,
            this.clTenKH,
            this.clNgayDat,
            this.clGioBatDau,
            this.clGioKetThuc,
            this.clTrangThai,
            this.clDonGiaThucTe});
            this.dgvDatSan.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvDatSan.EnableHeadersVisualStyles = false;
            this.dgvDatSan.GridColor = System.Drawing.Color.Silver;
            this.dgvDatSan.Location = new System.Drawing.Point(306, 240);
            this.dgvDatSan.Name = "dgvDatSan";
            this.dgvDatSan.RowHeadersVisible = false;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.WhiteSmoke;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle2.Padding = new System.Windows.Forms.Padding(5);
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(230)))), ((int)(((byte)(200)))));
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.Black;
            this.dgvDatSan.RowsDefaultCellStyle = dataGridViewCellStyle2;
            this.dgvDatSan.RowTemplate.Height = 30;
            this.dgvDatSan.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvDatSan.Size = new System.Drawing.Size(723, 371);
            this.dgvDatSan.TabIndex = 13;
            this.dgvDatSan.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvDatSan_CellClick);
            // 
            // clMaLich
            // 
            this.clMaLich.HeaderText = "Mã Lịch";
            this.clMaLich.Name = "clMaLich";
            // 
            // clMaSan
            // 
            this.clMaSan.HeaderText = "Mã Sân";
            this.clMaSan.Name = "clMaSan";
            // 
            // clSDT_KH
            // 
            this.clSDT_KH.HeaderText = "SĐT KH";
            this.clSDT_KH.Name = "clSDT_KH";
            // 
            // clTenKH
            // 
            this.clTenKH.HeaderText = "Tên KH";
            this.clTenKH.Name = "clTenKH";
            // 
            // clNgayDat
            // 
            this.clNgayDat.HeaderText = "Ngày Đặt";
            this.clNgayDat.Name = "clNgayDat";
            // 
            // clGioBatDau
            // 
            this.clGioBatDau.HeaderText = "Bắt Đầu";
            this.clGioBatDau.Name = "clGioBatDau";
            // 
            // clGioKetThuc
            // 
            this.clGioKetThuc.HeaderText = "Kết Thúc";
            this.clGioKetThuc.Name = "clGioKetThuc";
            // 
            // clTrangThai
            // 
            this.clTrangThai.HeaderText = "Trạng Thái";
            this.clTrangThai.Name = "clTrangThai";
            // 
            // clDonGiaThucTe
            // 
            this.clDonGiaThucTe.HeaderText = "Đơn Giá";
            this.clDonGiaThucTe.Name = "clDonGiaThucTe";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(67, 83);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(45, 19);
            this.label1.TabIndex = 8;
            this.label1.Text = "Sân 1";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(263, 85);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(45, 19);
            this.label2.TabIndex = 9;
            this.label2.Text = "Sân 2";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(67, 176);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(45, 19);
            this.label3.TabIndex = 10;
            this.label3.Text = "Sân 3";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(620, 175);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(45, 19);
            this.label4.TabIndex = 11;
            this.label4.Text = "Sân 6";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(275, 175);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(45, 19);
            this.label5.TabIndex = 11;
            this.label5.Text = "Sân 4";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(445, 175);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(45, 19);
            this.label6.TabIndex = 12;
            this.label6.Text = "Sân 5";
            // 
            // btnSan1
            // 
            this.btnSan1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSan1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSan1.Image = ((System.Drawing.Image)(resources.GetObject("btnSan1.Image")));
            this.btnSan1.Location = new System.Drawing.Point(40, 25);
            this.btnSan1.Name = "btnSan1";
            this.btnSan1.Size = new System.Drawing.Size(100, 55);
            this.btnSan1.TabIndex = 14;
            this.btnSan1.UseVisualStyleBackColor = true;
            // 
            // btnSan2
            // 
            this.btnSan2.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSan2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSan2.Image = ((System.Drawing.Image)(resources.GetObject("btnSan2.Image")));
            this.btnSan2.Location = new System.Drawing.Point(233, 24);
            this.btnSan2.Name = "btnSan2";
            this.btnSan2.Size = new System.Drawing.Size(100, 55);
            this.btnSan2.TabIndex = 15;
            this.btnSan2.UseVisualStyleBackColor = true;
            // 
            // btnSan3
            // 
            this.btnSan3.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSan3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSan3.Image = ((System.Drawing.Image)(resources.GetObject("btnSan3.Image")));
            this.btnSan3.Location = new System.Drawing.Point(40, 116);
            this.btnSan3.Name = "btnSan3";
            this.btnSan3.Size = new System.Drawing.Size(100, 55);
            this.btnSan3.TabIndex = 16;
            this.btnSan3.UseVisualStyleBackColor = true;
            // 
            // btnSan4
            // 
            this.btnSan4.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSan4.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSan4.Image = ((System.Drawing.Image)(resources.GetObject("btnSan4.Image")));
            this.btnSan4.Location = new System.Drawing.Point(233, 115);
            this.btnSan4.Name = "btnSan4";
            this.btnSan4.Size = new System.Drawing.Size(100, 55);
            this.btnSan4.TabIndex = 17;
            this.btnSan4.UseVisualStyleBackColor = true;
            // 
            // btnSan5
            // 
            this.btnSan5.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSan5.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSan5.Image = ((System.Drawing.Image)(resources.GetObject("btnSan5.Image")));
            this.btnSan5.Location = new System.Drawing.Point(414, 23);
            this.btnSan5.Name = "btnSan5";
            this.btnSan5.Size = new System.Drawing.Size(100, 147);
            this.btnSan5.TabIndex = 18;
            this.btnSan5.UseVisualStyleBackColor = true;
            // 
            // btnSan6
            // 
            this.btnSan6.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSan6.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSan6.Image = ((System.Drawing.Image)(resources.GetObject("btnSan6.Image")));
            this.btnSan6.Location = new System.Drawing.Point(596, 24);
            this.btnSan6.Name = "btnSan6";
            this.btnSan6.Size = new System.Drawing.Size(100, 146);
            this.btnSan6.TabIndex = 19;
            this.btnSan6.UseVisualStyleBackColor = true;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.label12.ForeColor = System.Drawing.Color.DarkGreen;
            this.label12.Location = new System.Drawing.Point(148, 87);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(70, 20);
            this.label12.TabIndex = 35;
            this.label12.Text = "SÂN 5v5";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.label16.ForeColor = System.Drawing.Color.Green;
            this.label16.Location = new System.Drawing.Point(520, 94);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(70, 20);
            this.label16.TabIndex = 36;
            this.label16.Text = "SÂN 7v7";
            // 
            // btnTimKiem
            // 
            this.btnTimKiem.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnTimKiem.BackColor = System.Drawing.Color.WhiteSmoke;
            this.btnTimKiem.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnTimKiem.FlatAppearance.BorderColor = System.Drawing.Color.Silver;
            this.btnTimKiem.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTimKiem.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnTimKiem.Image = ((System.Drawing.Image)(resources.GetObject("btnTimKiem.Image")));
            this.btnTimKiem.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnTimKiem.Location = new System.Drawing.Point(357, 207);
            this.btnTimKiem.Name = "btnTimKiem";
            this.btnTimKiem.Size = new System.Drawing.Size(108, 33);
            this.btnTimKiem.TabIndex = 37;
            this.btnTimKiem.Text = "Tìm Kiếm";
            this.btnTimKiem.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnTimKiem.UseVisualStyleBackColor = false;
            this.btnTimKiem.Click += new System.EventHandler(this.btnTimKiem_Click);
            // 
            // txtTimKiem
            // 
            this.txtTimKiem.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtTimKiem.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtTimKiem.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtTimKiem.Location = new System.Drawing.Point(463, 207);
            this.txtTimKiem.Multiline = true;
            this.txtTimKiem.Name = "txtTimKiem";
            this.txtTimKiem.Size = new System.Drawing.Size(140, 33);
            this.txtTimKiem.TabIndex = 35;
            // 
            // btnTaiLai
            // 
            this.btnTaiLai.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnTaiLai.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.btnTaiLai.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnTaiLai.FlatAppearance.BorderColor = System.Drawing.Color.Silver;
            this.btnTaiLai.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTaiLai.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnTaiLai.Image = ((System.Drawing.Image)(resources.GetObject("btnTaiLai.Image")));
            this.btnTaiLai.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnTaiLai.Location = new System.Drawing.Point(633, 207);
            this.btnTaiLai.Name = "btnTaiLai";
            this.btnTaiLai.Size = new System.Drawing.Size(90, 30);
            this.btnTaiLai.TabIndex = 38;
            this.btnTaiLai.Text = "Tải Lại";
            this.btnTaiLai.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnTaiLai.UseVisualStyleBackColor = false;
            this.btnTaiLai.Click += new System.EventHandler(this.btnTaiLai_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.BackColor = System.Drawing.Color.White;
            this.groupBox2.Controls.Add(this.btnTaiLai);
            this.groupBox2.Controls.Add(this.txtTimKiem);
            this.groupBox2.Controls.Add(this.btnTimKiem);
            this.groupBox2.Controls.Add(this.label16);
            this.groupBox2.Controls.Add(this.label12);
            this.groupBox2.Controls.Add(this.btnSan6);
            this.groupBox2.Controls.Add(this.btnSan5);
            this.groupBox2.Controls.Add(this.btnSan4);
            this.groupBox2.Controls.Add(this.btnSan3);
            this.groupBox2.Controls.Add(this.btnSan2);
            this.groupBox2.Controls.Add(this.btnSan1);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox2.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.Location = new System.Drawing.Point(306, 0);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(723, 240);
            this.groupBox2.TabIndex = 27;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "SƠ ĐỒ SÂN BÓNG";
            // 
            // LichDat
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ClientSize = new System.Drawing.Size(1029, 611);
            this.Controls.Add(this.dgvDatSan);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "LichDat";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "QUẢN LÝ ĐẶT SÂN";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDatSan)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.ComboBox cbxMaSan;
        private System.Windows.Forms.TextBox txtTenKhachHang;
        private System.Windows.Forms.DateTimePicker dtpGioBatDau;
        private System.Windows.Forms.DateTimePicker dtpGioKetThuc;
        private System.Windows.Forms.Button btnDatSAn;
        private System.Windows.Forms.Button btnHuySan;
        private System.Windows.Forms.Button btnXoa;
        private System.Windows.Forms.Button btnSua;
        private System.Windows.Forms.Label lblSDT_KH;
        private System.Windows.Forms.TextBox txtSDT;
        private System.Windows.Forms.DateTimePicker dtpNgayDat;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnQuayLai;
        private System.Windows.Forms.DataGridView dgvDatSan;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.TextBox txtDonGia;
        private System.Windows.Forms.DataGridViewTextBoxColumn clMaLich;
        private System.Windows.Forms.DataGridViewTextBoxColumn clMaSan;
        private System.Windows.Forms.DataGridViewTextBoxColumn clSDT_KH;
        private System.Windows.Forms.DataGridViewTextBoxColumn clTenKH;
        private System.Windows.Forms.DataGridViewTextBoxColumn clNgayDat;
        private System.Windows.Forms.DataGridViewTextBoxColumn clGioBatDau;
        private System.Windows.Forms.DataGridViewTextBoxColumn clGioKetThuc;
        private System.Windows.Forms.DataGridViewTextBoxColumn clTrangThai;
        private System.Windows.Forms.DataGridViewTextBoxColumn clDonGiaThucTe;
        private System.Windows.Forms.Button btnThemDivhVu;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button btnSan1;
        private System.Windows.Forms.Button btnSan2;
        private System.Windows.Forms.Button btnSan3;
        private System.Windows.Forms.Button btnSan4;
        private System.Windows.Forms.Button btnSan5;
        private System.Windows.Forms.Button btnSan6;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Button btnTimKiem;
        private System.Windows.Forms.TextBox txtTimKiem;
        private System.Windows.Forms.Button btnTaiLai;
        private System.Windows.Forms.Button btnThanhToan;
        private System.Windows.Forms.GroupBox groupBox2;
    }
}