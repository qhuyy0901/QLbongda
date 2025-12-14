using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BUS;
using DAL;

namespace TrangChu
{
    public partial class QuanLyTK : Form
    {
        private UserBUS busUser = new UserBUS();
        private bool isEditing = false;

        public QuanLyTK()
        {
            InitializeComponent();
            this.Load += QuanLyTK_Load;
        }

        private void QuanLyTK_Load(object sender, EventArgs e)
        {
            try
            {
                // Gán event handlers cho các nút
                btnThem.Click += BtnThem_Click;
                btnSua.Click += BtnSua_Click;
                btnXoa.Click += BtnXoa_Click;
                dgvThongTinTK.CellClick += DataGridView1_CellClick;

                // ===== ẨN PASSWORD BẰNG DẤU * =====
                txtPassword.PasswordChar = '*';
                txtNhapLaiPassword.PasswordChar = '*';

                // Tải dữ liệu từ database
                LoadDataFromDatabase();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"❌ Lỗi khởi tạo form: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ===== LOAD DỮ LIỆU TỪ DATABASE LÊN DATAGRIDVIEW =====
        private void LoadDataFromDatabase()
        {
            try
            {
                // Lấy dữ liệu từ UserBUS
                var userList = busUser.GetAll();
                
                if (userList == null)
                {
                    MessageBox.Show("❌ Lỗi kết nối database!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Xóa dữ liệu cũ trong DataGridView
                dgvThongTinTK.Rows.Clear();

                // Gán dữ liệu vào DataGridView
                if (userList.Count > 0)
                {
                    foreach (var user in userList)
                    {
                        dgvThongTinTK.Rows.Add(user.ID, user.Role, user.TenNguoiDung);
                    }
                    
                }
                else
                {
                    MessageBox.Show("⚠ Không có dữ liệu tài khoản trong database!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"❌ Lỗi tải dữ liệu từ database: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ===== GÁN PROPERTY NAME CHO CÁC CỘT =====
        private void ApplyColumnBindings()
        {
            // Không cần gian DataPropertyName khi thêm dữ liệu manual
            // Method này có thể xóa hoàn toàn
        }

        // ===== THÊM TÀI KHOẢN =====
        private void BtnThem_Click(object sender, EventArgs e)
        {
            try
            {
                // Kiểm tra dữ liệu nhập
                if (!ValidateInput())
                    return;

                // Kiểm tra password trùng khớp
                if (txtPassword.Text != txtNhapLaiPassword.Text)
                {
                    MessageBox.Show("❌ Password không trùng khớp!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Kiểm tra ID đã tồn tại trong database
                var existingUser = busUser.GetByID(txtID.Text.Trim());
                if (existingUser != null)
                {
                    MessageBox.Show("❌ ID đã tồn tại trong database!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Tạo tài khoản mới
                DAL.User user = new DAL.User
                {
                    ID = txtID.Text.Trim(),
                    Password = txtPassword.Text.Trim(),
                    Role = cbbRole.SelectedItem?.ToString() ?? "NhanVien",
                    TenNguoiDung = txtTenNguoiDung.Text.Trim()
                };

                // Xác nhận thêm
                DialogResult dr = MessageBox.Show(
                    $"Bạn có chắc chắn muốn thêm tài khoản [{user.ID}] không?",
                    "Xác nhận",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (dr == DialogResult.Yes)
                {
                    if (busUser.Insert(user))
                    {
                        MessageBox.Show("✔ Thêm tài khoản thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadDataFromDatabase();
                        ResetForm();
                    }
                    else
                    {
                        MessageBox.Show("❌ Thêm tài khoản thất bại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"❌ Lỗi thêm tài khoản: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ===== SỬA TÀI KHOẢN =====
        private void BtnSua_Click(object sender, EventArgs e)
        {
            try
            {
                // Kiểm tra đã chọn hàng chưa
                if (dgvThongTinTK.SelectedRows.Count == 0)
                {
                    MessageBox.Show("❌ Vui lòng chọn tài khoản cần sửa!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Kiểm tra dữ liệu nhập (bỏ kiểm tra password bắt buộc khi sửa)
                if (string.IsNullOrWhiteSpace(txtID.Text))
                {
                    MessageBox.Show("❌ Vui lòng nhập ID!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtID.Focus();
                    return;
                }

                if (string.IsNullOrWhiteSpace(txtTenNguoiDung.Text))
                {
                    MessageBox.Show("❌ Vui lòng nhập Tên Người Dùng!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtTenNguoiDung.Focus();
                    return;
                }

                if (cbbRole.SelectedItem == null)
                {
                    MessageBox.Show("❌ Vui lòng chọn Quyền!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    cbbRole.Focus();
                    return;
                }

                // Kiểm tra password trùng khớp (nếu có nhập password mới)
                if (!string.IsNullOrWhiteSpace(txtPassword.Text))
                {
                    if (txtPassword.Text != txtNhapLaiPassword.Text)
                    {
                        MessageBox.Show("❌ Password không trùng khớp!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }

                // Xác nhận sửa
                DialogResult dr = MessageBox.Show(
                    $"Bạn có chắc chắn muốn cập nhật tài khoản [{txtID.Text}] không?",
                    "Xác nhận",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (dr == DialogResult.Yes)
                {
                    // ===== CẬP NHẬT PASSWORD: NẾU CÓ NHẬP MẬT KHẨU MỚI THÌ DÙNG CÁI MỚI, NGƯỢC LẠI DÙNG CÁI CŨ =====
                    string newPassword = !string.IsNullOrWhiteSpace(txtPassword.Text) 
                        ? txtPassword.Text.Trim() 
                        : GetCurrentPassword();

                    DAL.User userUpdate = new DAL.User
                    {
                        ID = txtID.Text.Trim(),
                        Password = newPassword,
                        Role = cbbRole.SelectedItem?.ToString() ?? "NhanVien",
                        TenNguoiDung = txtTenNguoiDung.Text.Trim()
                    };

                    if (busUser.Update(userUpdate))
                    {
                        MessageBox.Show("✔ Cập nhật tài khoản thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadDataFromDatabase();
                        ResetForm();
                    }
                    else
                    {
                        MessageBox.Show("❌ Cập nhật tài khoản thất bại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"❌ Lỗi sửa tài khoản: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ===== XÓA TÀI KHOẢN =====
        private void BtnXoa_Click(object sender, EventArgs e)
        {
            try
            {
                // Kiểm tra đã chọn hàng chưa
                if (dgvThongTinTK.SelectedRows.Count == 0)
                {
                    MessageBox.Show("❌ Vui lòng chọn tài khoản cần xóa!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                string idDelete = dgvThongTinTK.SelectedRows[0].Cells["clID"].Value?.ToString();

                if (string.IsNullOrWhiteSpace(idDelete))
                {
                    MessageBox.Show("❌ ID không hợp lệ!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Xác nhận xóa
                DialogResult result = MessageBox.Show(
                    $"❓ Bạn có chắc chắn muốn xóa tài khoản [{idDelete}] không?\n\n⚠️ Hành động này không thể hoàn tác!",
                    "Xác nhận xóa",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    if (busUser.Delete(idDelete))
                    {
                        MessageBox.Show("✔ Xóa tài khoản thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadDataFromDatabase();
                        ResetForm();
                    }
                    else
                    {
                        MessageBox.Show("❌ Xóa tài khoản thất bại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"❌ Lỗi xóa tài khoản: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ===== LẤY THÔNG TIN HÀNG ĐÃ CHỌN =====
        private void DataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex < 0) return;

                // Lấy dữ liệu từ hàng được chọn (sử dụng tên cột đúng)
                string id = dgvThongTinTK.Rows[e.RowIndex].Cells["clID"].Value?.ToString();
                string role = dgvThongTinTK.Rows[e.RowIndex].Cells["clRole"].Value?.ToString();
                string tenNguoiDung = dgvThongTinTK.Rows[e.RowIndex].Cells["clTenNguoiDung"].Value?.ToString();

                // Hiển thị lên TextBox
                txtID.Text = id;
                cbbRole.SelectedItem = role;
                txtTenNguoiDung.Text = tenNguoiDung;

                // Lấy password từ database - hiển thị dấu * trong textbox nhưng lưu giữ password thật
                var user = busUser.GetByID(id);
                if (user != null)
                {
                    // Lưu password thật vào textbox (sẽ hiển thị dấu * do PasswordChar)
                    txtPassword.Text = user.Password;
                    txtNhapLaiPassword.Text = user.Password;
                }

                isEditing = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"❌ Lỗi lấy dữ liệu: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ===== TẢI LẠI TRANG =====
        
        // ===== RESET FORM =====
        private void ResetForm()
        {
            txtID.Clear();
            txtPassword.Clear();
            txtNhapLaiPassword.Clear();
            cbbRole.SelectedIndex = 0;
            txtTenNguoiDung.Clear();
            dgvThongTinTK.ClearSelection();
            isEditing = false;
        }

        // ===== KIỂM TRA DỮ LIỆU NHẬP - BỎ KIỂM TRA PASSWORD KHI THÊM (NẾU MUỐN) =====
        private bool ValidateInput()
        {
            if (string.IsNullOrWhiteSpace(txtID.Text))
            {
                MessageBox.Show("❌ Vui lòng nhập ID!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtID.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtPassword.Text))
            {
                MessageBox.Show("❌ Vui lòng nhập Password!", "Cảnh cáo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtPassword.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtTenNguoiDung.Text))
            {
                MessageBox.Show("❌ Vui lòng nhập Tên Người Dùng!", "Cảnh cáo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTenNguoiDung.Focus();
                return false;
            }

            if (cbbRole.SelectedItem == null)
            {
                MessageBox.Show("❌ Vui lòng chọn Quyền!", "Cảnh cáo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cbbRole.Focus();
                return false;
            }

            return true;
        }

        // ===== LẤY PASSWORD HIỆN TẠI KHI CHỈNH SỬA =====
        private string GetCurrentPassword()
        {
            try
            {
                var user = busUser.GetByID(txtID.Text.Trim());
                return user?.Password ?? "";
            }
            catch
            {
                return "";
            }
        }

        private void btnQuayLai_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            try
            {
                string keyword = txtTimKiem.Text.Trim();

                // Kiểm tra từ khóa có rỗng không
                if (string.IsNullOrWhiteSpace(keyword))
                {
                    MessageBox.Show("❌ Vui lòng nhập từ khóa tìm kiếm!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtTimKiem.Focus();
                    return;
                }

                // Sử dụng method Search từ UserBUS
                var searchResults = busUser.Search(keyword);

                // Xóa dữ liệu cũ trong DataGridView
                dgvThongTinTK.Rows.Clear();

                // Hiển thị kết quả tìm kiếm
                if (searchResults.Count > 0)
                {
                    foreach (var user in searchResults)
                    {
                        dgvThongTinTK.Rows.Add(user.ID, user.Role, user.TenNguoiDung);
                    }

                    MessageBox.Show($"✔ Tìm thấy {searchResults.Count} tài khoản!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show($"⚠ Không tìm thấy tài khoản nào chứa '{keyword}'", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    dgvThongTinTK.Rows.Clear();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"❌ Lỗi tìm kiếm: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnTaiLaiTrang_Click_1(object sender, EventArgs e)
        {
            try
            {
                LoadDataFromDatabase();
                ResetForm();
                MessageBox.Show("✔ Tải lại dữ liệu thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"❌ Lỗi tải lại: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
