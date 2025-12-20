using BUS;
using DAL;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

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

                dtpNgayDat.MinDate = DateTime.Now.Date;
                dtpNgayDat.MaxDate = DateTime.Now.AddDays(30);

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

                // ===== THÊM SỰ KIỆN SẮP XẾP THEO HEADER =====
                dgvDatSan.ColumnHeaderMouseClick += DgvDatSan_ColumnHeaderMouseClick;

                // ===== NGĂN CHẶN CHỈNH SỬA TRỰC TIẾP TRÊN DATAGRIDVIEW =====
                dgvDatSan.ReadOnly = true;
                dgvDatSan.AllowUserToAddRows = false;
                dgvDatSan.AllowUserToDeleteRows = false;
                dgvDatSan.AllowUserToResizeRows = false;
                dgvDatSan.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

                // ===== THIẾT LẬP SỰ KIỆN CHO CÁC NÚT SÂN =====
                SetupFieldButtonEvents();

                LoadComboBoxSan();
                RefreshData();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
        }

        // ===== SỰ KIỆN SẮP XẾP KHI CLICK VÀO HEADER CỘT =====
        private void DgvDatSan_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                string columnName = dgvDatSan.Columns[e.ColumnIndex].Name;

                // ===== NẾU CLICK CÙNG CỘT LẦN THỨ 2, ĐẢO CHIỀU SẮP XẾP =====
                if (sortedColumn == columnName)
                {
                    sortOrder = sortOrder == SortOrder.Ascending ? SortOrder.Descending : SortOrder.Ascending;
                }
                else
                {
                    sortedColumn = columnName;
                    sortOrder = SortOrder.Ascending;
                }

                // ===== SẮP XẾP DỮ LIỆU =====
                SortDataGridView(columnName, sortOrder);

                // ===== HIỂN THỊ DẤU TAM GIÁC =====
                ShowSortIndicator(e.ColumnIndex, sortOrder);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi sắp xếp: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ===== METHOD SẮP XẾP DATAGRIDVIEW =====
        private void SortDataGridView(string columnName, SortOrder sortOrder)
        {
            try
            {
                if (dgvDatSan.DataSource == null)
                    return;

                // ===== LẤY DANH SÁCH HIỆN TẠI =====
                List<DAL.LichDat> currentData = null;

                // ===== KIỂM TRA NẾU LÀ BINDING SOURCE =====
                if (dgvDatSan.DataSource is BindingSource bs)
                {
                    currentData = (List<DAL.LichDat>)bs.DataSource;
                }
                else if (dgvDatSan.DataSource is List<DAL.LichDat> list)
                {
                    currentData = list;
                }

                if (currentData == null || currentData.Count == 0)
                    return;

                // ===== SẮP XẾP DANH SÁCH DỰA TRÊN REFLECTION =====
                if (sortOrder == SortOrder.Ascending)
                {
                    currentData = currentData
                        .OrderBy(x => GetPropertyValue(x, columnName))
                        .ToList();
                }
                else
                {
                    currentData = currentData
                        .OrderByDescending(x => GetPropertyValue(x, columnName))
                        .ToList();
                }

                // ===== CẬP NHẬT DATAGRIDVIEW =====
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

        // ===== HELPER METHOD LẤY GIÁ TRỊ PROPERTY =====
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

        // ===== HIỂN THỊ DẤU TAM GIÁC (SORT INDICATOR) =====
        private void ShowSortIndicator(int columnIndex, SortOrder sortOrder)
        {
            try
            {
                // ===== XÓA DẤU TẤT CẢ CỘT CŨ =====
                foreach (DataGridViewColumn col in dgvDatSan.Columns)
                {
                    col.HeaderText = col.HeaderText.Replace(" ▲", "").Replace(" ▼", "");
                }

                // ===== THÊM DẤU TAM GIÁC VÀO CỘT ĐƯỢC CHỌN =====
                string sortSymbol = sortOrder == SortOrder.Ascending ? " ▲" : " ▼";
                dgvDatSan.Columns[columnIndex].HeaderText += sortSymbol;
            }
            catch { }
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

                FormatDonGiaColumn();

                // ===== RESET TRẠNG THÁI SẮP XẾP =====
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

        private void dgvDatSan_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            try
            {
                DataGridViewRow row = dgvDatSan.Rows[e.RowIndex];

                if (row.Cells[0].Value != null)
                {
                }

                if (row.Cells[1].Value != null)
                {
                    cbxMaSan.Text = row.Cells[1].Value.ToString();
                    cbxMaSan.SelectedValue = row.Cells[1].Value.ToString();
                }

                if (row.Cells[2].Value != null)
                    txtSDT.Text = row.Cells[2].Value.ToString();

                if (row.Cells[3].Value != null)
                    txtTenKhachHang.Text = row.Cells[3].Value.ToString();

                // ===== TÍNH ĐƠN GIÁ/GIỜ GỐC =====
                try
                {
                    int gioBD = 0;
                    int gioKT = 0;
                    decimal giaThucTe = 0;

                    // Lấy giờ bắt đầu
                    if (row.Cells[5].Value != null)
                    {
                        gioBD = Convert.ToInt32(row.Cells[5].Value);
                    }

                    // Lấy giờ kết thúc
                    if (row.Cells[6].Value != null)
                    {
                        gioKT = Convert.ToInt32(row.Cells[6].Value);
                    }

                    // Lấy giá thực tế đã tính
                    if (row.DataBoundItem is DAL.LichDat lichData)
                    {
                        if (lichData.DonGiaThucTe.HasValue)
                        {
                            giaThucTe = lichData.DonGiaThucTe.Value;
                        }
                    }
                    else if (row.Cells[8].Value != null && row.Cells[8].Value != DBNull.Value)
                    {
                        if (decimal.TryParse(row.Cells[8].Value.ToString(), out decimal gia))
                        {
                            giaThucTe = gia;
                        }
                    }

                    // ===== TÍNH Đơn giá/giờ = Giá thực tế / Số giờ =====
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
                catch (Exception ex)
                {
                    MessageBox.Show($"Lỗi chuyển đổi giá: {ex.Message}");
                    txtDonGia.Text = "0.00";
                }

                if (row.Cells[4].Value != null)
                {
                    DateTime ngayDat = Convert.ToDateTime(row.Cells[4].Value);
                    dtpNgayDat.MinDate = new DateTime(1900, 1, 1);
                    dtpNgayDat.MaxDate = DateTime.Now.AddDays(30);
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

        // ===== VALIDATE SỐ ĐIỆN THOẠI =====
        private bool IsValidPhoneNumber(string phoneNumber)
        {
            if (string.IsNullOrWhiteSpace(phoneNumber))
            {
                MessageBox.Show("❌ Vui lòng nhập số điện thoại!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtSDT.Focus();
                return false;
            }

            string cleanPhone = phoneNumber.Trim();

            // Kiểm tra chỉ có chữ số
            if (!System.Text.RegularExpressions.Regex.IsMatch(cleanPhone, @"^\d+$"))
            {
                MessageBox.Show("❌ Số điện thoại chỉ được chứa chữ số!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtSDT.Focus();
                return false;
            }

            if (cleanPhone.Length != 10)
            {
                MessageBox.Show($"❌ Số điện thoại phải có đúng 10 chữ số!\nSố bạn nhập: {cleanPhone.Length} chữ số", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtSDT.Focus();
                return false;
            }

            return true;
        }

        // ===== VALIDATE GIÁ SÂN
        private bool IsValidPrice(string priceText)
        {
            if (string.IsNullOrWhiteSpace(priceText))
            {
                MessageBox.Show("❌ Vui lòng nhập đơn giá!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtDonGia.Focus();
                return false;
            }

            string cleanPrice = priceText.Trim();

            // ===== CHỈ CHO PHÉP CHỮ SỐ, DẤU PHẨY, DẤU CHẤM 
            if (!System.Text.RegularExpressions.Regex.IsMatch(cleanPrice, @"^[\d,.]+$"))
            {
                MessageBox.Show(
                    "❌ Đơn giá chỉ được phép chứa số!\n\n" +
                    "❌ Không được dùng chữ cái hoặc ký tự đặc biệt (trừ dấu phẩy/chấm).\n\n" +
                    "💡 Ví dụ: 100000 hoặc 100,000",
                    "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtDonGia.Focus();
                return false;
            }

            if (!decimal.TryParse(cleanPrice, out decimal price))
            {
                MessageBox.Show("❌ Đơn giá phải là số hợp lệ!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtDonGia.Focus();
                return false;
            }

            if (price < 0)
            {
                MessageBox.Show("❌ Đơn giá không được âm!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtDonGia.Focus();
                return false;
            }

            if (price == 0)
            {
                MessageBox.Show("❌ Đơn giá phải lớn hơn 0!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtDonGia.Focus();
                return false;
            }

            return true;
        }

        // ===== VALIDATE MÃ SÂN
        private bool IsValidSanCode(string maSan)
        {
            if (string.IsNullOrWhiteSpace(maSan))
            {
                MessageBox.Show("❌ Mã sân không được để trống!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cbxMaSan.Focus();
                return false;
            }

            // ===== DANH SÁCH MÃ SÂN HỢPLỆ (CÓ PHÂN BIỆT CHỮ HOA/THƯỜng) =====
            string[] validSans = { "San1", "San2", "San3", "San4", "San5", "San6" };

            string cleanMaSan = maSan.Trim();

            // ===== KIỂM TRA TRỰC TIẾP - CHỈ CHẤP NHẬN EXACT MATCH =====
            if (!validSans.Contains(cleanMaSan))
            {
                MessageBox.Show(
                    $"❌ Mã sân '{cleanMaSan}' không tồn tại!\n\n" +
                    "Mã sân hợp lệ: San1, San2, San3, San4, San5, San6\n\n" +
                    "💡 Lưu ý: Phải viết đúng chữ hoa/thường! (VD: San1, không phải san1 hay SAN1)",
                    "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cbxMaSan.Focus();
                return false;
            }

            return true;
        }

        private void btnDatSAn_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtTenKhachHang.Text))
            {
                MessageBox.Show("❌ Vui lòng nhập tên khách hàng!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTenKhachHang.Focus();
                return;
            }

            if (!IsValidPhoneNumber(txtSDT.Text))
            {
                return;
            }

            if (string.IsNullOrWhiteSpace(cbxMaSan.Text))
            {
                MessageBox.Show("❌ Vui lòng chọn sân!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cbxMaSan.Focus();
                return;
            }

            // ===== KIỂM TRA MÃ SÂN CÓ ĐÚNG ĐỊNH DẠNG =====
            if (!IsValidSanCode(cbxMaSan.Text))
            {
                return;
            }

            if (!IsValidPrice(txtDonGia.Text))
            {
                return;
            }

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
                MessageBox.Show("❌ Giờ kết thúc phải lớn hơn giờ bắt đầu!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

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

            // ===== TÍNH GIÁ SÂN =====
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
                    MessageBox.Show(
                        $"✔ Đặt sân thành công!\n" +
                        $"Mã lịch: {lich.MaLich}\n" +
                        $"Số giờ: {soGio}h\n" +
                        $"Đơn giá/giờ: {donGiaHangGio:N0} đ\n" +
                        $"Tổng giá: {giaThucTe:N0} đ",
                        "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    RefreshData();
                    ResetForm();
                    isEditing = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("❌ Lỗi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                var results = busLichDat.Search(keyword);

                if (results.Count == 0)
                {
                    MessageBox.Show($"Không tìm thấy lịch đặt nào phù hợp với '{keyword}'", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    dgvDatSan.DataSource = null;
                }
                else
                {
                    dgvDatSan.DataSource = null;
                    dgvDatSan.DataSource = results;
                    ReapplyColumnBindings();
                    FormatDonGiaColumn();
                    MessageBox.Show($"Tìm thấy {results.Count} kết quả!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
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
                    var results = busLichDat.Search(keyword);

                    dgvDatSan.DataSource = null;
                    dgvDatSan.DataSource = results;
                    ReapplyColumnBindings();
                    FormatDonGiaColumn();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Lỗi cập nhật kết quả tìm kiếm: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void ResetSearch()
        {
            txtTimKiem.Clear();
            RefreshData();
        }

        private void btnResetTimKiem_Click(object sender, EventArgs e)
        {
            ResetSearch();
        }

        private void btnTaiLai_Click(object sender, EventArgs e)
        {
            try
            {
                txtTimKiem.Clear();
                ResetForm();
                RefreshData();
                MessageBox.Show("Đã tải lại dữ liệu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi tải lại: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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
                        MessageBox.Show("Hủy sân thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        RefreshDataWithSearch();
                        ResetForm();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (dgvDatSan.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn lịch cần sửa từ danh sách!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string maLich = null;
            DAL.LichDat lichGoc = null;

            try
            {
                if (dgvDatSan.SelectedRows[0].DataBoundItem is DAL.LichDat lichData)
                {
                    maLich = lichData.MaLich;
                    lichGoc = lichData;
                }
                else if (dgvDatSan.SelectedRows[0].Cells[0].Value != null)
                {
                    maLich = dgvDatSan.SelectedRows[0].Cells[0].Value.ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"❌ Lỗi lấy mã lịch: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (string.IsNullOrWhiteSpace(maLich))
            {
                MessageBox.Show("Mã lịch không hợp lệ!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // ===== KIỂM TRA XEM LỊCH CÓ PHẢI TRONG QUÁ KHỨ KHÔNG =====
            DateTime homNay = DateTime.Now.Date;
            
            if (lichGoc == null)
            {
                // Nếu chưa lấy được từ DataBoundItem, lấy từ database
                var allLichs = busLichDat.GetAll();
                lichGoc = allLichs.FirstOrDefault(l => l.MaLich == maLich);
            }

            if (lichGoc != null && lichGoc.NgayDat.HasValue)
            {
                DateTime ngayDatGoc = lichGoc.NgayDat.Value;

                if (ngayDatGoc < homNay)
                {
                    MessageBox.Show(
                        $"❌ Không thể sửa lịch trong quá khứ!\n\n" +
                        $"Ngày đặt: {ngayDatGoc:dd/MM/yyyy}\n" +
                        $"Hôm nay: {homNay:dd/MM/yyyy}\n\n" +
                        $"💡 Bạn chỉ có thể:\n" +
                        $"   • Hủy lịch (trạng thái sẽ thành 'Đã hủy')\n" +
                        $"   • Xóa lịch (xóa vĩnh viễn)",
                        "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }

            if (string.IsNullOrWhiteSpace(txtTenKhachHang.Text))
            {
                MessageBox.Show("❌ Vui lòng nhập tên khách hàng!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTenKhachHang.Focus();
                return;
            }

            if (!IsValidPhoneNumber(txtSDT.Text))
            {
                return;
            }

            if (string.IsNullOrWhiteSpace(cbxMaSan.Text))
            {
                MessageBox.Show("❌ Vui lòng chọn sân!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cbxMaSan.Focus();
                return;
            }

            // ===== KIỂM TRA MÃ SÂN CÓ ĐÚNG ĐỊNH DẠNG =====
            if (!IsValidSanCode(cbxMaSan.Text))
            {
                return;
            }

            if (!IsValidPrice(txtDonGia.Text))
            {
                return;
            }

            int gioBD = dtpGioBatDau.Value.Hour;
            int gioKT = dtpGioKetThuc.Value.Hour;

            if (gioBD >= gioKT)
            {
                MessageBox.Show("❌ Giờ kết thúc phải lớn hơn giờ bắt đầu!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DateTime ngayDat = dtpNgayDat.Value.Date;

            if (ngayDat < homNay)
            {
                MessageBox.Show("❌ Không được sửa lịch trong quá khứ!\nVui lòng chọn ngày từ hôm nay trở đi.", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
                    dtpGioBatDau.Focus();
                    return;
                }
            }

            // ===== TÍNH GIÁ SÂN =====
            int soGio = gioKT - gioBD;
            decimal donGiaHangGio = decimal.Parse(txtDonGia.Text.Trim());
            decimal giaThucTe = soGio * donGiaHangGio;

            DAL.LichDat lichMoi = new DAL.LichDat
            {
                MaLich = maLich,
                MaSan = cbxMaSan.Text.Trim(),
                SDT_KH = txtSDT.Text.Trim(),
                TenKH = txtTenKhachHang.Text.Trim(),
                NgayDat = ngayDat,
                GioBD = gioBD,
                GioKT = gioKT,
                TrangThai = "Đã đặt",
                DonGiaThucTe = giaThucTe
            };

            DialogResult dr = MessageBox.Show(
                $"Bạn có chắc muốn cập nhật thông tin cho mã [{lichMoi.MaLich}]?\n\n" +
                $"Số giờ: {soGio}h\n" +
                $"Đơn giá/giờ: {donGiaHangGio:N0} đ\n" +
                $"Tổng giá: {giaThucTe:N0} đ",
                "Xác nhận sửa", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (dr == DialogResult.Yes)
            {
                try
                {
                    if (busLichDat.Update(lichMoi))
                    {
                        MessageBox.Show("✔ Cập nhật thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        RefreshDataWithSearch();
                        ResetForm();
                    }
                    else
                    {
                        MessageBox.Show("❌ Cập nhật thất bại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"❌ Lỗi cập nhật: {ex.Message}", "Lỗi cập nhật", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
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
                    if (busLichDat.Delete(maLich))
                    {
                        MessageBox.Show("Xóa lịch thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        RefreshDataWithSearch();
                        ResetForm();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        // ===== LẤY THÔNG TIN ĐẶT SÂN TRONG NGÀY =====
        private string GetFieldInfoForToday(string maSan)
        {
            try
            {
                DateTime today = DateTime.Now.Date;

                // Lấy thông tin sân (bao gồm loại sân)
                var sanInfo = busSanBong.GetListSanBong()
                    ?.FirstOrDefault(s => s.MaSan == maSan);

                string loaiSan = sanInfo?.LoaiSan ?? "Không xác định";
                string tenSan = sanInfo?.TenSan ?? maSan;

                // Lấy tất cả lịch đặt
                var allBookings = busLichDat.GetAll();

                // Lọc lịch đặt của sân này trong hôm nay
                var todayBookings = allBookings
                    ?.Where(l => l.MaSan == maSan && 
                                l.NgayDat == today && 
                                l.TrangThai == "Đã đặt")
                    .OrderBy(l => l.GioBD)
                    .ToList();

                int bookingCount = todayBookings?.Count ?? 0;

                string info = $"SÂN {maSan}\n";
                info += $"Tên sân: {tenSan}\n";
                info += $"Loại sân: {loaiSan}\n";
                info += $"Ngày: {today:dd/MM/yyyy}\n";
                info += $"─────────────────────\n";
                info += $"Tổng lượt đặt: {bookingCount}\n\n";

                if (bookingCount > 0)
                {
                    info += "Khung giờ đã đặt:\n";
                    foreach (var booking in todayBookings)
                    {
                        info += $"• {booking.GioBD}:00 - {booking.GioKT}:00\n";
                        info += $"  KH: {booking.TenKH} ({booking.SDT_KH})\n";
                    }
                }
                else
                {
                    info += "✓ Sân còn trống hôm nay!";
                }

                return info;
            }
            catch (Exception ex)
            {
                return $"Lỗi: {ex.Message}";
            }
        }

        // ===== LẤY MÃ SÂN TỪ NÚT =====
        private string GetMaSanFromButton(Button btn)
        {
            if (btn == null)
                return "";

            if (btn.Name == "btnSan1") return "San1";
            if (btn.Name == "btnSan2") return "San2";
            if (btn.Name == "btnSan3") return "San3";
            if (btn.Name == "btnSan4") return "San4";
            if (btn.Name == "btnSan5") return "San5";
            if (btn.Name == "btnSan6") return "San6";

            return "";
        }

        // ===== THIẾT LẬP SỰ KIỆN CHO CÁC NÚT SÂN =====
        private void SetupFieldButtonEvents()
        {
            Button[] fieldButtons = { btnSan1, btnSan2, btnSan3, btnSan4, btnSan5, btnSan6 };

            foreach (Button btn in fieldButtons)
            {
                btn.MouseEnter += FieldButton_MouseEnter;
                btn.Click += FieldButton_Click;
            }
        }

        // ===== SỰ KIỆN HOVER - HIỂN THỊ TOOLTIP =====
        private void FieldButton_MouseEnter(object sender, EventArgs e)
        {
            if (sender is Button btn)
            {
                string maSan = GetMaSanFromButton(btn);
                if (!string.IsNullOrWhiteSpace(maSan))
                {
                    string info = GetFieldInfoForToday(maSan);
                
                    ToolTip tooltip = new ToolTip();
                    tooltip.AutomaticDelay = 500;
                    tooltip.ShowAlways = true;
                    tooltip.SetToolTip(btn, info);
                }
            }
        }

        // ===== SỰ KIỆN CLICK - HIỂN THỊ CHI TIẾT TRONG MESSAGEBOX =====
        private void FieldButton_Click(object sender, EventArgs e)
        {
            if (sender is Button btn)
            {
                string maSan = GetMaSanFromButton(btn);
                if (!string.IsNullOrWhiteSpace(maSan))
                {
                    string info = GetFieldInfoForToday(maSan);
                    MessageBox.Show(info, $"Thông Tin {maSan}", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }
    }
}