using System.Collections.Generic;
using Fanex.Affiliate.User.Core.DTO;
using Fanex.Affiliate.User.Core.DTO.UserPanel;

namespace Fanex.Affiliate.User.Core.Repository.UserPanel
{
    public class MarketingRepository : IMarketingRepository
    {
        public IEnumerable<BannerType> GetBannerTypeList(bool isInternal)
        {
            const string uSpKey = "Aff_BannerSelType_1";
            var dbMode = isInternal ? DBMode.View : DBMode.Main;

            using (var objectDb = new ObjectDBBuilder(uSpKey, dbMode))
            {
                return objectDb.DB.Query<BannerType>();
            }
        }

        public IEnumerable<Domain> GetDomainList(int affId, bool isInternal)
        {
            const string uSpKey = "Aff_DomainAffClientSel";

            var dbMode = isInternal ? DBMode.View : DBMode.Main;

            using (var objectDb = new ObjectDBBuilder(uSpKey, dbMode))
            {
                return objectDb.DB.Query<Domain>(new { AffId = affId });
            }
        }
    }
}
