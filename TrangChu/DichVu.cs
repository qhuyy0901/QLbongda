using BUS;
using DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;

namespace TrangChu
{
    public partial class DichVu : Form
    {
        private DichVuBUS busDichVu = new DichVuBUS();
        private LichDatBUS busLichDat = new LichDatBUS();

        private User currentLoggedInUser;

        private DataTable dtDichVu = new DataTable();
        private DataTable dtGioHang = new DataTable();

        private List<LichDat> listLichDat = new List<LichDat>();

        private string defaultTenKH = "";
        private string defaultSDT = "";
        private string defaultMaLich = "";
        private bool isLoadingDefaultData = false;

        public DichVu()
        {
            InitializeComponent();
            this.Load += DichVu_Load;
        }

        public void SetCurrentUser(User user)
        {
            currentLoggedInUser = user;
        }

        private void DichVu_Load(object sender, EventArgs e)
        {
            try
            {
                InitTable();
                LoadDichVu();
                LoadLichDat();
                SetupEventHandlers();

                if (!string.IsNullOrWhiteSpace(defaultMaLich))
                {
                    ApplyDefaultCustomer();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("❌ Lỗi khởi tạo form Dịch vụ:\n" + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void SetDefaultCustomer(string tenKH, string sdtKH, string maLich)
        {
            try
            {
                defaultTenKH = tenKH ?? "";
                defaultSDT = sdtKH ?? "";
                defaultMaLich = maLich ?? "";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"❌ Lỗi thiết lập khách hàng mặc định: {ex.Message}", 
                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ApplyDefaultCustomer()
        {
            try
            {
                isLoadingDefaultData = true;

                // ===== ĐIỀN THÔNG TIN KHÁCH HÀNG =====
                txtTenKH.Text = defaultTenKH;
                txtSDT.Text = defaultSDT;
                
                txtTenKH.ReadOnly = true;
                txtSDT.ReadOnly = true;
                txtTenKH.BackColor = System.Drawing.Color.LightGray;
                txtSDT.BackColor = System.Drawing.Color.LightGray;

                // ===== TÌM VÀ CHỌN LỊCH ĐẶT =====
                if (!string.IsNullOrWhiteSpace(defaultMaLich))
                {
                    if (cbxLichDat.DataSource != null && cbxLichDat.DataSource is List<DAL.LichDat> lichDatList)
                    {
                        var selectedLich = lichDatList.FirstOrDefault(l => l.MaLich == defaultMaLich);
                        
                        if (selectedLich != null)
                        {
                            cbxLichDat.Enabled = true;
                            cbxLichDat.SelectedItem = selectedLich;
                            cbxLichDat.Enabled = false;
                        }
                        else
                        {
                            MessageBox.Show($"⚠️ Không tìm thấy lịch đặt [{defaultMaLich}] trong danh sách!", 
                                "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            cbxLichDat.Enabled = false;
                        }
                    }
                    else
                    {
                        cbxLichDat.Enabled = false;
                    }
                }
                else
                {
                    cbxLichDat.Enabled = false;
                }

                isLoadingDefaultData = false;
            }
            catch (Exception ex)
            {
                isLoadingDefaultData = false;
                MessageBox.Show($"❌ Lỗi áp dụng dữ liệu mặc định: {ex.Message}", 
                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SetupEventHandlers()
        {
            try
            {
                cbxLichDat.SelectedIndexChanged += CbxLichDat_SelectedIndexChanged;
                dgvDichVu.CellClick += dgvDichVu_CellClick;
            }
            catch (Exception ex)
            {
                MessageBox.Show("❌ Lỗi thiết lập sự kiện:\n" + ex.Message);
            }
        }

        private void LoadLichDat()
        {
            try
            {
                List<DAL.LichDat> allLichDat = busLichDat.GetAll();
                
                DateTime homNay = DateTime.Now.Date;
                List<DAL.LichDat> lichDatList = allLichDat
                    .Where(l => l.NgayDat.HasValue && 
                                l.NgayDat.Value >= homNay && 
                                l.TrangThai == "Đã đặt")
                    .OrderBy(l => l.NgayDat)
                    .ThenBy(l => l.GioBD)
                    .ToList();

                cbxLichDat.DataSource = null;
                cbxLichDat.Items.Clear();
                cbxLichDat.DisplayMember = "ThongTinLich";
                cbxLichDat.ValueMember = "MaLich";
                cbxLichDat.DataSource = lichDatList;

                if (lichDatList.Count == 0)
                {
                    MessageBox.Show("⚠️ Không có lịch đặt nào từ hôm nay trở đi!", 
                        "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    cbxLichDat.Enabled = false;
                }
                else
                {
                    cbxLichDat.Enabled = string.IsNullOrWhiteSpace(defaultMaLich);
                    cbxLichDat.SelectedIndex = -1;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("❌ Lỗi tải danh sách lịch đặt:\n" + ex.Message, 
                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CbxLichDat_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (isLoadingDefaultData)
                    return;

                if (cbxLichDat.SelectedIndex < 0)
                {
                    txtTenKH.Clear();
                    txtSDT.Clear();

                    // ===== KHI KHÔNG CHỌN LỊCH, MỞ KHÓA CÁC TEXTBOX VÀ NÚT DỊCH VỤ =====
                    txtMaDVu.Enabled = true;
                    txtTenDVu.Enabled = true;
                    txtDonGiaDVu.Enabled = true;
                    txtMaDVu.BackColor = System.Drawing.Color.White;
                    txtTenDVu.BackColor = System.Drawing.Color.White;
                    txtDonGiaDVu.BackColor = System.Drawing.Color.White;

                    btnThemDVu.Enabled = true;
                    btnSuaDVu.Enabled = true;
                    btnXoaDVu.Enabled = true;

                    return;
                }

                var selectedLich = cbxLichDat.SelectedItem as DAL.LichDat;

                if (selectedLich != null)
                {
                    txtTenKH.Text = selectedLich.TenKH ?? "";
                    txtSDT.Text = selectedLich.SDT_KH ?? "";

                    // ===== KHI ĐÃ CHỌN LỊCH, KHÓA CÁC TEXTBOX VÀ NÚT DỊCH VỤ =====
                    txtMaDVu.Enabled = false;
                    txtTenDVu.Enabled = false;
                    txtDonGiaDVu.Enabled = false;
                    txtMaDVu.BackColor = System.Drawing.Color.LightGray;
                    txtTenDVu.BackColor = System.Drawing.Color.LightGray;
                    txtDonGiaDVu.BackColor = System.Drawing.Color.LightGray;

                    btnThemDVu.Enabled = false;
                    btnSuaDVu.Enabled = false;
                    btnXoaDVu.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("❌ Lỗi lấy thông tin lịch:\n" + ex.Message, 
                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void InitTable()
        {
            try
            {
                dtDichVu.Columns.Clear();
                dtDichVu.Rows.Clear();

                dtDichVu.Columns.Add("MaDV", typeof(string));
                dtDichVu.Columns.Add("TenDV", typeof(string));
                dtDichVu.Columns.Add("DonGia", typeof(decimal));
                dgvDichVu.DataSource = dtDichVu;
                dgvDichVu.DataSource = dtDichVu;
                dgvDichVu.CellClick += dgvDichVu_CellClick; 

                dtGioHang.Columns.Clear();
                dtGioHang.Rows.Clear();

                dtGioHang.Columns.Add("MaDV", typeof(string));
                dtGioHang.Columns.Add("TenDV", typeof(string));
                dtGioHang.Columns.Add("DonGia", typeof(decimal));
                dtGioHang.Columns.Add("SoLuong", typeof(int));
                dtGioHang.Columns.Add("ThanhTien", typeof(decimal), "DonGia * SoLuong");
                dgvGioHang.DataSource = dtGioHang;



                if (dgvGioHang.Columns.Count > 0)
                {
                    dgvGioHang.Columns["MaDV"].ReadOnly = true;
                    dgvGioHang.Columns["TenDV"].ReadOnly = true;
                    dgvGioHang.Columns["DonGia"].ReadOnly = true;
                    dgvGioHang.Columns["ThanhTien"].ReadOnly = true;
                    
                    if (dgvGioHang.Columns.Contains("SoLuong"))
                    {
                        dgvGioHang.Columns["SoLuong"].ReadOnly = false;
                    }
                }

                dgvGioHang.CellEndEdit += DgvGioHang_CellEndEdit;
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khởi tạo bảng: " + ex.Message);
            }
        }

        private void DgvGioHang_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex != 3)
                    return;

                if (e.RowIndex < 0 || e.RowIndex >= dtGioHang.Rows.Count)
                    return;

                DataRow row = dtGioHang.Rows[e.RowIndex];

                decimal donGia = Convert.ToDecimal(row["DonGia"]);
                int soLuong = 0;

                if (!int.TryParse(dgvGioHang.Rows[e.RowIndex].Cells[3].Value?.ToString() ?? "0", out soLuong))
                {
                    soLuong = 1;
                    dgvGioHang.Rows[e.RowIndex].Cells[3].Value = 1;
                }

                if (soLuong <= 0)
                {
                    soLuong = 1;
                    dgvGioHang.Rows[e.RowIndex].Cells[3].Value = 1;
                }

                row["SoLuong"] = soLuong;

                decimal thanhTien = donGia * soLuong;
                row["ThanhTien"] = thanhTien;

                dgvGioHang.Rows[e.RowIndex].Cells[4].Value = thanhTien;
            }
            catch
            {
            }
        }

        private void LoadDichVu()
        {
            try
            {
                dtDichVu.Rows.Clear();
                var listDichVu = busDichVu.GetAllDichVu();

                if (listDichVu != null && listDichVu.Count > 0)
                {
                    foreach (var dv in listDichVu)
                    {
                        dtDichVu.Rows.Add(dv.MaDV, dv.TenDV, dv.DonGia ?? 0);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("❌ Lỗi tải dịch vụ:\n" + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnThemDV_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvDichVu.CurrentRow == null)
                {
                    MessageBox.Show("❌ Vui lòng chọn dịch vụ cần thêm!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                string ma = dgvDichVu.CurrentRow.Cells["MaDV"].Value?.ToString();
                string ten = dgvDichVu.CurrentRow.Cells["TenDV"].Value?.ToString();

                if (!decimal.TryParse(dgvDichVu.CurrentRow.Cells["DonGia"].Value?.ToString() ?? "0", out decimal gia))
                {
                    gia = 0;
                }

                var row = dtGioHang.AsEnumerable().FirstOrDefault(x => x["MaDV"].ToString() == ma);

                if (row == null)
                {
                    dtGioHang.Rows.Add(ma, ten, gia, 1);
                }
                else
                {
                    row["SoLuong"] = (int)row["SoLuong"] + 1;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("❌ Lỗi thêm dịch vụ:\n" + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnXoaDV_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvGioHang.CurrentRow == null)
                {
                    MessageBox.Show("❌ Vui lòng chọn dịch vụ cần xóa!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                dgvGioHang.Rows.RemoveAt(dgvGioHang.CurrentRow.Index);
            }
            catch (Exception ex)
            {
                MessageBox.Show("❌ Lỗi xóa dịch vụ:\n" + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnThanhToan_Click(object sender, EventArgs e)
        {
            try
            {
                // ===== KIỂM TRA GIỎ HÀNG CÓ TRỐNG KHÔNG =====
                if (dtGioHang.Rows.Count == 0)
                {
                    MessageBox.Show("❌ Giỏ hàng trống! Vui lòng thêm dịch vụ trước khi thanh toán.",
                        "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // ✅ KIỂM TRA LỊCH ĐẶT BẮTBUỘC
                string maLich = "";
                if (cbxLichDat.SelectedIndex >= 0)
                {
                    var selectedItem = cbxLichDat.SelectedItem as DAL.LichDat;
                    if (selectedItem != null)
                    {
                        maLich = selectedItem.MaLich;
                    }
                }

                if (string.IsNullOrWhiteSpace(maLich))
                {
                    MessageBox.Show("❌ Vui lòng chọn lịch đặt sân! (Lịch đặt là bắt buộc)",
                        "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    cbxLichDat.Focus();
                    return;
                }

                // ===== LẤY THÔNG TIN KHÁCH HÀNG =====
                string tenKH = txtTenKH.Text.Trim();
                string sdtKH = txtSDT.Text.Trim();

                if (string.IsNullOrWhiteSpace(tenKH) || string.IsNullOrWhiteSpace(sdtKH))
                {
                    MessageBox.Show("❌ Thông tin khách hàng không đầy đủ!",
                        "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // ===== CHUẨN BỊ DANH SÁCH CHI TIẾT DỊCH VỤ =====
                List<DAL.CT_HoaDon_DichVu> listChiTiet = new List<DAL.CT_HoaDon_DichVu>();
                decimal tongTienDichVu = 0;

                foreach (DataRow row in dtGioHang.Rows)
                {
                    decimal thanhTien = (decimal)row["ThanhTien"];
                    listChiTiet.Add(new DAL.CT_HoaDon_DichVu
                    {
                        MaDV = row["MaDV"].ToString(),
                        SoLuong = (int)row["SoLuong"],
                        ThanhTien = thanhTien
                    });
                    tongTienDichVu += thanhTien;
                }

                // ===== MỞ FORM CHI TIẾT THANH TOÁN =====
                CT_HoaDon_DichVu frmThanhToan = new CT_HoaDon_DichVu();

                frmThanhToan.SetKhachHang(tenKH, sdtKH);
                frmThanhToan.SetMaLich(maLich);
                frmThanhToan.SetChiTietDichVu(listChiTiet, tongTienDichVu);

                // ===== ẨN FORM HIỆN TẠI VÀ HIỂN THỊ FORM THANH TOÁN =====
                this.Hide();
                frmThanhToan.ShowDialog();
                this.Show();

                // ===== NẾU THANH TOÁN THÀNH CÔNG, RESET FORM VÀ LẤY LẠI DỮ LIỆU =====
                if (frmThanhToan.IsThanhToanThanhCong)
                {
                    dtGioHang.Rows.Clear();
                    txtTenKH.Clear();
                    txtSDT.Clear();
                    cbxLichDat.SelectedIndex = -1;

                    // ✅ LẤY LẠI DANH SÁCH LỊCH ĐẶT (LOẠI LỊCH ĐẶT ĐÃ THANH TOÁN)
                    LoadLichDat();

                    MessageBox.Show($"✔ Thanh toán thành công!\n\n📋 Hóa đơn đã được lưu.\n💰 Trạng thái lịch đặt [{maLich}] được cập nhật thành 'Đã thanh toán'",
                        "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("❌ Lỗi thanh toán:\n" + ex.Message,
                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnQuayLai_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnThemDVu_Click(object sender, EventArgs e)
        {
            try
            {
                string maDV = txtMaDVu.Text.Trim();
                string tenDV = txtTenDVu.Text.Trim();
                string donGiaText = txtDonGiaDVu.Text.Trim();

                // ===== KIỂM TRA VALIDATION =====
                if (string.IsNullOrWhiteSpace(maDV))
                {
                    MessageBox.Show("❌ Vui lòng nhập Mã Dịch Vụ!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtMaDVu.Focus();
                    return;
                }

                if (string.IsNullOrWhiteSpace(tenDV))
                {
                    MessageBox.Show("❌ Vui lòng nhập Tên Dịch Vụ!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtTenDVu.Focus();
                    return;
                }

                if (string.IsNullOrWhiteSpace(donGiaText) || !decimal.TryParse(donGiaText, out decimal donGia) || donGia <= 0)
                {
                    MessageBox.Show("❌ Vui lòng nhập Đơn Giá hợp lệ (phải > 0)!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtDonGiaDVu.Focus();
                    return;
                }

                // ===== KIỂM TRA TRÙNG LẬP MÃ =====
                var existingDV = dtDichVu.AsEnumerable().FirstOrDefault(x => x["MaDV"].ToString() == maDV);
                if (existingDV != null)
                {
                    MessageBox.Show($"❌ Mã Dịch Vụ [{maDV}] đã tồn tại!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtMaDVu.Focus();
                    return;
                }

                // ===== THÊM DỮ LIỆU VÀO BẢNG =====
                DAL.DichVu dichVuMoi = new DAL.DichVu
                {
                    MaDV = maDV,
                    TenDV = tenDV,
                    DonGia = donGia
                };

                // ===== GỌI BUS ĐỂ THÊM VÀO DATABASE =====
                try
                {
                    bool success = busDichVu.InsertDichVu(dichVuMoi);
                    if (success)
                    {
                        MessageBox.Show("✔ Thêm dịch vụ thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadDichVu();
                        ResetDichVuForm();
                    }
                    else
                    {
                        MessageBox.Show("❌ Lỗi thêm dịch vụ vào database!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception exBus)
                {
                    MessageBox.Show($"❌ Lỗi từ BUS: {exBus.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("❌ Lỗi thêm dịch vụ:\n" + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSuaDVu_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvDichVu.CurrentRow == null)
                {
                    MessageBox.Show("❌ Vui lòng chọn dịch vụ cần sửa!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                string maDV = dgvDichVu.CurrentRow.Cells["MaDV"].Value?.ToString() ?? "";
                string tenDV = txtTenDVu.Text.Trim();
                string donGiaText = txtDonGiaDVu.Text.Trim();

                // ===== KIỂM TRA VALIDATION =====
                if (string.IsNullOrWhiteSpace(tenDV))
                {
                    MessageBox.Show("❌ Vui lòng nhập Tên Dịch Vụ!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtTenDVu.Focus();
                    return;
                }

                if (string.IsNullOrWhiteSpace(donGiaText) || !decimal.TryParse(donGiaText, out decimal donGia) || donGia <= 0)
                {
                    MessageBox.Show("❌ Vui lòng nhập Đơn Giá hợp lệ (phải > 0)!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtDonGiaDVu.Focus();
                    return;
                }

                // ===== CHUẨN BỊ DỮ LIỆU CẬP NHẬT =====
                DAL.DichVu dichVuCapNhat = new DAL.DichVu
                {
                    MaDV = maDV,
                    TenDV = tenDV,
                    DonGia = donGia
                };

                // ===== GỌI BUS ĐỂ CẬP NHẬT DATABASE =====
                try
                {
                    bool success = busDichVu.UpdateDichVu(dichVuCapNhat);
                    if (success)
                    {
                        MessageBox.Show("✔ Cập nhật dịch vụ thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadDichVu();
                        ResetDichVuForm();
                    }
                    else
                    {
                        MessageBox.Show("❌ Lỗi cập nhật dịch vụ!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception exBus)
                {
                    MessageBox.Show($"❌ Lỗi từ BUS: {exBus.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("❌ Lỗi cập nhật dịch vụ:\n" + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnXoaDVu_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvDichVu.CurrentRow == null)
                {
                    MessageBox.Show("❌ Vui lòng chọn dịch vụ cần xóa!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                string maDV = dgvDichVu.CurrentRow.Cells["MaDV"].Value?.ToString() ?? "";
                string tenDV = dgvDichVu.CurrentRow.Cells["TenDV"].Value?.ToString() ?? "";

                DialogResult result = MessageBox.Show(
                    $"Bạn có chắc chắn muốn xóa dịch vụ [{maDV}] - {tenDV}?\n\nHành động này không thể hoàn tác!",
                    "Xác Nhận Xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    try
                    {
                        bool success = busDichVu.DeleteDichVu(maDV);
                        if (success)
                        {
                            MessageBox.Show("✔ Xóa dịch vụ thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            LoadDichVu();
                            ResetDichVuForm();
                        }
                        else
                        {
                            MessageBox.Show("❌ Lỗi xóa dịch vụ!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    catch (Exception exBus)
                    {
                        MessageBox.Show($"❌ Lỗi từ BUS: {exBus.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("❌ Lỗi xóa dịch vụ:\n" + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtTimKiemDVu_TextChanged(object sender, EventArgs e)
        {
            try
            {
                string searchKeyword = txtTimKiemDVu.Text.Trim().ToLower();

                // ===== NẾU TỪ KHÓA TRỐNG, LOAD LẠI TẬT CẢ DỊCH VỤ TỪ DATABASE =====
                if (string.IsNullOrWhiteSpace(searchKeyword))
                {
                    LoadDichVu();
                    dgvDichVu.DataSource = dtDichVu;
                    return;
                }

                // ===== LỌC DỊCH VỤ THEO TỪ KHÓA =====
                DataTable dtFiltered = dtDichVu.Clone();

                foreach (DataRow row in dtDichVu.Rows)
                {
                    string maDV = row["MaDV"]?.ToString() ?? "";
                    string tenDV = row["TenDV"]?.ToString() ?? "";

                    // Tìm kiếm theo Mã DV hoặc Tên DV (không phân biệt hoa/thường)
                    if (maDV.ToLower().Contains(searchKeyword) || tenDV.ToLower().Contains(searchKeyword))
                    {
                        dtFiltered.ImportRow(row);
                    }
                }

                // ===== CẬP NHẬT HIỂN THỊ =====
                dgvDichVu.DataSource = dtFiltered;
            }
            catch (Exception ex)
            {
                MessageBox.Show("❌ Lỗi tìm kiếm dịch vụ:\n" + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ===== HỖ TRỢ: TẠI KHI CHỌN DÒNG TRONG BẢNG, ĐIỀN DỮ LIỆU VÀO TEXTBOX =====
        private void dgvDichVu_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex < 0)
                    return;

                DataGridViewRow row = dgvDichVu.Rows[e.RowIndex];

                if (row.Cells["MaDV"].Value != null)
                    txtMaDVu.Text = row.Cells["MaDV"].Value.ToString();

                if (row.Cells["TenDV"].Value != null)
                    txtTenDVu.Text = row.Cells["TenDV"].Value.ToString();

                if (row.Cells["DonGia"].Value != null)
                    txtDonGiaDVu.Text = row.Cells["DonGia"].Value.ToString();

                // ===== KHÓA MÃ DỊCH VỤ KHI CHỌN ĐỂ SỬA =====
                txtMaDVu.ReadOnly = true;
                txtMaDVu.BackColor = System.Drawing.Color.LightGray;
            }
            catch (Exception ex)
            {
                MessageBox.Show("❌ Lỗi lấy dữ liệu:\n" + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ===== HELPER: RESET FORM DỊCH VỤ =====
        private void ResetDichVuForm()
        {
            try
            {
                txtMaDVu.Clear();
                txtTenDVu.Clear();
                txtDonGiaDVu.Clear();

                // ===== MỞ KHÓA MÃ DỊCH VỤ KHI RESET (CHUẨN BỊ THÊM MỚI) =====
                txtMaDVu.ReadOnly = false;
                txtMaDVu.BackColor = System.Drawing.Color.White;

                txtMaDVu.Focus();
            }
            catch { }
        }

        // ===== TRONG METHOD InitTable(), THÊM EVENT HANDLER NÀY =====
        // Gọi trong DichVu_Load hoặc InitTable()
        private void SetupDichVuGridEvents()
        {
            try
            {
                dgvDichVu.CellClick += dgvDichVu_CellClick;
            }
            catch (Exception ex)
            {
                MessageBox.Show("❌ Lỗi thiết lập sự kiện:\n" + ex.Message);
            }
        }
    }
}