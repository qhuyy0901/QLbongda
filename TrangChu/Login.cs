using BUS;
using DAL;
using System;
using System.Windows.Forms;

namespace TrangChu
{
    public partial class Login : Form
    {
        // Sử dụng UserBUS (Entity Framework)
        UserBUS userBUS = new UserBUS();

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
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin!", "Thông báo");
                return;
            }

            // Gọi BUS kiểm tra đăng nhập
            User u = userBUS.Login(user, pass);

            if (u != null)
            {
                MessageBox.Show("Đăng nhập thành công!", "Thông báo");

                this.Hide(); // Ẩn form Login

                // --- LOGIC MỚI ---
                // Mở TrangChu và gửi kèm đối tượng 'u' (chứa Role, UserName...)
                TrangChu frmMain = new TrangChu(u);

                frmMain.ShowDialog(); // Chờ TrangChu xử lý xong (Đăng xuất)

                // Khi TrangChu đóng lại, hiện lại Login và xóa mật khẩu cũ
                this.Show();
                txtPass.Text = "";
                txtUser.Focus();
            }
            else
            {
                MessageBox.Show("Sai tên đăng nhập hoặc mật khẩu!", "Lỗi đăng nhập");
            }
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            Register frm = new Register();
            this.Hide();
            frm.ShowDialog();
            this.Show();
        }

        private void Login_Load(object sender, EventArgs e)
        {
        }
    }
}