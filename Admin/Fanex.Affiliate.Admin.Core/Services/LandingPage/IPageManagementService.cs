using Fanex.Affiliate.Admin.Core.DTO;
using Fanex.Affiliate.Admin.Core.DTO.LandingPage;
using Fanex.Affiliate.Admin.Core.MetaModels.LandingPage;

namespace Fanex.Affiliate.Admin.Core.Services.LandingPage
{
    public interface IPageManagementService
    {
        EntityResult<PageManagement> GetPageManagementList(PageManagementParam param);

        ResultInfo ToggleLandingPageStatus(UpdateLandingPageStatusParam param);
    }
}
