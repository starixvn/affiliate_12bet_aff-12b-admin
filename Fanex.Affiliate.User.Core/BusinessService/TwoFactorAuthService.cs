using System;
using OtpNet;

namespace Fanex.Affiliate.User.Core.BusinessService
{
    public class TwoFactorAuthService : ITwoFactorAuthService
    {
        public string GenerateTotp(string secretKey)
        {
            int affId = 927841;
            var base32Bytes = Base32Encoding.ToBytes(secretKey);

            Hotp hotp = new Hotp(base32Bytes);

            return hotp.ComputeHOTP(affId);
        }

        public bool VerifyTotp(string totp)
        {
            int affId = 927841;
            var secretKey = "GEZDGNBVGY3TQOJQGEZDGNBVGY3TQOJQ";
            var base32Bytes = Base32Encoding.ToBytes(secretKey);
            Hotp hotp = new Hotp(base32Bytes);

            return hotp.VerifyHotp(totp, affId);
        }
    }
}
