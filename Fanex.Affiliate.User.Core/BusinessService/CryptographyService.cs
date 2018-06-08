using Fanex.Affiliate.User.Core.Utilities;

namespace Fanex.Affiliate.User.Core.BusinessService
{
    public class CryptographyService : ICryptographyService
    {
        public string EncryptPassword(string inputPassword, string encryptionKey, string iv)
        {
            var plainPassword = CryptographyHelper.DecryptWithAES(inputPassword, encryptionKey, iv);

            return CryptographyHelper.Encrypt(plainPassword);
        }
    }
}
