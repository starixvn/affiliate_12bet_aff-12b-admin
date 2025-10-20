using Fanex.Affiliate.Admin.Core.Criterias.UserPanel.ReferrersStatistics;
using Fanex.Affiliate.Admin.Core.DTO;
using Fanex.Affiliate.Admin.Core.DTO.UserPanel;
using Fanex.Affiliate.Admin.Core.MetaModels.UserPanel;
using Fanex.Data.Repository;
using System.Collections.Generic;

namespace Fanex.Affiliate.Admin.Core.Services.UserPanel.Statistics
{
    public class ReferrersStatisticsService : BaseDynamicService, IReferrersStatisticsService
    {
        public ReferrersStatisticsService(
            IDynamicRepository dynamicRepository) : base(dynamicRepository) { }

        public IEnumerable<ReferrersStatistics> GetReferrersList(ReferrersParam param)
        {
            var criteria = new ReferrersStatisticsCriteria();
            AssignCriteria(criteria, param);

            return FetchTData<ReferrersStatistics>(criteria);
        }

        public EntityResult<ReferrersStatistics> GetReferrersDetailList(ReferrersDetailParam param)
        {
            var criteria = new ReferrersStatisticsDetailCriteria
            {
                Domain = param.SelectedSiteUrl,
                PageIndex = param.PageIndex,
                PageSize = param.PageSize,
            };
            AssignCriteria(criteria, param);

            var dataList = DynamicRepository.Fetch<ReferrersStatistics>(criteria);

            return new EntityResult<ReferrersStatistics>
            {
                MainData = dataList,
                TotalItem = criteria.TotalItem,
            };
        }

        private static void AssignCriteria(BaseReferrersStatisticsCriteria criteria, ReferrersParam param)
        {
            criteria.AffId = param.AffId;
            criteria.FromDate = param.FromDate;
            criteria.ToDate = param.ToDate;
            criteria.DeviceType = param.DeviceType;
        }
    }
}
