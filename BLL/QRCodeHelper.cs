using System;
using System.Drawing;
using System.Text;
using QRCoder;

namespace BUS
{
    public static class QRCodeHelper
    {
        private const string BankCode = "970422";
        private const string AccountNumber = "0399750340";
        private const string AccountName = "NGUYEN QUANG HUY";
        private const string BankName = "MB Bank";
        private const string ProvinceName = "HO CHI MINH";

        public static Bitmap GenerateQRCode(decimal amount, string description)
        {
            try
            {
                string qrContent = GenerateVietQRString(amount, description);

                using (QRCodeGenerator qrGenerator = new QRCodeGenerator())
                {
                    QRCodeData qrCodeData = qrGenerator.CreateQrCode(qrContent, QRCodeGenerator.ECCLevel.M);
                    using (QRCode qrCode = new QRCode(qrCodeData))
                    {
                        return qrCode.GetGraphic(10);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi tạo mã QR: {ex.Message}", ex);
            }
        }

        public static string GetTransferInfo(decimal amount)
        {
            return $"Ngân hàng: {BankName}\n" +
                   $"Số tài khoản: {AccountNumber}\n" +
                   $"Chủ tài khoản: {AccountName}\n" +
                   $"Số tiền: {amount:N0} VNĐ\n" +
                   $"Nội dung: Thanh toan dich vu";
        }

        private static string GenerateVietQRString(decimal amount, string description)
        {
            StringBuilder qr = new StringBuilder();

            // Field 00: Phiên bản QR
            qr.Append(TLV("00", "01"));

            // Field 01: Point of Initiation Method (11 = Static)
            qr.Append(TLV("01", "11"));

            // Field 26: Dữ liệu Việt QR (Merchant Account Information)
            qr.Append(TLV("26", BuildMerchantData()));

            // Field 52: Merchant Category Code (1234 = Chuyển tiền)
            qr.Append(TLV("52", "1234"));

            // Field 53: Currency Code (156 = VNĐ)
            qr.Append(TLV("53", "156"));

            // Field 54: Amount
            if (amount > 0)
                qr.Append(TLV("54", amount.ToString("0")));

            // Field 55: Tip or Convenience Indicator
            qr.Append(TLV("55", "2"));

            // Field 58: Country Code
            qr.Append(TLV("58", "VN"));

            // Field 59: Merchant Name
            qr.Append(TLV("59", AccountName));

            // Field 60: Merchant City
            qr.Append(TLV("60", ProvinceName));

            // Field 61: Transaction Description
            string desc = description.Length > 25 ? description.Substring(0, 25) : description;
            qr.Append(TLV("61", desc));

            // Field 62: Additional Data Field Template
            qr.Append(TLV("62", ""));

            // Field 63: CRC Checksum
            string dataBeforeCRC = qr.ToString() + "6304";
            string crc = ComputeCRC16(dataBeforeCRC);
            qr.Append(TLV("63", crc));

            return qr.ToString();
        }

        private static string BuildMerchantData()
        {
            StringBuilder merchant = new StringBuilder();

            // Field 00: Merchant Account Information Template ID (01 = Việt QR)
            merchant.Append(TLV("00", "01"));

            // Field 01: Bank Code (Mã ngân hàng)
            merchant.Append(TLV("01", BankCode));

            // Field 02: Account Type (01 = Thanh toán)
            merchant.Append(TLV("02", "01"));

            // Field 03: Account Number (Số tài khoản)
            merchant.Append(TLV("03", AccountNumber));

            string merchantContent = merchant.ToString();
            return $"{merchantContent.Length:D2}{merchantContent}";
        }

        private static string TLV(string tag, string value)
        {
            if (string.IsNullOrEmpty(value))
                return string.Empty;

            byte[] bytes = Encoding.UTF8.GetBytes(value);
            return $"{tag}{bytes.Length:D2}{value}";
        }

        private static string ComputeCRC16(string data)
        {
            ushort crc = 0xFFFF;
            byte[] bytes = Encoding.UTF8.GetBytes(data);

            foreach (byte b in bytes)
            {
                crc ^= (ushort)(b << 8);
                for (int i = 0; i < 8; i++)
                {
                    crc = (ushort)((crc & 0x8000) == 0 ? (crc << 1) : ((crc << 1) ^ 0x1021));
                }
            }

            return crc.ToString("X4");
        }
    }
}