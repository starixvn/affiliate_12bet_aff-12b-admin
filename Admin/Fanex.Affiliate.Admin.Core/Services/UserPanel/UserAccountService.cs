using Fanex.Affiliate.Admin.Core.Constants;
using Fanex.Affiliate.Admin.Core.DTO;
using Fanex.Affiliate.Admin.Core.DTO.UserPanel;
using Fanex.Affiliate.Admin.Core.MetaModels.UserPanel;
using Fanex.Affiliate.Admin.Core.Repository.UserPanel;
using Fanex.Affiliate.Admin.Core.Services.Common;
using System.Collections.Generic;

namespace Fanex.Affiliate.Admin.Core.Services.UserPanel
{
    public class UserAccountService : ProtectingPropertiesService, IUserAccountService
    {
        private readonly IUserAccountRepository _userAccountRepository;

        public UserAccountService(IUserAccountRepository userAccountRepository,
            IAuthService authService,
            ISessionService sessionService,
            IAffiliateService affiliateService)
            : base(authService, sessionService, affiliateService)
        {
            _userAccountRepository = userAccountRepository;
        }

        public UserAccount GetUserAccountInfo(int affId)
        {
            return HasReadMultiPermissions(affId, Function.EditAffiliate)
                ? _userAccountRepository.GetUserAccountInfo(affId)
                : null;
        }

        public ResultInfo UpdateUserAccount(UserAccountParam param)
        {
            if (!HasHostMultiPermissions(param.AffId, Function.EditAffiliate))
            {
                return NotHavePermissionOnAffResult;
            }

            var validateWebSite = ValidateWebSite(param);
            if (!validateWebSite.IsSuccessful)
            {
                return validateWebSite;
            }

            var isValidPaymentMethod = ValidatePaymentMethod(param);
            if (isValidPaymentMethod)
            {
                SetNullForPropertiesNotHavePermission(param);
                return _userAccountRepository.UpdateUserAccount(param);
            }

            return ResultInfo.GenerateErr("The payment method is invalid!");
        }

        public IEnumerable<SecurityQuestion> GetSecurityQuestions(string language, bool isIncludeOldData = false)
            => _userAccountRepository.GetSecurityQuestions(language, isIncludeOldData);

        private ResultInfo ValidateWebSite(UserAccountParam param)
        {
            if (!HasHostFunctionPermission(Function.AffWebUrl))
            {
                return new ResultInfo();
            }

            if (param.HasWebSite && string.IsNullOrEmpty(param.SiteUrl))
            {
                return ResultInfo.GenerateErr("Website URL is required");
            }

            if (!param.HasWebSite && string.IsNullOrEmpty(param.NoSiteDescription))
            {
                return ResultInfo.GenerateErr($"Please describe how you would like to promote 12Bet if you don't have the URL is required");
            }

            return new ResultInfo();
        }

        private bool ValidatePaymentMethod(UserAccountParam param)
        {
            if (!HasHostFunctionPermission(Function.AffPaymentInfo))
            {
                return true;
            }

            var isPlayerUsername = PaymentMethod.Player.Equals(param.PreferredPayment) && !string.IsNullOrEmpty(param.PlayerUsername);
            var isNetellerAccountId = PaymentMethod.Neteller.Equals(param.PreferredPayment) && !string.IsNullOrEmpty(param.NetellerAccountId);
            var isMoneybookersEmail = PaymentMethod.Moneybookers.Equals(param.PreferredPayment) && !string.IsNullOrEmpty(param.MoneybookersEmail);
            var isBankTranfer = PaymentMethod.Wire.Equals(param.PreferredPayment) && IsValidWirePaymentMethod(param);

            return isPlayerUsername || isNetellerAccountId || isMoneybookersEmail || isBankTranfer;
        }

        private static bool IsValidWirePaymentMethod(UserAccountParam param)
        {
            return !(string.IsNullOrEmpty(param.BankName) || string.IsNullOrEmpty(param.BankAddress) ||
                string.IsNullOrEmpty(param.BankAccountName) || string.IsNullOrEmpty(param.BankAccountNumber));
        }

    }
}
