namespace Fanex.Affiliate.User.Core.Extensions.Mappers
{
    using Criterias;
    using MetalModels.UserPanel;

    public static class GetPlayerDetailListExtension
    {
        public static void Map(this GetPlayerDetailListCriteria criteria, PlayerDetailParam param)
        {
            criteria.AffId = param.AffId;
            criteria.PageIndex = param.PageIndex;
            criteria.PageSize = param.PageSize;
            criteria.MonthDate = param.MonthDate;
            criteria.SortById = param.SortById;
            criteria.IsSortAsc = param.IsSortAsc;
            criteria.NRows = param.NRows;
        }
    }
}
