using System.Collections.Generic;
using Fanex.Affiliate.Admin.Core.DTO.MediaGallery;

namespace Fanex.Affiliate.Admin.Core.Repository.MediaGallery
{
    public interface ILandingPageRepository
    {
        IEnumerable<LandingPage> GetLandingPageList();
    }
}
