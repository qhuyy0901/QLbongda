using System;
using System.Security.Cryptography;
using System.Text;

namespace BUS
{
    // ===== HELPER MÃ HÓA PASSWORD CHỈ CHO QUẢN LÝ TK (KHÔNG ẢNH HƯỞNG LOGIN) =====
    public static class PasswordHelper
    {
        private static readonly byte[] AES_KEY = Encoding.UTF8.GetBytes("MySecureKey1234MySecureKey1234"); // 32 bytes
        private static readonly byte[] AES_IV = Encoding.UTF8.GetBytes("MyInitialVector1");                 // 16 bytes

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
                            cs.FlushFinalBlock();
                            return Convert.ToBase64String(ms.ToArray());
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("❌ Lỗi mã hóa password: " + ex.Message);
            }
        }

        // ===== GIẢI MÃ PASSWORD BẰNG AES =====
        public static string DecryptPassword(string encryptedPassword)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(encryptedPassword))
                    return "";

                byte[] buffer = Convert.FromBase64String(encryptedPassword);

                using (Aes aes = Aes.Create())
                {
                    aes.Key = AES_KEY;
                    aes.IV = AES_IV;

                    ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);

                    using (MemoryStream ms = new MemoryStream(buffer))
                    {
                        using (CryptoStream cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read))
                        {
                            using (StreamReader sr = new StreamReader(cs, Encoding.UTF8))
                            {
                                return sr.ReadToEnd();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("❌ Lỗi giải mã password: " + ex.Message);
            }
        }
    }
}