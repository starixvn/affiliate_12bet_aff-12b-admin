using Fanex.Affiliate.User.Core.ViewModels;

namespace Fanex.Affiliate.User.Core.Repository.UserPanel
{
    public class AccountRepository : IAccountRepository
    {
        public AccountInformationViewModel GetAccountInfo(int affId)
        {
            const string uSpKey = "Aff_AffiliateSelEditInfo";

            using (var objectDb = new ObjectDBBuilder(uSpKey))
            {
                var parameterSet = new { affId };

                return objectDb.DB.QueryEntity<AccountInformationViewModel>(parameterSet);
            }
        }
    }
}