using Fanex.Affiliate.Admin.Core.DTO;
using Fanex.Affiliate.Admin.Core.DTO.NewViewLog;
using Fanex.Affiliate.Admin.Core.MetaModels.NewsViewLog;

namespace Fanex.Affiliate.Admin.Core.Repository.Affiliate
{
    public interface INewViewLogRepository
    {
        EntityResult<NewViewLogInfo> GetNewViewLogList(NewViewLogParam param);

        EntityResult<NewViewLogDetailInfo> GetNewViewLogDetailList(NewViewLogParam param);
    }
}
