using Fanex.Affiliate.Admin.Core.Criterias.UserPanel.LandingPageStatistics;
using Fanex.Affiliate.Admin.Core.DTO;
using Fanex.Affiliate.Admin.Core.DTO.UserPanel;
using Fanex.Affiliate.Admin.Core.MetaModels.UserPanel;
using Fanex.Data.Repository;
using System.Collections.Generic;

namespace Fanex.Affiliate.Admin.Core.Services.UserPanel.Statistics
{
    public class LandingPageStatisticsService : BaseDynamicService, ILandingPageStatisticsService
    {
        public LandingPageStatisticsService(
            IDynamicRepository dynamicRepository) : base(dynamicRepository)
        {
        }

        public IEnumerable<LandingPageStatistics> GetLandingPageStatisticsList(LandingPageStatisticsParam param)
        {
            var criteria = new LandingPageStatisticsCriteria();
            AssignCriteria(criteria, param);

            return FetchTData<LandingPageStatistics>(criteria);
        }

        public EntityResult<LandingPageStatistics> GetLandingPageStatisticsDetailList(LandingPageStatisticsDetailParam param)
        {
            var criteria = new LandingPageStatisticsDetailCriteria
            {
                PageIndex = param.PageIndex,
                PageSize = param.PageSize,
            };
            AssignCriteria(criteria, param);

            var dataList = DynamicRepository.Fetch<LandingPageStatistics>(criteria);

            return new EntityResult<LandingPageStatistics>
            {
                MainData = dataList,
                TotalItem = criteria.TotalItem,
            };
        }

        private static void AssignCriteria(BaseLandingPageStatisticsCritera criteria, LandingPageStatisticsParam param)
        {
            criteria.AffId = param.AffId;
            criteria.FromDate = param.FromDate;
            criteria.ToDate = param.ToDate;
            criteria.SportCode = param.SelectedPage;
            criteria.LanguageCode = param.LanguageCode;
            criteria.DeviceType = param.DeviceType;
        }
    }
}
