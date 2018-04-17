using System;
using System.IO;
using System.Security.Cryptography;
using DNAide.Utils;

namespace Fanex.Affiliate.User.Core.Utilities
{
    public static class CryptographyHelper
    {
        public static string Encrypt(string inputString)
        {
            return Encryption.CfsEnCode(inputString);
        }

        public static string DecryptWithAES(string encryptedString, string keyString, string ivString)
        {
            var plaintext = string.Empty;
            try
            {
                var key = System.Text.Encoding.UTF8.GetBytes(keyString);
                var iv = System.Text.Encoding.UTF8.GetBytes(ivString);

                var encryptedBytes = Convert.FromBase64String(encryptedString);

                using (var rijndaelManaged = new RijndaelManaged { Key = key, IV = iv, Padding = PaddingMode.PKCS7, Mode = CipherMode.CBC })
                using (var memoryStream = new MemoryStream(encryptedBytes))
                using (var cryptoStream = new CryptoStream(memoryStream, rijndaelManaged.CreateDecryptor(key, iv), CryptoStreamMode.Read))
                using (var srDecrypt = new StreamReader(cryptoStream))
                {
                    plaintext = srDecrypt.ReadToEnd();
                }
            }
            catch (Exception exception)
            {
                Logging.LogService.Error(exception.Message, exception);
            }

            return plaintext;
        }
    }
}