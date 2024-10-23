using Fanex.Affiliate.User.Core.DTO;
using Fanex.Affiliate.User.Core.MetalModels;
using Fanex.Affiliate.User.Core.MetalModels.Email;
using System.Threading.Tasks;

namespace Fanex.Affiliate.User.Core.BusinessService
{
    public interface IAccountService
    {
        Task<ResultInfo> CheckPlayerAccountIsActivedAsync(int affId, string language);

        ResultInfo CreateAffiliate(UserSignUpParam signUpParam, EmailSignUpParam emailParam);
    }
}
