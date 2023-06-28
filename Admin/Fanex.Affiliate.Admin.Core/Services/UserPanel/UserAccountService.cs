using Fanex.Affiliate.Admin.Core.Configurations;
using Fanex.Affiliate.Admin.Core.Constants;
using Fanex.Affiliate.Admin.Core.DTO;
using Fanex.Affiliate.Admin.Core.DTO.UserPanel;
using Fanex.Affiliate.Admin.Core.Extensions;
using Fanex.Affiliate.Admin.Core.MetaModels.UserPanel;
using Fanex.Affiliate.Admin.Core.Repository.UserPanel;
using Fanex.Affiliate.Admin.Core.Resources;
using Fanex.Affiliate.Admin.Core.Services.Common;
using System.Collections.Generic;

namespace Fanex.Affiliate.Admin.Core.Services.UserPanel
{
    public class UserAccountService : BaseService, IUserAccountService
    {
        private readonly IUserAccountRepository _userAccountRepository;

        public UserAccountService(IUserAccountRepository userAccountRepository,
            IAuthService authService,
            ISessionService sessionService)
            : base(authService, sessionService)
        {
            _userAccountRepository = userAccountRepository;
        }

        public UserAccount GetUserAccountInfo(int affId)
        {
            var userId = SessionService.UserId;
            return AuthService.IsUserHasPermissionOnAff(userId, affId)
                ? _userAccountRepository.GetUserAccountInfo(affId)
                : null;
        }

        public ResultInfo UpdateUserAccount(UserAccountParam param)
        {
            var validateWebSite = ValidateWebSite(param);
            if (!validateWebSite.IsSuccessful)
            {
                return validateWebSite;
            }

            var isValidPaymentMethod = ValidatePaymentMethod(param);
            var isUserHasPermissionOnAff = AuthService.IsUserHasPermissionOnAff(SessionService.UserId, param.AffId);
            SetNullForPropertiesNotHavePermission(param);

            if ((!HasUpdatePaymentMethodPermission() || isValidPaymentMethod) && isUserHasPermissionOnAff)
            {
                return _userAccountRepository.UpdateUserAccount(param);
            }

            return ResultInfo.GenerateErr(Lang.UpdateIsFailed);
        }

        public IEnumerable<SecurityQuestion> GetSecurityQuestions(string language, bool isIncludeOldData = false)
            => _userAccountRepository.GetSecurityQuestions(language, isIncludeOldData);

        private static ResultInfo ValidateWebSite(UserAccountParam param)
        {
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

        private static bool ValidatePaymentMethod(UserAccountParam param)
        {
            var isPlayerUsername = PaymentMethod.Player.Equals(param.PreferredPayment) && !string.IsNullOrEmpty(param.PlayerUsername);
            var isNetellerAccountId = PaymentMethod.Neteller.Equals(param.PreferredPayment) && !string.IsNullOrEmpty(param.NetellerAccountId);
            var isMoneybookersEmail = PaymentMethod.Moneybookers.Equals(param.PreferredPayment) && !string.IsNullOrEmpty(param.MoneybookersEmail);
            var isBankTranfer = PaymentMethod.Wire.Equals(param.PreferredPayment) && IsValidWirePaymentMethod(param);

            return isPlayerUsername || isNetellerAccountId || isMoneybookersEmail || isBankTranfer;
        }

        private bool HasUpdatePaymentMethodPermission()
        {
            var userClaim = SessionService.UserClaim;
            return userClaim.HasClaim(Function.AffPaymentInfo);
        }

        private static bool IsValidWirePaymentMethod(UserAccountParam param)
        {
            return !(string.IsNullOrEmpty(param.BankName) || string.IsNullOrEmpty(param.BankAddress) ||
                string.IsNullOrEmpty(param.BankAccountName) || string.IsNullOrEmpty(param.BankAccountNumber));
        }

    }
}
