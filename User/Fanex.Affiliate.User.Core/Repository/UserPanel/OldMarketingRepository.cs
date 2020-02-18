using System;
using System.Collections.Generic;
using Fanex.Affiliate.User.Core.DTO;
using Fanex.Affiliate.User.Core.DTO.UserPanel;
using Fanex.Affiliate.User.Core.MetalModels.UserPanel;

namespace Fanex.Affiliate.User.Core.Repository.UserPanel
{
    public class OldMarketingRepository : IOldMarketingRepository
    {
        public IEnumerable<OldBannerType> GetBannerTypeList(bool isInternal)
        {
            const string uSpKey = "Aff_BannerSelType_1";
            var dbMode = isInternal ? DBMode.View : DBMode.Main;

            using (var objectDb = new ObjectDBBuilder(uSpKey, dbMode))
            {
                return objectDb.DB.Query<OldBannerType>();
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

        public IEnumerable<MarketingItem> GetBannerList(OldMarketingBannerParam bannerParam, string uSpKey)
        {
            var dbMode = bannerParam.IsInternal ? DBMode.View : DBMode.Main;

            using (var objectDb = new ObjectDBBuilder(uSpKey, dbMode))
            {
                return objectDb.DB.Query<MarketingItem>(bannerParam);
            }
        }

        public IEnumerable<MarketingItem> GetTextList(OldMarketingTextParam textParam)
        {
            const string uSpKey = "Aff_TextSel_1";
            var dbMode = textParam.IsInternal ? DBMode.View : DBMode.Main;

            using (var objectDb = new ObjectDBBuilder(uSpKey, dbMode))
            {
                return objectDb.DB.Query<MarketingItem>(textParam);
            }
        }

        public IEnumerable<LandingPage> GetLandingPageList(bool isInternal)
        {
            const string SpKey = "Aff_Admin_LandingPage_Sel";
            var dbMode = isInternal ? DBMode.View : DBMode.Main;
            var result = new List<LandingPage>();

            using (var objectDb = new ObjectDBBuilder(SpKey, dbMode))
            {
                using (var reader = objectDb.DB.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        result.Add(new LandingPage
                        {
                            PageName = Convert.ToString(reader["PageName"]),
                            PageCode = Convert.ToString(reader["PageCode"])
                        });
                    }
                }
            }

            return result;
        }
    }
}
