using System.Security.Cryptography;
using System.Text;

namespace Services.Settings
{
    public static class EncryptDecrypt
    {
        public static readonly string InitializationVector = "5045755YRGHZZSWU";
        public static readonly string SecretKey = "8H7JR5869863HGA625A59F7CE5683POP";

        public static string EncryptText(string plainText)
        {
            using Aes aesAlg = Aes.Create();
            aesAlg.Key = Encoding.UTF8.GetBytes(SecretKey);
            aesAlg.IV = Encoding.UTF8.GetBytes(InitializationVector);

            ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

            using MemoryStream msEncrypt = new();
            using (CryptoStream csEncrypt = new(msEncrypt, encryptor, CryptoStreamMode.Write))
            {
                using StreamWriter swEncrypt = new(csEncrypt);
                swEncrypt.Write(plainText);
            }

            return Convert.ToBase64String(msEncrypt.ToArray());
        }

        public static string DecryptText(string cipherText)
        {
            using Aes aesAlg = Aes.Create();
            aesAlg.Key = Encoding.UTF8.GetBytes(SecretKey);
            aesAlg.IV = Encoding.UTF8.GetBytes(InitializationVector);

            ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

            using MemoryStream msDecrypt = new(Convert.FromBase64String(cipherText));
            using CryptoStream csDecrypt = new(msDecrypt, decryptor, CryptoStreamMode.Read);
            using StreamReader srDecrypt = new(csDecrypt);

            return srDecrypt.ReadToEnd();
        }
    }
}
