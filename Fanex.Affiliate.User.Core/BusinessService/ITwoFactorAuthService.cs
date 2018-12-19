namespace Fanex.Affiliate.User.Core.BusinessService
{
    public interface ITwoFactorAuthService
    {
        string GenerateTotp(string secretKey);

        bool VerifyTotp(string totp);
    }
}
