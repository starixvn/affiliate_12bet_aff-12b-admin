using System.Collections.Generic;
using Fanex.Affiliate.Admin.Core.DTO;
using Fanex.Affiliate.Admin.Core.DTO.MediaGallery;
using Fanex.Affiliate.Admin.Core.MetaModels.MediaGallery;

namespace Fanex.Affiliate.Admin.Core.Repository.MediaGallery
{
    public interface ILandingPageRepository
    {
        IEnumerable<LandingPage> GetLandingPageList();

        LandingPage GetLandingPageById(int pageId);

        ResultInfo InsertLandingPage(InsertUpdateLandingPageParam param);

        ResultInfo UpdateLandingPage(InsertUpdateLandingPageParam param);

        ResultInfo DeleteLandingPage(DeleteLandingPageParam param);
    }
}
