using Fanex.Affiliate.Admin.Core.DTO;
using Fanex.Affiliate.Admin.Core.DTO.Common;
using Fanex.Affiliate.Admin.Core.MetaModels.LandingPage;
using Fanex.Affiliate.Admin.Core.MetaModels.MediaGallery;
using System.Collections.Generic;

namespace Fanex.Affiliate.Admin.Core.Services.MediaGallery
{
    public interface ILandingPageListService
    {
        IEnumerable<DTO.MediaGallery.LandingPage> GetLandingPageList();

        DTO.MediaGallery.LandingPage GetLandingPageById(int pageId);

        IEnumerable<TargetCountry> GetExcludedTargetCountryList(int pageId);

        ResultInfo HandleSaveLandingPage(InsertUpdateLandingPageParam param, bool isInsert = false);

        ResultInfo DeleteLandingPage(DeleteLandingPageParam param);
    }
}
