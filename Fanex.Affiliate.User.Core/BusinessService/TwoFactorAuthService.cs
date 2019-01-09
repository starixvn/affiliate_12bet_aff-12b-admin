using OtpNet;

namespace Fanex.Affiliate.User.Core.BusinessService
{
    public class TwoFactorAuthService : ITwoFactorAuthService
    {
        public string GenerateHotp(string secretKey, int counter)
        {
            var base32Bytes = Base32Encoding.ToBytes(secretKey);

            Hotp hotp = new Hotp(base32Bytes);

            return hotp.ComputeHOTP(counter);
        }

        public bool VerifyHotp(string secretKey, int counter, string otp)
        {
            var base32Bytes = Base32Encoding.ToBytes(secretKey);
            Hotp hotp = new Hotp(base32Bytes);

            return hotp.VerifyHotp(otp, counter);
        }
    }
}
