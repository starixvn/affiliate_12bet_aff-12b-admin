namespace Fanex.Affiliate.User.Core.BusinessService
{
    public interface ITwoFactorAuthService
    {
        string GenerateHotp(string secretKey);

        bool VerifyHotp(string otp);
    }
}
