using Fanex.Affiliate.User.Core.DTO;
using Fanex.Affiliate.User.Core.MetalModels;

namespace Fanex.Affiliate.User.Core.BusinessService
{
    public interface IVerificationService
    {
        SendCodeResult SendCode(SendCodeParam param);

        bool IsValidOtp(string secretKey, int counter, string otp);
    }
}
