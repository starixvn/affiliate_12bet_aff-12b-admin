namespace Fanex.Affiliate.User.Core.Extensions.Mappers
{
    using Criterias;
    using MetalModels.UserPanel;

    public static class MediaSizeExtension
    {
        public static void Map(this GetSizeListCriteria criteria, SizeParam param)
        {
            criteria.Category = param.Category;
            criteria.SubCategory = param.SubCategory;
            criteria.Language = param.Language;
            criteria.Currency = param.Currency;
            criteria.ImageType = param.ImageType;
            criteria.DeviceType = param.DeviceType;
            criteria.AffType = param.AffType;
            criteria.TargetId = param.TargetId;
        }
    }
}
