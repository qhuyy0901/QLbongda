using BUS;
using DAL;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using TrangChu;

namespace TrangChu
{
    public partial class LichDat : Form
    {
        private LichDatBUS busLichDat = new LichDatBUS();
        private SanBongBUS busSanBong = new SanBongBUS();
        private bool isEditing = false;

        // ===== BIẾN LƯU TRỮ TRẠNG THÁI SẮP XẾP =====
        private string sortedColumn = "";
        private SortOrder sortOrder = SortOrder.Ascending;

        // ✅ BIẾN HỖ TRỢ TÌM KIẾM REAL-TIME (THROTTLE)
        private System.Timers.Timer searchTimer = null;
        private string lastSearchKeyword = "";

        public LichDat()
        {
            InitializeComponent();
            this.Load += LichDat_Load;
        }

        private void LichDat_Load(object sender, EventArgs e)
        {
            try
            {
                InitializeSearchTimer(); // ✅ Khởi tạo Timer tìm kiếm

                dgvDatSan.CellClick += dgvDatSan_CellClick;
                dgvDatSan.CellContentClick += dgvDatSan_CellContentClick;

                SetupFieldButtonEvents();
                LoadComboBoxSan();
                RefreshData();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
        }

        // =================================================================================
        // ✅ PHẦN LOGIC TÌM KIẾM REAL-TIME (QUAN TRỌNG)
        // =================================================================================

        // 1. Khởi tạo Timer
        private void InitializeSearchTimer()
        {
            if (searchTimer == null)
            {
                // Delay 300ms: Đủ nhanh để thấy mượt, đủ chậm để không spam database
                searchTimer = new System.Timers.Timer(300);
                searchTimer.Elapsed += SearchTimer_Elapsed;
                searchTimer.AutoReset = false; // Chỉ chạy 1 lần sau khi dừng gõ
            }
        }

        // 2. Sự kiện khi gõ phím (TxtTimKiem_TextChanged)
        private void TxtTimKiem_TextChanged(object sender, EventArgs e)
        {
            try
            {
                string keyword = txtTimKiem.Text.Trim();

                // Nếu không nhập gì → load lại toàn bộ
                if (string.IsNullOrEmpty(keyword))
                {
                    RefreshData();
                    return;
                }

                // Gọi BUS tìm kiếm
                var result = busLichDat.Search(keyword);

                dgvDatSan.DataSource = null;
                dgvDatSan.DataSource = result;

                ReapplyColumnBindings();
                FormatDonGiaColumn();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tìm kiếm: " + ex.Message);
            }
        }


        // 3. Sự kiện khi Timer kết thúc đếm (Chạy trên luồng phụ)
        private void SearchTimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            try
            {
                // Invoke để quay lại luồng giao diện (UI Thread) cập nhật DataGridView
                if (!this.IsDisposed && !this.Disposing)
                {
                    this.Invoke(new Action(() =>
                    {
                        if (!this.IsDisposed)
                            PerformSearch(lastSearchKeyword);
                    }));
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"❌ Lỗi SearchTimer_Elapsed: {ex.Message}");
            }
        }

