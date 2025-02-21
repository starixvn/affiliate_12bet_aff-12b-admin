using Fanex.Affiliate.Admin.Core.Criterias.UserPanel.TrackingStatistics;
using Fanex.Affiliate.Admin.Core.DTO.UserPanel;
using Fanex.Affiliate.Admin.Core.MetaModels.UserPanel;
using Fanex.Data.Repository;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Fanex.Affiliate.Admin.Core.Services.UserPanel.Statistics
{
    public class TrackingStatisticsService : BaseDynamicService, ITrackingStatisticsService
    {
        public TrackingStatisticsService(
            IDynamicRepository dynamicRepository) : base(dynamicRepository) { }

        public IEnumerable<string> GetTrackingCodeList(int affId)
        {
            var criteria = new TrackingCodeCriteria
            {
                AffId = affId
            };

            var trackingInfos = FetchTData<TrackingInfo>(criteria);

            return trackingInfos.Select(t => t.TrackingCode);
        }

        public IEnumerable<TrackingStatistics> GetTrackingStatisticsList(TrackingStatisticsParam param)
        {
            var criteria = new TrackingStatisticsCriteria();
            AssignCommonCriteria(criteria, param);

            var (FirstResultSet, SecondResultSet) = DynamicRepository.FetchMultiple<TrackingStatistics, TrackingStatistics>(criteria);

            return FirstResultSet.Concat(SecondResultSet);
        }

        public IEnumerable<TrackingStatistics> GetTrackingStatisticsDetailList(TrackingStatisticsDetailParam param)
        {
            var criteria = new TrackingStatisticsDetailCriteria
            {
                Month = param.SelectedMonth.Month,
                Year = param.SelectedMonth.Year,
            };
            AssignCommonCriteria(criteria, param);

            return FetchTData<TrackingStatistics>(criteria);
        }

        private static void AssignCommonCriteria(BaseTrackingStatisticsCriteria criteria, TrackingStatisticsParam param)
        {
            criteria.AffId = param.AffId;
            criteria.TrackingCode = param.TrackingCode;
        }
    }
}
