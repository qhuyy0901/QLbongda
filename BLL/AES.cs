using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace BUS
{
    public static class AES
    {
        private static readonly byte[] AES_KEY = Encoding.UTF8.GetBytes("MySecureKey12345MySecureKey12345");
        private static readonly byte[] AES_IV = Encoding.UTF8.GetBytes("MyInitialVector1");  

        // ===== MÃ HÓA PASSWORD BẰNG AES =====
        public static string EncryptPassword(string plainPassword)
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
                        }
                        return Convert.ToBase64String(ms.ToArray());
                    }
                }
        }

        public static string DecryptPassword(string encryptedPassword)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(encryptedPassword))
                    return "";

                //GIẢI MÃ NHƯ BASE64
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
                            }
                        }
                    }
                }
                catch (FormatException)
                {
                    return encryptedPassword;
                }
            }
            catch (Exception ex)
            {
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
                Convert.FromBase64String(password);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}