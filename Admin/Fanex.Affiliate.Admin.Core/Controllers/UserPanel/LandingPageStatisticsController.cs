using Fanex.Affiliate.Admin.Core.Attributes;
using Fanex.Affiliate.Admin.Core.Constants;
using Fanex.Affiliate.Admin.Core.Extensions;
using Fanex.Affiliate.Admin.Core.MetaModels.UserPanel;
using Fanex.Affiliate.Admin.Core.Services.Common;
using Fanex.Affiliate.Admin.Core.Services.UserPanel.Statistics;
using System.Web.Mvc;

namespace Fanex.Affiliate.Admin.Core.Controllers.UserPanel
{
    [FunctionAuthorize(FunctionName = Function.Affiliate)]
    public class LandingPageStatisticsController : ReadOnlySessionBaseController
    {
        private readonly ILandingPageStatisticsService _landingPageStatisticsService;

        public LandingPageStatisticsController(
            ISessionService sessionService,
            ILandingPageStatisticsService landingPageStatisticsService) : base(sessionService)
        {
            _landingPageStatisticsService = landingPageStatisticsService;
        }

        [HttpPost]
        [ValidateAntiForgeryHeader]
        public ActionResult GetLandingPageStatisticsList(LandingPageStatisticsParam param)
        {
            if (ModelState.IsValid)
            {
                return GenerateJsonNetResult(_landingPageStatisticsService.GetLandingPageStatisticsList(param));
            }

            return new JsonNetResult();
        }

        [HttpPost]
        [ValidateAntiForgeryHeader]
        public ActionResult GetLandingPageStatisticsDetailList(LandingPageStatisticsDetailParam param)
        {
            if (ModelState.IsValid)
            {
                return GenerateJsonNetResult(_landingPageStatisticsService.GetLandingPageStatisticsDetailList(param));
            }

            return new JsonNetResult();
        }
    }
}
