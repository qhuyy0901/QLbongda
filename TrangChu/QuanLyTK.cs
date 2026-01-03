using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
        private string originalID = ""; 
        
        // STATIC FIELD LƯU NGƯỜI DÙNG ĐANG ĐĂNG NHẬP
        public static User CurrentLoggedInUser { get; set; }

        private readonly string[] VALID_ROLES = { "NhanVien", "Admin" };
        private const string DEFAULT_ROLE = "NhanVien";

        public QuanLyTK()
        {
            InitializeComponent();
            this.Load += QuanLyTK_Load;
        }

        private void QuanLyTK_Load(object sender, EventArgs e)
        {
            try
            {
                btnThem.Click += BtnThem_Click;
                btnSua.Click += BtnSua_Click;
                btnXoa.Click += BtnXoa_Click;
                dgvThongTinTK.CellClick += DataGridView1_CellClick;

                txtPassword.PasswordChar = '*';
                txtNhapLaiPassword.PasswordChar = '*';

                cbbRole.DataSource = new List<string> { "NhanVien", "Admin" };
                cbbRole.SelectedItem = DEFAULT_ROLE;

              
                LoadDataFromDatabase();
            }
            catch (Exception ex){}
        }

        private void LoadDataFromDatabase()
        {
            try
            {
                // Lấy dữ liệu từ UserBUS
                var userList = busUser.GetAll();


                dgvThongTinTK.Rows.Clear();

                if (userList.Count > 0)
                {
                    foreach (var user in userList)
                    {
                        if (!VALID_ROLES.Contains(user.Role))
                        {
                            MessageBox.Show(
                                $"⚠️ Cảnh báo: Tài khoản [{user.ID}] có quyền '{user.Role}' không hợp lệ!\n\n💡 Quyền hợp lệ: {string.Join(", ", VALID_ROLES)}", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            continue;
                        }

                        dgvThongTinTK.Rows.Add(user.ID, user.Role, user.TenNguoiDung);
                    }

                }
                else
                {
                    MessageBox.Show("⚠ Không có dữ liệu tài khoản trong database!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex) { }
        }

        // VALIDATE TÊN NGƯỜI DÙNG KHÔNG SỐ KHÔNG KÝ TỰ ĐẶC BIỆT
        private bool IsValidUsername(string username)
        {
            if (string.IsNullOrWhiteSpace(username))
            {
                MessageBox.Show("❌ Tên người dùng không được để trống!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTenNguoiDung.Focus();
                return false;
            }

            string cleanUsername = username.Trim();

            // ===== CHỈ CHO PHÉP CHỮ CÁI VÀ KHOẢNG TRẮNG =====
            if (!Regex.IsMatch(cleanUsername, @"^[a-zA-ZÀ-ỹ\s]+$"))
            {
                MessageBox.Show("❌ Tên người dùng chỉ được chứa chữ cái!\n\n" +"❌ Không được dùng số hoặc ký tự đặc biệt.\n\n","Cảnh báo",MessageBoxButtons.OK,MessageBoxIcon.Warning);
                txtTenNguoiDung.Focus();
                return false;
            }

            if (cleanUsername.Length < 1 || cleanUsername.Length > 50)
            {
                MessageBox.Show("❌ Tên người dùng phải từ 1 đến 50 ký tự!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTenNguoiDung.Focus();
                return false;
            }

            return true;
        }

        // ===== THÊM TÀI KHOẢN 
        private void BtnThem_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtID.Text))
                {
                    MessageBox.Show("❌ Vui lòng nhập ID!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtID.Focus();
                    return;
                }

                string idToCheck = txtID.Text.Trim();

                if (!Regex.IsMatch(idToCheck, @"^[a-zA-Z0-9_]+$"))
                {
                    MessageBox.Show( "❌ ID chỉ được chứa chữ cái, số và dấu gạch dưới!\n\n" +"❌ Không được dùng khoảng trắng hoặc ký tự đặc biệt khác.", "Cảnh báo",MessageBoxButtons.OK,MessageBoxIcon.Warning);
                    txtID.Focus();
                    return;
                }

                if (string.IsNullOrWhiteSpace(txtPassword.Text))
                {
                    MessageBox.Show("❌ Vui lòng nhập Password!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtPassword.Focus();
                    return;
                }

                if (!IsValidUsername(txtTenNguoiDung.Text))
                    return;


                string selectedRole = cbbRole.SelectedItem?.ToString();
                if (!VALID_ROLES.Contains(selectedRole))
                {
                    MessageBox.Show( $"❌ Quyền '{selectedRole}' không hợp lệ!\n\n" + $"💡 Quyền hợp lệ: {string.Join(", ", VALID_ROLES)}","Cảnh báo",MessageBoxButtons.OK,MessageBoxIcon.Warning);
                    cbbRole.Focus();
                    return;
                }

                if (txtPassword.Text != txtNhapLaiPassword.Text)
                {
                    MessageBox.Show("❌ Password không trùng khớp!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtPassword.Focus();
                    return;
                }

                var existingUser = busUser.GetByID(idToCheck);
                if (existingUser != null)
                {
                    MessageBox.Show("❌ ID đã tồn tại trong database! Vui lòng dùng ID khác.", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtID.Focus();
                    return;
                }

                //MÃ HÓA PASSWORD BẰNG AES 
                string plainPassword = txtPassword.Text.Trim();
                string encryptedPassword = AES.EncryptPassword(plainPassword);

                // Tạo tài khoản mới
                DAL.User user = new DAL.User
                {
                    ID = idToCheck,
                    Password = encryptedPassword,  //LƯU PASSWORD ĐÃ MÃ HÓA
                    Role = selectedRole,
                    TenNguoiDung = txtTenNguoiDung.Text.Trim()
                };

                DialogResult dr = MessageBox.Show($"Bạn có chắc chắn muốn thêm tài khoản [{user.ID}] không?","Xác nhận",MessageBoxButtons.YesNo,MessageBoxIcon.Question);
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
            catch (Exception ex){}
        }

        // SỬA TÀI KHOẢN 
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

                // ===== KHÔNG CHO PHÉP THAY ĐỔI ID 
                if (string.IsNullOrWhiteSpace(txtID.Text))
                {
                    MessageBox.Show("❌ ID không được để trống!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtID.Focus();
                    return;
                }

                string currentID = txtID.Text.Trim();
                
                if (currentID != originalID)
                {
                    MessageBox.Show( "❌ Không được phép thay đổi ID!\n\n", "Cảnh báo",MessageBoxButtons.OK,MessageBoxIcon.Warning);
                    txtID.Text = originalID;
                    txtID.Focus();
                    return;
                }

                if (!IsValidUsername(txtTenNguoiDung.Text))
                    return;

                string selectedRole = cbbRole.SelectedItem?.ToString();
                if (!VALID_ROLES.Contains(selectedRole))
                {
                    MessageBox.Show( $"❌ Quyền '{selectedRole}' không hợp lệ!\n\n" + $"💡 Quyền hợp lệ: {string.Join(", ", VALID_ROLES)}","Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    cbbRole.Focus();
                    return;
                }

                if (!string.IsNullOrWhiteSpace(txtPassword.Text))
                {
                    if (txtPassword.Text != txtNhapLaiPassword.Text)
                    {
                        MessageBox.Show("❌ Password không trùng khớp!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }

                DialogResult dr = MessageBox.Show($"Bạn có chắc chắn muốn cập nhật tài khoản [{txtID.Text}] không?","Xác nhận",MessageBoxButtons.YesNo,MessageBoxIcon.Question);
                if (dr == DialogResult.Yes)
                {
                    string newPassword;
                    
                    // ===== NẾU CÓ NHẬP PASSWORD MỚI THÌ MÃ HÓA =====
                    if (!string.IsNullOrWhiteSpace(txtPassword.Text))
                    {
                        newPassword = AES.EncryptPassword(txtPassword.Text.Trim());
                    }
                    else
                    {
                        // ===== NGƯỢC LẠI GIỮ NGUYÊN PASSWORD CŨ =====
                        var currentUser = busUser.GetByID(txtID.Text.Trim());
                        newPassword = currentUser?.Password ?? "";
                    }

                    DAL.User userUpdate = new DAL.User
                    {
                        ID = currentID,
                        Password = newPassword,
                        Role = selectedRole,
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
            }catch (Exception ex){}
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

                // ===== NGĂN CHẶN XÓA TÀI KHOẢN ADMIN ĐANG ĐĂNG NHẬP =====
                if (CurrentLoggedInUser != null && idDelete == CurrentLoggedInUser.ID)
                {
                    MessageBox.Show("❌ Không được phép xóa tài khoản Admin đang đăng nhập!\n\n","Cảnh báo",MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Xác nhận xóa
                DialogResult result = MessageBox.Show( $"❓ Bạn có chắc chắn muốn xóa tài khoản [{idDelete}] không?\n\n⚠️ Hành động này không thể hoàn tác!", "Xác nhận xóa", MessageBoxButtons.YesNo,MessageBoxIcon.Question);

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
            catch (Exception ex){}
        }

        private void DataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex < 0) return;

                string id = dgvThongTinTK.Rows[e.RowIndex].Cells["clID"].Value?.ToString();
                string role = dgvThongTinTK.Rows[e.RowIndex].Cells["clRole"].Value?.ToString();
                string tenNguoiDung = dgvThongTinTK.Rows[e.RowIndex].Cells["clTenNguoiDung"].Value?.ToString();

                originalID = id;

                txtID.Text = id;
                txtID.ReadOnly = true;  
                cbbRole.SelectedItem = role ?? DEFAULT_ROLE;
                txtTenNguoiDung.Text = tenNguoiDung;

                // Lấy password từ database
                var user = busUser.GetByID(id);
                if (user != null)
                {
                    // ===== GIẢI MÃ PASSWORD ĐỂ HIỂN THỊ =====
                    string decryptedPassword = AES.DecryptPassword(user.Password);
                    txtPassword.Text = decryptedPassword;
                    txtNhapLaiPassword.Text = decryptedPassword;
                }

                isEditing = true;
            }
            catch (Exception ex){}
        }
        private void ResetForm()
        {
            txtID.Clear();
            txtID.ReadOnly = false; 
            txtPassword.Clear();
            txtNhapLaiPassword.Clear();
            cbbRole.SelectedItem = DEFAULT_ROLE;  
            txtTenNguoiDung.Clear();
            dgvThongTinTK.ClearSelection();
            isEditing = false;
            originalID = "";
            txtID.Focus();
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

                if (string.IsNullOrWhiteSpace(keyword))
                {
                    MessageBox.Show("❌ Vui lòng nhập từ khóa tìm kiếm!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtTimKiem.Focus();
                    return;
                }

                var searchResults = busUser.Search(keyword);

                dgvThongTinTK.Rows.Clear();

                if (searchResults.Count > 0)
                {
                    foreach (var user in searchResults)
                    {
                        if (VALID_ROLES.Contains(user.Role))
                        {
                            dgvThongTinTK.Rows.Add(user.ID, user.Role, user.TenNguoiDung);
                        }
                    }

                    MessageBox.Show($"✔ Tìm thấy {dgvThongTinTK.Rows.Count} tài khoản!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show($"⚠ Không tìm thấy tài khoản nào chứa '{keyword}'", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    dgvThongTinTK.Rows.Clear();
                }
            }
            catch (Exception ex){ }
        }

        private void btnTaiLaiTrang_Click_1(object sender, EventArgs e)
        {
                LoadDataFromDatabase();
                ResetForm();
                MessageBox.Show("✔ Tải lại dữ liệu thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);  
        }

        private void btnTaiLaiTK_Click(object sender, EventArgs e)
        {
            LoadDataFromDatabase();
            ResetForm();

        }

        private void txtTimKiem_TextChanged(object sender, EventArgs e)
        {
            var keyword = txtTimKiem.Text.Trim();
            dgvThongTinTK.Rows.Clear();

            if (string.IsNullOrEmpty(keyword))
            {
                LoadDataFromDatabase();
                return;
            }

            var results = busUser.Search(keyword)
                                 .Where(u => VALID_ROLES.Contains(u.Role));

            foreach (var user in results)
            {
                dgvThongTinTK.Rows.Add(user.ID, user.Role, user.TenNguoiDung);
            }

        }
    }
}
