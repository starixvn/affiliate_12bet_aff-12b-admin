namespace Fanex.Affiliate.User.Core.Extensions.Mappers
{
    using Criterias;
    using MetalModels.UserPanel;

    public static class GetMarketingPageStatisticsExtension
    {
        public static void Map(this GetMarketingPageStatisticsCriteria criteria, MarketingPageStatisticsParam param)
        {
            criteria.AffId = param.AffId;
            criteria.FromDate = param.FromDate;
            criteria.ToDate = param.ToDate;
            criteria.LanguageCodes = param.LanguageCodes;
            criteria.IncludedInActive = param.IncludedInActive;
        }
    }
}
