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
            IDynamicRepository dynamicRepository) : base(dynamicRepository) { }

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

        private static void AssignCriteria(BaseLandingPageStatisticsCritera critera, LandingPageStatisticsParam param)
        {
            critera.AffId = param.AffId;
            critera.FromDate = param.FromDate;
            critera.ToDate = param.ToDate;
            critera.SportCode = param.SelectedPage;
            critera.LanguageCode = param.LanguageCode;
            critera.DeviceType = param.DeviceType;
        }
    }
}
