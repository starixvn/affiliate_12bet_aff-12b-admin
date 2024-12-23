namespace Fanex.Affiliate.User.Core.Extensions.Mappers
{
    using Criterias;
    using MetalModels.UserPanel;

    public static class GetBannerListExtension
    {
        public static void Map(this GetBannerListCriteria criteria, MarketingBannerParam param)
        {
            criteria.Category = param.Category;
            criteria.Language = param.Language;
            criteria.Size = param.Size;
            criteria.Currency = param.Currency;
            criteria.AffType = param.AffType;
            criteria.PageSize = param.PageSize;
            criteria.PageIndex = param.PageIndex;
            criteria.DeviceType = param.DeviceType;
            criteria.TargetId = param.TargetId;
            criteria.Nrows = param.Nrows;
            criteria.SubCategory = param.SubCategory;
            criteria.IsInternal = param.IsInternal;
        }
    }
}
