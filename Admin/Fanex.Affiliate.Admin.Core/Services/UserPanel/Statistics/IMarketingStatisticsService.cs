namespace Fanex.Affiliate.Admin.Core.Services.UserPanel.Statistics
{
    using System.Collections.Generic;
    using DTO.UserPanel.Statistics;
    using MetaModels.UserPanel;

    public interface IMarketingStatisticsService
    {
        IEnumerable<MarketingStatistics> GetMarketingStatistics(MarketingStatisticsParam param);
    }
}
