using System.Collections.Generic;
using Fanex.Affiliate.Admin.Core.DTO.UserPanel;
using Fanex.Affiliate.Admin.Core.MetaModels.UserPanel;
using Fanex.Affiliate.Admin.Core.DTO;
using Fanex.Affiliate.Admin.Core.MetaModels;

namespace Fanex.Affiliate.Admin.Core.Repository.UserPanel
{
    public interface IStatementHistoryRepository
    {
        EntityResult<StatementHistory> GetStatementHistoryList(AffiliatePagingByDateParam param);

        DailyEarningResult GetDailyEarning(DailyEarningParam param);

        IEnumerable<DailyEarningDetail> GetDailyEarningDetail(DailyEarningParam param);
    }
}
