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
        private BUS.LichDatBUS busLichDat = new BUS.LichDatBUS();
        private SanBongBUS busSanBong = new SanBongBUS();
        private bool isEditing = false;

        // BIẾN LƯU TRỮ TRẠNG THÁI SẮP XẾP 
        private string sortedColumn = "";
        private SortOrder sortOrder = SortOrder.Ascending;

        //BIẾN HỖ TRỢ TÌM KIẾM REAL-TIME 
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
                dgvDatSan.AutoGenerateColumns = false;

                InitializeSearchTimer();

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



        //Khởi tạo Timer
        private void InitializeSearchTimer()
        {
            if (searchTimer == null)
            {
                searchTimer = new System.Timers.Timer(300);
                searchTimer.Elapsed += SearchTimer_Elapsed;
                searchTimer.AutoReset = false; 
            }
        }

        private void TxtTimKiem_TextChanged(object sender, EventArgs e)
        {
                lastSearchKeyword = txtTimKiem.Text.Trim(); // Lưu từ khóa mới nhất
                searchTimer.Stop();
                searchTimer.Start();
        }


        // 3. Sự kiện khi Timer kết thúc đếm 
        private void SearchTimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
                if (!this.IsDisposed && !this.Disposing)
                {
                    this.Invoke(new Action(() =>{if (!this.IsDisposed)PerformSearch(lastSearchKeyword);}));
                }
        }

        // Hàm thực hiện tìm kiếm và cập nhật Grid 
        private void PerformSearch(string keyword)
        {
           
            
                if (string.IsNullOrWhiteSpace(keyword))
                {
                    RefreshData();
                    return;
                }

                var results = busLichDat.Search(keyword);

                dgvDatSan.DataSource = null; 

                if (results != null && results.Count > 0)
                {
                    dgvDatSan.DataSource = results;
                }
                else
                {
                    dgvDatSan.DataSource = new List<DAL.LichDat>();
                }
                ReapplyColumnBindings();
                FormatDonGiaColumn();
        }
     
        

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            PerformSearch(txtTimKiem.Text.Trim());
            txtTimKiem.Focus();
        }




        private void dgvDatSan_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            try
            {
                DataGridViewRow row = dgvDatSan.Rows[e.RowIndex];

                if (row.Cells["clMaSan"].Value != null)
                {
                    cbxMaSan.Text = row.Cells["clMaSan"].Value.ToString();
                    foreach (var item in cbxMaSan.Items)
                    {
                        if (item.ToString() == row.Cells["clMaSan"].Value.ToString())
                        {
                            cbxMaSan.SelectedItem = item;
                            break;
                        }
                    }
                }

                if (row.Cells["clSDT_KH"].Value != null)
                    txtSDT.Text = row.Cells["clSDT_KH"].Value.ToString();

                if (row.Cells["clTenKH"].Value != null)
                    txtTenKhachHang.Text = row.Cells["clTenKH"].Value.ToString();

                // Tính toán và hiển thị đơn giá
                try
                {
                    int gioBD = 0;
                    int gioKT = 0;
                    decimal giaThucTe = 0;

                    if (row.Cells["clGioBatDau"].Value != null) gioBD = Convert.ToInt32(row.Cells["clGioBatDau"].Value);
                    if (row.Cells["clGioKetThuc"].Value != null) gioKT = Convert.ToInt32(row.Cells["clGioKetThuc"].Value);

                    if (row.Cells["clDonGiaThucTe"].Value != null && row.Cells["clDonGiaThucTe"].Value != DBNull.Value)
                    {
                        decimal.TryParse(row.Cells["clDonGiaThucTe"].Value.ToString(), out giaThucTe);
                    }

                    int soGio = gioKT - gioBD;

                    if (soGio > 0 && giaThucTe > 0)
                    {
                        decimal donGiaHangGio = giaThucTe / soGio;
                        txtDonGia.Text = donGiaHangGio.ToString("0");
                    }
                    else if (giaThucTe > 0)
                    {
                        txtDonGia.Text = giaThucTe.ToString("0");
                    }
                    else
                    {
                        txtDonGia.Text = "0";
                    }
                }
                catch
                {
                    txtDonGia.Text = "0";
                }

                if (row.Cells["clNgayDat"].Value != null)
                {
                    DateTime ngayDat = Convert.ToDateTime(row.Cells["clNgayDat"].Value);
                    if (ngayDat >= dtpNgayDat.MinDate && ngayDat <= dtpNgayDat.MaxDate)
                        dtpNgayDat.Value = ngayDat;
                }

                if (row.Cells["clGioBatDau"].Value != null)
                {
                    int gioBD = Convert.ToInt32(row.Cells["clGioBatDau"].Value);
                    dtpGioBatDau.Value = DateTime.Today.AddHours(gioBD);
                }

                if (row.Cells["clGioKetThuc"].Value != null)
                {
                    int gioKT = Convert.ToInt32(row.Cells["clGioKetThuc"].Value);
                    dtpGioKetThuc.Value = DateTime.Today.AddHours(gioKT);
                }

                isEditing = true;
            }
            catch (Exception ex) { }
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
                string propName = "";
                if (columnName == "clMaLich") propName = "MaLich";
                else if (columnName == "clMaSan") propName = "MaSan";
                else if (columnName == "clSDT_KH") propName = "SDT_KH";
                else if (columnName == "clTenKH") propName = "TenKH";
                else if (columnName == "clNgayDat") propName = "NgayDat";
                else if (columnName == "clGioBatDau") propName = "GioBD";
                else if (columnName == "clGioKetThuc") propName = "GioKT";
                else if (columnName == "clDonGiaThucTe") propName = "DonGiaThucTe";
                else if (columnName == "clTrangThai") propName = "TrangThai";
                else return; 

                if (sortOrder == SortOrder.Ascending)
                    currentData = currentData.OrderBy(x => GetPropertyValue(x, propName)).ToList();
                else
                    currentData = currentData.OrderByDescending(x => GetPropertyValue(x, propName)).ToList();

                dgvDatSan.DataSource = null;
                dgvDatSan.DataSource = currentData;
                ReapplyColumnBindings();
                FormatDonGiaColumn();
            }catch (Exception ex){}
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
                var data = busLichDat.GetAll();
                dgvDatSan.DataSource = null;
                dgvDatSan.DataSource = data;
                ReapplyColumnBindings();
                FormatDonGiaColumn();
                sortedColumn = "";
                sortOrder = SortOrder.Ascending;
        }

        private void FormatDonGiaColumn()
        {
            try
            {
                if (dgvDatSan.Columns["clDonGiaThucTe"] != null)
                {
                    dgvDatSan.Columns["clDonGiaThucTe"].DefaultCellStyle.Format = "N0";
                    dgvDatSan.Columns["clDonGiaThucTe"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                }
            }
            catch { }
        }

        private void ReapplyColumnBindings()
        {
            void Bind(string colName, string propName)
            {
                var col = dgvDatSan.Columns[colName];
                if (col != null)
                    col.DataPropertyName = propName;
            }
            Bind("clMaLich", "MaLich");
            Bind("clMaSan", "MaSan");
            Bind("clSDT_KH", "SDT_KH");
            Bind("clTenKH", "TenKH");
            Bind("clNgayDat", "NgayDat");
            Bind("clGioBatDau", "GioBD");
            Bind("clGioKetThuc", "GioKT");
            Bind("clTrangThai", "TrangThai");
            Bind("clDonGiaThucTe", "DonGiaThucTe");
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
            if (cleanPhone.Length < 9 || cleanPhone.Length > 11) 
            {
                MessageBox.Show($"❌ Số điện thoại không hợp lệ (9-11 số)!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
                txtDonGia.Focus();return false;
            }
            if (!decimal.TryParse(priceText.Trim(), out decimal price) || price <= 0)
            {
                MessageBox.Show("❌ Đơn giá không hợp lệ!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtDonGia.Focus();
                return false;
            }return true;
        }

        private bool IsValidSanCode(string maSan)
        {
            if (string.IsNullOrWhiteSpace(maSan))
            {
                MessageBox.Show("❌ Vui lòng chọn sân!", "Cảnh báo");
                return false;
            }            return true;
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
                MessageBox.Show("Vui lòng chọn lịch để hủy!");
                return;
            }

            var trangThai = dgvDatSan.CurrentRow.Cells["clTrangThai"].Value?.ToString().Trim() ?? "";
            if (trangThai.Equals("Đã thanh toán", StringComparison.OrdinalIgnoreCase) ||
                trangThai.Equals("Đã hoàn thành", StringComparison.OrdinalIgnoreCase) ||
                trangThai.Equals("Hoàn thành", StringComparison.OrdinalIgnoreCase))
            {
                MessageBox.Show("⛔ Lịch đã thanh toán, không được phép hủy!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string maLich = dgvDatSan.CurrentRow.Cells["clMaLich"].Value?.ToString();
            if (string.IsNullOrWhiteSpace(maLich))
            {
                MessageBox.Show("Không xác định được mã lịch!");
                return;
            }
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
            if (dgvDatSan.SelectedRows.Count == 0 || dgvDatSan.CurrentRow == null)
            {
                MessageBox.Show("Vui lòng chọn lịch để sửa!");
                return;
            }

            string maLich = dgvDatSan.CurrentRow.Cells["clMaLich"].Value.ToString();

            DateTime ngayDat = dtpNgayDat.Value.Date;
            if (ngayDat < DateTime.Now.Date)
            {
                MessageBox.Show("❌ Không thể sửa lịch chuyển về ngày quá khứ!", "Cảnh báo");
                return;
            }

            if (!IsValidPhoneNumber(txtSDT.Text)) return;
            if (!IsValidPrice(txtDonGia.Text)) return;

            int gioBD = dtpGioBatDau.Value.Hour;
            int gioKT = dtpGioKetThuc.Value.Hour;
            if (gioBD >= gioKT)
            {
                MessageBox.Show("Giờ bắt đầu phải nhỏ hơn giờ kết thúc!");
                return;
            }

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
            if (dgvDatSan.CurrentRow == null)
            {
                MessageBox.Show("Vui lòng chọn lịch để xóa!");
                return;
            }
            string maLich = dgvDatSan.CurrentRow.Cells["clMaLich"].Value?.ToString();

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
                if (btn != null)
                {
                    btn.MouseEnter += FieldButton_MouseEnter;
                    btn.Click += FieldButton_Click;
                }
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
            try
            {
                // 1. KIỂM TRA ĐÃ CHỌN DÒNG CHƯA
                if (dgvDatSan.SelectedRows.Count == 0 || dgvDatSan.CurrentRow == null)
                {
                    MessageBox.Show("❌ Vui lòng chọn lịch đặt để thanh toán!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                DataGridViewRow row = dgvDatSan.CurrentRow;

                string maLich = row.Cells["clMaLich"].Value?.ToString()?.Trim() ?? "";
                string trangThai = row.Cells["clTrangThai"].Value?.ToString()?.Trim() ?? "";
                string ngayDatString = row.Cells["clNgayDat"].Value?.ToString()?.Trim() ?? "";

                string tenKH = row.Cells["clTenKH"].Value?.ToString()?.Trim() ?? "";
                string sdtKH = row.Cells["clSDT_KH"].Value?.ToString()?.Trim() ?? "";

                decimal tienSan = 0;
                if (row.Cells["clDonGiaThucTe"].Value != null)
                {
                    decimal.TryParse(row.Cells["clDonGiaThucTe"].Value.ToString(), out tienSan);
                }

                // 1. RÀNG BUỘC TRẠNG THÁI
                if (trangThai.Equals("Đã hủy", StringComparison.OrdinalIgnoreCase))
                {
                    MessageBox.Show("⛔ Lịch này ĐÃ HỦY, không thể thực hiện thanh toán!",
                        "Cấm", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (trangThai.Equals("Đã thanh toán", StringComparison.OrdinalIgnoreCase))
                {
                    MessageBox.Show("⛔ Lịch này ĐÃ THANH TOÁN rồi!",
                        "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                if (!trangThai.Equals("Đã đặt", StringComparison.OrdinalIgnoreCase))
                {
                    MessageBox.Show($"⚠️ Trạng thái hiện tại là '{trangThai}'.\nChỉ được phép thanh toán các lịch có trạng thái 'Đã đặt'.",
                        "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (DateTime.TryParse(ngayDatString, out DateTime ngayDat))
                {
                    if (ngayDat.Date < DateTime.Now.Date)
                    {
                        MessageBox.Show($"⛔ Không được phép thanh toán cho lịch trong quá khứ!\n\n" + "Vui lòng kiểm tra lại ngày đặt.", "Vi phạm quy tắc", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                        return;
                    }
                }
                else
                {
                    MessageBox.Show("❌ Lỗi định dạng ngày tháng của lịch đặt!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                CT_HoaDon_DichVu frmThanhToan = new CT_HoaDon_DichVu();

                frmThanhToan.SetKhachHang(tenKH, sdtKH);
                frmThanhToan.SetMaLich(maLich);


                this.Hide();
                frmThanhToan.ShowDialog();
                this.Show();

                if (frmThanhToan.IsThanhToanThanhCong)
                {
                    bool updateResult = busLichDat.UpdateTrangThai(maLich, "Đã thanh toán");

                    RefreshData();

                }
            }
            catch (Exception ex) { }
        }

        private void btnThemDivhVu_Click(object sender, EventArgs e)
        {
            if (dgvDatSan.SelectedRows.Count == 0) return;

            string maLich = dgvDatSan.CurrentRow.Cells["clMaLich"].Value.ToString();
            string tenKH = dgvDatSan.CurrentRow.Cells["clTenKH"].Value.ToString();
            string sdtKH = dgvDatSan.CurrentRow.Cells["clSDT_KH"].Value.ToString();

            DichVu frm = new DichVu();
            frm.SetDefaultCustomer(tenKH, sdtKH, maLich);
            this.Hide();
            frm.ShowDialog();
            this.Show();
        }

        private void dgvDatSan_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1)
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