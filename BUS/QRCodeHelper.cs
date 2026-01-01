using System;
using System.Drawing;
using System.Text;
using QRCoder;

namespace BUS
{
    /// <summary>
    /// Hỗ trợ tạo mã VietQR theo chuẩn NAPAS VietQR v1.0
    /// Tích hợp thanh toán chuyển khoản MB Bank
    /// </summary>
    public static class QRCodeHelper
    {
        // ===== THÔNG TIN NGÂN HÀNG MB BANK =====
        private const string BANK_CODE = "970422";           // Mã ngân hàng MB
        private const string ACCOUNT_NUMBER = "0399750340";  // STK
        private const string ACCOUNT_HOLDER = "NGUYEN QUANG HUY";  // Chủ TK
        private const string BANK_NAME = "MB Bank";
        private const string PROVINCE = "HO CHI MINH";

        /// <summary>
        /// Tạo mã QR VietQR từ số tiền và nội dung thanh toán
        /// Tuân theo chuẩn NAPAS VietQR v1.0
        /// </summary>
        /// <param name="amount">Số tiền (VNĐ)</param>
        /// <param name="description">Nội dung thanh toán (tối đa 25 ký tự)</param>
        /// <returns>Bitmap chứa mã QR</returns>
        public static Bitmap GenerateQRCode(decimal amount, string description = "Thanh toan dich vu")
        {
            try
            {
                string qrContent = BuildVietQRPayload(amount, description);

                using (QRCodeGenerator qrGenerator = new QRCodeGenerator())
                using (QRCodeData qrCodeData = qrGenerator.CreateQrCode(qrContent, QRCodeGenerator.ECCLevel.M))
                using (QRCode qrCode = new QRCode(qrCodeData))
                {
                    return qrCode.GetGraphic(20);
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"❌ Lỗi tạo mã VietQR: {ex.Message}");
            }
        }

        /// <summary>
        /// Xây dựng payload VietQR theo chuẩn EMV QR Code
        /// Tham khảo: https://napas.com.vn/vietqr
        /// </summary>
        private static string BuildVietQRPayload(decimal amount, string description)
        {
            StringBuilder qr = new StringBuilder();

            // ===== FIELD 00: QR Version =====
            qr.Append(TLV("00", "01"));

            // ===== FIELD 01: Point of Initiation Method (11 = Static QR) =====
            qr.Append(TLV("01", "11"));

            // ===== FIELD 26: Merchant Account Information (VietQR) =====
            string merchantData = BuildMerchantAccountInformation();
            qr.Append(TLV("26", merchantData));

            // ===== FIELD 52: Merchant Category Code (5411 = Retail) =====
            qr.Append(TLV("52", "5411"));

            // ===== FIELD 53: Currency Code (156 = VND) =====
            qr.Append(TLV("53", "156"));

            // ===== FIELD 54: Transaction Amount (nếu có) =====
            if (amount > 0)
            {
                qr.Append(TLV("54", ((long)amount).ToString()));
            }

            // ===== FIELD 55: Tip or Convenience Indicator (2 = No tip) =====
            qr.Append(TLV("55", "2"));

            // ===== FIELD 58: Country Code (VN = Vietnam) =====
            qr.Append(TLV("58", "VN"));

            // ===== FIELD 59: Merchant Name =====
            qr.Append(TLV("59", ACCOUNT_HOLDER));

            // ===== FIELD 60: Merchant City =====
            qr.Append(TLV("60", PROVINCE));

            // ===== FIELD 61: Transaction Purpose (tối đa 25 ký tự) =====
            string purpose = LimitLength(description, 25);
            if (!string.IsNullOrEmpty(purpose))
            {
                qr.Append(TLV("61", purpose));
            }

            // ===== FIELD 62: Additional Data Field Template (để trống) =====
            qr.Append(TLV("62", ""));

            // ===== FIELD 63: CRC-16 Checksum =====
            string dataForCRC = qr.ToString() + "6304";
            string crc = ComputeCRC16(dataForCRC);
            qr.Append(TLV("63", crc));

            return qr.ToString();
        }

        /// <summary>
        /// Xây dựng Merchant Account Information (Field 26) cho VietQR
        /// </summary>
        private static string BuildMerchantAccountInformation()
        {
            StringBuilder merchant = new StringBuilder();

            // ===== Field 00: Merchant Account Information Template ID (01 = VietQR) =====
            merchant.Append(TLV("00", "01"));

            // ===== Field 01: Bank Code (Mã ngân hàng theo NAPAS) =====
            merchant.Append(TLV("01", BANK_CODE));

            // ===== Field 02: Account Type (01 = Thanh toán) =====
            merchant.Append(TLV("02", "01"));

            // ===== Field 03: Account Number (Số tài khoản) =====
            merchant.Append(TLV("03", ACCOUNT_NUMBER));

            string merchantContent = merchant.ToString();

            // Thêm length vào đầu Field 26
            return $"{merchantContent.Length:D2}{merchantContent}";
        }

        /// <summary>
        /// Format TLV (Tag-Length-Value)
        /// Tag: 2 ký tự
        /// Length: 2 ký tự (số byte của value)
        /// Value: nội dung
        /// </summary>
        private static string TLV(string tag, string value)
        {
            if (string.IsNullOrEmpty(value))
                return string.Empty;

            byte[] valueBytes = Encoding.UTF8.GetBytes(value);
            int length = valueBytes.Length;

            return $"{tag}{length:D2}{value}";
        }

        /// <summary>
        /// Tính CRC-16/XMODEM (dùng cho VietQR)
        /// </summary>
        private static string ComputeCRC16(string data)
        {
            ushort crc = 0xFFFF;
            byte[] bytes = Encoding.UTF8.GetBytes(data);

            foreach (byte b in bytes)
            {
                crc ^= (ushort)(b << 8);

                for (int i = 0; i < 8; i++)
                {
                    if ((crc & 0x8000) != 0)
                    {
                        crc = (ushort)((crc << 1) ^ 0x1021);
                    }
                    else
                    {
                        crc = (ushort)(crc << 1);
                    }
                }
            }

            return crc.ToString("X4");
        }

        /// <summary>
        /// Giới hạn độ dài chuỗi
        /// </summary>
        private static string LimitLength(string input, int maxLength)
        {
            if (string.IsNullOrEmpty(input))
                return "";

            return input.Length > maxLength ? input.Substring(0, maxLength) : input;
        }

        /// <summary>
        /// Lấy thông tin ngân hàng
        /// </summary>
        public static (string BankCode, string AccountNumber, string AccountHolder, string BankName) GetBankInfo()
        {
            return (BANK_CODE, ACCOUNT_NUMBER, ACCOUNT_HOLDER, BANK_NAME);
        }
    }
}