using System.Collections.Generic;
using Fanex.Affiliate.User.Core.DTO;
using Fanex.Affiliate.User.Core.DTO.UserPanel;
using Fanex.Affiliate.User.Core.MetalModels;
using Fanex.Affiliate.User.Core.MetalModels.UserPanel;

namespace Fanex.Affiliate.User.Core.Repository.UserPanel
{
    public interface IAccountRepository
    {
        AccountInformation GetAccountInfo(int affId);

        DepositAccountInfo GetDepositPlayerAccount(string playerUserName);

        ResultInfo UpdateAccountInformation(UpdateAccountParam accountUpdateParam);

        ResultInfo UpdatePassword(UpdatePasswordParam updatePasswordParam);

        UserInfo GetUserInfoByEmail(UserInfoByEmailParam userInfoParam);

        UserInfo RecoverPassword(PasswordRecoveryParam passwordRecoveryParam);

        UserInfo GetUserInfoById(int affId, bool isInternal);

        PlayerAccountCheckingResult CheckPlayerAccountIsActived(int affId, string language);

        IbcPlayerAccountInsertResult InsertPlayerAccountForIbc(DepositAccountInfo accountInfo);

        ResultInfo UpdatePlayerAccountStatusForDeposit(DepositPlayerAccountParam account);

        ResultInfo CreateAffiliate(UserSignUpParam signUpParam);

        int CreateSecretKey(int affId, string secretKey);

        IEnumerable<CommissionSetting> GetCommissionSettingList(int affId, bool isInternal);

    }
}