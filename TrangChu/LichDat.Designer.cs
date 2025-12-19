namespace TrangChu // <-- Chú ý: Đổi thành namespace đúng của bạn (VD: TrangChu hoặc GUI)
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
            this.txtDonGia = new System.Windows.Forms.TextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.btnQuayLai = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnTaiLai = new System.Windows.Forms.Button();
            this.txtTimKiem = new System.Windows.Forms.TextBox();
            this.btnTimKiem = new System.Windows.Forms.Button();
            this.label16 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.btnSan6 = new System.Windows.Forms.Button();
            this.btnSan5 = new System.Windows.Forms.Button();
            this.btnSan4 = new System.Windows.Forms.Button();
            this.btnSan3 = new System.Windows.Forms.Button();
            this.btnSan2 = new System.Windows.Forms.Button();
            this.btnSan1 = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
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
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDatSan)).BeginInit();
            this.SuspendLayout();
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(6, 132);
            this.label7.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(47, 13);
            this.label7.TabIndex = 22;
            this.label7.Text = "Mã Sân:";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(2, 62);
            this.label8.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(92, 13);
            this.label8.TabIndex = 21;
            this.label8.Text = "Tên Khách Hàng:";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(5, 180);
            this.label9.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(55, 13);
            this.label9.TabIndex = 20;
            this.label9.Text = "Ngày Đặt:";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(5, 215);
            this.label10.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(68, 13);
            this.label10.TabIndex = 19;
            this.label10.Text = "Giờ Bất Đầu:";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(5, 244);
            this.label11.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(69, 13);
            this.label11.TabIndex = 18;
            this.label11.Text = "Giờ Kết thúc:";
            // 
            // cbxMaSan
            // 
            this.cbxMaSan.Items.AddRange(new object[] {
            "San1",
            "San2",
            "San3",
            "San4",
            "San5",
            "San6",
            "San7"});
            this.cbxMaSan.Location = new System.Drawing.Point(91, 129);
            this.cbxMaSan.Margin = new System.Windows.Forms.Padding(2);
            this.cbxMaSan.Name = "cbxMaSan";
            this.cbxMaSan.Size = new System.Drawing.Size(139, 21);
            this.cbxMaSan.TabIndex = 4;
            // 
            // txtTenKhachHang
            // 
            this.txtTenKhachHang.Location = new System.Drawing.Point(93, 62);
            this.txtTenKhachHang.Margin = new System.Windows.Forms.Padding(2);
            this.txtTenKhachHang.Name = "txtTenKhachHang";
            this.txtTenKhachHang.Size = new System.Drawing.Size(137, 20);
            this.txtTenKhachHang.TabIndex = 2;
            // 
            // dtpGioBatDau
            // 
            this.dtpGioBatDau.Format = System.Windows.Forms.DateTimePickerFormat.Time;
            this.dtpGioBatDau.Location = new System.Drawing.Point(92, 212);
            this.dtpGioBatDau.Margin = new System.Windows.Forms.Padding(2);
            this.dtpGioBatDau.Name = "dtpGioBatDau";
            this.dtpGioBatDau.ShowUpDown = true;
            this.dtpGioBatDau.Size = new System.Drawing.Size(138, 20);
            this.dtpGioBatDau.TabIndex = 6;
            // 
            // dtpGioKetThuc
            // 
            this.dtpGioKetThuc.Format = System.Windows.Forms.DateTimePickerFormat.Time;
            this.dtpGioKetThuc.Location = new System.Drawing.Point(92, 247);
            this.dtpGioKetThuc.Margin = new System.Windows.Forms.Padding(2);
            this.dtpGioKetThuc.Name = "dtpGioKetThuc";
            this.dtpGioKetThuc.ShowUpDown = true;
            this.dtpGioKetThuc.Size = new System.Drawing.Size(139, 20);
            this.dtpGioKetThuc.TabIndex = 7;
            // 
            // btnDatSAn
            // 
            this.btnDatSAn.BackColor = System.Drawing.Color.White;
            this.btnDatSAn.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDatSAn.Location = new System.Drawing.Point(41, 348);
            this.btnDatSAn.Margin = new System.Windows.Forms.Padding(2);
            this.btnDatSAn.Name = "btnDatSAn";
            this.btnDatSAn.Size = new System.Drawing.Size(77, 33);
            this.btnDatSAn.TabIndex = 9;
            this.btnDatSAn.Text = "ĐẶT SÂN";
            this.btnDatSAn.UseVisualStyleBackColor = false;
            this.btnDatSAn.Click += new System.EventHandler(this.btnDatSAn_Click);
            // 
            // btnHuySan
            // 
            this.btnHuySan.BackColor = System.Drawing.Color.White;
            this.btnHuySan.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnHuySan.Location = new System.Drawing.Point(144, 348);
            this.btnHuySan.Margin = new System.Windows.Forms.Padding(2);
            this.btnHuySan.Name = "btnHuySan";
            this.btnHuySan.Size = new System.Drawing.Size(82, 33);
            this.btnHuySan.TabIndex = 10;
            this.btnHuySan.Text = "HỦY SÂN";
            this.btnHuySan.UseVisualStyleBackColor = false;
            this.btnHuySan.Click += new System.EventHandler(this.btnHuySan_Click);
            // 
            // btnXoa
            // 
            this.btnXoa.BackColor = System.Drawing.Color.White;
            this.btnXoa.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnXoa.Location = new System.Drawing.Point(144, 395);
            this.btnXoa.Margin = new System.Windows.Forms.Padding(2);
            this.btnXoa.Name = "btnXoa";
            this.btnXoa.Size = new System.Drawing.Size(82, 30);
            this.btnXoa.TabIndex = 12;
            this.btnXoa.Text = "XÓA";
            this.btnXoa.UseVisualStyleBackColor = false;
            this.btnXoa.Click += new System.EventHandler(this.btnXoa_Click);
            // 
            // btnSua
            // 
            this.btnSua.BackColor = System.Drawing.Color.White;
            this.btnSua.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSua.Location = new System.Drawing.Point(40, 395);
            this.btnSua.Margin = new System.Windows.Forms.Padding(2);
            this.btnSua.Name = "btnSua";
            this.btnSua.Size = new System.Drawing.Size(79, 30);
            this.btnSua.TabIndex = 11;
            this.btnSua.Text = "SỬA";
            this.btnSua.UseVisualStyleBackColor = false;
            this.btnSua.Click += new System.EventHandler(this.btnSua_Click);
            // 
            // lblSDT_KH
            // 
            this.lblSDT_KH.AutoSize = true;
            this.lblSDT_KH.Location = new System.Drawing.Point(2, 98);
            this.lblSDT_KH.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblSDT_KH.Name = "lblSDT_KH";
            this.lblSDT_KH.Size = new System.Drawing.Size(95, 13);
            this.lblSDT_KH.TabIndex = 23;
            this.lblSDT_KH.Text = "SDT Khách Hàng:";
            // 
            // txtSDT
            // 
            this.txtSDT.Location = new System.Drawing.Point(93, 95);
            this.txtSDT.Margin = new System.Windows.Forms.Padding(2);
            this.txtSDT.Name = "txtSDT";
            this.txtSDT.Size = new System.Drawing.Size(137, 20);
            this.txtSDT.TabIndex = 3;
            // 
            // dtpNgayDat
            // 
            this.dtpNgayDat.Location = new System.Drawing.Point(92, 176);
            this.dtpNgayDat.Name = "dtpNgayDat";
            this.dtpNgayDat.Size = new System.Drawing.Size(138, 20);
            this.dtpNgayDat.TabIndex = 5;
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.LimeGreen;
            this.groupBox1.Controls.Add(this.txtDonGia);
            this.groupBox1.Controls.Add(this.label15);
            this.groupBox1.Controls.Add(this.btnQuayLai);
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
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(1, 2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(249, 480);
            this.groupBox1.TabIndex = 26;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Thông Tin Đặt";
            // 
            // txtDonGia
            // 
            this.txtDonGia.Location = new System.Drawing.Point(92, 292);
            this.txtDonGia.Name = "txtDonGia";
            this.txtDonGia.Size = new System.Drawing.Size(139, 20);
            this.txtDonGia.TabIndex = 8;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(6, 292);
            this.label15.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(49, 13);
            this.label15.TabIndex = 34;
            this.label15.Text = "Đơn Giá:";
            // 
            // btnQuayLai
            // 
            this.btnQuayLai.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnQuayLai.Image = global::TrangChu.Properties.Resources.Screenshot_2025_12_14_150219_removebg_preview;
            this.btnQuayLai.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnQuayLai.Location = new System.Drawing.Point(-1, 451);
            this.btnQuayLai.Name = "btnQuayLai";
            this.btnQuayLai.Size = new System.Drawing.Size(91, 23);
            this.btnQuayLai.TabIndex = 26;
            this.btnQuayLai.Text = "Quay Lại";
            this.btnQuayLai.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnQuayLai.UseVisualStyleBackColor = true;
            this.btnQuayLai.Click += new System.EventHandler(this.btnQuayLai_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
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
            this.groupBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.Location = new System.Drawing.Point(249, 2);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(715, 223);
            this.groupBox2.TabIndex = 27;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Sơ Đồ Sân";
            // 
            // btnTaiLai
            // 
            this.btnTaiLai.Image = ((System.Drawing.Image)(resources.GetObject("btnTaiLai.Image")));
            this.btnTaiLai.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnTaiLai.Location = new System.Drawing.Point(631, 201);
            this.btnTaiLai.Name = "btnTaiLai";
            this.btnTaiLai.Size = new System.Drawing.Size(84, 23);
            this.btnTaiLai.TabIndex = 38;
            this.btnTaiLai.Text = "Tải Lại";
            this.btnTaiLai.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnTaiLai.UseVisualStyleBackColor = true;
            this.btnTaiLai.Click += new System.EventHandler(this.btnTaiLai_Click);
            // 
            // txtTimKiem
            // 
            this.txtTimKiem.Location = new System.Drawing.Point(510, 203);
            this.txtTimKiem.Margin = new System.Windows.Forms.Padding(2);
            this.txtTimKiem.Name = "txtTimKiem";
            this.txtTimKiem.Size = new System.Drawing.Size(102, 20);
            this.txtTimKiem.TabIndex = 35;
            // 
            // btnTimKiem
            // 
            this.btnTimKiem.Image = ((System.Drawing.Image)(resources.GetObject("btnTimKiem.Image")));
            this.btnTimKiem.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnTimKiem.Location = new System.Drawing.Point(428, 203);
            this.btnTimKiem.Name = "btnTimKiem";
            this.btnTimKiem.Size = new System.Drawing.Size(88, 23);
            this.btnTimKiem.TabIndex = 37;
            this.btnTimKiem.Text = "Tìm Kiếm:";
            this.btnTimKiem.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnTimKiem.UseVisualStyleBackColor = true;
            this.btnTimKiem.Click += new System.EventHandler(this.btnTimKiem_Click);
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label16.Location = new System.Drawing.Point(507, 80);
            this.label16.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(31, 16);
            this.label16.TabIndex = 36;
            this.label16.Text = "7v7";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.Location = new System.Drawing.Point(161, 86);
            this.label12.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(31, 16);
            this.label12.TabIndex = 35;
            this.label12.Text = "5v5";
            // 
            // btnSan6
            // 
            this.btnSan6.Image = ((System.Drawing.Image)(resources.GetObject("btnSan6.Image")));
            this.btnSan6.Location = new System.Drawing.Point(543, 18);
            this.btnSan6.Name = "btnSan6";
            this.btnSan6.Size = new System.Drawing.Size(107, 149);
            this.btnSan6.TabIndex = 19;
            this.btnSan6.UseVisualStyleBackColor = true;
            // 
            // btnSan5
            // 
            this.btnSan5.Image = ((System.Drawing.Image)(resources.GetObject("btnSan5.Image")));
            this.btnSan5.Location = new System.Drawing.Point(393, 19);
            this.btnSan5.Name = "btnSan5";
            this.btnSan5.Size = new System.Drawing.Size(107, 150);
            this.btnSan5.TabIndex = 18;
            this.btnSan5.UseVisualStyleBackColor = true;
            // 
            // btnSan4
            // 
            this.btnSan4.Image = ((System.Drawing.Image)(resources.GetObject("btnSan4.Image")));
            this.btnSan4.Location = new System.Drawing.Point(202, 107);
            this.btnSan4.Name = "btnSan4";
            this.btnSan4.Size = new System.Drawing.Size(116, 62);
            this.btnSan4.TabIndex = 17;
            this.btnSan4.UseVisualStyleBackColor = true;
            // 
            // btnSan3
            // 
            this.btnSan3.Image = ((System.Drawing.Image)(resources.GetObject("btnSan3.Image")));
            this.btnSan3.Location = new System.Drawing.Point(45, 109);
            this.btnSan3.Name = "btnSan3";
            this.btnSan3.Size = new System.Drawing.Size(116, 62);
            this.btnSan3.TabIndex = 16;
            this.btnSan3.UseVisualStyleBackColor = true;
            // 
            // btnSan2
            // 
            this.btnSan2.Image = ((System.Drawing.Image)(resources.GetObject("btnSan2.Image")));
            this.btnSan2.Location = new System.Drawing.Point(202, 18);
            this.btnSan2.Name = "btnSan2";
            this.btnSan2.Size = new System.Drawing.Size(116, 62);
            this.btnSan2.TabIndex = 15;
            this.btnSan2.UseVisualStyleBackColor = true;
            // 
            // btnSan1
            // 
            this.btnSan1.Image = ((System.Drawing.Image)(resources.GetObject("btnSan1.Image")));
            this.btnSan1.Location = new System.Drawing.Point(45, 19);
            this.btnSan1.Name = "btnSan1";
            this.btnSan1.Size = new System.Drawing.Size(116, 62);
            this.btnSan1.TabIndex = 14;
            this.btnSan1.UseVisualStyleBackColor = true;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(425, 176);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(40, 13);
            this.label6.TabIndex = 12;
            this.label6.Text = "Sân 5";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(235, 173);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(40, 13);
            this.label5.TabIndex = 11;
            this.label5.Text = "Sân 4";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(572, 173);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(40, 13);
            this.label4.TabIndex = 11;
            this.label4.Text = "Sân 6";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(88, 173);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(40, 13);
            this.label3.TabIndex = 10;
            this.label3.Text = "Sân 3";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(235, 82);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(40, 13);
            this.label2.TabIndex = 9;
            this.label2.Text = "Sân 2";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(83, 83);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(40, 13);
            this.label1.TabIndex = 8;
            this.label1.Text = "Sân 1";
            // 
            // dgvDatSan
            // 
            this.dgvDatSan.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.dgvDatSan.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
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
            this.dgvDatSan.Location = new System.Drawing.Point(249, 224);
            this.dgvDatSan.Name = "dgvDatSan";
            this.dgvDatSan.Size = new System.Drawing.Size(715, 258);
            this.dgvDatSan.TabIndex = 13;
            this.dgvDatSan.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvDatSan_CellClick);
            // 
            // clMaLich
            // 
            this.clMaLich.HeaderText = "Mã Lịch";
            this.clMaLich.Name = "clMaLich";
            this.clMaLich.Width = 50;
            // 
            // clMaSan
            // 
            this.clMaSan.HeaderText = "Mã Sân";
            this.clMaSan.Name = "clMaSan";
            this.clMaSan.Width = 50;
            // 
            // clSDT_KH
            // 
            this.clSDT_KH.HeaderText = "SĐT KH";
            this.clSDT_KH.Name = "clSDT_KH";
            this.clSDT_KH.Width = 90;
            // 
            // clTenKH
            // 
            this.clTenKH.HeaderText = "Tên KH";
            this.clTenKH.Name = "clTenKH";
            this.clTenKH.Width = 140;
            // 
            // clNgayDat
            // 
            this.clNgayDat.HeaderText = "Ngày Đặt";
            this.clNgayDat.Name = "clNgayDat";
            this.clNgayDat.Width = 80;
            // 
            // clGioBatDau
            // 
            this.clGioBatDau.HeaderText = "Giờ Bắt Đầu";
            this.clGioBatDau.Name = "clGioBatDau";
            this.clGioBatDau.Width = 50;
            // 
            // clGioKetThuc
            // 
            this.clGioKetThuc.HeaderText = "Giờ Kết Thúc";
            this.clGioKetThuc.Name = "clGioKetThuc";
            this.clGioKetThuc.Width = 50;
            // 
            // clTrangThai
            // 
            this.clTrangThai.HeaderText = "Trạng Thái";
            this.clTrangThai.Name = "clTrangThai";
            this.clTrangThai.Width = 80;
            // 
            // clDonGiaThucTe
            // 
            this.clDonGiaThucTe.HeaderText = "Đơn Giá Thực Tế";
            this.clDonGiaThucTe.Name = "clDonGiaThucTe";
            this.clDonGiaThucTe.Width = 80;
            // 
            // LichDat
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(981, 496);
            this.Controls.Add(this.dgvDatSan);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "LichDat";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Trang ĐẶT SÂN";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDatSan)).EndInit();
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
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnQuayLai;
        private System.Windows.Forms.DataGridView dgvDatSan;
        private System.Windows.Forms.Button btnSan1;
        private System.Windows.Forms.Button btnSan5;
        private System.Windows.Forms.Button btnSan4;
        private System.Windows.Forms.Button btnSan3;
        private System.Windows.Forms.Button btnSan2;
        private System.Windows.Forms.Button btnSan6;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox txtTimKiem;
        private System.Windows.Forms.Button btnTimKiem;
        private System.Windows.Forms.Button btnTaiLai;
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
    }
}