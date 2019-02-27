using Fanex.Affiliate.User.Core.DTO;
using Fanex.Affiliate.User.Core.MetalModels;
using Fanex.Affiliate.User.Core.MetalModels.Email;

namespace Fanex.Affiliate.User.Core.BusinessService
{
    public interface IAccountService
    {
        ResultInfo CheckPlayerAccountIsActived(int affId, string language);

        ResultInfo CreateAffiliate(UserSignUpParam signUpParam, EmailBaseParam emailParam);
    }
}
