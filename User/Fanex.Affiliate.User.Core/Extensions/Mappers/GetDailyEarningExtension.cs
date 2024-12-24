namespace Fanex.Affiliate.User.Core.Extensions.Mappers
{
    using Criterias;
    using MetalModels.UserPanel;

    public static class GetDailyEarningExtension
    {
        public static void Map(this GetDailyEarningCriteria criteria, DailyEarningParam param)
        {
            criteria.AffId = param.AffId;
            criteria.BaseCurrency = param.BaseCurrency;
            criteria.ProductId = param.ProductId;
            criteria.MonthDate = param.MonthDate;
            criteria.IsInternal = param.IsInternal;
        }
    }
}
