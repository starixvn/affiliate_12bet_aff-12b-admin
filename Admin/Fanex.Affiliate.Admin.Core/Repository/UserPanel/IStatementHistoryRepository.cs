using System.Collections.Generic;
using Fanex.Affiliate.Admin.Core.DTO.UserPanel;
using Fanex.Affiliate.Admin.Core.MetaModels.UserPanel;
using Fanex.Affiliate.Admin.Core.DTO;

namespace Fanex.Affiliate.Admin.Core.Repository.UserPanel
{
    public interface IStatementHistoryRepository
    {
        EntityResult<StatementHistory> GetStatementHistoryList(StatementHistoryParam param);

        DailyEarningResult GetDailyEarning(DailyEarningParam param);

        IEnumerable<DailyEarningDetail> GetDailyEarningDetail(DailyEarningParam param);
    }
}
