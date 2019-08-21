using Fanex.Affiliate.Admin.Core.DTO;
using Fanex.Affiliate.Admin.Core.DTO.UserPanel;
using Fanex.Affiliate.Admin.Core.MetaModels;
using Fanex.Affiliate.Admin.Core.MetaModels.UserPanel;

namespace Fanex.Affiliate.Admin.Core.Services.UserPanel
{
    public interface IStatementHistoryService
    {
        DailyEarningResult GetDailyEarning(DailyEarningParam param);

        DailyEarningDetailResult GetDailyEarningDetail(DailyEarningParam param);

        EntityResult<StatementHistory> GetStatementHistoryList(AffiliatePagingByDateParam param);
    }
}
