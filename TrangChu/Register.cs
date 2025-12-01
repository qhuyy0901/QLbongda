using BUS;
using DAL;
using System;
using System.Windows.Forms;

namespace TrangChu
{
    public partial class Register : Form
    {
        // Gọi lớp nghiệp vụ
        UserBUS UserBUS = new UserBUS();

        public Register()
        {
            InitializeComponent();
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            // 1. Lấy dữ liệu từ giao diện
            string user = txtUser.Text.Trim();
            string pass = txtPass.Text.Trim();
            string confirmPass = txtConfirmPass.Text.Trim();
            string tenNguoiDung = txtTenNguoiDung.Text.Trim();
            string sdt = txtSDT.Text.Trim();

            // 2. Kiểm tra nhập thiếu
            if (string.IsNullOrEmpty(user) || string.IsNullOrEmpty(pass))
            {
                MessageBox.Show("Vui lòng nhập Tên đăng nhập và Mật khẩu!", "Thông báo");
                return;
            }

            // 3. Kiểm tra mật khẩu nhập lại
            if (pass != confirmPass)
            {
                MessageBox.Show("Mật khẩu xác nhận không khớp!", "Lỗi");
                return;
            }

            // 4. Kiểm tra trùng tên đăng nhập
            if (UserBUS.CheckUsernameExist(user))
            {
                MessageBox.Show("Tên đăng nhập đã tồn tại!", "Cảnh báo");
                txtUser.Focus();
                return;
            }

            try
            {
                // 5. Tạo đối tượng User
                User newUser = new User()
                {
                    UserName = user,
                    Password = pass,
                    TenNguoiDung = string.IsNullOrEmpty(tenNguoiDung) ? user : tenNguoiDung,
                    SDT = sdt,

                    // --- QUAN TRỌNG: Mặc định đăng ký là NHÂN VIÊN ---
                    Role = "NhanVien",

                 
                };

                // 6. Lưu xuống Database
                UserBUS.AddUser(newUser);

                MessageBox.Show("Đăng ký thành công! Bạn có thể đăng nhập với quyền Nhân Viên.", "Thông báo");
                this.Close(); // Đóng form Đăng ký về lại Login
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi hệ thống: " + ex.Message);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}