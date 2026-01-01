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
        private const string BANK_BIN = "970422";      // Mã BIN của MB Bank
        private const string ACCOUNT_NUMBER = "0399750340";  // Số tài khoản
        private const string ACCOUNT_NAME = "NGUYEN QUANG HUY"; // Tên chủ tài khoản (Viết hoa không dấu)

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
                // 1. Tạo chuỗi dữ liệu VietQR raw
                string qrPayload = BuildVietQRString(amount, content);

                // 2. Sử dụng thư viện QRCoder để vẽ ảnh
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
            // Xử lý nội dung: Bỏ dấu tiếng Việt, ký tự đặc biệt
            string cleanContent = ConvertToUnSign(content);
            // Giới hạn độ dài nội dung (tối đa khoảng 50 ký tự để đảm bảo QR không quá dày)
            if (cleanContent.Length > 50) cleanContent = cleanContent.Substring(0, 50);

            StringBuilder sb = new StringBuilder();

            // 00: Payload Format Indicator (01)
            sb.Append(TLV("00", "01"));

            // 01: Point of Initiation Method (11: Tĩnh, 12: Động - có số tiền)
            sb.Append(TLV("01", amount > 0 ? "12" : "11"));

            // 38: Merchant Account Information (Cấu trúc định danh tài khoản)
            string bankInfo = "";
            bankInfo += TLV("00", "A000000727"); // GUID VietQR
            bankInfo += TLV("01",                // Beneficiary Organization
                TLV("00", BANK_BIN) +            // Mã BIN Ngân hàng
                TLV("01", ACCOUNT_NUMBER)        // Số tài khoản
            );
            bankInfo += TLV("02", "QRIBFTTA");   // Service Code (Chuyển khoản nhanh)

            sb.Append(TLV("38", bankInfo));

            // 53: Transaction Currency (704 = VND)
            sb.Append(TLV("53", "704"));

            // 54: Transaction Amount (Số tiền)
            if (amount > 0)
            {
                sb.Append(TLV("54", ((long)amount).ToString()));
            }

            // 58: Country Code (VN)
            sb.Append(TLV("58", "VN"));

            // 62: Additional Data Field Template (Nội dung)
            if (!string.IsNullOrEmpty(cleanContent))
            {
                string addInfo = TLV("08", cleanContent); // 08 là Tag cho nội dung chuyển khoản
                sb.Append(TLV("62", addInfo));
            }

            // 63: CRC (Cyclic Redundancy Check)
            string dataToCrc = sb.ToString() + "6304"; // Thêm ID và Length của CRC
            string crcCode = ComputeCRC16(dataToCrc);

            return dataToCrc + crcCode;
        }

        // ===== CÁC HÀM BỔ TRỢ (HELPER) =====

        // Tạo chuỗi Tag-Length-Value
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
            return crc.ToString("X4"); // Trả về mã Hex 4 ký tự
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