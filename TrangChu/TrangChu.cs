using DAL;
using System;
using System.Windows.Forms;

namespace TrangChu
{
    public partial class TrangChu : Form
    {
        private User currentUser;
        private bool isThoat = true; // Cờ kiểm tra thoát

        public TrangChu(User user)
        {
            InitializeComponent();
            this.currentUser = user;

            this.Load += TrangChu_Load;
            this.FormClosing += TrangChu_FormClosing;
        }

        private void TrangChu_Load(object sender, EventArgs e)
        {
            if (currentUser != null)
            {
                lblUserName.Text = "Xin chào: " + (currentUser.TenNguoiDung ?? currentUser.ID);
                PhanQuyen();
            }
        }

        private void PhanQuyen()
        {
            // Chuẩn hóa Role về chữ thường
            string role = currentUser.Role.Trim().ToLower();

            // Nếu là Admin / Chủ / Quản trị -> Hiện tất cả
            if (role == "admin" || role == "quantri")
            {
                if (btnDoanhThu != null) btnDoanhThu.Visible = true;
                if (btnQuanLyNV != null) btnQuanLyNV.Visible = true;
            }
            else // Nhân viên -> Ẩn nút quản lý
            {
                if (btnDoanhThu != null) btnDoanhThu.Visible = false;
                if (btnQuanLyNV != null) btnQuanLyNV.Visible = false;
            }
        }

        private void bntDangXuat_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show(
                "Bạn có muốn đăng xuất không?", "Đăng xuất",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                isThoat = false; // Đánh dấu là Đăng xuất, không phải Thoát App
                this.Close();
            }
        }

        private void TrangChu_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (isThoat) // Nếu bấm dấu X
            {
                DialogResult result = MessageBox.Show(
                    "Bạn có chắc chắn muốn thoát chương trình?", "Cảnh báo",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (result == DialogResult.No)
                {
                    e.Cancel = true; // Hủy lệnh đóng
                }
                else
                {
                    Environment.Exit(0); // Thoát hoàn toàn
                }
            }
        }

        private void btnDatSan_Click(object sender, EventArgs e)
        {
            this.Hide();
            LichDat frm = new LichDat();
            frm.ShowDialog();
            this.Show();
        }


    }
}