using System;
using System.Collections.Generic;
using Fanex.Affiliate.User.Core.DTO;
using Fanex.Affiliate.User.Core.DTO.UserPanel;
using Fanex.Affiliate.User.Core.MetalModels.UserPanel;
using Fanex.Affiliate.User.Core.DTO.UserPanel.MediaGallery;

namespace Fanex.Affiliate.User.Core.Repository.UserPanel
{
    public class MarketingRepository : IMarketingRepository
    {
        private const int AffiliateLink = 1;
        private const int MediaGallery = 2;

        public IEnumerable<CategoryType> GetCategoryList(bool isMediaPage, bool isInternal)
        {
            const string uSpKey = "Aff_Admin_MediaCategory_Sel";
            var dbMode = isInternal ? DBMode.View : DBMode.Main;

            using (var objectDb = new ObjectDBBuilder(uSpKey, dbMode))
            {
                return objectDb.DB.Query<CategoryType>(new { TypeId = isMediaPage ? AffiliateLink : MediaGallery });
            }
        }

        public IEnumerable<LanguageType> GetLanguageList(bool isInternal)
        {
            const string uSpKey = "Aff_Market_Sel";
            var dbMode = isInternal ? DBMode.View : DBMode.Main;

            using (var objectDb = new ObjectDBBuilder(uSpKey, dbMode))
            {
                return objectDb.DB.Query<LanguageType>();
            }
        }

        public IEnumerable<CurrencyType> GetCurrencyList(bool isInternal)
        {
            const string uSpKey = "Aff_Admin_MediaCurrency_Sel";
            var dbMode = isInternal ? DBMode.View : DBMode.Main;

            using (var objectDb = new ObjectDBBuilder(uSpKey, dbMode))
            {
                return objectDb.DB.Query<CurrencyType>();
            }
        }

        public IEnumerable<BannerType> GetBannerTypeList(bool isInternal)
        {
            const string uSpKey = "Aff_Admin_MediaType_Sel";
            var dbMode = isInternal ? DBMode.View : DBMode.Main;

            using (var objectDb = new ObjectDBBuilder(uSpKey, dbMode))
            {
                return objectDb.DB.Query<BannerType>();
            }
        }

        public IEnumerable<SizeType> GetSizeList(SizeParam param, bool isInternal)
        {
            const string uSpKey = "Aff_Admin_MediaSize_Sel";
            var dbMode = isInternal ? DBMode.View : DBMode.Main;

            using (var objectDb = new ObjectDBBuilder(uSpKey, dbMode))
            {
                return objectDb.DB.Query<SizeType>(param);
            }
        }

        public IEnumerable<Domain> GetDomainList(int affId, bool isInternal)
        {
            const string uSpKey = "Aff_User_Domain_Sel";

            var dbMode = isInternal ? DBMode.View : DBMode.Main;

            using (var objectDb = new ObjectDBBuilder(uSpKey, dbMode))
            {
                return objectDb.DB.Query<Domain>(new { AffId = affId });
            }
        }

        public IEnumerable<MarketingItem> GetBannerList(MarketingBannerParam bannerParam)
        {
            const string uSpKey = "Aff_Admin_Banner_Sel";

            var dbMode = bannerParam.IsInternal ? DBMode.View : DBMode.Main;

            using (var objectDb = new ObjectDBBuilder(uSpKey, dbMode))
            {
                return objectDb.DB.Query<MarketingItem>(bannerParam);
            }
        }

        public IEnumerable<MarketingItem> GetTextList(MarketingTextParam textParam)
        {
            const string uSpKey = "Aff_Admin_Text_Sel";
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