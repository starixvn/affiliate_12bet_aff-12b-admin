using Fanex.Affiliate.Admin.Core.DTO;

namespace Fanex.Affiliate.Admin.Core.Repository
{
    public class AccountRepository : BaseRepository, IAccountRepository
    {
        public DepositAccountInfo GetDepositPlayerAccount(string playerUserName)
        {
            const string uSpKey = "Deposit_GetCustDataForCreateIBCACByUserName";

            using (var objectDb = GetObjectDb(uSpKey))
            {
                return objectDb.QueryEntity<DepositAccountInfo>(new { deposit_username = playerUserName });
            }
        }
    }
}