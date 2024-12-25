namespace Fanex.Affiliate.User.Core.BusinessService
{
    using System.Threading.Tasks;
    using DTO;
    using DTO.UserPanel;
    using Fanex.Affiliate.User.Core.DTO.Apis;
    using MetalModels;
    using MetalModels.Email;

    public interface IAccountService
    {
        Task<ResultInfo> CreateAffiliateAsync(UserSignUpParam signUpParam, EmailSignUpParam emailParam);

        GeneralAffInfo GetGeneralAffInfo(int? affId = null, string affCode = null);

        Task<ResultInfo> CheckAndUnlockUserIfLocked(string userName);

        Task<MemberSiteCustomerInfo> GetMemberSiteCustomer(string userName);

        AccountInformation GetAccountInfo(int affId);

        ResultInfo UpdateAccountInformation(UpdateAccountParam accountUpdateParam);

        UserInfo GetUserInfoById(int affId, bool isInternal);

        int CreateSecretKey(int affId, string secretKey);

        (int, UserInfo) GetUserInfoByEmail(UserInfoByEmailParam userInfoParam);

        (int, UserInfo) RecoverPassword(PasswordRecoveryParam passwordRecoveryParam);

        ResultInfo UpdatePassword(UpdatePasswordParam updatePasswordParam);
    }
}
