using Fanex.Affiliate.User.Core.DTO;
using Fanex.Affiliate.User.Core.DTO.UserPanel;
using Fanex.Affiliate.User.Core.MetalModels;
using Fanex.Affiliate.User.Core.MetalModels.UserPanel;
using System;

namespace Fanex.Affiliate.User.Core.Repository.UserPanel
{
    public class AccountRepository : BaseRepository, IAccountRepository
    {
        private readonly IObjectDBBuilder _builder;

        public AccountRepository(IObjectDBBuilder builder)
        {
            _builder = builder;
        }

        public AccountInformation GetAccountInfo(int affId)
        {
            const string uSpKey = "Aff_User_UserInfoSel";

            using (var db = _builder.Build(uSpKey))
            {
                var parameterSet = new { affId };

                return db.QueryEntity<AccountInformation>(parameterSet);
            }
        }

        public DepositCustomerInfo GetDepositPlayerAccount(string playerUserName)
        {
            const string uSpKey = "Aff_Deposit_GetCustDataForCreateIBCACByUserName";

            using (var db = _builder.Build(uSpKey))
            {
                return db.QueryEntity<DepositCustomerInfo>(new { deposit_username = playerUserName });
            }
        }

        public ResultInfo UpdateAccountInformation(UpdateAccountParam accountUpdateParam)
        {
            const string uSpKey = "Aff_User_UserInfoUpd";

            using (var db = _builder.Build(uSpKey))
            {
                db.ExecuteNonQuery(accountUpdateParam);
            }

            return new ResultInfo
            {
                IsOk = accountUpdateParam.ErrCode == 0,
                Message = accountUpdateParam.ErrMsg
            };
        }

        public ResultInfo UpdatePassword(UpdatePasswordParam updatePasswordParam)
        {
            const string uSpKey = "Aff_AffiliateUpdPwd";
            var parameterSet = new
            {
                updatePasswordParam.AffId,
                updatePasswordParam.UserId,
                updatePasswordParam.Ip,
                updatePasswordParam.Language,
                updatePasswordParam.ErrCode,
                updatePasswordParam.ErrMsg,
                OldPwd = updatePasswordParam.OldPassword,
                AffPwd = updatePasswordParam.NewPassword
            };

            using (var db = _builder.Build(uSpKey))
            {
                db.ExecuteNonQuery(parameterSet);
            }

            return new ResultInfo
            {
                IsOk = parameterSet.ErrCode == 0,
                Message = parameterSet.ErrMsg
            };
        }

        public UserInfo GetUserInfoByEmail(UserInfoByEmailParam userInfoParam)
        {
            const string uSpKey = "Aff_User_ForgotPassword";

            using (var db = _builder.Build(uSpKey))
            {
                return db.QueryEntity<UserInfo>(userInfoParam);
            }
        }

        public UserInfo RecoverPassword(PasswordRecoveryParam passwordRecoveryParam)
        {
            const string uSpKey = "Aff_User_RecoverPassWord";

            using (var db = _builder.Build(uSpKey))
            {
                return db.QueryEntity<UserInfo>(passwordRecoveryParam);
            }
        }

        public UserInfo GetUserInfoById(int affId, bool isInternal)
        {
            const string uSpKey = "Aff_User_GeneralInfo_Sel";
            var mode = isInternal ? DBMode.View : DBMode.Main;

            using (var db = _builder.Build(uSpKey, mode))
            {
                return db.QueryEntity<UserInfo>(new { affId });
            }
        }

        public PlayerAccountCheckingResult CheckPlayerAccountIsActived(int affId, string language)
        {
            var result = new PlayerAccountCheckingResult();
            const string uSpKey = "Aff_CustomerCheck12BetPlayerAccountActivated";
            var parameterSet = new
            {
                AffId = affId,
                Language = language,
                Result = false,
                ErrCode = 0,
                ErrMsg = string.Empty
            };

            using (var db = _builder.Build(uSpKey))
            {
                db.ExecuteNonQuery(parameterSet);

                result.IsOk = parameterSet.Result;
                result.Message = parameterSet.ErrMsg;
                result.PlayerUserName = db.GetParameterValue("12BetPlayerUserName") as string;
            }

            return result;
        }

        public ResultInfo UpdatePlayerAccountStatusForDeposit(DepositPlayerAccountParam account)
        {
            const string uSpKey = "Aff_Deposit_MemberIBCDetail";

            using (var db = _builder.Build(uSpKey))
            {
                db.ExecuteNonQuery(account);
            }

            return new ResultInfo
            {
                IsOk = account.ErrorCode == 0,
                Message = account.ErrorMsg
            };
        }

        public ResultInfo CreateAffiliate(UserSignUpParam signUpParam)
        {
            const string uSpKey = "Aff_User_Affiliate_Ins";

            using (var db = _builder.Build(uSpKey))
            {
                db.ExecuteNonQuery(signUpParam);
            }

            return new ResultInfo
            {
                IsOk = signUpParam.ErrCode == 0,
                Message = signUpParam.ErrMsg
            };
        }

        public int CreateSecretKey(int affId, string secretKey)
        {
            const string uSpKey = "Aff_User_SecretKeyIns";

            using (var db = _builder.Build(uSpKey))
            {
                db.SetParameterValue("AffId", affId);
                db.SetParameterValue("SecretKey", secretKey);
                AssignResultOutputParam(db);

                db.ExecuteNonQuery();

                var code = db.GetParameterValue("ErrCode");

                return code == DBNull.Value ? 0 : Convert.ToInt32(code);
            }
        }

        public GeneralAffInfo GetGeneralAffInfo(int? affId = null, string affCode = null)
        {
            const string uSpKey = "Aff_Get_GeneralAffInfo";

            using (var objectDb = _builder.Build(uSpKey))
            {
                var parameterSet = new { affId, affCode };

                return objectDb.QueryEntity<GeneralAffInfo>(parameterSet);
            }
        }
    }
}