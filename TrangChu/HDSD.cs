using System;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;
using System.Drawing;

namespace TrangChu
{
    public partial class HDSD : Form
    {
        public HDSD()
        {
            InitializeComponent();
            ShowWelcomeMessage();
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            string selectedNode = e.Node.Text;

            switch (selectedNode)
            {
                case "Bắt đầu": ShowWelcomeMessage(); break;
                case "Đăng nhập": ShowLoginGuide(); break;
                case "Quản lý tài khoản": ShowAccountGuide(); break;
                case "Quản lý lịch đặt": ShowBookingGuide(); break;
                case "Thống kê doanh thu": ShowRevenueGuide(); break;
                case "Tổng hợp hóa đơn": ShowInvoiceGuide(); break;
                case "Thoát": ShowExitGuide(); break;
                default:
                    rtbContent.Clear();
                    rtbContent.SelectionFont = new Font("Segoe UI", 12, FontStyle.Italic);
                    rtbContent.AppendText("Nội dung được thực hiện bởi Nhóm 2");
                    break;
            }
        }

        private void SetHeader(string title)
        {
            rtbContent.Clear();
            // Căn giữa tiêu đề và làm cực to
            rtbContent.SelectionAlignment = HorizontalAlignment.Center;
            rtbContent.SelectionFont = new Font("Segoe UI", 22, FontStyle.Bold);
            rtbContent.SelectionColor = Color.DarkBlue;
            rtbContent.AppendText(title.ToUpper() + "\n");

            rtbContent.SelectionAlignment = HorizontalAlignment.Center;
            rtbContent.SelectionColor = Color.LightGray;
            rtbContent.AppendText("__________________________________________________________________________________________\n\n");

            // Trả về căn lề trái cho nội dung bên dưới
            rtbContent.SelectionAlignment = HorizontalAlignment.Left;
        }

        private void ShowWelcomeMessage()
        {
            SetHeader("Giới thiệu hệ thống");
            rtbContent.SelectionFont = new Font("Segoe UI", 14);
            rtbContent.AppendText("Sản phẩm đang trong quá trình thử nghiệm và đang được cập nhật theo thời gian...\n\n");
            rtbContent.AppendText("Đây là hệ thống quản lý chuyên sâu, vui lòng chọn mục bên dưới để xem hướng dẫn đầy đủ bằng file PDF.");

        }

        private void ShowLoginGuide()
        {
            SetHeader("Hướng dẫn Đăng nhập");
            rtbContent.SelectionFont = new Font("Segoe UI", 14);
            rtbContent.AppendText("1. Nhập tên tài khoản vào ô 'Username'.\n");
            rtbContent.AppendText("2. Nhập mật khẩu vào ô 'Password'.\n");
            rtbContent.AppendText("3. Nhấn nút Đăng nhập để truy cập hệ thống.\n\n");

            rtbContent.SelectionFont = new Font("Segoe UI", 15, FontStyle.Bold);
            rtbContent.SelectionColor = Color.Red;
            rtbContent.AppendText("⚠️ LƯU Ý QUÊN MẬT KHẨU:\n");
            rtbContent.SelectionColor = Color.Black;
            rtbContent.SelectionFont = new Font("Segoe UI", 14);
            rtbContent.AppendText("Nếu quên mật khẩu vui lòng liên hệ trực tiếp với QUẢN LÝ để được hỗ trợ cấp lại tài khoản và mật khẩu.");
        }

        private void ShowBookingGuide()
        {
            SetHeader("Quản lý Lịch đặt");
            rtbContent.SelectionFont = new Font("Segoe UI", 14);
            rtbContent.AppendText("• Trạng thái: Di chuột vào các nút sân để xem thông tin nhanh chóng.\n");
            rtbContent.AppendText("•Lưu ý thời gian đặt không được quyền bé hơn thời gian kết thúc và không được đặt thời điểm trong quá khứ.\n");
            rtbContent.AppendText("• Sau khi hoàn tất lịch đặt nhân viên có thể thực hiện tác vụ dịch vụ cho khách hàng.\n");
            rtbContent.AppendText("• Thanh toán: Chỉ thanh toán được cho các lịch có trạng thái 'Đã đặt'.");
        }

        private void ShowAccountGuide() { SetHeader("Quản lý Tài khoản"); rtbContent.AppendText("- Thêm, sửa, xóa tài khoản nhân viên ( chỉ sử dụng cho tài khoản QUẢN LÝ) ."); }
        private void ShowRevenueGuide() { SetHeader("Báo cáo Doanh thu"); rtbContent.AppendText("- Thống kê doanh thu theo tháng hoặc năm bao gồm thông tin đặt và hủy cũng như số lượng sử dụng các sân để xuất dữ liệu ra file Exel."); }
        private void ShowInvoiceGuide() { SetHeader("Hóa đơn"); rtbContent.AppendText("- Xem lại lịch sử thanh toán của khách hàng và những dịch vụ sử dụng nếu có."); }
        private void ShowExitGuide() { SetHeader("Thoát"); rtbContent.AppendText("- Đóng ứng dụng bằng cách nhấn alt + f4 hoặc bấm vào dấu X hiển thị góc trên hoặc ấn nút đóng."); }

        private void btnOpenPDF_Click(object sender, EventArgs e)
        {
            // Chỉnh phần này để thay đổi địa điểm lưu file PDF 
            string filePath = "C:\\Users\\HUY\\OneDrive\\Documents\\GitHub\\QLbongda\\HDSD-QLSanBongNhatVuong.pdf";
            try
            {
                if (File.Exists(filePath)) Process.Start(new ProcessStartInfo(filePath) { UseShellExecute = true });
                else MessageBox.Show("Không tìm thấy file PDF.");
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void rtbContent_TextChanged(object sender, EventArgs e)
        {
        }
    }
}

//HDSD.cs