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
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string user = txtUser.Text.Trim();
            string pass = txtPass.Text.Trim();

            if (string.IsNullOrEmpty(user) || string.IsNullOrEmpty(pass))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin!", "Thông báo");
                return;
            }

            try
            {
                // Gọi BUS kiểm tra
                User u = userBUS.Login(user, pass);

                if (u != null)
                {
                    MessageBox.Show("Đăng nhập thành công!", "Thông báo");
                    this.Hide();

                    // Mở trang chủ
                    TrangChu frmMain = new TrangChu(u);
                    frmMain.ShowDialog();

                    // Khi đăng xuất quay lại đây
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
                // QUAN TRỌNG: Hiển thị lỗi kết nối nếu có
                MessageBox.Show("Lỗi kết nối CSDL: " + ex.Message + "\n" + ex.InnerException?.Message, "Lỗi Hệ Thống");
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }


    }
}