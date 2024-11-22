namespace Fanex.Affiliate.User.Core.BusinessService
{
    using System.Collections.Generic;
    using System.Web.Mvc;
    using Fanex.Affiliate.User.Core.DTO.UserPanel.Statistics;
    using Fanex.Affiliate.User.Core.MetalModels.UserPanel;

    public interface IStatisticsService
    {
        IEnumerable<SelectListItem> GetCountryList(bool isInternal);

        string GetCountryCodes(bool isInternal);
        IEnumerable<ReferrerStatistics> GetReferrerStatistics(ReferrerStatisticsParam referrerStatisticsParam);

        IEnumerable<ReferrerStatistics> GetStatisticsDetailBySite(StatisticsDetailBySiteParam statisticsDetailParam);

        IEnumerable<LandingPageStatistics> GetLandingPageStatistics(LandingPageStatisticsParam landingPageStatisticsParam);
    }
}
