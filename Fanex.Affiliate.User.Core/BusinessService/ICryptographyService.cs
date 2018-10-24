using System.Security.Cryptography;
using Fanex.Affiliate.User.Core.DTO;

namespace Fanex.Affiliate.User.Core.BusinessService
{
    public interface ICryptographyService
    {
        string EncryptPassword(RSAParameters privateKey, string cipherText);

        RsaKeyPair GenerateKeyPair();

        string ConvertPublicKeyToString(RSAParameters parameters);

        string ConvertKeyToXmlString(RSAParameters keyParam);

        RSAParameters ConvertXmlStringToParam(string xmlString);
    }
}