        // 4. Hàm thực hiện tìm kiếm và cập nhật Grid (CÓ XỬ LÝ LỖI TOÀN DIỆN)
        private void PerformSearch(string keyword)
        {
            try
            {
                // Đảm bảo keyword không null
                if (string.IsNullOrWhiteSpace(keyword))
                {
                    RefreshData();
                    return;
                }

                var results = busLichDat.Search(keyword);

                // Cập nhật DataGridView
                dgvDatSan.DataSource = null; // Clear trước để tránh lỗi binding
                
                if (results != null && results.Count > 0)
                {
                    dgvDatSan.DataSource = results;
                }
                else
                {
                    // Nếu không tìm thấy, gán list rỗng để hiển thị trống
                    dgvDatSan.DataSource = new List<DAL.LichDat>();
                }

                // Reapply binding và formatting
                ReapplyColumnBindings();
                FormatDonGiaColumn();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"❌ Lỗi PerformSearch: {ex.Message}");
                MessageBox.Show($"Lỗi tìm kiếm: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            // Vì đã tìm kiếm real-time, nút này chỉ cần focus vào ô nhập
            txtTimKiem.Focus();
        }

        // =================================================================================
        // END PHẦN TÌM KIẾM
        // =================================================================================

        private void dgvDatSan_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            try
            {
                DataGridViewRow row = dgvDatSan.Rows[e.RowIndex];

                if (row.Cells[1].Value != null)
                {
                    cbxMaSan.Text = row.Cells[1].Value.ToString();
                    cbxMaSan.SelectedValue = row.Cells[1].Value.ToString();
                }

                if (row.Cells[2].Value != null)
                    txtSDT.Text = row.Cells[2].Value.ToString();

                if (row.Cells[3].Value != null)
                    txtTenKhachHang.Text = row.Cells[3].Value.ToString();

                // Tính toán và hiển thị đơn giá
                try
                {
                    int gioBD = 0;
                    int gioKT = 0;
                    decimal giaThucTe = 0;

                    if (row.Cells[5].Value != null) gioBD = Convert.ToInt32(row.Cells[5].Value);
                    if (row.Cells[6].Value != null) gioKT = Convert.ToInt32(row.Cells[6].Value);

                    if (row.DataBoundItem is DAL.LichDat lichData && lichData.DonGiaThucTe.HasValue)
                    {
                        giaThucTe = lichData.DonGiaThucTe.Value;
                    }
                    else if (row.Cells[8].Value != null && row.Cells[8].Value != DBNull.Value)
                    {
                        decimal.TryParse(row.Cells[8].Value.ToString(), out giaThucTe);
                    }

                    int soGio = gioKT - gioBD;
                    if (soGio > 0 && giaThucTe > 0)
                    {
                        decimal donGiaHangGio = giaThucTe / soGio;
                        txtDonGia.Text = donGiaHangGio.ToString("0.00");
                    }
                    else if (giaThucTe > 0)
                    {
                        txtDonGia.Text = giaThucTe.ToString("0.00");
                    }
                    else
                    {
                        txtDonGia.Text = "0.00";
                    }
                }
                catch
                {
                    txtDonGia.Text = "0.00";
                }

                if (row.Cells[4].Value != null)
                {
                    DateTime ngayDat = Convert.ToDateTime(row.Cells[4].Value);
                    dtpNgayDat.MinDate = new DateTime(1900, 1, 1);
                    dtpNgayDat.MaxDate = DateTime.Now.AddDays(365);
                    dtpNgayDat.Value = ngayDat;
                }

                if (row.Cells[5].Value != null)
                {
                    int gioBD = Convert.ToInt32(row.Cells[5].Value);
                    dtpGioBatDau.Value = DateTime.Today.AddHours(gioBD);
                }

                if (row.Cells[6].Value != null)
                {
                    int gioKT = Convert.ToInt32(row.Cells[6].Value);
                    dtpGioKetThuc.Value = DateTime.Today.AddHours(gioKT);
                }

                isEditing = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi lấy dữ liệu: {ex.Message}");
            }
        }

        private void SortDataGridView(string columnName, SortOrder sortOrder)
        {
            try
            {
                if (dgvDatSan.DataSource == null) return;

                List<DAL.LichDat> currentData = null;

                if (dgvDatSan.DataSource is BindingSource bs)
                    currentData = (List<DAL.LichDat>)bs.DataSource;
                else if (dgvDatSan.DataSource is List<DAL.LichDat> list)
                    currentData = list;

                if (currentData == null || currentData.Count == 0) return;

                if (sortOrder == SortOrder.Ascending)
                    currentData = currentData.OrderBy(x => GetPropertyValue(x, columnName)).ToList();
                else
                    currentData = currentData.OrderByDescending(x => GetPropertyValue(x, columnName)).ToList();

                dgvDatSan.DataSource = null;
                dgvDatSan.DataSource = currentData;
                ReapplyColumnBindings();
                FormatDonGiaColumn();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi sắp xếp: {ex.Message}");
            }
        }

