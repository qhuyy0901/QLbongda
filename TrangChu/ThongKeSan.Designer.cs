namespace TrangChu
{
    partial class ThongKeSan
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.DataGridView dgvThongKeSan;
        private System.Windows.Forms.Button btnTaiLai;
        private System.Windows.Forms.Button btnChart;
        private System.Windows.Forms.ComboBox cboNam;
        private System.Windows.Forms.Label lblTongDoanhThuLichDat;
        private System.Windows.Forms.Label lblTongDoanhThuDichVu;
        private System.Windows.Forms.Label lblTongCong;
        private System.Windows.Forms.Label lblTongSoLanDat;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.dgvThongKeSan = new System.Windows.Forms.DataGridView();
            this.btnTaiLai = new System.Windows.Forms.Button();
            this.btnChart = new System.Windows.Forms.Button();
            this.cboNam = new System.Windows.Forms.ComboBox();
            this.lblTongDoanhThuLichDat = new System.Windows.Forms.Label();
            this.lblTongDoanhThuDichVu = new System.Windows.Forms.Label();
            this.lblTongCong = new System.Windows.Forms.Label();
            this.lblTongSoLanDat = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvThongKeSan)).BeginInit();
            this.SuspendLayout();

            // dgvThongKeSan
            this.dgvThongKeSan.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvThongKeSan.Location = new System.Drawing.Point(12, 50);
            this.dgvThongKeSan.Name = "dgvThongKeSan";
            this.dgvThongKeSan.Size = new System.Drawing.Size(1000, 350);
            this.dgvThongKeSan.TabIndex = 0;

            // btnTaiLai
            this.btnTaiLai.Location = new System.Drawing.Point(12, 15);
            this.btnTaiLai.Name = "btnTaiLai";
            this.btnTaiLai.Size = new System.Drawing.Size(100, 25);
            this.btnTaiLai.TabIndex = 1;
            this.btnTaiLai.Text = "🔄 Tải lại";
            this.btnTaiLai.UseVisualStyleBackColor = true;

            // btnChart
            this.btnChart.Location = new System.Drawing.Point(120, 15);
            this.btnChart.Name = "btnChart";
            this.btnChart.Size = new System.Drawing.Size(100, 25);
            this.btnChart.TabIndex = 2;
            this.btnChart.Text = "📊 Biểu đồ";
            this.btnChart.UseVisualStyleBackColor = true;

            // cboNam
            this.cboNam.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboNam.Location = new System.Drawing.Point(850, 15);
            this.cboNam.Name = "cboNam";
            this.cboNam.Size = new System.Drawing.Size(150, 20);
            this.cboNam.TabIndex = 3;

            // lblTongDoanhThuLichDat
            this.lblTongDoanhThuLichDat.AutoSize = true;
            this.lblTongDoanhThuLichDat.Location = new System.Drawing.Point(12, 415);
            this.lblTongDoanhThuLichDat.Name = "lblTongDoanhThuLichDat";
            this.lblTongDoanhThuLichDat.Size = new System.Drawing.Size(200, 13);
            this.lblTongDoanhThuLichDat.Text = "Tổng Doanh Thu Lịch Đặt: 0 VNĐ";

            // lblTongDoanhThuDichVu
            this.lblTongDoanhThuDichVu.AutoSize = true;
            this.lblTongDoanhThuDichVu.Location = new System.Drawing.Point(12, 435);
            this.lblTongDoanhThuDichVu.Name = "lblTongDoanhThuDichVu";
            this.lblTongDoanhThuDichVu.Size = new System.Drawing.Size(200, 13);
            this.lblTongDoanhThuDichVu.Text = "Tổng Doanh Thu Dịch Vụ: 0 VNĐ";

            // lblTongCong
            this.lblTongCong.AutoSize = true;
            this.lblTongCong.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.lblTongCong.Location = new System.Drawing.Point(12, 455);
            this.lblTongCong.Name = "lblTongCong";
            this.lblTongCong.Size = new System.Drawing.Size(200, 13);
            this.lblTongCong.Text = "TỔNG CỘNG: 0 VNĐ";

            // lblTongSoLanDat
            this.lblTongSoLanDat.AutoSize = true;
            this.lblTongSoLanDat.Location = new System.Drawing.Point(450, 415);
            this.lblTongSoLanDat.Name = "lblTongSoLanDat";
            this.lblTongSoLanDat.Size = new System.Drawing.Size(200, 13);
            this.lblTongSoLanDat.Text = "Tổng Số Lần Đặt: 0";

            // ThongKeSan
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1024, 500);
            this.Controls.Add(this.lblTongSoLanDat);
            this.Controls.Add(this.lblTongCong);
            this.Controls.Add(this.lblTongDoanhThuDichVu);
            this.Controls.Add(this.lblTongDoanhThuLichDat);
            this.Controls.Add(this.cboNam);
            this.Controls.Add(this.btnChart);
            this.Controls.Add(this.btnTaiLai);
            this.Controls.Add(this.dgvThongKeSan);
            this.Name = "ThongKeSan";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "THỐNG KÊ DOANH THU THEO SÂN";
            ((System.ComponentModel.ISupportInitialize)(this.dgvThongKeSan)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}