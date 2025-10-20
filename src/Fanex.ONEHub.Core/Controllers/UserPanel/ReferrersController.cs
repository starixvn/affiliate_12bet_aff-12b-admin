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
    public class ReferrersController : ReadOnlySessionBaseController
    {
        private readonly IReferrersStatisticsService _referrersStatisticsService;
        public ReferrersController(
            ISessionService sessionService,
            IReferrersStatisticsService referrersStatisticsService) : base(sessionService)
        {
            _referrersStatisticsService = referrersStatisticsService;
        }

        [HttpPost]
        [ValidateAntiForgeryHeader]
        public ActionResult GetReferrersList(ReferrersParam param)
        {
            if (ModelState.IsValid)
            {
                return GenerateJsonNetResult(_referrersStatisticsService.GetReferrersList(param));
            }

            return new JsonNetResult();
        }

        [HttpPost]
        [ValidateAntiForgeryHeader]
        public ActionResult GetReferrersDetailList(ReferrersDetailParam param)
        {
            if (ModelState.IsValid)
            {
                return GenerateJsonNetResult(_referrersStatisticsService.GetReferrersDetailList(param));
            }

            return new JsonNetResult();
        }
    }
}
