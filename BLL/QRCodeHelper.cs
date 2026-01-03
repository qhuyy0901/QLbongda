using System;
using System.Drawing;
using System.Text;
using System.Text.RegularExpressions;
using QRCoder;

namespace BUS
{
    /// <summary>
    /// Hỗ trợ tạo mã VietQR theo chuẩn NAPAS 247
    /// Tích hợp thanh toán chuyển khoản ngân hàng
    /// </summary>
    public static class QRCodeHelper
    {
        // ===== THÔNG TIN TÀI KHOẢN THỤ HƯỞNG (CẤU HÌNH TẠI ĐÂY) =====
        // Tra cứu mã BIN ngân hàng tại: https://vietqr.io/danh-sach-api/danh-sach-ngan-hang/
        private const string BANK_BIN = "970422";     
        private const string ACCOUNT_NUMBER = "0399750340";  
        private const string ACCOUNT_NAME = "NGUYEN QUANG HUY"; 

        /// <summary>
        /// Tạo ảnh mã QR VietQR
        /// </summary>
        /// <param name="amount">Số tiền cần thanh toán</param>
        /// <param name="content">Nội dung chuyển khoản</param>
        /// <returns>Bitmap hình ảnh QR</returns>
        public static Bitmap GenerateQRCode(decimal amount, string content)
        {
            try
            {
                string qrPayload = BuildVietQRString(amount, content);

                using (QRCodeGenerator qrGenerator = new QRCodeGenerator())
                using (QRCodeData qrCodeData = qrGenerator.CreateQrCode(qrPayload, QRCodeGenerator.ECCLevel.M))
                using (QRCode qrCode = new QRCode(qrCodeData))
                {
                    return qrCode.GetGraphic(20); // 20 là độ phân giải pixel/module
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi tạo mã QR: {ex.Message}");
            }
        }

        /// <summary>
        /// Xây dựng chuỗi VietQR theo chuẩn EMVCo
        /// </summary>
        private static string BuildVietQRString(decimal amount, string content)
        {
            string cleanContent = ConvertToUnSign(content);
            if (cleanContent.Length > 50) cleanContent = cleanContent.Substring(0, 50);

            StringBuilder sb = new StringBuilder();

            sb.Append(TLV("00", "01"));

            sb.Append(TLV("01", amount > 0 ? "12" : "11"));

            string bankInfo = "";
            bankInfo += TLV("00", "A000000727");
            bankInfo += TLV("01",               
                TLV("00", BANK_BIN) +            // Mã BIN Ngân hàng
                TLV("01", ACCOUNT_NUMBER)        // Số tài khoản
            );
            bankInfo += TLV("02", "QRIBFTTA");   // Service Code Chuyển khoản nhanh

            sb.Append(TLV("38", bankInfo));

            sb.Append(TLV("53", "704"));

            if (amount > 0)
            {
                sb.Append(TLV("54", ((long)amount).ToString()));
            }

            sb.Append(TLV("58", "VN"));

            if (!string.IsNullOrEmpty(cleanContent))
            {
                string addInfo = TLV("08", cleanContent); // 08 là Tag cho nội dung chuyển khoản
                sb.Append(TLV("62", addInfo));
            }

            string dataToCrc = sb.ToString() + "6304"; // Thêm ID và Length của CRC
            string crcCode = ComputeCRC16(dataToCrc);

            return dataToCrc + crcCode;
        }

        // ===== CÁC HÀM BỔ TRỢ (HELPER) =====

        private static string TLV(string id, string value)
        {
            if (string.IsNullOrEmpty(value)) return "";
            return $"{id}{value.Length:00}{value}";
        }

        // Tính toán CRC16 (CCITT-FALSE)
        private static string ComputeCRC16(string data)
        {
            ushort crc = 0xFFFF;
            byte[] bytes = Encoding.ASCII.GetBytes(data);

            foreach (byte b in bytes)
            {
                crc ^= (ushort)(b << 8);
                for (int i = 0; i < 8; i++)
                {
                    if ((crc & 0x8000) > 0)
                        crc = (ushort)((crc << 1) ^ 0x1021);
                    else
                        crc <<= 1;
                }
            }
            return crc.ToString("X4"); 
        }

        // Hàm chuyển tiếng Việt có dấu thành không dấu (Quan trọng cho App ngân hàng)
        public static string ConvertToUnSign(string s)
        {
            if (string.IsNullOrEmpty(s)) return string.Empty;

            Regex regex = new Regex("\\p{IsCombiningDiacriticalMarks}+");
            string temp = s.Normalize(NormalizationForm.FormD);
            string unsigned = regex.Replace(temp, String.Empty).Replace('\u0111', 'd').Replace('\u0110', 'D');

            // Chỉ giữ lại chữ cái, số và khoảng trắng
            return Regex.Replace(unsigned, @"[^a-zA-Z0-9 ]", "");
        }
    }
}