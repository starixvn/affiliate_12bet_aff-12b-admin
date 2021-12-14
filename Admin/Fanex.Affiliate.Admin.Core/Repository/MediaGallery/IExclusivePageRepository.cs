using Fanex.Affiliate.Admin.Core.DTO;
using Fanex.Affiliate.Admin.Core.DTO.MediaGallery;
using Fanex.Affiliate.Admin.Core.MetaModels.MediaGallery;

namespace Fanex.Affiliate.Admin.Core.Repository.MediaGallery
{
    public interface IExclusivePageRepository
    {
        ResultInfo InsertExclusivePage(InsertExclusivePageParam param);

        ResultInfo UpdateExclusivePage(UpdateExclusivePageParam param);

        ResultInfo DeleteExclusivePage(DeleteExclusivePageParam param);

        EntityResult<ExclusivePage> GetExclusivePageList(SelectExclusivePageParam param);

        ExclusivePage GetExclusivePageById(int exclusivePageId);
    }
}
