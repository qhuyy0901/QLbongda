using BUS;
using DAL;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace TrangChu
{
    public partial class LichDat : Form
    {
        private LichDatBUS busLichDat = new LichDatBUS();
        private SanBongBUS busSanBong = new SanBongBUS();
        private bool isEditing = false;

        public LichDat()
        {
            InitializeComponent();
            this.Load += LichDat_Load;
        }

        private void LichDat_Load(object sender, EventArgs e)
        {
            try
            {
                clMaLich.DataPropertyName = "MaLich";
                clMaSan.DataPropertyName = "MaSan";
                clSDT_KH.DataPropertyName = "SDT_KH";
                clTenKH.DataPropertyName = "TenKH";
                clNgayDat.DataPropertyName = "NgayDat";
                clGioBatDau.DataPropertyName = "GioBD";
                clGioKetThuc.DataPropertyName = "GioKT";
                clTrangThai.DataPropertyName = "TrangThai";
                clDonGiaThucTe.DataPropertyName = "DonGiaThucTe";

                dtpNgayDat.Format = DateTimePickerFormat.Custom;
                dtpNgayDat.CustomFormat = "dd/MM/yyyy";
                dtpNgayDat.Value = DateTime.Now;
                
                // ===== THIẾT LẬP GIỚI HẠN NGÀY ĐẶT =====
                dtpNgayDat.MinDate = DateTime.Now.Date; 
                dtpNgayDat.MaxDate = DateTime.Now.AddDays(7);

                dtpGioBatDau.Format = DateTimePickerFormat.Custom;
                dtpGioBatDau.CustomFormat = "HH:00"; 
                dtpGioBatDau.ShowUpDown = true;

                dtpGioKetThuc.Format = DateTimePickerFormat.Custom;
                dtpGioKetThuc.CustomFormat = "HH:00";
                dtpGioKetThuc.ShowUpDown = true;

                dtpGioBatDau.Value = DateTime.Today.AddHours(15);
                dtpGioKetThuc.Value = DateTime.Today.AddHours(23);

                txtTimKiem.KeyDown += TxtTimKiem_KeyDown;
                dgvDatSan.CellClick += dgvDatSan_CellClick;

                LoadComboBoxSan();
                RefreshData();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
        }

        private void TxtTimKiem_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnTimKiem_Click(sender, e);
                e.Handled = true;
            }
        }

        private void LoadComboBoxSan()
        {
            cbxMaSan.DataSource = busSanBong.GetListSanBong();
            cbxMaSan.DisplayMember = "MaSan";
            cbxMaSan.ValueMember = "MaSan";
        }

        private void RefreshData()
        {
            try
            {
                var data = busLichDat.GetAll();
                dgvDatSan.DataSource = null;
                dgvDatSan.DataSource = data;
                ReapplyColumnBindings();
                
                // ===== ĐỊNH DẠNG CỘT ĐƠN GIÁ =====
                FormatDonGiaColumn();
            }
            catch (Exception ex)
            {
                Log($"❌ Lỗi làm mới dữ liệu: {ex.Message}");
            }
        }

        private void FormatDonGiaColumn()
        {
            try
            {
                if (dgvDatSan.Columns["clDonGiaThucTe"] != null)
                {
                    dgvDatSan.Columns["clDonGiaThucTe"].DefaultCellStyle.Format = "0.00";
                    dgvDatSan.Columns["clDonGiaThucTe"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                }
            }
            catch { }
        }

        private void ReapplyColumnBindings()
        {
            clMaLich.DataPropertyName = "MaLich";
            clMaSan.DataPropertyName = "MaSan";
            clSDT_KH.DataPropertyName = "SDT_KH";
            clTenKH.DataPropertyName = "TenKH";
            clNgayDat.DataPropertyName = "NgayDat";
            clGioBatDau.DataPropertyName = "GioBD";
            clGioKetThuc.DataPropertyName = "GioKT";
            clTrangThai.DataPropertyName = "TrangThai";
            clDonGiaThucTe.DataPropertyName = "DonGiaThucTe";
        }

        private void dgvDatSan_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            try
            {
                DataGridViewRow row = dgvDatSan.Rows[e.RowIndex];

                if (row.Cells[0].Value != null)
                    txtMaDat.Text = row.Cells[0].Value.ToString();

                if (row.Cells[1].Value != null)
                {
                    cbxMaSan.Text = row.Cells[1].Value.ToString();
                    cbxMaSan.SelectedValue = row.Cells[1].Value.ToString();
                }

                if (row.Cells[2].Value != null)
                    txtSDT.Text = row.Cells[2].Value.ToString();

                if (row.Cells[3].Value != null)
                    txtTenKhachHang.Text = row.Cells[3].Value.ToString();

                try
                {
                    if (row.DataBoundItem is DAL.LichDat lichData)
                    {
                        if (lichData.DonGiaThucTe.HasValue)
                        {
                            txtDonGia.Text = lichData.DonGiaThucTe.Value.ToString("0.00");
                            Log($"✔ Đơn giá: {lichData.DonGiaThucTe.Value:0.00}");
                        }
                        else
                        {
                            txtDonGia.Text = "0.00";
                            Log("⚠ Đơn giá = NULL");
                        }
                    }
                    else
                    {
                        if (row.Cells[8].Value != null && row.Cells[8].Value != DBNull.Value)
                        {
                            string giaStr = row.Cells[8].Value.ToString().Trim();
                            if (!string.IsNullOrWhiteSpace(giaStr) && decimal.TryParse(giaStr, out decimal gia))
                            {
                                txtDonGia.Text = gia.ToString("0.00");
                            }
                            else
                            {
                                txtDonGia.Text = "0.00";
                            }
                        }
                        else
                        {
                            txtDonGia.Text = "0.00";
                        }
                    }
                }
                catch (Exception ex)
                {
                    Log($"⚠ Lỗi chuyển đổi DonGia: {ex.Message}");
                    txtDonGia.Text = "0.00";
                }

                // ===== SỬA: TẠM THỜI BỎ GIỚ HẠN MINDATE ĐỂ LẤY NGÀY CŨ =====
                if (row.Cells[4].Value != null)
                {
                    DateTime ngayDat = Convert.ToDateTime(row.Cells[4].Value);
                    
                    // Tạm thời cho phép chọn ngày trong quá khứ
                    dtpNgayDat.MinDate = new DateTime(1900, 1, 1);
                    dtpNgayDat.MaxDate = DateTime.Now.AddDays(7);
                    dtpNgayDat.Value = ngayDat;
                    
                    Log($"✔ Ngày đặt: {ngayDat:dd/MM/yyyy}");
                }

                if (row.Cells[5].Value != null)
                {
                    int gioBD = Convert.ToInt32(row.Cells[5].Value);
                    dtpGioBatDau.Value = DateTime.Today.AddHours(gioBD);
                    Log($"✔ Giờ bắt đầu: {gioBD}:00");
                }

                if (row.Cells[6].Value != null)
                {
                    int gioKT = Convert.ToInt32(row.Cells[6].Value);
                    dtpGioKetThuc.Value = DateTime.Today.AddHours(gioKT);
                    Log($"✔ Giờ kết thúc: {gioKT}:00");
                }

                isEditing = true;
                Log($"► Chỉnh sửa lịch: {txtMaDat.Text}");
            }
            catch (Exception ex)
            {
                Log($"⚠ Lỗi lấy dữ liệu: {ex.Message}");
            }
        }

        private void btnDatSAn_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtSDT.Text) || string.IsNullOrWhiteSpace(cbxMaSan.Text))
            {
                MessageBox.Show("Nhập thiếu thông tin!");
                return;
            }

            // ===== KIỂM TRA NGÀY ĐẶT =====
            DateTime ngayDat = dtpNgayDat.Value.Date;
            DateTime homNay = DateTime.Now.Date;

            if (ngayDat < homNay)
            {
                MessageBox.Show("❌ Không được phép đặt ngày trong quá khứ!\nVui lòng chọn ngày từ hôm nay trở đi.", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                dtpNgayDat.Focus();
                return;
            }

            int gioBD = dtpGioBatDau.Value.Hour;
            int gioKT = dtpGioKetThuc.Value.Hour;

            if (gioBD >= gioKT)
            {
                MessageBox.Show("Giờ kết thúc phải lớn hơn giờ bắt đầu!");
                return;
            }

            // ===== KIỂM TRA GIỜ ĐẶT NẾU LÀ HÔM NAY =====
            if (ngayDat == homNay)
            {
                int gioHienTai = DateTime.Now.Hour;
                if (gioBD <= gioHienTai)
                {
                    MessageBox.Show($"❌ Không được phép đặt giờ trong quá khứ!\nGiờ hiện tại: {gioHienTai}:00\nVui lòng chọn giờ từ {gioHienTai + 1}:00 trở đi.", 
                        "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    dtpGioBatDau.Focus();
                    return;
                }
            }

            DAL.LichDat lich = new DAL.LichDat
            {
                MaLich = txtMaDat.Text.Trim(),
                MaSan = cbxMaSan.Text.Trim(),
                SDT_KH = txtSDT.Text.Trim(),
                TenKH = txtTenKhachHang.Text.Trim(),
                NgayDat = ngayDat,
                GioBD = gioBD,
                GioKT = gioKT,
                TrangThai = "Đã đặt",
                DonGiaThucTe = decimal.TryParse(txtDonGia.Text, out decimal gia) ? gia : 0
            };

            Log("===== DATA GỬI XUỐNG DB =====");
            Log($"MaLich: {lich.MaLich}");
            Log($"MaSan: {lich.MaSan}");
            Log($"SDT: {lich.SDT_KH}");
            Log($"TenKH: {lich.TenKH}");
            Log($"NgayDat: {lich.NgayDat:dd/MM/yyyy}");
            Log($"GioBD: {lich.GioBD}");
            Log($"GioKT: {lich.GioKT}");
            Log($"DonGia: {lich.DonGiaThucTe}");
            Log("=============================");

            try
            {
                Log("▶ Bắt đầu Insert lịch đặt");

                if (busLichDat.Insert(lich))
                {
                    Log("✔ Insert thành công");
                    RefreshData();
                    ResetForm();
                    isEditing = false;
                }
            }
            catch (Exception ex)
            {
                Log("❌ LỖI: " + ex.Message);
            }
        }

        private void Log(string message)
        {
            rtbLog.AppendText($"[{DateTime.Now:HH:mm:ss}] {message}\n");
            rtbLog.ScrollToCaret();
        }

        private void btnQuayLai_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ResetForm()
        {
            txtMaDat.Clear();
            txtTenKhachHang.Clear();
            txtSDT.Clear();
            cbxMaSan.SelectedIndex = -1;
            txtDonGia.Text = "";
            isEditing = false;
        }

        private void btnHuySan_Click(object sender, EventArgs e)
        {
            if (dgvDatSan.CurrentRow == null)
            {
                MessageBox.Show("Vui lòng chọn lịch cần hủy từ danh sách!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string maLich = dgvDatSan.CurrentRow.Cells[0].Value?.ToString();

            if (string.IsNullOrWhiteSpace(maLich))
            {
                MessageBox.Show("Mã lịch không hợp lệ!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult result = MessageBox.Show(
                $"Bạn có chắc chắn muốn hủy lịch đặt [{maLich}] không?",
                "Xác nhận hủy",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                try
                {
                    if (busLichDat.HuyDat(maLich))
                    {
                        Log($"✔ Đã hủy lịch: {maLich}");
                        MessageBox.Show("Hủy sân thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        RefreshDataWithSearch();
                        ResetForm();
                    }
                }
                catch (Exception ex)
                {
                    Log($"❌ Lỗi hủy: {ex.Message}");
                    MessageBox.Show(ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtMaDat.Text))
            {
                MessageBox.Show("Vui lòng chọn lịch cần sửa từ danh sách!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(txtSDT.Text) || string.IsNullOrWhiteSpace(cbxMaSan.Text))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin (Sân, SĐT)!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int gioBD = dtpGioBatDau.Value.Hour;
            int gioKT = dtpGioKetThuc.Value.Hour;

            if (gioBD >= gioKT)
            {
                MessageBox.Show("Giờ kết thúc phải lớn hơn giờ bắt đầu!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // ===== KIỂM TRA NGÀY GIỜ TRONG QUÁ KHỨ KHI SỬA =====
            DateTime ngayDat = dtpNgayDat.Value.Date;
            DateTime homNay = DateTime.Now.Date;

            if (ngayDat < homNay)
            {
                MessageBox.Show("❌ Không được sửa lịch trong quá khứ!\nVui lòng chọn ngày từ hôm nay trở đi.", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                
                // Khôi phục giới hạn ngày
                dtpNgayDat.MinDate = DateTime.Now.Date;
                dtpNgayDat.MaxDate = DateTime.Now.AddDays(7);
                dtpNgayDat.Focus();
                return;
            }

            if (ngayDat == homNay)
            {
                int gioHienTai = DateTime.Now.Hour;
                if (gioBD <= gioHienTai)
                {
                    MessageBox.Show($"❌ Không được sửa giờ trong quá khứ!\nGiờ hiện tại: {gioHienTai}:00\nVui lòng chọn giờ từ {gioHienTai + 1}:00 trở đi.", 
                        "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    
                    // Khôi phục giới hạn ngày
                    dtpNgayDat.MinDate = DateTime.Now.Date;
                    dtpNgayDat.MaxDate = DateTime.Now.AddDays(7);
                    dtpGioBatDau.Focus();
                    return;
                }
            }

            DAL.LichDat lichMoi = new DAL.LichDat
            {
                MaLich = txtMaDat.Text.Trim(),
                MaSan = cbxMaSan.Text.Trim(),
                SDT_KH = txtSDT.Text.Trim(),
                TenKH = txtTenKhachHang.Text.Trim(),
                NgayDat = ngayDat,
                GioBD = gioBD,
                GioKT = gioKT,
                DonGiaThucTe = decimal.TryParse(txtDonGia.Text, out decimal gia) ? gia : 0
            };

            DialogResult dr = MessageBox.Show($"Bạn có chắc muốn cập nhật thông tin cho mã [{lichMoi.MaLich}]?",
                                              "Xác nhận sửa", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (dr == DialogResult.Yes)
            {
                try
                {
                    Log("▶ Bắt đầu cập nhật lịch đặt");

                    if (busLichDat.Update(lichMoi))
                    {
                        Log($"✎ Đã cập nhật lịch: {lichMoi.MaLich}");
                        MessageBox.Show("Cập nhật thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        
                        // ===== KHÔI PHỤC GIỚI HẠN NGÀY SAU KHI SỬA THÀNH CÔNG =====
                        dtpNgayDat.MinDate = DateTime.Now.Date;
                        dtpNgayDat.MaxDate = DateTime.Now.AddDays(7);
                        
                        RefreshDataWithSearch();
                        ResetForm();
                    }
                    else
                    {
                        Log($"❌ Cập nhật thất bại");
                        MessageBox.Show("Cập nhật thất bại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        
                        // ===== KHÔI PHỤC GIỚI HẠN NGÀY KHI CÓ LỖI =====
                        dtpNgayDat.MinDate = DateTime.Now.Date;
                        dtpNgayDat.MaxDate = DateTime.Now.AddDays(7);
                    }
                }
                catch (Exception ex)
                {
                    Log($"❌ Lỗi sửa: {ex.Message}");
                    MessageBox.Show(ex.Message, "Lỗi cập nhật", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    
                    // ===== KHÔI PHỤC GIỚI HẠN NGÀY KHI CÓ LỖI =====
                    dtpNgayDat.MinDate = DateTime.Now.Date;
                    dtpNgayDat.MaxDate = DateTime.Now.AddDays(7);
                }
            }
            else
            {
                // ===== KHÔI PHỤC GIỚI HẠN NGÀY NẾU NGƯỜI DÙNG CANCEL =====
                dtpNgayDat.MinDate = DateTime.Now.Date;
                dtpNgayDat.MaxDate = DateTime.Now.AddDays(7);
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (dgvDatSan.CurrentRow == null)
            {
                MessageBox.Show("Vui lòng chọn lịch cần xóa từ danh sách!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string maLich = dgvDatSan.CurrentRow.Cells[0].Value?.ToString();

            if (string.IsNullOrWhiteSpace(maLich))
            {
                MessageBox.Show("Mã lịch không hợp lệ!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult result = MessageBox.Show(
                $"Bạn có chắc chắn muốn xóa lịch đặt [{maLich}] không?\n\n⚠️ Hành động này không thể hoàn tác!",
                "Xác nhận xóa",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                try
                {
                    Log("▶ Bắt đầu xóa lịch đặt");

                    if (busLichDat.Delete(maLich))
                    {
                        Log($"✔ Đã xóa lịch: {maLich}");
                        MessageBox.Show("Xóa lịch thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        RefreshDataWithSearch();
                        ResetForm();
                    }
                }
                catch (Exception ex)
                {
                    Log($"❌ Lỗi xóa: {ex.Message}");
                    MessageBox.Show(ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            string keyword = txtTimKiem.Text.Trim();

            if (string.IsNullOrWhiteSpace(keyword))
            {
                MessageBox.Show("Vui lòng nhập từ khóa tìm kiếm!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTimKiem.Focus();
                return;
            }

            try
            {
                Log($"▶ Bắt đầu tìm kiếm: '{keyword}'");
                var results = busLichDat.Search(keyword);

                if (results.Count == 0)
                {
                    Log($"⚠ Không tìm thấy kết quả cho: '{keyword}'");
                    MessageBox.Show($"Không tìm thấy lịch đặt nào phù hợp với '{keyword}'", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    dgvDatSan.DataSource = null;
                }
                else
                {
                    Log($"✔ Tìm thấy {results.Count} kết quả");
                    dgvDatSan.DataSource = null;
                    dgvDatSan.DataSource = results;
                    ReapplyColumnBindings();
                    FormatDonGiaColumn();  // ===== THÊM DÒNG NÀY =====
                    MessageBox.Show($"Tìm thấy {results.Count} kết quả! Nhấn vào hàng để chọn.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                Log($"❌ Lỗi tìm kiếm: {ex.Message}");
                MessageBox.Show($"Lỗi tìm kiếm: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void RefreshDataWithSearch()
        {
            string keyword = txtTimKiem.Text.Trim();

            if (string.IsNullOrWhiteSpace(keyword))
            {
                RefreshData();
            }
            else
            {
                try
                {
                    Log($"▶ Cập nhật kết quả tìm kiếm: '{keyword}'");
                    var results = busLichDat.Search(keyword);

                    dgvDatSan.DataSource = null;
                    dgvDatSan.DataSource = results;
                    ReapplyColumnBindings();
                    FormatDonGiaColumn();  // ===== THÊM DÒNG NÀY =====

                    if (results.Count == 0)
                    {
                        Log($"⚠ Không còn kết quả nào cho: '{keyword}'");
                    }
                    else
                    {
                        Log($"✔ Cập nhật {results.Count} kết quả");
                    }
                }
                catch (Exception ex)
                {
                    Log($"❌ Lỗi cập nhật kết quả tìm kiếm: {ex.Message}");
                }
            }
        }

        private void ResetSearch()
        {
            txtTimKiem.Clear();
            RefreshData();
            Log("◀ Đã reset tìm kiếm - Hiển thị tất cả dữ liệu");
        }

        private void btnResetTimKiem_Click(object sender, EventArgs e)
        {
            ResetSearch();
        }

        private void btnTaiLai_Click(object sender, EventArgs e)
        {
            try
            {
                Log("▶ Bắt đầu tải lại dữ liệu");
                
                txtTimKiem.Clear();
                ResetForm();
                RefreshData();
                
                Log("✔ Tải lại dữ liệu thành công");
                MessageBox.Show("Đã tải lại dữ liệu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                Log($"❌ Lỗi tải lại: {ex.Message}");
                MessageBox.Show($"Lỗi tải lại: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}