using Fanex.Affiliate.Admin.Core.DTO;
using Fanex.Affiliate.Admin.Core.DTO.MediaGallery;
using Fanex.Affiliate.Admin.Core.DTO.UserPanel;
using Fanex.Affiliate.Admin.Core.MetaModels.UserPanel;
using System.Collections.Generic;

namespace Fanex.Affiliate.Admin.Core.Repository.UserPanel
{
    public interface ILandingPagesRepository
    {
        IEnumerable<LandingPages> GetLandingPagesList(LandingPagesParam param);

        EntityResult<LandingPages> GetLandingPagesDetailList(LandingPagesDetailParam param);
    }
}
