using Fanex.Affiliate.Admin.Core.Attributes;
using Fanex.Affiliate.Admin.Core.Constants;
using Fanex.Affiliate.Admin.Core.Services.MediaGallery;
using Fanex.Affiliate.Admin.Core.Services.UserPanel.Statistics;
using System.Web.Mvc;

namespace Fanex.Affiliate.Admin.Core.Controllers
{
    [FunctionAuthorize(FunctionName = Function.Affiliate)]
    public class CommonSettingController : BaseController
    {
        private readonly ILandingPageService _landingPageService;
        private readonly ITrackingStatisticsService _trackingStatisticsService;

        public CommonSettingController(
            ILandingPageService landingPageService,
            ITrackingStatisticsService trackingStatisticsService
            )
        {
            _landingPageService = landingPageService;
            _trackingStatisticsService = trackingStatisticsService;
        }

        [HttpPost]
        [ValidateAntiForgeryHeader]
        public ActionResult GetPageList()
        {
            return GenerateJsonNetResult(_landingPageService.GetLandingPageList());
        }

        [HttpPost]
        [ValidateAntiForgeryHeader]
        public ActionResult GetTrackingCodeList(int affId)
        {
            return GenerateJsonNetResult(_trackingStatisticsService.GetTrackingCodeList(affId));
        }
    }
}