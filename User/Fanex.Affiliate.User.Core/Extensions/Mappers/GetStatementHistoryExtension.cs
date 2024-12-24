namespace Fanex.Affiliate.User.Core.Extensions.Mappers
{
    using Criterias;
    using MetalModels.UserPanel;

    public static class GetStatementHistoryExtension
    {
        public static void Map(this GetStatementHistoryCriteria criteria, StatementHistoryParam param)
        {
            criteria.AffId = param.AffId;
            criteria.BaseCurrency = param.BaseCurrency;
            criteria.PageSize = param.PageSize;
            criteria.PageIndex = param.PageIndex;
            criteria.NRows = param.NRows;
            criteria.IsInternal = param.IsInternal;
        }
    }
}
