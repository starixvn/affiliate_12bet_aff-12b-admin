namespace Fanex.Affiliate.Admin.Core.Services.UserPanel.Statistics
{
    using Criterias.UserPanel.MarketingStatistics;
    using Data.Repository;
    using DTO.UserPanel.Statistics;
    using MetaModels.UserPanel;
    using System.Collections.Generic;

    public class MarketingStatisticsService : BaseDynamicService, IMarketingStatisticsService
    {
        public MarketingStatisticsService(IDynamicRepository dynamicRepository) : base(dynamicRepository)
        {
        }

        public IEnumerable<MarketingStatistics> GetMarketingStatistics(MarketingStatisticsParam param)
        {
            var criteria = new MarketingStatisticsCriteria
            {
                AffId = param.AffId,
                FromDate = param.FromDate,
                ToDate = param.ToDate,
                LanguageCodes = param.LanguageCodes,
                IncludedInActive = param.IncludedInActive
            };

            return FetchTData<MarketingStatistics>(criteria);
        }
    }
}
