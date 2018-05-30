using Fanex.Affiliate.User.Core.Constants;
using Fanex.Affiliate.User.Core.Utilities;

namespace Fanex.Affiliate.User.Core.BusinessService
{
    public class CryptographyService : ICryptographyService
    {
        private readonly ICookieService _cookieService;

        public CryptographyService(ICookieService cookieService)
        {
            _cookieService = cookieService;
        }

        public string EncryptPassword(string inputPassword)
        {
            var encryptionKey = _cookieService.GetCookie(CookieName.EncryptionKey);
            var iv = _cookieService.GetCookie(CookieName.InitializationVector);

            var plainPassword = CryptographyHelper.DecryptWithAES(inputPassword, encryptionKey, iv);

            return CryptographyHelper.Encrypt(plainPassword);
        }
    }
}
