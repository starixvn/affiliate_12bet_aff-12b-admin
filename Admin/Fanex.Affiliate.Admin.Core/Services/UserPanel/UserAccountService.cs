using Fanex.Affiliate.Admin.Core.Constants;
using Fanex.Affiliate.Admin.Core.DTO;
using Fanex.Affiliate.Admin.Core.DTO.UserPanel;
using Fanex.Affiliate.Admin.Core.MetaModels.UserPanel;
using Fanex.Affiliate.Admin.Core.Repository.UserPanel;
using Fanex.Affiliate.Admin.Core.Resources;
using Fanex.Affiliate.Admin.Core.Services.Common;
using System.Collections.Generic;

namespace Fanex.Affiliate.Admin.Core.Services.UserPanel
{
    public class UserAccountService : BaseService ,IUserAccountService
    {
        private readonly IUserAccountRepository _userAccountRepository;

        public UserAccountService(IUserAccountRepository userAccountRepository, IAuthService authService, ISessionService sessionService)
            : base(authService, sessionService)
        {
            _userAccountRepository = userAccountRepository;
        }

        public UserAccount GetUserAccountInfo(int affId, bool isSpecialUser = false)
        {
            return _userAccountRepository.GetUserAccountInfo(affId, isSpecialUser);
        }

        public ResultInfo UpdateUserAccount(UserAccountParam param)
        {
            var isValidPaymentMethod = ValidatePaymentMethod(param);
            SetNullForPropertiesNotHavePermission(param);

            if (isValidPaymentMethod)
            {
                return _userAccountRepository.UpdateUserAccount(param);
            }

            return new ResultInfo
            {
                Code = Code.GeneralErrorCode,
                Message = Lang.UpdateIsFailed
            };
        }

        public IEnumerable<SecurityQuestion> GetSecurityQuestions(string language, bool isIncludeOldData = false)
            => _userAccountRepository.GetSecurityQuestions(language, isIncludeOldData);

        private static bool ValidatePaymentMethod(UserAccountParam param)
        {
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
