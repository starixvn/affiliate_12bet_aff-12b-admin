namespace Fanex.Affiliate.User.Core.BusinessService
{
    using System.Collections.Generic;
    using DTO;
    using DTO.UserPanel;
    using MetalModels.UserPanel;

    public interface IStatementHistoryService
    {
        IEnumerable<StatementHistory> GetStatementHistory(StatementHistoryParam statementHistoryParam);

        IEnumerable<ProductItem> GetProductList(bool isInternal);

        EntityResult<DailyEarning> GetDailyEarning(DailyEarningParam dailyEarningParam);

        IEnumerable<SubAffComInfo> GetSubAffCommission(SubAffComParam subAffParam);
    }
}
