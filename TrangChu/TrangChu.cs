using System;
using System.Windows.Forms;

namespace TrangChu
{
    public partial class TrangChu : Form
    {
        private string tenDangNhap;

        // Constructor NHẬN tên đăng nhập
        public TrangChu(string ten)
        {
            InitializeComponent();
            tenDangNhap = ten;

            // Bắt sự kiện Load (phòng khi Designer chưa gán)
            this.Load += TrangChu_Load;
        }

        // Sự kiện Load của Form
        private void TrangChu_Load(object sender, EventArgs e)
        {
            // LƯU Ý: lblWelcome phải tồn tại trong Designer
            lblUserName.Text = "Xin chào! " + tenDangNhap;
        }
    }
}
