using BUS;
using DAL;
using System;
using System.Windows.Forms;

namespace TrangChu
{
    public partial class Login : Form
    {   
        UserBUS userBUS = new UserBUS();

        public Login()
        {
            // khởi tạo giao diện - sự kiện hiện ẩn pass 
            InitializeComponent();
            chkShowPassword.CheckedChanged += chkShowPassword_CheckedChanged;
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            //lấy thông tin từ GUI ( kiểu giá trị trên các bảng text)
            string user = txtUser.Text.Trim();
            string pass = txtPass.Text.Trim();

            if (string.IsNullOrEmpty(user) || string.IsNullOrEmpty(pass))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin!", "Thông báo");
                return;
            }

            try
            {
                // Gọi BUS kiểm tra + login
                User u = userBUS.Login(user, pass);

                if (u != null)
                {
                    MessageBox.Show("Đăng nhập thành công!", "Thông báo");
                    this.Hide();

                    TrangChu frmMain = new TrangChu(u);
                    frmMain.ShowDialog();

                    this.Show();
                    txtPass.Text = "";
                    txtUser.Focus();
                }
                else
                {
                    MessageBox.Show("Sai tên đăng nhập hoặc mật khẩu!", "Lỗi đăng nhập");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi kết nối CSDL: " + ex.Message + "\n" + ex.InnerException?.Message, "Lỗi Hệ Thống");
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void Login_Load(object sender, EventArgs e)
        {
            txtPass.UseSystemPasswordChar = true;
            txtPass.PasswordChar = '*';

            try
            {                        // +lấy toàn bộ user
                var testUser = userBUS.GetAll();
                if (testUser == null)
                {
                    throw new Exception("Không thể kết nối database!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("⚠️ Lỗi kết nối CSDL:\n" + ex.Message, "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
            }
        }

        private void chkShowPassword_CheckedChanged(object sender, EventArgs e)
        {
            txtPass.UseSystemPasswordChar = !chkShowPassword.Checked;
            txtPass.PasswordChar = txtPass.UseSystemPasswordChar ? '*' : '\0';
        }
    }
}