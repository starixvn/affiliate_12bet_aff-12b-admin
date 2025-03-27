using Fanex.Affiliate.Admin.Core.Criterias.UserPanel.LandingPageStatistics;
using Fanex.Affiliate.Admin.Core.DTO;
using Fanex.Affiliate.Admin.Core.DTO.UserPanel;
using Fanex.Affiliate.Admin.Core.MetaModels.UserPanel;
using Fanex.Affiliate.Admin.Core.Services.Common;
using Fanex.Data.Repository;
using System.Collections.Generic;
using System.Linq;

namespace Fanex.Affiliate.Admin.Core.Services.UserPanel.Statistics
{
    public class LandingPageStatisticsService : BaseDynamicService, ILandingPageStatisticsService
    {
        private readonly ISystemService _systemService;

        public LandingPageStatisticsService(
            IDynamicRepository dynamicRepository,
            ISystemService systemService) : base(dynamicRepository)
        {
            _systemService = systemService;
        }

        public IEnumerable<LandingPageStatistics> GetLandingPageStatisticsList(LandingPageStatisticsParam param)
        {
            var criteria = new LandingPageStatisticsCriteria();
            AssignCriteria(criteria, param);
            criteria.LanguageCode = ConvertSelectedShortedCodes(param.LanguageCode);

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
            criteria.LanguageCode = param.LanguageCode;

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
            criteria.DeviceType = param.DeviceType;
        }

        private string ConvertSelectedShortedCodes(string languageCode)
        {
            if (string.IsNullOrWhiteSpace(languageCode))
            {
                var allLanguages = _systemService.GetLanguageList();

                return string.Join(",", allLanguages.Select(l => l.ShortedLanguageCode));
            }

            return languageCode;
        }
    }
}
