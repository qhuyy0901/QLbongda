using BUS;
using DAL;
using System;
using System.Windows.Forms;

namespace TrangChu
{
    public partial class Register : Form
    {
        // Gọi lớp BUS
        UserBUS UserBUS = new UserBUS();

        public Register()
        {
            InitializeComponent();
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            // Lấy dữ liệu từ TextBox
            string user = txtUser.Text.Trim();
            string pass = txtPass.Text.Trim();
            string confirmPass = txtConfirmPass.Text.Trim();
            string tenNguoiDung = txtTenNguoiDung.Text.Trim();
            string sdt = txtSDT.Text.Trim();

            // --- 1. VALIDATION (Kiểm tra nhập liệu) ---
            if (string.IsNullOrEmpty(user) || string.IsNullOrEmpty(pass))
            {
                MessageBox.Show("Vui lòng nhập Tên đăng nhập và Mật khẩu!", "Cảnh báo");
                return;
            }

            if (pass != confirmPass)
            {
                MessageBox.Show("Mật khẩu xác nhận không khớp!", "Lỗi");
                return;
            }

            // --- 2. LOGIC KIỂM TRA TRÙNG (Gọi BUS) ---
            if (UserBUS.CheckUsernameExist(user))
            {
                MessageBox.Show("Tên đăng nhập này đã tồn tại! Vui lòng chọn tên khác.", "Lỗi trùng lặp");
                txtUser.Focus(); // Đưa con trỏ chuột về ô User
                return;
            }

            // --- 3. THỰC HIỆN LƯU (Gọi BUS) ---
            try
            {
                User newUser = new User()
                {
                    UserName = user,
                    Password = pass,
                    TenNguoiDung = string.IsNullOrEmpty(tenNguoiDung) ? user : tenNguoiDung, // Nếu không nhập tên thì lấy UserName làm tên
                    SDT = sdt,
                    Role = "KhachHang", // Mặc định là khách hàng (KHÔNG để là admin/chu)
                };

                UserBUS.AddUser(newUser);

                MessageBox.Show("Đăng ký thành công! Hãy đăng nhập ngay.", "Thông báo");
                this.Close(); // Đóng form Đăng ký để quay về Login
            }
            catch (Exception ex)
            {
                MessageBox.Show("Có lỗi xảy ra: " + ex.Message);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}