using System;
using System.Security.Cryptography;
using System.Text;
using Fanex.Logging;

namespace Fanex.Affiliate.DataService.Importer.Core.Utilities
{
    public static class RsaHelper
    {
        //private Key encrypt
        public static string SignAndHash(string dataToSign, string privatekey)
        {
            var rsaProvider = new RSACryptoServiceProvider();
            rsaProvider.FromXmlString(privatekey);

            var byteArrayData = Encoding.UTF8.GetBytes(dataToSign);

            var sign = rsaProvider.SignData(byteArrayData, new MD5CryptoServiceProvider());

            return Convert.ToBase64String(sign);
        }

        //public Key verify
        public static bool VerifySignedHash(string dataToSign, string SignedData, string publicKey)
        {
            try
            {
                var encoder = new UTF8Encoding();
                var rsaProvider = new RSACryptoServiceProvider();
                var privateKeyParam = rsaProvider.ExportParameters(true);
                rsaProvider.ImportParameters(privateKeyParam);
                rsaProvider.FromXmlString(publicKey);

                var plaintext = encoder.GetBytes(dataToSign);

                return rsaProvider.VerifyData(plaintext, CryptoConfig.MapNameToOID("MD5"), Convert.FromBase64String(SignedData));
            }
            catch (CryptographicException ex)
            {
                LogService.Error(ex.Message, ex);
                return false;
            }
        }
    }
}
