namespace Fanex.Affiliate.User.Core.Extensions.Mappers
{
    using Criterias;
    using MetalModels.UserPanel;

    public static class GetStatisticsDetailBySiteExtension
    {
        public static void Map(this GetStatisticsDetailBySiteCriteria criteria, StatisticsDetailBySiteParam param)
        {
            criteria.AffId = param.AffId;
            criteria.FromDate = param.FromDate;
            criteria.ToDate = param.ToDate;
            criteria.DeviceType = param.DeviceType;
            criteria.Domain = param.Domain;
            criteria.SportCode = param.SportCode;
            criteria.LanguageCode = param.LanguageCode;
            criteria.PageSize = param.PageSize;
            criteria.PageIndex = param.PageIndex;
            criteria.TotalPages = param.TotalPages;
            criteria.NRows = param.NRows;
        }
    }
}
