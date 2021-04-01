using Fanex.Affiliate.Admin.Core.DTO;
using Fanex.Affiliate.Admin.Core.DTO.NewViewLog;
using Fanex.Affiliate.Admin.Core.DTO.UserPanel;
using Fanex.Affiliate.Admin.Core.MetaModels.NewViewLog;
using System.Collections.Generic;

namespace Fanex.Affiliate.Admin.Core.Services.NewViewLog
{
    public interface INewViewLogService
    {
        IEnumerable<LogAction> GetLogActionList();

        EntityResult<NewViewLogInfo> GetNewViewLogList(NewViewLogParam param);

        EntityResult<NewViewLogDetailInfo> GetNewViewLogDetailList(NewViewLogParam param);
    }
}