        private object GetPropertyValue(DAL.LichDat item, string propertyName)
        {
            try
            {
                var property = item.GetType().GetProperty(propertyName);
                return property?.GetValue(item) ?? "";
            }
            catch
            {
                return "";
            }
        }

        private void ShowSortIndicator(int columnIndex, SortOrder sortOrder)
        {
            try
            {
                foreach (DataGridViewColumn col in dgvDatSan.Columns)
                {
                    col.HeaderText = col.HeaderText.Replace(" ▲", "").Replace(" ▼", "");
                }
                string sortSymbol = sortOrder == SortOrder.Ascending ? " ▲" : " ▼";
                dgvDatSan.Columns[columnIndex].HeaderText += sortSymbol;
            }
            catch { }
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
                FormatDonGiaColumn();

                sortedColumn = "";
                sortOrder = SortOrder.Ascending;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi làm mới dữ liệu: {ex.Message}");
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

        private bool IsValidPhoneNumber(string phoneNumber)
        {
            if (string.IsNullOrWhiteSpace(phoneNumber))
            {
                MessageBox.Show("❌ Vui lòng nhập số điện thoại!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtSDT.Focus();
                return false;
            }
            string cleanPhone = phoneNumber.Trim();
            if (!System.Text.RegularExpressions.Regex.IsMatch(cleanPhone, @"^\d+$"))
            {
                MessageBox.Show("❌ Số điện thoại chỉ được chứa chữ số!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtSDT.Focus();
                return false;
            }
            if (cleanPhone.Length != 10)
            {
                MessageBox.Show($"❌ Số điện thoại phải có đúng 10 chữ số!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtSDT.Focus();
                return false;
            }
            return true;
        }

        private bool IsValidPrice(string priceText)
        {
            if (string.IsNullOrWhiteSpace(priceText))
            {
                MessageBox.Show("❌ Vui lòng nhập đơn giá!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtDonGia.Focus();
                return false;
            }
            if (!decimal.TryParse(priceText.Trim(), out decimal price) || price <= 0)
            {
                MessageBox.Show("❌ Đơn giá không hợp lệ!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtDonGia.Focus();
                return false;
            }
            return true;
        }

        private bool IsValidSanCode(string maSan)
        {
            if (string.IsNullOrWhiteSpace(maSan)) return false;
            string[] validSans = { "San1", "San2", "San3", "San4", "San5", "San6" };
            if (!validSans.Contains(maSan.Trim()))
            {
                MessageBox.Show($"❌ Mã sân '{maSan}' không tồn tại!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cbxMaSan.Focus();
                return false;
            }
            return true;
        }

        private void btnDatSAn_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtTenKhachHang.Text))
            {
                MessageBox.Show("❌ Vui lòng nhập tên khách hàng!", "Cảnh báo");
                txtTenKhachHang.Focus();
                return;
            }
            if (!IsValidPhoneNumber(txtSDT.Text)) return;
            if (!IsValidSanCode(cbxMaSan.Text)) return;
            if (!IsValidPrice(txtDonGia.Text)) return;

            DateTime ngayDat = dtpNgayDat.Value.Date;
            if (ngayDat < DateTime.Now.Date)
            {
                MessageBox.Show("❌ Không được đặt ngày trong quá khứ!", "Cảnh báo");
                return;
            }

            int gioBD = dtpGioBatDau.Value.Hour;
            int gioKT = dtpGioKetThuc.Value.Hour;
            if (gioBD >= gioKT)
            {
                MessageBox.Show("❌ Giờ kết thúc phải lớn hơn giờ bắt đầu!", "Cảnh báo");
                return;
            }

            int soGio = gioKT - gioBD;
            decimal donGiaHangGio = decimal.Parse(txtDonGia.Text.Trim());
            decimal giaThucTe = soGio * donGiaHangGio;

            DAL.LichDat lich = new DAL.LichDat
            {
                MaLich = null,
                MaSan = cbxMaSan.Text.Trim(),
                SDT_KH = txtSDT.Text.Trim(),
                TenKH = txtTenKhachHang.Text.Trim(),
                NgayDat = ngayDat,
                GioBD = gioBD,
                GioKT = gioKT,
                TrangThai = "Đã đặt",
                DonGiaThucTe = giaThucTe
            };

            try
            {
                if (busLichDat.Insert(lich))
                {
                    MessageBox.Show("✔ Đặt sân thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    RefreshData();
                    ResetForm();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("❌ Lỗi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnResetTimKiem_Click(object sender, EventArgs e)
        {
            ResetSearch();
        }

        private void ResetSearch()
        {
            txtTimKiem.Clear();
            RefreshData();
        }

        private void btnTaiLai_Click(object sender, EventArgs e)
        {
            txtTimKiem.Clear();
            ResetForm();
            RefreshData();
            MessageBox.Show("Đã tải lại dữ liệu!", "Thông báo");
        }

        private void btnQuayLai_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ResetForm()
        {
            txtTenKhachHang.Clear();
            txtSDT.Clear();
            cbxMaSan.SelectedIndex = -1;
            txtDonGia.Text = "";
            isEditing = false;
        }

        private void btnHuySan_Click(object sender, EventArgs e)
        {
            if (dgvDatSan.CurrentRow == null) return;
            string maLich = dgvDatSan.CurrentRow.Cells[0].Value?.ToString();

            if (MessageBox.Show($"Bạn có chắc muốn hủy lịch [{maLich}]?", "Xác nhận", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                if (busLichDat.HuyDat(maLich))
                {
                    MessageBox.Show("Hủy sân thành công!");
                    RefreshData();
                    ResetForm();
                }
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (dgvDatSan.SelectedRows.Count == 0) return;

            string maLich = dgvDatSan.CurrentRow.Cells[0].Value.ToString();

            // Logic kiểm tra ngày quá khứ
            DateTime ngayDat = dtpNgayDat.Value.Date;
            if (ngayDat < DateTime.Now.Date)
            {
                MessageBox.Show("❌ Không thể sửa lịch trong quá khứ!", "Cảnh báo");
                return;
            }

            int gioBD = dtpGioBatDau.Value.Hour;
            int gioKT = dtpGioKetThuc.Value.Hour;
            int soGio = gioKT - gioBD;
            decimal donGia = decimal.Parse(txtDonGia.Text);
            decimal tongGia = soGio * donGia;

            DAL.LichDat lichMoi = new DAL.LichDat
            {
                MaLich = maLich,
                MaSan = cbxMaSan.Text,
                SDT_KH = txtSDT.Text,
                TenKH = txtTenKhachHang.Text,
                NgayDat = ngayDat,
                GioBD = gioBD,
                GioKT = gioKT,
                TrangThai = "Đã đặt",
                DonGiaThucTe = tongGia
            };

            if (busLichDat.Update(lichMoi))
            {
                MessageBox.Show("✔ Cập nhật thành công!");
                RefreshData();
                ResetForm();
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (dgvDatSan.CurrentRow == null) return;
            string maLich = dgvDatSan.CurrentRow.Cells[0].Value?.ToString();

            if (MessageBox.Show($"Bạn có chắc chắn xóa lịch [{maLich}]? Không thể hoàn tác!", "Xác nhận", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                if (busLichDat.Delete(maLich))
                {
                    MessageBox.Show("Xóa thành công!");
                    RefreshData();
                    ResetForm();
                }
            }
        }

        private string GetFieldInfoForToday(string maSan)
        {
            try
            {
                DateTime today = DateTime.Now.Date;
                var sanInfo = busSanBong.GetListSanBong()?.FirstOrDefault(s => s.MaSan == maSan);
                string tenSan = sanInfo?.TenSan ?? maSan;

                var todayBookings = busLichDat.GetAll()
                    ?.Where(l => l.MaSan == maSan && l.NgayDat == today && l.TrangThai == "Đã đặt")
                    .OrderBy(l => l.GioBD).ToList();

                string info = $"SÂN {maSan} - {tenSan}\nNgày: {today:dd/MM/yyyy}\nLượt đặt: {todayBookings?.Count ?? 0}\n";
                if (todayBookings != null)
                {
                    foreach (var b in todayBookings)
                        info += $"• {b.GioBD}h - {b.GioKT}h: {b.TenKH}\n";
                }
                return info;
            }
            catch { return "Lỗi lấy thông tin"; }
        }

        private string GetMaSanFromButton(Button btn)
        {
            if (btn == null) return "";
            if (btn.Name == "btnSan1") return "San1";
            if (btn.Name == "btnSan2") return "San2";
            if (btn.Name == "btnSan3") return "San3";
            if (btn.Name == "btnSan4") return "San4";
            if (btn.Name == "btnSan5") return "San5";
            if (btn.Name == "btnSan6") return "San6";
            return "";
        }

        private void SetupFieldButtonEvents()
        {
            Button[] btns = { btnSan1, btnSan2, btnSan3, btnSan4, btnSan5, btnSan6 };
            foreach (Button btn in btns)
            {
                btn.MouseEnter += FieldButton_MouseEnter;
                btn.Click += FieldButton_Click;
            }
        }

        private void FieldButton_MouseEnter(object sender, EventArgs e)
        {
            if (sender is Button btn)
            {
                string maSan = GetMaSanFromButton(btn);
                if (!string.IsNullOrEmpty(maSan))
                {
                    ToolTip tt = new ToolTip();
                    tt.SetToolTip(btn, GetFieldInfoForToday(maSan));
                }
            }
        }

        private void FieldButton_Click(object sender, EventArgs e)
        {
            if (sender is Button btn)
            {
                string maSan = GetMaSanFromButton(btn);
                MessageBox.Show(GetFieldInfoForToday(maSan), "Thông tin sân");
            }
        }

        private void btnThanhToan_Click(object sender, EventArgs e)
        {
            if (dgvDatSan.SelectedRows.Count == 0) return;

            // Lấy thông tin từ row được chọn
            string maLich = dgvDatSan.CurrentRow.Cells[0].Value.ToString();
            string trangThai = dgvDatSan.CurrentRow.Cells[7].Value.ToString(); // Index 7 là cột Trạng Thái
            string tenKH = dgvDatSan.CurrentRow.Cells[3].Value.ToString();
            string sdtKH = dgvDatSan.CurrentRow.Cells[2].Value.ToString();

            if (trangThai != "Đã đặt")
            {
                MessageBox.Show("Chỉ thanh toán được lịch có trạng thái 'Đã đặt'!");
                return;
            }

            CT_HoaDon_DichVu frm = new CT_HoaDon_DichVu();
            frm.SetKhachHang(tenKH, sdtKH);
            frm.SetMaLich(maLich);
            this.Hide();
            frm.ShowDialog();
            this.Show();
            RefreshData();
        }

        private void btnThemDivhVu_Click(object sender, EventArgs e)
        {
            if (dgvDatSan.SelectedRows.Count == 0) return;

            string maLich = dgvDatSan.CurrentRow.Cells[0].Value.ToString();
            string tenKH = dgvDatSan.CurrentRow.Cells[3].Value.ToString();
            string sdtKH = dgvDatSan.CurrentRow.Cells[2].Value.ToString();

            DichVu frm = new DichVu();
            frm.SetDefaultCustomer(tenKH, sdtKH, maLich);
            this.Hide();
            frm.ShowDialog();
            this.Show();
        }

        private void dgvDatSan_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1) // Click Header để sắp xếp
            {
                string colName = dgvDatSan.Columns[e.ColumnIndex].Name;
                if (sortedColumn == colName)
                    sortOrder = sortOrder == SortOrder.Ascending ? SortOrder.Descending : SortOrder.Ascending;
                else
                {
                    sortedColumn = colName;
                    sortOrder = SortOrder.Ascending;
                }
                SortDataGridView(colName, sortOrder);
                ShowSortIndicator(e.ColumnIndex, sortOrder);
            }
        }
    }
}