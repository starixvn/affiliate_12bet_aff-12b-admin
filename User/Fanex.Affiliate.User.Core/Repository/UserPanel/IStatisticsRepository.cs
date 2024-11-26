namespace Fanex.Affiliate.User.Core.Repository.UserPanel
{
    using System.Collections.Generic;
    using DTO.UserPanel;
    using Fanex.Affiliate.User.Core.DTO.UserPanel.MediaGallery;
    using Fanex.Affiliate.User.Core.DTO.UserPanel.Statistics;
    using Fanex.Affiliate.User.Core.MetalModels.UserPanel;

    public interface IStatisticsRepository
    {
        IEnumerable<Country> GetCountryList(bool isInternal);

        IEnumerable<ReferrerStatistics> GetReferrerStatistics(ReferrerStatisticsParam referrerStatisticsParam);

        IEnumerable<ReferrerStatistics> GetStatisticsDetailBySite(StatisticsDetailBySiteParam statisticsDetailParam);

        IEnumerable<LandingPageStatistics> GetLandingPageStatistics(LandingPageStatisticsParam landingPageStatisticsParam);

        IEnumerable<LandingPage> GetLandingPageList(bool isInternal);

        IEnumerable<MarketingPageStatistics> GetMarketingPageStatistics(MarketingPageStatisticsParam param);
    }
}
