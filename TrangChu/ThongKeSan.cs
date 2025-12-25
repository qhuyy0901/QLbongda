using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using BUS;

namespace TrangChu
{
    public partial class ThongKeSan : Form
    {
        private ThongKeBUS busThongKe = new ThongKeBUS();

        public ThongKeSan()
        {
            InitializeComponent();
            this.Load += ThongKeSan_Load;
        }

        private void ThongKeSan_Load(object sender, EventArgs e)
        {
            try
            {
                // ===== CẤU HÌNH DATAGRIDVIEW =====
                dgvThongKeSan.AutoGenerateColumns = false;
                dgvThongKeSan.AllowUserToAddRows = false;
                dgvThongKeSan.AllowUserToDeleteRows = false;
                dgvThongKeSan.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

                // ===== CẤU HÌNH CÁC CỘT =====
                if (dgvThongKeSan.Columns.Count == 0)
                {
                    dgvThongKeSan.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Mã Sân", DataPropertyName = "MaSan", Width = 80 });
                    dgvThongKeSan.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Tên Sân", DataPropertyName = "TenSan", Width = 150 });
                    dgvThongKeSan.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Loại Sân", DataPropertyName = "LoaiSan", Width = 100 });
                    dgvThongKeSan.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Doanh Thu Lịch Đặt", DataPropertyName = "DoanhThuLichDat", Width = 150, DefaultCellStyle = new DataGridViewCellStyle { Format = "N0", Alignment = DataGridViewContentAlignment.MiddleRight } });
                    dgvThongKeSan.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Doanh Thu Dịch Vụ", DataPropertyName = "DoanhThuDichVu", Width = 150, DefaultCellStyle = new DataGridViewCellStyle { Format = "N0", Alignment = DataGridViewContentAlignment.MiddleRight } });
                    dgvThongKeSan.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Tổng Doanh Thu", DataPropertyName = "TongDoanhThu", Width = 150, DefaultCellStyle = new DataGridViewCellStyle { Format = "N0", Alignment = DataGridViewContentAlignment.MiddleRight } });
                    dgvThongKeSan.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Số Lần Đặt", DataPropertyName = "SoLanDat", Width = 100, DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleCenter } });
                }

                // ===== LOAD DỮ LIỆU =====
                LoadThongKeSan();

                // ===== GẮN EVENT =====
                btnTaiLai.Click += BtnTaiLai_Click;
                btnChart.Click += BtnChart_Click;
                cboNam.SelectedIndexChanged += CboNam_SelectedIndexChanged;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"❌ Lỗi khởi tạo: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ===== LOAD THỐNG KÊ =====
        private void LoadThongKeSan()
        {
            try
            {
                var data = busThongKe.GetRevenueBySan();

                // ===== THÊM CỘT TÍNH TOÁN =====
                var displayData = data.Select(x => new
                {
                    x.MaSan,
                    x.TenSan,
                    x.LoaiSan,
                    x.DoanhThuLichDat,
                    x.DoanhThuDichVu,
                    TongDoanhThu = (decimal)x.DoanhThuLichDat + (decimal)x.DoanhThuDichVu,
                    x.SoLanDat
                }).Cast<dynamic>().ToList();

                dgvThongKeSan.DataSource = displayData;

                // ===== ĐỊNH DẠNG CỘT =====
                foreach (DataGridViewColumn col in dgvThongKeSan.Columns)
                {
                    if (col.HeaderText.Contains("Doanh Thu") || col.HeaderText == "Tổng Doanh Thu")
                    {
                        col.DefaultCellStyle.Format = "N0";
                        col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    }
                }

                // ===== HIỂN THỊ TỔNG =====
                DisplayTotal(displayData);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"❌ Lỗi tải dữ liệu: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ===== HIỂN THỊ TỔNG CỘNG =====
        private void DisplayTotal(List<dynamic> data)
        {
            try
            {
                decimal tongDoanhThuLichDat = 0;
                decimal tongDoanhThuDichVu = 0;
                int tongSoLanDat = 0;

                foreach (var item in data)
                {
                    tongDoanhThuLichDat += (decimal)item.DoanhThuLichDat;
                    tongDoanhThuDichVu += (decimal)item.DoanhThuDichVu;
                    tongSoLanDat += (int)item.SoLanDat;
                }

                lblTongDoanhThuLichDat.Text = tongDoanhThuLichDat.ToString("N0") + " VNĐ";
                lblTongDoanhThuDichVu.Text = tongDoanhThuDichVu.ToString("N0") + " VNĐ";
                lblTongCong.Text = (tongDoanhThuLichDat + tongDoanhThuDichVu).ToString("N0") + " VNĐ";
                lblTongSoLanDat.Text = tongSoLanDat.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"❌ Lỗi hiển thị tổng: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ===== NÚT TẢI LẠI =====
        private void BtnTaiLai_Click(object sender, EventArgs e)
        {
            LoadThongKeSan();
            MessageBox.Show("✔ Tải lại thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        // ===== NÚT BIỂU ĐỒ =====
        private void BtnChart_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvThongKeSan.DataSource == null || dgvThongKeSan.Rows.Count == 0)
                {
                    MessageBox.Show("❌ Không có dữ liệu để hiển thị!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                
            }
            catch (Exception ex)
            {
                MessageBox.Show($"❌ Lỗi: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CboNam_SelectedIndexChanged(object sender, EventArgs e)
        {
            // TODO: Xử lý thống kê theo năm nếu cần
        }
    }
}
