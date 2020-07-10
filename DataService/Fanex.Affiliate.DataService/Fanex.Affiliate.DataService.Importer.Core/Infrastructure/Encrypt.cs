using System;
using System.Web;
using System.Text;
using System.Security.Cryptography;

namespace Fanex.Affiliate.DataService.Importer.Core.Infrastructure
{
    public static class Encrypt
    {
        public static string AESEncrypt(string str, string key)
        {
            if (string.IsNullOrEmpty(str))
            {
                return null;
            }
            var toEncryptArray = Encoding.UTF8.GetBytes(str);
            var rm = new RijndaelManaged
            {
                Key = Encoding.UTF8.GetBytes(key),
                Mode = CipherMode.ECB,
                Padding = PaddingMode.PKCS7
            };
            var ctransform = rm.CreateEncryptor();
            var resultArray = ctransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);
            return HttpUtility.UrlEncode(Convert.ToBase64String(resultArray, 0, resultArray.Length));
        }

        public static string MD5Encrypt(string str)
        {
            var md5 = new MD5CryptoServiceProvider();
            var data = Encoding.UTF8.GetBytes(str);
            var result = md5.ComputeHash(data);

            StringBuilder ret = new StringBuilder();
            for (int i = 0; i < result.Length; i++)
            {
                ret.Append(result[i].ToString("x").PadLeft(2, '0'));
            }
            return ret.ToString();
        }
    }
}
