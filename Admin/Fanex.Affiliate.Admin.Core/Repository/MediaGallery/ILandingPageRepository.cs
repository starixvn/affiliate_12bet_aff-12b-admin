using Fanex.Affiliate.Admin.Core.DTO;
using Fanex.Affiliate.Admin.Core.DTO.Common;
using Fanex.Affiliate.Admin.Core.DTO.MediaGallery;
using Fanex.Affiliate.Admin.Core.MetaModels.MediaGallery;
using System.Collections.Generic;

namespace Fanex.Affiliate.Admin.Core.Repository.MediaGallery
{
    public interface ILandingPageRepository
    {
        IEnumerable<LandingPage> GetLandingPageList();

        LandingPage GetLandingPageById(int pageId);

        IEnumerable<TargetCountry> GetExcludedTargetCountryList(ExcludedTargetCountryParam param);

        ResultInfo InsertLandingPage(InsertUpdateLandingPageParam param);

        ResultInfo UpdateLandingPage(InsertUpdateLandingPageParam param);

        ResultInfo DeleteLandingPage(DeleteLandingPageParam param);
    }
}
