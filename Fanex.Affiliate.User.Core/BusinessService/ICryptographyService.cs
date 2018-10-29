using System.Security.Cryptography;

namespace Fanex.Affiliate.User.Core.BusinessService
{
    public interface ICryptographyService
    {
        string EncryptPassword(string privateKey, string cipherText);

        string ConvertPublicKeyToString(RSAParameters parameters);
    }
}
