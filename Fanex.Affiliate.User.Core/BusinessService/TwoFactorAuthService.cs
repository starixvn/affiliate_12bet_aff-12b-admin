using System;

namespace Fanex.Affiliate.User.Core.BusinessService
{
    public class TwoFactorAuthService : ITwoFactorAuthService
    {
        public string GenerateTotp(string secretKey)
        {
            throw new NotImplementedException();
        }

        public bool VerifyTotp(string totp)
        {
            throw new NotImplementedException();
        }
    }
}
