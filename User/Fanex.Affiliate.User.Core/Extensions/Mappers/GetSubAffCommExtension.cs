namespace Fanex.Affiliate.User.Core.Extensions.Mappers
{
    using Criterias;
    using MetalModels.UserPanel;

    public static class GetSubAffCommExtension
    {
        public static void Map(this GetSubAffCommissionCriteria criteria, SubAffComParam param)
        {
            criteria.AffId = param.AffId;
            criteria.MonthDate = param.MonthDate;
            criteria.BaseCurrency = param.BaseCurrency;
            criteria.IsInternal = param.IsInternal;
        }
    }
}
