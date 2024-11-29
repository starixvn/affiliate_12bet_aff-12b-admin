using System.Collections.Generic;
using System.Threading.Tasks;
using Fanex.Affiliate.User.Core.Apis;
using Fanex.Affiliate.User.Core.BusinessService.Apis;
using Fanex.Affiliate.User.Core.Constants.Apis;
using Fanex.Affiliate.User.Core.DTO;
using Fanex.Affiliate.User.Core.DTO.Apis;
using Fanex.Affiliate.User.Core.MetalModels;
using Fanex.Affiliate.User.Core.MetalModels.Email;
using Fanex.Affiliate.User.Core.MetalModels.UserPanel;
using Fanex.Affiliate.User.Core.Repository.UserPanel;
using Fanex.Affiliate.User.Core.Resources;
using Fanex.Affiliate.User.Core.Utilities;

namespace Fanex.Affiliate.User.Core.BusinessService
{
    public class AccountService : IAccountService
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IEmailService _emailService;
        private readonly ILicenseeApi _licenseeApi;
        private readonly IAppSetting _appSetting;

        private readonly ILicenseeCustDbApiService _licenseeCustDbApiService;

        private static readonly IDictionary<int, string> MessageByCode = new Dictionary<int, string>
        {
            [101] = Lang.errUserNotFound,
            [102] = Lang.errExistAccount
        };

        private const int TEST_CURRENCY = 20;

        public AccountService(
            IAccountRepository accountRepository,
            IEmailService emailService,
            ILicenseeCustDbApiService licenseeCustDbApiService,
            ILicenseeApi licenseeApi,
            IAppSetting appSetting)
        {
            _accountRepository = accountRepository;
            _emailService = emailService;
            _licenseeCustDbApiService = licenseeCustDbApiService;
            _licenseeApi = licenseeApi;
            _appSetting = appSetting;
        }

        /// <summary>
        /// CodeReview:
        /// clean code
        /// </summary>
        public async Task<ResultInfo> CheckPlayerAccountIsActivedAsync(int affId, string language)
        {
            var checkingResult = _accountRepository.CheckPlayerAccountIsActived(affId, language);

            if (!checkingResult.IsOk)
            {
                return await ActivePlayerAccountAsync(checkingResult.PlayerUserName);
            }

            return checkingResult;
        }

        private async Task<ResultInfo> ActivePlayerAccountAsync(string playerUserName)
        {
            var depositAccount = _accountRepository.GetDepositPlayerAccount(playerUserName);

            if (!depositAccount.IsValid)
            {
                return new ResultInfo
                {
                    IsOk = false,
                    Message = Lang.errDepositDBConnectFailed
                };
            }

            var insertingResult = await _licenseeCustDbApiService.Insert12BetPlayerAccountToIBCAsync(depositAccount);
            if (!insertingResult.IsSuccessful)
            {
                return new ResultInfo
                {
                    IsOk = insertingResult.IsSuccessful,
                    Message = Lang.errDepositDBConnectFailed
                };
            }
            return UpdatePlayerAccountStatus(playerUserName, insertingResult.Result.CustID, insertingResult.Result.Username);
        }

        private ResultInfo UpdatePlayerAccountStatus(string playerUserName, int custId, string ibcUserName)
        {
            var depositAccountParam = new DepositPlayerAccountParam
            {
                DepUserName = playerUserName,
                IbcCustId = custId,
                IbcUserName = ibcUserName
            };

            var updateResult = _accountRepository.UpdatePlayerAccountStatusForDeposit(depositAccountParam);
            if (MessageByCode.ContainsKey(depositAccountParam.ErrorCode))
            {
                updateResult.Message = MessageByCode[depositAccountParam.ErrorCode];
            }

            return updateResult;
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

            var result = _accountRepository.CreateAffiliate(signUpParam);
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
            return _accountRepository.GetGeneralAffInfo(affId, affCode) ?? new GeneralAffInfo();
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