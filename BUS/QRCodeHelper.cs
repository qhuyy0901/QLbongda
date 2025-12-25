using System;
using System.Drawing;      // ✅ QUAN TRỌNG
using QRCoder;             // ✅ QUAN TRỌNG

namespace BUS
{
    public static class QRCodeHelper
    {
        // ===== THÔNG TIN NGÂN HÀNG MB BANK =====
        private const string BANK_ACCOUNT = "0399750340";
        private const string ACCOUNT_HOLDER = "NGUYEN QUANG HUY";

        /// <summary>
        /// Tạo mã QR cho chuyển khoản MB Bank
        /// </summary>
        public static Bitmap GenerateQRCode(decimal amount, string description = "Thanh toan dich vu")
        {
            try
            {
                string qrContent = $"{ACCOUNT_HOLDER}|{BANK_ACCOUNT}|{(long)amount}|{description}";

                using (QRCodeGenerator qrGenerator = new QRCodeGenerator())
                using (QRCodeData qrCodeData = qrGenerator.CreateQrCode(qrContent, QRCodeGenerator.ECCLevel.Q))
                using (QRCode qrCode = new QRCode(qrCodeData))
                {
                    return qrCode.GetGraphic(20);
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"❌ Lỗi tạo mã QR: {ex.Message}");
            }
        }

        /// <summary>
        /// Lấy thông tin chuyển khoản để hiển thị
        /// </summary>
        public static string GetTransferInfo(decimal amount)
        {
            return $"===== THÔNG TIN CHUYỂN KHOẢN =====\n" +
                   $"Ngân hàng: MB Bank\n" +
                   $"Số tài khoản: {BANK_ACCOUNT}\n" +
                   $"Tên tài khoản: {ACCOUNT_HOLDER}\n" +
                   $"Số tiền: {amount:N0} VNĐ\n" +
                   $"====================================";
        }
    }
}