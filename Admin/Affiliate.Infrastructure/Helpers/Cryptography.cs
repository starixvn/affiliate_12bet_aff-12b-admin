using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Affiliate.Infrastructure.Helpers
{
    public static class Cryptography
    {
        #region Const Params

        private const int Two = 2;
        private const int Three = 3;
        private const string CfsKey = "&％#@?,:*!~^.";
        private static readonly AesCryptoServiceProvider CryptoServiceProvider = new AesCryptoServiceProvider();
        private static readonly byte[] RgbKey = { 0x21, 0x63, 0x54, 0x5a, 0x91, 0xac, 110, 0xcc };
        private static readonly byte[] RgbIv = Encoding.UTF8.GetBytes("&％#@?,:*!~^.");

        #endregion

        #region Public functions

        /// <summary>
        /// CFS the encrypt.
        /// </summary>
        /// <param name="plainText">The plain text.</param>
        /// <returns>The encrypt string.</returns>
        public static string CfsEnCrypt(string plainText)
        {
            var encryptedText = string.Empty;
            var rgbKey = new byte[] { 0x21, 0x63, 0x54, 0x5a, 0x91, 0xac, 110, 0xcc };
            var rgbIV = Encoding.UTF8.GetBytes(CfsKey);
            using (var provider = new AesCryptoServiceProvider())
            {
                var bytes = Encoding.UTF8.GetBytes(plainText);
                using (MemoryStream stream = new MemoryStream())
                {
                    using (var stream2 = new CryptoStream(stream, provider.CreateEncryptor(rgbKey, rgbIV), CryptoStreamMode.Write))
                    {
                        stream2.Write(bytes, 0, bytes.Length);
                        stream2.FlushFinalBlock();
                        encryptedText = Convert.ToBase64String(stream.ToArray());
                    }
                }
            }

            return encryptedText;
        }

        /// <summary>
        /// CFS the decrypt.
        /// </summary>
        /// <param name="encryptedText">The encrypted text.</param>
        /// <returns>The string decrypt.</returns>
        public static string CfsDeCrypt(string encryptedText)
        {
            var decryptedText = string.Empty;

            if (!string.IsNullOrWhiteSpace(encryptedText))
            {
                var buffer = Convert.FromBase64String(encryptedText);
                using (var stream = CreateMemoryStream(buffer))
                {
                    decryptedText = Encoding.UTF8.GetString(stream.ToArray());
                }
            }

            return decryptedText;
        }

        /// <summary>
        /// Decrypt the password of connection string.
        /// </summary>
        /// <param name="connStr">The connection string.</param>
        /// <returns>The connection string with password be decrypt.</returns>
        /// <exception cref="System.ArgumentException">The connection string is not null or white space.</exception>
        public static string DeCryptConnectionString(string connStr)
        {
            if (string.IsNullOrWhiteSpace(connStr))
            {
                throw new ArgumentException(connStr);
            }

            var decryptPwd = GetDecryptPwd(connStr, "=");
            var str = CfsDeCrypt(decryptPwd[1].Trim());

            return decryptPwd[0].Trim() + str + ";";
        }
        #endregion

        /// <summary>
        /// Creates the memory stream.
        /// </summary>
        /// <param name="buffer">The buffer.</param>
        /// <returns>The memory stream.</returns>
        private static MemoryStream CreateMemoryStream(byte[] buffer)
        {
            var stream = new MemoryStream();
            using (var stream2 = new CryptoStream(stream, CryptoServiceProvider.CreateDecryptor(RgbKey, RgbIv), CryptoStreamMode.Write))
            {
                stream2.Write(buffer, 0, buffer.Length);
                stream2.FlushFinalBlock();
            }

            return stream;
        }

        private static string[] GetDecryptPwd(string sourceString, string searchString)
        {
            int length = 1;
            var str = new StringBuilder();
            var str2 = new StringBuilder();
            var str3 = new StringBuilder();
            string str4;

            for (int i = sourceString.Length - 1; i >= 0; i--)
            {
                str = str.Append(sourceString.Substring(i, 1));
            }

            for (int j = searchString.Length - 1; j >= 0; j--)
            {
                str2 = str2.Append(searchString.Substring(j, 1));
            }

            if (str.ToString(1, Two) != "==")
            {
                if (str.ToString(1, 1) != "=")
                {
                    length += getNewLength(str);

                    str4 = str.ToString(0, length);
                    for (int num9 = str4.Length; num9 > 1; num9--)
                    {
                        str3 = str3.Append(str4.Substring(num9 - 1, 1));
                    }

                    return new[] { sourceString.Substring(0, sourceString.Length - length), str3.ToString() };
                }

                length += 1 + getNewLength(str, Two);
            }
            else
            {
                length += Two + getNewLength(str, Three);

                str4 = str.ToString(0, length);
                for (int num5 = str4.Length; num5 > 1; num5--)
                {
                    str3 = str3.Append(str4.Substring(num5 - 1, 1));
                }

                return new[] { sourceString.Substring(0, sourceString.Length - length), str3.ToString() };
            }

            str4 = str.ToString(0, length);
            for (int k = str4.Length; k > 1; k--)
            {
                str3 = str3.Append(str4.Substring(k - 1, 1));
            }

            return new[] { sourceString.Substring(0, sourceString.Length - length), str3.ToString() };
        }

        private static int getNewLength(StringBuilder str, int initialValue = 1)
        {
            var length = 0;

            while(initialValue <= str.Length && str.ToString(initialValue, 1) != "=")
            {
                length++;
                initialValue++;
            }

            return length;
        }
    }
}
