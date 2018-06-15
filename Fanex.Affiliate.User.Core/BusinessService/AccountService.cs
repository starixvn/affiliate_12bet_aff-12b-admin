using System.Collections.Generic;
using Fanex.Affiliate.User.Core.DTO;
using Fanex.Affiliate.User.Core.MetalModels;
using Fanex.Affiliate.User.Core.MetalModels.UserPanel;
using Fanex.Affiliate.User.Core.Repository.UserPanel;
using Fanex.Affiliate.User.Core.Resources;

namespace Fanex.Affiliate.User.Core.BusinessService
{
    public class AccountService : IAccountService
    {
        private readonly IAccountRepository _accountRepository;

        private static readonly IDictionary<int, string> MessageByCode = new Dictionary<int, string>
        {
            [101] = Lang.errUserNotFound,
            [102] = Lang.errExistAccount
        };

        public AccountService(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        public ResultInfo CreateAffiliate(UserSignUpParam signUpParam)
        {
            return new ResultInfo();
        }

        public ResultInfo CheckPlayerAccountIsActived(int affId, string language)
        {
            var checkingResult = _accountRepository.CheckPlayerAccountIsActived(affId, language);

            if (!checkingResult.IsOk)
            {
                return ActivePlayerAccount(checkingResult.PlayerUserName);
            }

            return checkingResult;
        }

        private ResultInfo ActivePlayerAccount(string playerUserName)
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

            var insertingResult = _accountRepository.InsertPlayerAccountForIbc(depositAccount);
            if (insertingResult.IsOk)
            {
                return UpdatePlayerAccountStatus(playerUserName, insertingResult.CustId, insertingResult.UserName);
            }

            return insertingResult;
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