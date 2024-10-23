using Fanex.Affiliate.User.Core.BusinessService.Apis;
using Fanex.Affiliate.User.Core.DTO;
using Fanex.Affiliate.User.Core.MetalModels;
using Fanex.Affiliate.User.Core.MetalModels.Email;
using Fanex.Affiliate.User.Core.MetalModels.UserPanel;
using Fanex.Affiliate.User.Core.Repository.UserPanel;
using Fanex.Affiliate.User.Core.Resources;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Fanex.Affiliate.User.Core.BusinessService
{
    public class AccountService : IAccountService
    {
        private readonly IAccountRepository _accountRepository;

        private readonly IEmailService _emailService;

        private readonly ILicenseeCustDbApiService _licenseeCustDbApiService;

        private static readonly IDictionary<int, string> MessageByCode = new Dictionary<int, string>
        {
            [101] = Lang.errUserNotFound,
            [102] = Lang.errExistAccount
        };

        public AccountService(
            IAccountRepository accountRepository,
            IEmailService emailService,
            ILicenseeCustDbApiService licenseeCustDbApiService)
        {
            _accountRepository = accountRepository;
            _emailService = emailService;
            _licenseeCustDbApiService = licenseeCustDbApiService;
        }

        public ResultInfo CreateAffiliate(UserSignUpParam signUpParam, EmailSignUpParam emailParam)
        {
            if (!signUpParam.IsLocal)
            {
                var depositAccount = _accountRepository.GetDepositPlayerAccount(signUpParam.Player_UserName);

                if (!depositAccount.IsValid)
                {
                    return new ResultInfo { IsOk = false, Message = Lang.errDepositDBConnectFailed };
                }
            }

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
    }
}