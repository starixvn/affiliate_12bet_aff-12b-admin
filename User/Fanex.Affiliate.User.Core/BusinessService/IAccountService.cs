using Fanex.Affiliate.User.Core.DTO;
using Fanex.Affiliate.User.Core.DTO.Apis;
using Fanex.Affiliate.User.Core.MetalModels;
using Fanex.Affiliate.User.Core.MetalModels.Email;
using System.Threading.Tasks;

namespace Fanex.Affiliate.User.Core.BusinessService
{
    public interface IAccountService
    {
        Task<ResultInfo> CreateAffiliateAsync(UserSignUpParam signUpParam, EmailSignUpParam emailParam);

        GeneralAffInfo GetGeneralAffInfo(int? affId = null, string affCode = null);

        Task<ResultInfo> CheckAndUnlockUserIfLocked(string userName);

        Task<MemberSiteCustomerInfo> GetMemberSiteCustomer(string userName);


        // Need Removed
        Task<ResultInfo> CheckPlayerAccountIsActivedAsync(int affId, string language);
    }
}
