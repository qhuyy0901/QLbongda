using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace BUS
{
    // ===== HELPER MÃ HÓA PASSWORD CHỈ CHO QUẢN LÝ TK (KHÔNG ẢNH HƯỞNG LOGIN) =====
    public static class MaHoaASCII
    {
        // ===== KEY: 32 BYTES (256 BIT) =====
        private static readonly byte[] AES_KEY = Encoding.UTF8.GetBytes("MySecureKey12345MySecureKey12345");  // ✅ 32 bytes

        // ===== IV: 16 BYTES (128 BIT) =====
        private static readonly byte[] AES_IV = Encoding.UTF8.GetBytes("MyInitialVector1");  // ✅ 16 bytes

        // ===== MÃ HÓA PASSWORD BẰNG AES =====
        public static string EncryptPassword(string plainPassword)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(plainPassword))
                    return "";

                using (Aes aes = Aes.Create())
                {
                    aes.Key = AES_KEY;
                    aes.IV = AES_IV;
                    aes.Mode = CipherMode.CBC;
                    aes.Padding = PaddingMode.PKCS7;

                    ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);

                    using (MemoryStream ms = new MemoryStream())
                    {
                        using (CryptoStream cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
                        {
                            using (StreamWriter sw = new StreamWriter(cs, Encoding.UTF8))
                            {
                                sw.Write(plainPassword);
                                sw.Flush();
                            }
                            // ===== KHÔNG GỌLOSHFLUSHFINALBLOCK() - StreamWriter.Dispose() đã gọi rồi =====
                        }
                        return Convert.ToBase64String(ms.ToArray());
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("❌ Lỗi mã hóa password: " + ex.Message);
            }
        }

        // ===== GIẢI MÃ PASSWORD BẰNG AES (TỰ ĐỘNG PHÁT HIỆN PLAINTEXT) =====
        public static string DecryptPassword(string encryptedPassword)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(encryptedPassword))
                    return "";

                // ===== CỐ GẮNG GIẢI MÃ NHƯ BASE64 =====
                try
                {
                    byte[] buffer = Convert.FromBase64String(encryptedPassword);

                    using (Aes aes = Aes.Create())
                    {
                        aes.Key = AES_KEY;
                        aes.IV = AES_IV;
                        aes.Mode = CipherMode.CBC;
                        aes.Padding = PaddingMode.PKCS7;

                        ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);

                        using (MemoryStream ms = new MemoryStream(buffer))
                        {
                            using (CryptoStream cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read))
                            {
                                using (StreamReader sr = new StreamReader(cs, Encoding.UTF8))
                                {
                                    return sr.ReadToEnd();
                                }
                                // ===== KHÔNG GỌLOSHFLUSHFINALBLOCK() - StreamReader.Dispose() không cần =====
                            }
                        }
                    }
                }
                catch (FormatException)
                {
                    // ===== NẾU KHÔNG PHẢI BASE64, TRẢ VỀ PLAINTEXT =====
                    return encryptedPassword;
                }
            }
            catch (Exception ex)
            {
                // ===== NẾU LỖI, TRẢ VỀ PLAINTEXT CUỐI CÙNG =====
                return encryptedPassword;
            }
        }

        // ===== KIỂM TRA XEM PASSWORD CÓ PHẢI ENCRYPTED KHÔNG =====
        public static bool IsEncrypted(string password)
        {
            if (string.IsNullOrWhiteSpace(password))
                return false;

            try
            {
                // ===== NẾU CÓ THỂ CONVERT THÀNH BASE64, NGHĨA LÀ ĐÃ ENCRYPT =====
                Convert.FromBase64String(password);
                return true;
            }
            catch
            {
                // ===== KHÔNG PHẢI BASE64 = PLAINTEXT =====
                return false;
            }
        }
    }
}