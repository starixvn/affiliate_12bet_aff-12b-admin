using System.Collections.Generic;
using Fanex.Affiliate.Admin.Core.DTO.UserPanel;
using Fanex.Affiliate.Admin.Core.MetaModels.UserPanel;

namespace Fanex.Affiliate.Admin.Core.Repository.UserPanel
{
    public interface IStatementHistoryRepository
    {
        DailyEarningResult GetDailyEarning(DailyEarningParam param);

        IEnumerable<DailyEarningDetail> GetDailyEarningDetail(DailyEarningParam param);
    }
}
