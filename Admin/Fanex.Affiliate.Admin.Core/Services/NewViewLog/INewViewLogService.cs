using Fanex.Affiliate.Admin.Core.DTO;
using Fanex.Affiliate.Admin.Core.DTO.NewViewLog;
using Fanex.Affiliate.Admin.Core.MetaModels.NewsViewLog;

namespace Fanex.Affiliate.Admin.Core.Services.NewViewLog
{
    public interface INewViewLogService
    {
        EntityResult<NewViewLogInfo> GetNewViewLogList(NewViewLogParam param);

        EntityResult<NewViewLogDetailInfo> GetNewViewLogDetailList(NewViewLogParam param);
    }
}
