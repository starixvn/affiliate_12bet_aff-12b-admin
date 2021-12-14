using Fanex.Affiliate.Admin.Core.DTO;
using Fanex.Affiliate.Admin.Core.DTO.MediaGallery;
using Fanex.Affiliate.Admin.Core.MetaModels.MediaGallery;

namespace Fanex.Affiliate.Admin.Core.Services.MediaGallery
{
    public interface IExclusiveAffService
    {
        ResultInfo InsertExclusiveAff(InsertExclusiveAffParam param);

        ResultInfo DeleteExclusiveAff(DeleteExclusiveAffParam param);

        EntityResult<ExclusiveAff> GetExclusiveAffs(SelectExclusiveAffParam param);
    }
}
