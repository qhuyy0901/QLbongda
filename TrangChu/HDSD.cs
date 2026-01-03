using System;
using System.Drawing;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;
using System.Drawing.Printing;

namespace TrangChu
{
    public partial class HDSD : Form
    {
        public HDSD()
        {
            InitializeComponent();
        }

        private void HDSD_Load(object sender, EventArgs e)
        {
            InitMenu();
            ShowWelcomeMessage();
        }

        private void InitMenu()
        {
            treeView1.Nodes.Clear();

            TreeNode nStart = new TreeNode("🚀 BẮT ĐẦU SỬ DỤNG");
            nStart.Nodes.Add("Đăng nhập hệ thống");
            nStart.Nodes.Add("Thoát ứng dụng");

            TreeNode nManage = new TreeNode("⚙️ QUẢN LÝ NGHIỆP VỤ");
            nManage.Nodes.Add("Quản lý lịch đặt sân");

            TreeNode nReport = new TreeNode("📊 BÁO CÁO & DOANH THU");
            nReport.Nodes.Add("Hóa đơn thanh toán");
            nReport.Nodes.Add("Thống kê doanh thu");

            treeView1.Nodes.AddRange(new TreeNode[] { nStart, nManage, nReport });
            treeView1.ExpandAll();
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            string selected = e.Node.Text;
            switch (selected)
            {
                case "🚀 BẮT ĐẦU SỬ DỤNG": ShowWelcomeMessage(); break;
                case "Đăng nhập hệ thống": ShowLoginDetail(); break;
                case "Thoát ứng dụng": ShowExitDetail(); break;
                // Case "Quản lý tài khoản" đã bị xóa
                case "Quản lý lịch đặt sân": ShowBookingDetail(); break;
                case "Hóa đơn thanh toán": ShowInvoiceDetail(); break;
                case "Thống kê doanh thu": ShowRevenueDetail(); break;
            }
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            PrintDialog printDialog = new PrintDialog();
            PrintDocument printDoc = new PrintDocument();
            printDoc.DocumentName = "HuongDan_NhatVuong";
            printDoc.PrintPage += new PrintPageEventHandler(PrintPageContent);
            printDialog.Document = printDoc;

            if (printDialog.ShowDialog() == DialogResult.OK)
            {
                printDoc.Print();
            }
        }

        private void PrintPageContent(object sender, PrintPageEventArgs e)
        {
            string content = rtbContent.Text;
            Font font = new Font("Segoe UI", 12, FontStyle.Regular);
            float x = e.MarginBounds.Left;
            float y = e.MarginBounds.Top;

            e.Graphics.DrawString("TÀI LIỆU HƯỚING DẪN NGHIỆP VỤ - SÂN BÓNG NHẬT VƯƠNG",
                new Font("Segoe UI", 14, FontStyle.Bold), Brushes.Blue, x, y);
            y += 40;

            e.Graphics.DrawString(content, font, Brushes.Black,
                new RectangleF(x, y, e.MarginBounds.Width, e.MarginBounds.Height - 40));
        }

        #region Helper Methods (Định dạng nội dung)
        private void SetHeader(string title, string subtitle)
        {
            rtbContent.Clear();
            rtbContent.SelectionAlignment = HorizontalAlignment.Left;
            rtbContent.SelectionFont = new Font("Segoe UI", 24, FontStyle.Bold);
            rtbContent.SelectionColor = Color.FromArgb(0, 122, 204);
            rtbContent.AppendText(title.ToUpper() + "\n");

            rtbContent.SelectionFont = new Font("Segoe UI", 11, FontStyle.Italic);
            rtbContent.SelectionColor = Color.Gray;
            rtbContent.AppendText(subtitle + "\n");
            rtbContent.SelectionColor = Color.LightGray;
            rtbContent.AppendText("________________________________________________________________________________\n\n");
        }

        private void AddBullet(string boldPart, string detail)
        {
            rtbContent.SelectionBullet = true;
            rtbContent.BulletIndent = 20;
            rtbContent.SelectionFont = new Font("Segoe UI", 12, FontStyle.Bold);
            rtbContent.SelectionColor = Color.Black;
            rtbContent.AppendText(boldPart + ": ");

            rtbContent.SelectionFont = new Font("Segoe UI", 12, FontStyle.Regular);
            rtbContent.AppendText(detail + "\n");
            rtbContent.SelectionBullet = false;
        }

        private void AddSubTitle(string text)
        {
            rtbContent.AppendText("\n");
            rtbContent.SelectionFont = new Font("Segoe UI", 14, FontStyle.Bold);
            rtbContent.SelectionColor = Color.FromArgb(45, 45, 48);
            rtbContent.AppendText(text + "\n");
        }

