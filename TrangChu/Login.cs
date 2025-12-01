using BUS;
using DAL;
using System;
using System.Windows.Forms;

namespace TrangChu
{
    public partial class Login : Form
    {
        UserBLL userBLL = new UserBLL();

        public Login()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string user = txtUser.Text.Trim();
            string pass = txtPass.Text.Trim();

            if (user == "" || pass == "")
            {
                MessageBox.Show("Vui lòng nhập đầy đủ!", "Thông báo");
                return;
            }

            // Gọi BLL để kiểm tra User
            User u = userBLL.Login(user, pass);

            if (u != null)
            {
                MessageBox.Show("Đăng nhập thành công!", "Thông báo");

                // Mở form TrangChu + truyền TenNguoiDung hoặc UserName
                TrangChu frm = new TrangChu(u.TenNguoiDung ?? u.UserName);
                frm.Show();

                this.Hide();
            }
            else
            {
                MessageBox.Show("Sai tên đăng nhập hoặc mật khẩu!", "Lỗi đăng nhập");
            }
        }

        private void lblTitle_Click(object sender, EventArgs e)
        {

        }
    }
}
