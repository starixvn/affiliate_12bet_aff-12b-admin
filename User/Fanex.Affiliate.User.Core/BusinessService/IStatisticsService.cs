using Fanex.Affiliate.User.Core.DTO.UserPanel.Statistics;
using Fanex.Affiliate.User.Core.MetalModels.UserPanel;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Fanex.Affiliate.User.Core.BusinessService
{
    public interface IStatisticsService
    {
        IEnumerable<SelectListItem> GetCountryList();

        IEnumerable<string> GetTrackingCode(int affId, bool isInternal);

        IEnumerable<TrackingStatistics> GetTrackingStatistics(int affId, string trackingCode, bool isInternal);

        IEnumerable<TrackingStatistics> GetTrackingStatisticsDetail(TrackingStatisticsDetailParam trackingStatisticsDetailParam);

        IEnumerable<ReferrerStatistics> GetReferrerStatistics(ReferrerStatisticsParam referrerStatisticsParam);

        IEnumerable<ReferrerStatistics> GetStatisticsDetailBySite(StatisticsDetailBySiteParam statisticsDetailParam);

        IEnumerable<LandingPageStatistics> GetLandingPageStatistics(LandingPageStatisticsParam landingPageStatisticsParam);
    }
}
