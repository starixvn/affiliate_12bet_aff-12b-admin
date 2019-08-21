using Fanex.Affiliate.Admin.Core.DTO.UserPanel;
using Fanex.Affiliate.Admin.Core.MetaModels.UserPanel;
using System.Collections.Generic;

namespace Fanex.Affiliate.Admin.Core.Repository.UserPanel
{
    public interface ITrackingStatisticsRepository
    {
        IEnumerable<string> GetTrackingCodeList(int affId);

        IEnumerable<TrackingStatistics> GetTrackingStatisticsList(TrackingStatisticsParam param);

        IEnumerable<TrackingStatistics> GetTrackingStatisticsDetailList(TrackingStatisticsDetailParam param);
    }
}
