using System.Collections.Generic;
using Fanex.Affiliate.Admin.Core.DTO.Marketing;

namespace Fanex.Affiliate.Admin.Core.Repository.Marketing
{
    public interface ILandingPageRepository
    {
        IEnumerable<LandingPage> GetLandingPageList();
    }
}
