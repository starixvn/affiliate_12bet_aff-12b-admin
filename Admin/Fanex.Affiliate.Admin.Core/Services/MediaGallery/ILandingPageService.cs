using Fanex.Affiliate.Admin.Core.DTO;
using Fanex.Affiliate.Admin.Core.DTO.Common;
using Fanex.Affiliate.Admin.Core.DTO.MediaGallery;
using Fanex.Affiliate.Admin.Core.MetaModels.MediaGallery;
using System.Collections.Generic;

namespace Fanex.Affiliate.Admin.Core.Services.MediaGallery
{
    public interface ILandingPageService
    {
        IEnumerable<LandingPage> GetLandingPageList(int userId);

        LandingPage GetLandingPageById(int userId, int pageId);

        IEnumerable<TargetCountry> GetExcludedTargetCountryList(ExcludedTargetCountryParam param);

        ResultInfo HandleSaveLandingPage(InsertUpdateLandingPageParam param, bool isInsert = false);

        ResultInfo DeleteLandingPage(DeleteLandingPageParam param);
    }
}
