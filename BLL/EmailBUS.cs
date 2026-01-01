using System;
using System.Net;
using System.Net.Mail;

namespace BUS
{
    /// <summary>
    /// Dịch vụ gửi email thông báo thanh toán
    /// </summary>
    public static class EmailBUS
    {
        // ===== CẤU HÌNH EMAIL =====
        private const string SMTP_HOST = "smtp.gmail.com";
        private const int SMTP_PORT = 587;
        private const string SENDER_EMAIL = "qhuyy0901@gmail.com";
        private const string SENDER_PASSWORD = "mkui tshw tnzy ikux"; // App Password
        private const string SENDER_NAME = "Hệ Thống Quản Lý Sân Bóng";

        /// <summary>
        /// Gửi email thông báo thanh toán hóa đơn
        /// </summary>
        public static bool SendPaymentNotification(
            string toEmail,
            string tenKH,
            string maLich,
            string maSan,
            string khungGio,
            decimal tienSan,
            decimal tienDichVu,
            decimal tongTien,
            string hinhThucTT,
            DateTime thoiGianTT)
        {
            try
            {
                // ===== KIỂM TRA EMAIL HỢP LỆ =====
                if (string.IsNullOrWhiteSpace(toEmail) || !IsValidEmail(toEmail))
                {
                    System.Diagnostics.Debug.WriteLine($"❌ Email không hợp lệ: {toEmail}");
                    return false;
                }

                // ===== TẠO NỘI DUNG EMAIL =====
                string subject = $"🎉 Xác Nhận Thanh Toán - {SENDER_NAME}";
                string body = BuildEmailBody(tenKH, maLich, maSan, khungGio, tienSan, tienDichVu, tongTien, hinhThucTT, thoiGianTT);

                // ===== CẤU HÌNH SMTP CLIENT =====
                using (SmtpClient smtpClient = new SmtpClient(SMTP_HOST, SMTP_PORT))
                {
                    smtpClient.EnableSsl = true;
                    smtpClient.UseDefaultCredentials = false;
                    smtpClient.Credentials = new NetworkCredential(SENDER_EMAIL, SENDER_PASSWORD);
                    smtpClient.Timeout = 20000; // 20 giây

                    // ===== TẠO EMAIL MESSAGE =====
                    using (MailMessage mailMessage = new MailMessage())
                    {
                        mailMessage.From = new MailAddress(SENDER_EMAIL, SENDER_NAME);
                        mailMessage.To.Add(toEmail);
                        mailMessage.Subject = subject;
                        mailMessage.Body = body;
                        mailMessage.IsBodyHtml = true;
                        mailMessage.Priority = MailPriority.Normal;

                        // ===== GỬI EMAIL =====
                        smtpClient.Send(mailMessage);
                        System.Diagnostics.Debug.WriteLine($"✅ Đã gửi email thành công đến: {toEmail}");
                        return true;
                    }
                }
            }
            catch (SmtpException smtpEx)
            {
                System.Diagnostics.Debug.WriteLine($"❌ Lỗi SMTP: {smtpEx.Message}");
                return false;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"❌ Lỗi gửi email: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// Xây dựng nội dung HTML email
        /// </summary>
        private static string BuildEmailBody(
            string tenKH,
            string maLich,
            string maSan,
            string khungGio,
            decimal tienSan,
            decimal tienDichVu,
            decimal tongTien,
            string hinhThucTT,
            DateTime thoiGianTT)
        {
            return $@"
<!DOCTYPE html>
<html>
<head>
    <meta charset='UTF-8'>
    <style>
        body {{ font-family: Arial, sans-serif; background-color: #f4f4f4; margin: 0; padding: 20px; }}
        .container {{ max-width: 600px; margin: 0 auto; background-color: #ffffff; border-radius: 8px; overflow: hidden; box-shadow: 0 2px 10px rgba(0,0,0,0.1); }}
        .header {{ background-color: #4CAF50; color: white; padding: 20px; text-align: center; }}
        .content {{ padding: 30px; }}
        .invoice-detail {{ background-color: #f9f9f9; padding: 15px; border-radius: 5px; margin: 15px 0; }}
        .detail-row {{ display: flex; justify-content: space-between; margin: 8px 0; }}
        .detail-label {{ font-weight: bold; color: #555; }}
        .detail-value {{ color: #333; }}
        .total-row {{ border-top: 2px solid #4CAF50; margin-top: 10px; padding-top: 10px; font-size: 18px; font-weight: bold; color: #4CAF50; }}
        .footer {{ background-color: #f1f1f1; padding: 15px; text-align: center; font-size: 12px; color: #777; }}
    </style>
</head>
<body>
    <div class='container'>
        <div class='header'>
            <h1>⚽ {SENDER_NAME}</h1>
            <p>Xác Nhận Thanh Toán</p>
        </div>
        
        <div class='content'>
            <h2>Xin chào {tenKH},</h2>
            <p>Cảm ơn bạn đã sử dụng dịch vụ của chúng tôi! Dưới đây là thông tin thanh toán của bạn:</p>
            
            <div class='invoice-detail'>
                <div class='detail-row'>
                    <span class='detail-label'>📋 Mã lịch:</span>
                    <span class='detail-value'>{maLich}</span>
                </div>
                <div class='detail-row'>
                    <span class='detail-label'>⚽ Sân:</span>
                    <span class='detail-value'>{maSan}</span>
                </div>
                <div class='detail-row'>
                    <span class='detail-label'>🕒 Khung giờ:</span>
                    <span class='detail-value'>{khungGio}</span>
                </div>
                <div class='detail-row'>
                    <span class='detail-label'>💰 Tiền sân:</span>
                    <span class='detail-value'>{tienSan:N0} VNĐ</span>
                </div>
                <div class='detail-row'>
                    <span class='detail-label'>🍔 Tiền dịch vụ:</span>
                    <span class='detail-value'>{tienDichVu:N0} VNĐ</span>
                </div>
                <div class='detail-row total-row'>
                    <span>TỔNG THANH TOÁN:</span>
                    <span>{tongTien:N0} VNĐ</span>
                </div>
                <div class='detail-row'>
                    <span class='detail-label'>💳 Hình thức thanh toán:</span>
                    <span class='detail-value'>{hinhThucTT}</span>
                </div>
                <div class='detail-row'>
                    <span class='detail-label'>📅 Thời gian thanh toán:</span>
                    <span class='detail-value'>{thoiGianTT:dd/MM/yyyy HH:mm}</span>
                </div>
            </div>
            
            <p style='color: #4CAF50; font-weight: bold;'>✅ Thanh toán thành công!!</p>
            <p>Chúc bạn có trải nghiệm tuyệt vời tại sân của chúng tôi!</p>
        </div>
        
        <div class='footer'>
            <p>© 2025 {SENDER_NAME}. All rights reserved.</p>
            <p>Email này được gửi tự động, vui lòng không trả lời!!!.</p>
        </div>
    </div>
</body>
</html>";
        }

        /// <summary>
        /// Kiểm tra email hợp lệ
        /// </summary>
        private static bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }
    }
}