using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Fanex.Affiliate.User.Core.Utilities
{
    public static class CryptographyHelper
    {
        public static string Encrypt(string inputString)
        {
            return CfsEnCode(inputString);
        }

        public static string DecryptWithAES(string encryptedString, string keyString, string ivString)
        {
            var plaintext = string.Empty;
            try
            {
                var key = Encoding.UTF8.GetBytes(keyString);
                var iv = Encoding.UTF8.GetBytes(ivString);

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

        private static string CfsEnCode(string codeStr)
        {
            int num = 30;
            int num2 = num - codeStr.Length;

            var codeBuilder = new StringBuilder();
            codeBuilder.Append(codeStr);
            if (num2 > 1)
            {
                for (int i = 1; i <= num2; i++)
                {
                    codeBuilder.Append(Convert.ToChar(21));
                }
            }

            var codeString = codeBuilder.ToString();
            double num3 = 1.0;
            for (int j = 1; j <= num; j++)
            {
                int num4 = num + Convert.ToInt32(Convert.ToChar(codeString.Substring(j - 1, 1))) * j;
                num3 *= num4;
            }
            var num3String = num3.ToString();
            var textBuilder = new StringBuilder();
            for (int k = 1; k <= num3String.Length; k++)
            {
                var length = k + 3 > num3String.Length ? num3String.Length - k + 1 : 3;
                textBuilder.Append(CfsCode(num3String.Substring(k - 1, length)));
            }

            var text = textBuilder.ToString();
            var text2 = new StringBuilder();
            for (int l = 20; l <= text.Length - 18; l += 2)
            {
                text2.Append(text.Substring(l - 1, 1));
            }
            return text2.ToString();
        }

        private static string CfsCode(string nWord)
        {
            var text = new StringBuilder();
            for (int i = 1; i <= nWord.Length; i++)
            {
                text.Append(Convert.ToInt32(Convert.ToChar(nWord.Substring(i - 1, 1))));
            }

            return Convert.ToInt64(text.ToString()).ToString("X");
        }
    }
}