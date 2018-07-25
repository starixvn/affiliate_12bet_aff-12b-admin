using Fanex.Affiliate.Admin.Core.DTO;

namespace Fanex.Affiliate.Admin.Core.Repository
{
    public interface IAccountRepository
    {
        DepositAccountInfo GetDepositPlayerAccount(string playerUserName);
    }
}
