using System.Collections.Generic;
using Fanex.Affiliate.User.Core.DTO;
using Fanex.Affiliate.User.Core.DTO.UserPanel;
using Fanex.Affiliate.User.Core.MetalModels.UserPanel;

namespace Fanex.Affiliate.User.Core.Repository.UserPanel
{
    public interface IStatementHistoryRepository
    {
        IEnumerable<StatementHistory> GetStatementHistory(StatementHistoryParam statementHistoryParam);

        IEnumerable<ProductItem> GetProductList(bool isInternal);

        DailyEarningResult GetDailyEarning(DailyEarningParam dailyEarningParam);

        ExtraUserInfo GetExtraUserInfo(int affId, bool isInternal);

        IEnumerable<DailyEarningDetail> GetDailyEarningDetail(DailyEarningParam earningDetailParam);
    }
}
