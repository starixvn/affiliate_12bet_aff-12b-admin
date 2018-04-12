using DNAide.Utils;

namespace Fanex.Affiliate.User.Core.Utilities
{
    public static class CryptographyHelper
    {
        public static string Encrypt(string inputString)
        {
            return Encryption.CfsEnCode(inputString);
        }
    }
}
