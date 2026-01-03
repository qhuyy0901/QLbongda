using System.Windows.Forms;

namespace TrangChu
{
    partial class CT_HoaDon_DichVu
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
            this.grpThongTin = new System.Windows.Forms.GroupBox();
            this.txtEmaill = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtSDT = new System.Windows.Forms.TextBox();
            this.lblSDT = new System.Windows.Forms.Label();
            this.txtTenKH = new System.Windows.Forms.TextBox();
            this.lblTenKH = new System.Windows.Forms.Label();
            this.cbxMaLich = new System.Windows.Forms.ComboBox();
            this.lblMaLich = new System.Windows.Forms.Label();
            this.grpTienSan = new System.Windows.Forms.GroupBox();
            this.txtTienSan = new System.Windows.Forms.TextBox();
            this.lblGiaSan = new System.Windows.Forms.Label();
            this.txtKhungGio = new System.Windows.Forms.TextBox();
            this.lblGio = new System.Windows.Forms.Label();
            this.txtTenSan = new System.Windows.Forms.TextBox();
            this.lblSan = new System.Windows.Forms.Label();
            this.grpDichVu = new System.Windows.Forms.GroupBox();
            this.dgvCTDichVu = new System.Windows.Forms.DataGridView();
            this.grpTongTien = new System.Windows.Forms.GroupBox();
            this.picQRCode = new System.Windows.Forms.PictureBox();
            this.lblQRCode = new System.Windows.Forms.Label();
            this.btnHuy = new System.Windows.Forms.Button();
            this.btnThanhToan = new System.Windows.Forms.Button();
            this.cbxHinhThucTT = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.lblTongThanhToan = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.lblTongTienDV_Value = new System.Windows.Forms.Label();
            this.lblTongTienDV_Text = new System.Windows.Forms.Label();
            this.lblTongTienSan_Value = new System.Windows.Forms.Label();
            this.lblTongTienSan_Text = new System.Windows.Forms.Label();
            this.grpThongTin.SuspendLayout();
            this.grpTienSan.SuspendLayout();
            this.grpDichVu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCTDichVu)).BeginInit();
            this.grpTongTien.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picQRCode)).BeginInit();
            this.SuspendLayout();
            // 
            // grpThongTin
            // 
            this.grpThongTin.Controls.Add(this.txtEmaill);
            this.grpThongTin.Controls.Add(this.label1);
            this.grpThongTin.Controls.Add(this.txtSDT);
            this.grpThongTin.Controls.Add(this.lblSDT);
            this.grpThongTin.Controls.Add(this.txtTenKH);
            this.grpThongTin.Controls.Add(this.lblTenKH);
            this.grpThongTin.Controls.Add(this.cbxMaLich);
            this.grpThongTin.Controls.Add(this.lblMaLich);
            this.grpThongTin.Location = new System.Drawing.Point(12, 12);
            this.grpThongTin.Name = "grpThongTin";
            this.grpThongTin.Size = new System.Drawing.Size(266, 179);
            this.grpThongTin.TabIndex = 0;
            this.grpThongTin.TabStop = false;
            this.grpThongTin.Text = "Thông Tin Khách Hàng / Lịch Đặt";
            // 
            // txtEmaill
            // 
            this.txtEmaill.Location = new System.Drawing.Point(100, 140);
            this.txtEmaill.Name = "txtEmaill";
            this.txtEmaill.Size = new System.Drawing.Size(145, 20);
            this.txtEmaill.TabIndex = 9;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(20, 143);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 8;
            this.label1.Text = "Email:";
            // 
            // txtSDT
            // 
            this.txtSDT.Location = new System.Drawing.Point(100, 101);
            this.txtSDT.Name = "txtSDT";
            this.txtSDT.Size = new System.Drawing.Size(145, 20);
            this.txtSDT.TabIndex = 7;
            // 
            // lblSDT
            // 
            this.lblSDT.AutoSize = true;
            this.lblSDT.Location = new System.Drawing.Point(20, 104);
            this.lblSDT.Name = "lblSDT";
            this.lblSDT.Size = new System.Drawing.Size(32, 13);
            this.lblSDT.TabIndex = 6;
            this.lblSDT.Text = "SĐT:";
            // 
            // txtTenKH
            // 
            this.txtTenKH.Location = new System.Drawing.Point(100, 66);
            this.txtTenKH.Name = "txtTenKH";
            this.txtTenKH.Size = new System.Drawing.Size(145, 20);
            this.txtTenKH.TabIndex = 5;
            // 
            // lblTenKH
            // 
            this.lblTenKH.AutoSize = true;
            this.lblTenKH.Location = new System.Drawing.Point(20, 69);
            this.lblTenKH.Name = "lblTenKH";
            this.lblTenKH.Size = new System.Drawing.Size(47, 13);
            this.lblTenKH.TabIndex = 4;
            this.lblTenKH.Text = "Tên KH:";



            txtTenKH.ReadOnly = true;
            txtSDT.ReadOnly = true;
            txtTenSan.ReadOnly = true;
            txtKhungGio.ReadOnly = true;
            txtTienSan.ReadOnly = true;
            cbxMaLich.Enabled = true;
            // 
            // cbxMaLich
            // 



            cbxHinhThucTT.Items.Clear();
            cbxHinhThucTT.Items.Add("Tiền mặt");
            cbxHinhThucTT.Items.Add("Chuyển khoản");
            cbxHinhThucTT.SelectedIndex = 0;

            dgvCTDichVu.ReadOnly = true;
            dgvCTDichVu.AllowUserToAddRows = false;
            dgvCTDichVu.AllowUserToDeleteRows = false;
            dgvCTDichVu.AllowUserToResizeRows = false;
            dgvCTDichVu.SelectionMode = DataGridViewSelectionMode.FullRowSelect;


            this.cbxMaLich.Enabled = false;
            this.cbxMaLich.Location = new System.Drawing.Point(100, 31);
            this.cbxMaLich.Name = "cbxMaLich";
            this.cbxMaLich.Size = new System.Drawing.Size(145, 21);
            this.cbxMaLich.TabIndex = 3;
            // 
            // lblMaLich
            // 
            this.lblMaLich.AutoSize = true;
            this.lblMaLich.Location = new System.Drawing.Point(20, 34);
            this.lblMaLich.Name = "lblMaLich";
            this.lblMaLich.Size = new System.Drawing.Size(48, 13);
            this.lblMaLich.TabIndex = 2;
            this.lblMaLich.Text = "Mã Lịch:";
            // 
            // grpTienSan
            // 
            this.grpTienSan.Controls.Add(this.txtTienSan);
            this.grpTienSan.Controls.Add(this.lblGiaSan);
            this.grpTienSan.Controls.Add(this.txtKhungGio);
            this.grpTienSan.Controls.Add(this.lblGio);
            this.grpTienSan.Controls.Add(this.txtTenSan);
            this.grpTienSan.Controls.Add(this.lblSan);
            this.grpTienSan.Location = new System.Drawing.Point(12, 197);
            this.grpTienSan.Name = "grpTienSan";
            this.grpTienSan.Size = new System.Drawing.Size(266, 140);
            this.grpTienSan.TabIndex = 1;
            this.grpTienSan.TabStop = false;
            this.grpTienSan.Text = "Chi Tiết Tiền Sân";
            // 
            // txtTienSan
            // 
            this.txtTienSan.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTienSan.Location = new System.Drawing.Point(100, 97);
            this.txtTienSan.Name = "txtTienSan";
            this.txtTienSan.ReadOnly = true;
            this.txtTienSan.Size = new System.Drawing.Size(145, 20);
            this.txtTienSan.TabIndex = 5;
            this.txtTienSan.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lblGiaSan
            // 
            this.lblGiaSan.AutoSize = true;
            this.lblGiaSan.Location = new System.Drawing.Point(20, 100);
            this.lblGiaSan.Name = "lblGiaSan";
            this.lblGiaSan.Size = new System.Drawing.Size(53, 13);
            this.lblGiaSan.TabIndex = 4;
            this.lblGiaSan.Text = "Tiền Sân:";
            // 
            // txtKhungGio
            // 
            this.txtKhungGio.Location = new System.Drawing.Point(100, 62);
            this.txtKhungGio.Name = "txtKhungGio";
            this.txtKhungGio.ReadOnly = true;
            this.txtKhungGio.Size = new System.Drawing.Size(145, 20);
            this.txtKhungGio.TabIndex = 3;
            // 
            // lblGio
            // 
            this.lblGio.AutoSize = true;
            this.lblGio.Location = new System.Drawing.Point(20, 65);
            this.lblGio.Name = "lblGio";
            this.lblGio.Size = new System.Drawing.Size(58, 13);
            this.lblGio.TabIndex = 2;
            this.lblGio.Text = "Khung giờ:";
            // 
            // txtTenSan
            // 
            this.txtTenSan.Location = new System.Drawing.Point(100, 27);
            this.txtTenSan.Name = "txtTenSan";
            this.txtTenSan.ReadOnly = true;
            this.txtTenSan.Size = new System.Drawing.Size(145, 20);
            this.txtTenSan.TabIndex = 1;
            // 
            // lblSan
            // 
            this.lblSan.AutoSize = true;
            this.lblSan.Location = new System.Drawing.Point(20, 30);
            this.lblSan.Name = "lblSan";
            this.lblSan.Size = new System.Drawing.Size(29, 13);
            this.lblSan.TabIndex = 0;
            this.lblSan.Text = "Sân:";
            // 
            // grpDichVu
            // 
            this.grpDichVu.Controls.Add(this.dgvCTDichVu);
            this.grpDichVu.Location = new System.Drawing.Point(284, 12);
            this.grpDichVu.Name = "grpDichVu";
            this.grpDichVu.Size = new System.Drawing.Size(496, 328);
            this.grpDichVu.TabIndex = 2;
            this.grpDichVu.TabStop = false;
            this.grpDichVu.Text = "Chi Tiết Dịch Vụ";
            // 
            // dgvCTDichVu
            // 
            this.dgvCTDichVu.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvCTDichVu.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvCTDichVu.Location = new System.Drawing.Point(3, 16);
            this.dgvCTDichVu.Name = "dgvCTDichVu";
            this.dgvCTDichVu.ReadOnly = true;
            this.dgvCTDichVu.Size = new System.Drawing.Size(490, 309);
            this.dgvCTDichVu.TabIndex = 0;
            // 
            // grpTongTien
            // 
            this.grpTongTien.BackColor = System.Drawing.Color.WhiteSmoke;
            this.grpTongTien.Controls.Add(this.picQRCode);
            this.grpTongTien.Controls.Add(this.lblQRCode);
            this.grpTongTien.Controls.Add(this.btnHuy);
            this.grpTongTien.Controls.Add(this.btnThanhToan);
            this.grpTongTien.Controls.Add(this.cbxHinhThucTT);
            this.grpTongTien.Controls.Add(this.label5);
            this.grpTongTien.Controls.Add(this.lblTongThanhToan);
            this.grpTongTien.Controls.Add(this.label4);
            this.grpTongTien.Controls.Add(this.lblTongTienDV_Value);
            this.grpTongTien.Controls.Add(this.lblTongTienDV_Text);
            this.grpTongTien.Controls.Add(this.lblTongTienSan_Value);
            this.grpTongTien.Controls.Add(this.lblTongTienSan_Text);
            this.grpTongTien.Location = new System.Drawing.Point(12, 350);
            this.grpTongTien.Name = "grpTongTien";
            this.grpTongTien.Size = new System.Drawing.Size(768, 184);
            this.grpTongTien.TabIndex = 3;
            this.grpTongTien.TabStop = false;
            this.grpTongTien.Text = "Tổng Kết & Thanh Toán";
            // 
            // picQRCode
            // 
            this.picQRCode.BackColor = System.Drawing.Color.White;
            this.picQRCode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picQRCode.Location = new System.Drawing.Point(589, 21);
            this.picQRCode.Name = "picQRCode";
            this.picQRCode.Size = new System.Drawing.Size(150, 139);
            this.picQRCode.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picQRCode.TabIndex = 10;
            this.picQRCode.TabStop = false;
            this.picQRCode.Visible = false;
            // 
            // lblQRCode
            // 
            this.lblQRCode.AutoSize = true;
            this.lblQRCode.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblQRCode.Location = new System.Drawing.Point(586, 5);
            this.lblQRCode.Name = "lblQRCode";
            this.lblQRCode.Size = new System.Drawing.Size(64, 13);
            this.lblQRCode.TabIndex = 11;
            this.lblQRCode.Text = "💳 VietQR";
            this.lblQRCode.Visible = false;
            // 
            // btnHuy
            // 
            this.btnHuy.BackColor = System.Drawing.Color.IndianRed;
            this.btnHuy.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnHuy.ForeColor = System.Drawing.Color.White;
            this.btnHuy.Location = new System.Drawing.Point(459, 66);
            this.btnHuy.Name = "btnHuy";
            this.btnHuy.Size = new System.Drawing.Size(91, 40);
            this.btnHuy.TabIndex = 9;
            this.btnHuy.Text = "❌ THOÁT";
            this.btnHuy.UseVisualStyleBackColor = false;
            // 
            // btnThanhToan
            // 
            this.btnThanhToan.BackColor = System.Drawing.Color.LimeGreen;
            this.btnThanhToan.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnThanhToan.ForeColor = System.Drawing.Color.White;
            this.btnThanhToan.Location = new System.Drawing.Point(313, 66);
            this.btnThanhToan.Name = "btnThanhToan";
            this.btnThanhToan.Size = new System.Drawing.Size(140, 40);
            this.btnThanhToan.TabIndex = 8;
            this.btnThanhToan.Text = "💵 THANH TOÁN";
            this.btnThanhToan.UseVisualStyleBackColor = false;
            // 
            // cbxHinhThucTT
            // 
            this.cbxHinhThucTT.FormattingEnabled = true;
            this.cbxHinhThucTT.Location = new System.Drawing.Point(426, 31);
            this.cbxHinhThucTT.Name = "cbxHinhThucTT";
            this.cbxHinhThucTT.Size = new System.Drawing.Size(124, 21);
            this.cbxHinhThucTT.TabIndex = 7;
            this.cbxHinhThucTT.SelectedIndexChanged += new System.EventHandler(this.cbxHinhThucTT_SelectedIndexChanged_1);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(310, 34);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(110, 13);
            this.label5.TabIndex = 6;
            this.label5.Text = "Hình thức thanh toán:";
            // 
            // lblTongThanhToan
            // 
            this.lblTongThanhToan.AutoSize = true;
            this.lblTongThanhToan.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTongThanhToan.ForeColor = System.Drawing.Color.Red;
            this.lblTongThanhToan.Location = new System.Drawing.Point(187, 121);
            this.lblTongThanhToan.Name = "lblTongThanhToan";
            this.lblTongThanhToan.Size = new System.Drawing.Size(70, 24);
            this.lblTongThanhToan.TabIndex = 5;
            this.lblTongThanhToan.Text = "0 VNĐ";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(7, 127);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(174, 16);
            this.label4.TabIndex = 4;
            this.label4.Text = "💰 TỔNG THANH TOÁN:";
            // 
            // lblTongTienDV_Value
            // 
            this.lblTongTienDV_Value.AutoSize = true;
            this.lblTongTienDV_Value.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTongTienDV_Value.Location = new System.Drawing.Point(119, 66);
            this.lblTongTienDV_Value.Name = "lblTongTienDV_Value";
            this.lblTongTienDV_Value.Size = new System.Drawing.Size(44, 13);
            this.lblTongTienDV_Value.TabIndex = 3;
            this.lblTongTienDV_Value.Text = "0 VNĐ";
            // 
            // lblTongTienDV_Text
            // 
            this.lblTongTienDV_Text.AutoSize = true;
            this.lblTongTienDV_Text.Location = new System.Drawing.Point(29, 66);
            this.lblTongTienDV_Text.Name = "lblTongTienDV_Text";
            this.lblTongTienDV_Text.Size = new System.Drawing.Size(72, 13);
            this.lblTongTienDV_Text.TabIndex = 2;
            this.lblTongTienDV_Text.Text = "Tiền Dịch Vụ:";
            // 
            // lblTongTienSan_Value
            // 
            this.lblTongTienSan_Value.AutoSize = true;
            this.lblTongTienSan_Value.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTongTienSan_Value.Location = new System.Drawing.Point(119, 41);
            this.lblTongTienSan_Value.Name = "lblTongTienSan_Value";
            this.lblTongTienSan_Value.Size = new System.Drawing.Size(44, 13);
            this.lblTongTienSan_Value.TabIndex = 1;
            this.lblTongTienSan_Value.Text = "0 VNĐ";
            // 
            // lblTongTienSan_Text
            // 
            this.lblTongTienSan_Text.AutoSize = true;
            this.lblTongTienSan_Text.Location = new System.Drawing.Point(29, 41);
            this.lblTongTienSan_Text.Name = "lblTongTienSan_Text";
            this.lblTongTienSan_Text.Size = new System.Drawing.Size(53, 13);
            this.lblTongTienSan_Text.TabIndex = 0;
            this.lblTongTienSan_Text.Text = "Tiền Sân:";
            // 
            // CT_HoaDon_DichVu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(792, 546);
            this.Controls.Add(this.grpTongTien);
            this.Controls.Add(this.grpDichVu);
            this.Controls.Add(this.grpTienSan);
            this.Controls.Add(this.grpThongTin);
            this.Name = "CT_HoaDon_DichVu";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "CHI TIẾT THANH TOÁN";
            this.grpThongTin.ResumeLayout(false);
            this.grpThongTin.PerformLayout();
            this.grpTienSan.ResumeLayout(false);
            this.grpTienSan.PerformLayout();
            this.grpDichVu.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvCTDichVu)).EndInit();
            this.grpTongTien.ResumeLayout(false);
            this.grpTongTien.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picQRCode)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grpThongTin;
        private System.Windows.Forms.Label lblMaLich;
        private System.Windows.Forms.ComboBox cbxMaLich;
        private System.Windows.Forms.Label lblTenKH;
        private System.Windows.Forms.TextBox txtTenKH;
        private System.Windows.Forms.Label lblSDT;
        private System.Windows.Forms.TextBox txtSDT;
        private System.Windows.Forms.GroupBox grpTienSan;
        private System.Windows.Forms.Label lblSan;
        private System.Windows.Forms.TextBox txtTenSan;
        private System.Windows.Forms.Label lblGio;
        private System.Windows.Forms.TextBox txtKhungGio;
        private System.Windows.Forms.Label lblGiaSan;
        private System.Windows.Forms.TextBox txtTienSan;
        private System.Windows.Forms.GroupBox grpDichVu;
        private System.Windows.Forms.DataGridView dgvCTDichVu;
        private System.Windows.Forms.GroupBox grpTongTien;
        private System.Windows.Forms.Label lblTongTienSan_Text;
        private System.Windows.Forms.Label lblTongTienSan_Value;
        private System.Windows.Forms.Label lblTongTienDV_Text;
        private System.Windows.Forms.Label lblTongTienDV_Value;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lblTongThanhToan;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cbxHinhThucTT;
        private System.Windows.Forms.Button btnThanhToan;
        private System.Windows.Forms.Button btnHuy;
        // ===== THÊM CONTROL CHO QR CODE =====
        private System.Windows.Forms.PictureBox picQRCode;
        private System.Windows.Forms.Label lblQRCode;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtEmaill;
    }
}