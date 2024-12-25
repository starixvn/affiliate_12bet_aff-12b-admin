using Fanex.Affiliate.User.Core.DTO;
using Fanex.Affiliate.User.Core.DTO.UserPanel;
using Fanex.Affiliate.User.Core.MetalModels;

namespace Fanex.Affiliate.User.Core.Repository.UserPanel
{
    public interface IAccountRepository
    {
        AccountInformation GetAccountInfo(int affId);

        ResultInfo UpdatePassword(UpdatePasswordParam updatePasswordParam);

        UserInfo GetUserInfoByEmail(UserInfoByEmailParam userInfoParam);

        UserInfo RecoverPassword(PasswordRecoveryParam passwordRecoveryParam);

        UserInfo GetUserInfoById(int affId, bool isInternal);
    }
}