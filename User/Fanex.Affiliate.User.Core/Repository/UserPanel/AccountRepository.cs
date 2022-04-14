using Fanex.Affiliate.User.Core.Constants;
using Fanex.Affiliate.User.Core.DTO;
using Fanex.Affiliate.User.Core.DTO.UserPanel;
using Fanex.Affiliate.User.Core.MetalModels;
using Fanex.Affiliate.User.Core.MetalModels.UserPanel;
using System;
using System.Collections.Generic;
using System.Data;

namespace Fanex.Affiliate.User.Core.Repository.UserPanel
{
    public class AccountRepository : IAccountRepository
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

        public DepositAccountInfo GetDepositPlayerAccount(string playerUserName)
        {
            const string uSpKey = "Deposit_GetCustDataForCreateIBCACByUserName";

            using (var db = _builder.Build(uSpKey))
            {
                return db.QueryEntity<DepositAccountInfo>(new { deposit_username = playerUserName });
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


        public IbcPlayerAccountInsertResult InsertPlayerAccountForIbc(DepositAccountInfo accountInfo)
        {
            const string uSpKey = "Dot_Dep_CustomerInsMember_Ag_Cust";
            var result = new IbcPlayerAccountInsertResult();
            var parameterSet = new
            {
                accountInfo.FirstName,
                accountInfo.LastName,
                accountInfo.Email,
                accountInfo.DepositUserName,
                accountInfo.DepositUserId,
                accountInfo.Password,
                accountInfo.OddsType,
                accountInfo.AgentId,
                accountInfo.AffiliateId,
                accountInfo.BonusCode,
                accountInfo.Country,
                CustId = 0,
                UserName = string.Empty,
                ErrCode = 0,
                ErrMsg = string.Empty
            };

            using (var db = _builder.Build(uSpKey))
            {
                db.ExecuteNonQuery(parameterSet);

                result.IsOk = parameterSet.ErrCode == 0;
                result.Message = parameterSet.ErrMsg;

                result.CustId = parameterSet.CustId;
                result.UserName = parameterSet.UserName;
            }

            return result;
        }

        public ResultInfo UpdatePlayerAccountStatusForDeposit(DepositPlayerAccountParam account)
        {
            const string uSpKey = "Deposit_MemberIBCDetail";

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

        public IEnumerable<CommissionSetting> GetCommissionSettingList(int affId, bool isInternal)
        {
            const string uSpKey = "Aff_User_CommSetting_Sel";
            var mode = isInternal ? DBMode.View : DBMode.Main;

            using (var db = _builder.Build(uSpKey, mode))
            {
                return db.Query<CommissionSetting>(new { affId });
            }
        }

        public int CreateSecretKey(int affId, string secretKey)
        {
            const string uSpKey = "Aff_User_SecretKeyIns";

            using (var db = _builder.Build(uSpKey))
            {
                db.SetParameterValue("AffId", affId);
                db.SetParameterValue("SecretKey", secretKey);
                db.SetParameterValue("ErrCode", DbParam.ErrCodeSize);
                db.SetParameterValue("ErrMsg", DbParam.ErrMsgSize);

                db.ExecuteNonQuery();

                var code = db.GetParameterValue("ErrCode");

                return code == DBNull.Value ? 0 : Convert.ToInt32(code);
            }
        }

    }
}