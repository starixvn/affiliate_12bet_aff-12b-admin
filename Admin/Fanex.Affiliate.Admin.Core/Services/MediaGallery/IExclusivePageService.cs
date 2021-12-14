using Fanex.Affiliate.Admin.Core.DTO;
using Fanex.Affiliate.Admin.Core.DTO.MediaGallery;
using Fanex.Affiliate.Admin.Core.MetaModels.MediaGallery;
using System.Web;

namespace Fanex.Affiliate.Admin.Core.Services.MediaGallery
{
    public interface IExclusivePageService
    {
        ResultInfo InsertExclusivePage(HttpPostedFileBase file, InsertExclusivePageParam param);

        ResultInfo UpdateExclusivePage(HttpPostedFileBase file, UpdateExclusivePageParam param);

        ResultInfo DeleteExclusivePage(DeleteExclusivePageParam param);

        EntityResult<ExclusivePage> GetExclusivePageList(SelectExclusivePageParam param);

        ExclusivePage GetExclusivePageById(int exclusivePageId);
    }
}
