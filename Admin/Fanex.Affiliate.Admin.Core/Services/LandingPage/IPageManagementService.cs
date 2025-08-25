using Fanex.Affiliate.Admin.Core.DTO.LandingPage;
using Fanex.Affiliate.Admin.Core.MetaModels.LandingPage;
using System.Collections.Generic;

namespace Fanex.Affiliate.Admin.Core.Services.LandingPage
{
    public interface IPageManagementService
    {
        IEnumerable<PageManagement> GetPageManagementList(PageManagementParam param);
    }
}
