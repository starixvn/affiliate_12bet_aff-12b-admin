namespace Fanex.Affiliate.User.Core.BusinessService
{
    using Apis;
    using Commands;
    using Commands.Account;
    using CoreService;
    using Criterias;
    using Data.Repository;
    using DTO;
    using DTO.UserPanel;
    using Fanex.Affiliate.User.Core.Constants.Apis;
    using Fanex.Affiliate.User.Core.DTO.Apis;
    using MetalModels;
    using MetalModels.Email;
    using Resources;
    using System.Threading.Tasks;
    using Utilities;

    public class AccountService : BaseDynamicService, IAccountService
    {
        private readonly IEmailService _emailService;
        private readonly ILicenseeApi _licenseeApi;
        private readonly IAppSetting _appSetting;

        private const int TEST_CURRENCY = 20;

        public AccountService(
            IDynamicRepository dynamicRepository,
            IEmailService emailService,
            ILicenseeApi licenseeApi,
            IAppSetting appSetting) : base(dynamicRepository)
        {
            _emailService = emailService;
            _licenseeApi = licenseeApi;
            _appSetting = appSetting;
        }

        public async Task<ResultInfo> CreateAffiliateAsync(UserSignUpParam signUpParam, EmailSignUpParam emailParam)
        {
            var cust = await GetMemberSiteCustomer(signUpParam.Player_UserName);
            var validInfo = CheckPlayerUserNameValid(cust);

            if (!validInfo.IsOk)
            {
                return validInfo;
            }

            signUpParam.Player_CustId = cust.CustId;
            signUpParam.Player_Currency = cust.IsTest.GetValueOrDefault(true) ? TEST_CURRENCY : cust.CurrencyId.GetValueOrDefault();

            var result = ExecuteReturnResultInfo(signUpParam.MapTo<UserSignUpParam, CreateAffiliateCommand>());
            if (result.IsOk)
            {
                emailParam.MailTo = signUpParam.Email;
                emailParam.User = new UserInfo
                {
                    PreferredLanguage = signUpParam.PreferredLanguage,
                    FirstName = signUpParam.FirstName
                };

                _emailService.SendSignUpEmail(emailParam);

                var csdEmailParam = new NoticeEmailParam
                {
                    MailTo = emailParam.MailContact,
                    MailContact = emailParam.MailContact,
                    FirstName = signUpParam.FirstName,
                    LastName = signUpParam.LastName,
                    UserName = signUpParam.UserName,
                    SiteUrl = signUpParam.SiteUrl,
                    MoreSiteUrls = signUpParam.MoreSiteUrls,
                    NoSiteDesc = signUpParam.NoSiteDesc
                };

                _emailService.SendNoticeEmailToCsd(csdEmailParam);
            }

            return result;
        }

        public GeneralAffInfo GetGeneralAffInfo(int? affId = null, string affCode = null)
        {
            var affInfo = GetTData<GeneralAffInfo>(new GetGeneralAffInfoCriteria(affId, affCode)) ?? new GeneralAffInfo();

            return affInfo;
        }

        public async Task<ResultInfo> CheckAndUnlockUserIfLocked(string userName)
        {
            var cust = await GetMemberSiteCustomer(userName);
            var result = CheckPlayerUserNameValid(cust);
            if (cust?.Status == CustomerStatus.LOCKED)
            {
                var apiResult = await _licenseeApi.UnlockUser(new[] { cust.CustId });
                result.IsOk = apiResult.IsSuccess;
                result.Message = apiResult.ErrMsg;
            }
            return result;
        }

        public async Task<MemberSiteCustomerInfo> GetMemberSiteCustomer(string userName)
        {
            var siteId = _appSetting.MemberSiteId;
            return await _licenseeApi.GetCustomerInfoAsync(userName, siteId);
        }

        public AccountInformation GetAccountInfo(int affId)
        {
            return GetTData<AccountInformation>(new GetAccountInfoCriteria(affId));
        }

        public ResultInfo UpdateAccountInformation(UpdateAccountParam accountUpdateParam)
        {
            return ExecuteReturnResultInfo(accountUpdateParam.MapTo<UpdateAccountParam, UpdateAccountInfoCommand>());
        }

        public UserInfo GetUserInfoById(int affId, bool isInternal)
        {
            return GetTData<UserInfo>(new GetUserInfoByIdCriteria(affId, isInternal));
        }

        public int CreateSecretKey(int affId, string secretKey)
        {
            var result = ExecuteReturnResultInfo(new CreateSecretKeyCommand(affId, secretKey));

            return result.Code;
        }

        public (int, UserInfo) GetUserInfoByEmail(UserInfoByEmailParam userInfoParam)
        {
            var criteria = userInfoParam.MapTo<UserInfoByEmailParam, GetUserInfoByEmailCriteria>();
            var result = GetTData<UserInfo>(criteria);

            return (criteria.ErrCode, result);
        }

        public (int, UserInfo) RecoverPassword(PasswordRecoveryParam passwordRecoveryParam)
        {
            var criteria = passwordRecoveryParam.MapTo<PasswordRecoveryParam, GetRecoverPasswordCriteria>();
            var result = GetTData<UserInfo>(criteria);

            return (criteria.ErrCode, result);
        }

        public ResultInfo UpdatePassword(UpdatePasswordParam updatePasswordParam)
        {
            return ExecuteReturnResultInfo(updatePasswordParam.MapTo<UpdatePasswordParam, UpdatePasswordCommand>());
        }

        private static ResultInfo CheckPlayerUserNameValid(MemberSiteCustomerInfo cust)
        {
            var validInfo = new ResultInfo
            {
                IsOk = false
            };

            if (cust == null)
            {
                validInfo.Message = Lang.UserNameNotExist;
            }
            else
            {
                switch (cust.Status)
                {
                    case CustomerStatus.RUNNING:
                        validInfo.IsOk = true;
                        break;
                    case CustomerStatus.SUSPENDED:
                        validInfo.IsOk = false;
                        validInfo.Message = Lang.UserNameSuspended;
                        break;
                    case CustomerStatus.CLOSED:
                    case CustomerStatus.LOCKED:
                        validInfo.IsOk = false;
                        validInfo.Message = Lang.UserNameClosed;
                        break;
                    default:
                        validInfo.IsOk = false;
                        validInfo.Message = Lang.errDepositDBConnectFailed;
                        break;
                }
            }

            return validInfo;
        }
    }
}