        private void ShowWelcomeMessage()
        {
            SetHeader("Chào mừng bạn", "Hệ thống quản lý sân bóng đá Nhật Vượng v1.0");
            rtbContent.SelectionFont = new Font("Segoe UI", 13);
            rtbContent.AppendText("Đây là công cụ hỗ trợ nhân viên vận hành chuyên nghiệp. Hãy chọn mục menu bên trái để xem hướng dẫn nghiệp vụ.\n\n");

            AddSubTitle("🌟 Các tính năng cốt lõi:");
            AddBullet("Tối ưu hóa đặt lịch", "Tránh trùng lịch đá, quản lý sân trống thời gian thực.");
            AddBullet("Quản lý tài chính", "Tự động tính tiền ca đá, quản lý hóa đơn và dịch vụ.");
            AddBullet("Báo cáo thông minh", "Xuất file Excel thống kê doanh thu chính xác.");
        }

        private void ShowLoginDetail()
        {
            SetHeader("Hướng dẫn Đăng nhập", "Truy cập hệ thống an toàn");
            AddSubTitle("1. Quy trình thực hiện");
            AddBullet("Username", "Nhập tên đăng nhập được quản lý cấp phát (Ví dụ: nhanvien_01).");
            AddBullet("Password", "Nhập mật khẩu (Kiểm tra kỹ Unikey/Caps Lock).");
            AddBullet("Xác nhận", "Nhấn Enter hoặc nút 'Login' để vào màn hình chính.");

            AddSubTitle("2. Lưu ý bảo mật");
            rtbContent.SelectionColor = Color.Red;
            rtbContent.AppendText("• Tuyệt đối không chia sẻ tài khoản cá nhân.\n");
            rtbContent.AppendText("• Nếu quên mật khẩu, liên hệ trực tiếp QUẢN LÝ để đặt lại.\n");
        }

        private void ShowBookingDetail()
        {
            SetHeader("Quản lý Lịch đặt sân", "Nghiệp vụ cốt lõi");
            AddSubTitle("1. Sơ đồ màu sắc sân bóng");
            AddBullet("Màu xanh", "Sân trống, sẵn sàng tiếp nhận khách ngay.");
            AddBullet("Màu đỏ", "Sân đang có khách đá (Di chuột vào để xem giờ bắt đầu).");
            AddBullet("Màu vàng", "Sân đã đặt trước (Đang chờ khách đến).");

            AddSubTitle("2. Quy trình đặt sân");
            AddBullet("Bước 1", "Chọn sân trống và khung giờ khách yêu cầu.");
            AddBullet("Bước 2", "Nhập SĐT khách (Hệ thống tự hiện tên nếu là khách quen).");
            AddBullet("Bước 3", "Chọn khung giờ đá (60, 90 hoặc 120 phút).");
            AddBullet("Bước 4", "Nhấn 'Lưu đặt sân'.");

            AddSubTitle("3. Lưu ý quan trọng");
            rtbContent.SelectionColor = Color.DarkGreen;
            rtbContent.AppendText("• Thời gian bắt đầu không được nhỏ hơn thời gian hiện tại.\n");
            rtbContent.AppendText("• Sau khi lưu, nhân viên có thể thêm dịch vụ nước uống vào hóa đơn sân đó.");
        }

        private void ShowRevenueDetail()
        {
            SetHeader("Thống kê Doanh thu", "Phân tích kinh doanh");
            AddSubTitle("1. Bộ lọc báo cáo");
            AddBullet("Theo ngày/tháng", "Tổng hợp doanh thu để chốt ca trực.");
            AddSubTitle("2. Xuất dữ liệu");
            rtbContent.AppendText("Nhấn nút 'Xuất Excel' để lưu trữ báo cáo ngoại tuyến.\n");
        }

        private void ShowInvoiceDetail()
        {
            SetHeader("Hóa đơn thanh toán", "Lịch sử giao dịch");
            AddSubTitle("1. Kiểm tra hóa đơn");
            rtbContent.AppendText("Hóa đơn tự động sinh: Tiền sân + Tiền dịch vụ phát sinh.\n");
            AddBullet("Tra cứu", "Tìm lại hóa đơn theo Mã HD hoặc tên khách hàng.");
        }

        private void ShowExitDetail()
        {
            SetHeader("Thoát ứng dụng", "Đóng ca trực");
            AddBullet("Đăng xuất", "Sử dụng khi hết ca để bàn giao tài khoản cho người mới.");
            AddBullet("Thoát nhanh", "Sử dụng phím Alt + F4 hoặc nút X.");
        }
        #endregion

        private void btnOpenPDF_Click(object sender, EventArgs e)
        {
            string filePath = @"C:\Users\HUY\OneDrive\Documents\GitHub\QLbongda\HDSD-QLSanBongNhatVuong.pdf";
            try
            {
                if (File.Exists(filePath))
                    Process.Start(new ProcessStartInfo(filePath) { UseShellExecute = true });
                else
                    MessageBox.Show("Không tìm thấy file PDF tại: " + filePath, "Thông báo lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex) { MessageBox.Show("Lỗi hệ thống: " + ex.Message); }
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}