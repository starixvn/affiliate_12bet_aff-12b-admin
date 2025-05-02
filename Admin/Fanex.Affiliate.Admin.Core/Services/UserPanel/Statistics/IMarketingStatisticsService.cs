namespace Fanex.Affiliate.Admin.Core.Services.UserPanel.Statistics
{
    using DTO.UserPanel.Statistics;
    using MetaModels.UserPanel;
    using System.Collections.Generic;

    public interface IMarketingStatisticsService
    {
        IEnumerable<MarketingStatistics> GetMarketingStatistics(MarketingStatisticsParam param);
    }
}
