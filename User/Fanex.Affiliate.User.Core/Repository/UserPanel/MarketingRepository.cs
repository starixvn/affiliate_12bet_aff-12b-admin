using Fanex.Affiliate.User.Core.DTO;
using Fanex.Affiliate.User.Core.DTO.UserPanel;
using Fanex.Affiliate.User.Core.DTO.UserPanel.MediaGallery;
using Fanex.Affiliate.User.Core.MetalModels;
using Fanex.Affiliate.User.Core.MetalModels.UserPanel;
using System.Collections.Generic;

namespace Fanex.Affiliate.User.Core.Repository.UserPanel
{
    public class MarketingRepository : BaseRepository, IMarketingRepository
    {
        private readonly IObjectDBBuilder _builder;

        public MarketingRepository(IObjectDBBuilder builder)
        {
            _builder = builder;
        }
        public IEnumerable<CategoryType> GetCategoryList(MediaCategoryParam param, bool isInternal)
        {
            const string uSpKey = "Aff_Admin_MediaCategory_Sel";
            var dbMode = isInternal ? DBMode.View : DBMode.Main;

            using (var db = _builder.Build(uSpKey, dbMode))
            {
                return db.Query<CategoryType>(param);
            }
        }

        public IEnumerable<CurrencyType> GetCurrencyList(bool isInternal)
        {
            const string uSpKey = "Aff_Admin_MediaCurrency_Sel";
            var dbMode = isInternal ? DBMode.View : DBMode.Main;

            using (var db = _builder.Build(uSpKey, dbMode))
            {
                return db.Query<CurrencyType>();
            }
        }

        public IEnumerable<BannerType> GetBannerTypeList(bool isInternal)
        {
            const string uSpKey = "Aff_Admin_MediaType_Sel";
            var dbMode = isInternal ? DBMode.View : DBMode.Main;

            using (var db = _builder.Build(uSpKey, dbMode))
            {
                return db.Query<BannerType>();
            }
        }

        public IEnumerable<SizeType> GetSizeList(SizeParam param, bool isInternal)
        {
            const string uSpKey = "Aff_Admin_MediaSize_Sel";
            var dbMode = isInternal ? DBMode.View : DBMode.Main;

            using (var db = _builder.Build(uSpKey, dbMode))
            {
                return db.Query<SizeType>(param);
            }
        }

        public IEnumerable<Domain> GetAvailableDomainList(int affId, bool isMember, bool isInternal)
        {
            var uSpKey = isMember ? "Aff_User_Domain_Sel" : "Aff_User_SubAffiliateDomain_Sel";

            var dbMode = isInternal ? DBMode.View : DBMode.Main;

            using (var db = _builder.Build(uSpKey, dbMode))
            {
                return isMember ? db.Query<Domain>(new { AffId = affId }) : db.Query<Domain>();
            }
        }

        public IEnumerable<MarketingItem> GetBannerList(MarketingBannerParam bannerParam)
        {
            const string uSpKey = "Aff_Admin_Banner_Sel";

            var dbMode = bannerParam.IsInternal ? DBMode.View : DBMode.Main;

            using (var db = _builder.Build(uSpKey, dbMode))
            {
                return db.Query<MarketingItem>(bannerParam);
            }
        }

        public IEnumerable<MarketingItem> GetTextList(MarketingTextParam textParam)
        {
            const string uSpKey = "Aff_Admin_Text_Sel";
            var dbMode = textParam.IsInternal ? DBMode.View : DBMode.Main;

            using (var db = _builder.Build(uSpKey, dbMode))
            {
                return db.Query<MarketingItem>(textParam);
            }
        }

        public IEnumerable<LandingPage> GetLandingPageList(bool isInternal)
        {
            const string SpKey = "Aff_Admin_LandingPage_Sel";
            var dbMode = isInternal ? DBMode.View : DBMode.Main;

            using (var db = _builder.Build(SpKey, dbMode))
            {
                return db.Query<LandingPage>();
            }
        }

        public ResultInfo AddMarketingChannel(int userId, string channelCode, string channelName)
        {
            const string spKey = "Aff_User_AffMediaChannel_Ins";

            using (var db = _builder.Build(spKey))
            {
                AssignResultOutputParam(db);
                db.ExecuteNonQuery(new { AffId = userId, ChannelCode = channelCode, ChannelName = channelName });
                return GetResultInfo(db);
            }
        }

        public IEnumerable<MarketingChannel> GetMarketingChannels(int userId)
        {
            const string spKey = "Aff_User_AffMediaChannel_Sel";

            using (var db = _builder.Build(spKey))
            {
                return db.Query<MarketingChannel>(new { AffId = userId });
            }
        }

        public ResultInfo ToggleMarketingLink(MarketingLinkParam param)
        {
            const string spKey = "Aff_User_AffMediaLink_ToggleChannel";

            using (var db = _builder.Build(spKey))
            {
                AssignResultOutputParam(db);
                db.ExecuteNonQuery(param);
                return GetResultInfo(db);
            }
        }

        public IEnumerable<MarketingChannelLink> GetUsingMarketingLink(int userId)
        {
            const string spKey = "Aff_User_AffMediaChannel_Statistics";

            using (var db = _builder.Build(spKey))
            {
                return db.Query<MarketingChannelLink>(new { AffId = userId });
            }
        }

    }
}