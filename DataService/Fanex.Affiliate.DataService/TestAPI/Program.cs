using System;
using System.Web;
using System.Text;
using System.Security.Cryptography;
using Fanex.Affiliate.DataService.Importer.KYGaming;
using Fanex.Affiliate.DataService.Importer.Core.Utilities;
using Fanex.Affiliate.DataService.Importer.Core.Infrastructure;

namespace TestAPI
{
    internal static class Program
    {
        static void Main(string[] args)
        {
            AESEncrypt("s=6&startTime=1586253120&endTime=1586425920", "CD90BDB64D035297");
            var now = DateTime.Now;
            long xx = ConvertToUnixOfTime(now);
            var startTime = DateTime.Parse("2020/04/07 22:54");
            var endTime = DateTime.Parse("2020/04/07 22:56");

            KYGamingService service = new KYGamingService();

            var result = service.BuildParam(startTime, endTime, "RMB");

            Console.WriteLine("https://grecord_gpk.ky3671.com/getRecordHandle?agent=120127&timestamp=" + xx.ToString() + "&param=" + AESEncrypt("s=6&startTime=" + ConvertToUnixOfTime(DateTime.Parse("2020/04/07 22:54")) + "&endTime=" + ConvertToUnixOfTime(DateTime.Parse("2020/04/07 22:56")), "CD90BDB64D035297") + "&key=" + MD5Encrypt("120127" + xx.ToString() + "628EA93FDD1035F3"));
            Console.WriteLine("https://grecord_gpk.ky3671.com/getRecordHandle?agent=120127&timestamp=" + result.TimeStamp + "&param=" + result.EncryptedParam + "&key=" + result.EncryptedKey);
            Console.Read();
        }

        static string AESEncrypt(string str, string key)
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


        static string MD5Encrypt(string str)
        {
            var md5 = new MD5CryptoServiceProvider();
            var data = Encoding.UTF8.GetBytes(str);
            var result = md5.ComputeHash(data);
            //var ret = string.Empty;
            //for (int i = 0; i < result.Length; i++)
            //{
            //    ret += result[i].ToString("x").PadLeft(2, '0');
            //}
            //return ret;

            StringBuilder ret = new StringBuilder();
            for (int i = 0; i < result.Length; i++)
            {
                ret.Append(result[i].ToString("x").PadLeft(2, '0'));
            }
            return ret.ToString();
        }

        static long ConvertToUnixOfTime(DateTime time)
        {
            var startTime = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
            return (long)(time - startTime).TotalMilliseconds;
        }
    }
}
