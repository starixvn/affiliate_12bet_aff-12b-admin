namespace Fanex.Affiliate.User.Core.BusinessService
{
    using DTO.UserPanel;
    using DTO.UserPanel.Statistics;
    using MetalModels.UserPanel;
    using System.Collections.Generic;

    public interface IStatisticsService
    {
        IEnumerable<ReferrerStatistics> GetReferrerStatistics(ReferrerStatisticsParam referrerStatisticsParam);

        IEnumerable<ReferrerStatistics> GetStatisticsDetailBySite(StatisticsDetailBySiteParam statisticsDetailParam);

        IEnumerable<LandingPageStatistics> GetLandingPageStatistics(LandingPageStatisticsParam landingPageStatisticsParam);

        IEnumerable<MarketingPageStatistics> GetMarketingPageStatistics(MarketingPageStatisticsParam param);

        string ConvertSelectedLanguageCodes(string languageCode);
    }
}
