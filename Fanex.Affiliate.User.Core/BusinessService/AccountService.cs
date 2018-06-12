using System;
using Fanex.Affiliate.User.Core.DTO;
using Fanex.Affiliate.User.Core.Repository.UserPanel;

namespace Fanex.Affiliate.User.Core.BusinessService
{
    public class AccountService : IAccountService
    {
        private readonly IAccountRepository _accountRepository;

        public AccountService(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        public ResultInfo CheckPlayerAccountIsActived(int affId)
        {
            throw new NotImplementedException();
        }
    }
}
