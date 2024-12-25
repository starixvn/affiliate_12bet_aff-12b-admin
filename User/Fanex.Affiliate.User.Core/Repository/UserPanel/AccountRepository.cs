namespace Fanex.Affiliate.User.Core.Repository.UserPanel
{
    using System;
    using Commands.Account;
    using Criterias;
    using Data.Repository;
    using DTO;
    using Extensions.Mappers;
    using Fanex.Affiliate.User.Core.DTO.UserPanel;
    using MetalModels;

    public class AccountRepository : BaseRepository, IAccountRepository
    {
        private readonly IObjectDBBuilder _builder;
        private readonly IDynamicRepository _repository;

        public AccountRepository(IObjectDBBuilder builder, IDynamicRepository repository)
        {
            _builder = builder;
            _repository = repository;
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

        public ResultInfo CreateAffiliate(UserSignUpParam signUpParam)
        {
            var command = new CreateAffiliateCommand();
            command.Map(signUpParam);
            _repository.Execute(command);

            return new ResultInfo
            {
                IsOk = command.ErrCode == 0,
                Message = command.ErrMsg
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
            return _repository.Get<GeneralAffInfo>(new GetGeneralAffInfoCriteria(affId, affCode));
        }
    }
}