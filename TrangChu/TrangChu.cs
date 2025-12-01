using DAL;
using System;
using System.Windows.Forms;

namespace TrangChu
{
    public partial class TrangChu : Form
    {
        private User currentUser;

        // Biến cờ để kiểm tra xem người dùng bấm Đăng xuất hay bấm X
        // Mặc định là True (tức là đang hiểu là muốn thoát chương trình)
        private bool isThoat = true;

        public TrangChu(User user)
        {
            InitializeComponent();
            this.currentUser = user;

            // Gán các sự kiện
            this.Load += TrangChu_Load;

            // QUAN TRỌNG: Gán sự kiện khi form chuẩn bị đóng
            this.FormClosing += TrangChu_FormClosing;
        }

        private void TrangChu_Load(object sender, EventArgs e)
        {
            if (currentUser != null)
            {
                lblUserName.Text = "Xin chào: " + (currentUser.TenNguoiDung ?? currentUser.UserName);
                PhanQuyen();
            }
        }

        private void PhanQuyen()
        {
            string role = currentUser.Role.Trim().ToLower();

            if (role == "admin" || role == "chu" || role == "quantri")
            {
                if (btnDoanhThu != null) btnDoanhThu.Visible = true;
                if (btnQuanLyNV != null) btnQuanLyNV.Visible = true;
            }
            else
            {
                if (btnDoanhThu != null) btnDoanhThu.Visible = false;
                if (btnQuanLyNV != null) btnQuanLyNV.Visible = false;
            }
        }

        // --- SỰ KIỆN NÚT ĐĂNG XUẤT ---
        private void bntDangXuat_Click(object sender, EventArgs e)
        {
            // Hỏi xác nhận đăng xuất
            DialogResult result = MessageBox.Show(
                "Bạn có muốn đăng xuất để về màn hình đăng nhập không?",
                "Đăng xuất",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (result == DialogResult.Yes)
            {
                // Đánh dấu là đang Đăng xuất (không phải Thoát app)
                isThoat = false;
                this.Close(); // Đóng form này -> Code bên Login sẽ chạy tiếp để hiện form Login
            }
        }

        // --- SỰ KIỆN KHI BẤM DẤU X (HOẶC ALT+F4) ---
        private void TrangChu_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Trường hợp 1: Nếu người dùng bấm nút Đăng xuất (isThoat = false)
            if (isThoat == false)
            {
                // Cho phép đóng form bình thường để quay về Login
                return;
            }

            // Trường hợp 2: Người dùng bấm dấu X (isThoat vẫn là true)
            DialogResult result = MessageBox.Show(
                "Bạn có chắc chắn muốn thoát hoàn toàn chương trình không?",
                "Cảnh báo",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning
            );

            if (result == DialogResult.No)
            {
                // Nếu chọn NO -> Hủy lệnh đóng form (Form giữ nguyên)
                e.Cancel = true;
            }
            else
            {
                // Nếu chọn YES -> Thoát hoàn toàn ứng dụng (kể cả form Login đang ẩn)
                Environment.Exit(0);
            }
        }
    }
}