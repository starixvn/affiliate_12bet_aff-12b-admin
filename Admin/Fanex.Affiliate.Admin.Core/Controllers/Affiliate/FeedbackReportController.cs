using Fanex.Affiliate.Admin.Core.Attributes;
using Fanex.Affiliate.Admin.Core.Constants;
using Fanex.Affiliate.Admin.Core.Extensions;
using Fanex.Affiliate.Admin.Core.MetaModels.FeedbackReport;
using Fanex.Affiliate.Admin.Core.Services.Common;
using Fanex.Affiliate.Admin.Core.Services.FeedbackReport;
using System.Linq;
using System.Web.Mvc;

namespace Fanex.Affiliate.Admin.Core.Controllers.Affiliate
{
    [FunctionAuthorize(FunctionName = Function.FeedbackReport)]
    public class FeedbackReportController : ReadOnlySessionBaseController
    {
        private readonly IFeedbackReportService _feedbackReportService;

        public FeedbackReportController(
            ISessionService sessionService,
            IFeedbackReportService feedbackReportService) : base(sessionService)
        {
            _feedbackReportService = feedbackReportService;
        }

        public ActionResult Index()
        {
            var model = _feedbackReportService.GetRateList().OrderByDescending(r => r.RateId);

            return View("~/Areas/Portal/Views/Affiliate/FeebackReport.cshtml", model);
        }

        [HttpPost]
        [ValidateAntiForgeryHeader]
        [UpdateUnauthorizedProperty]
        public ActionResult GetFeedbackReportList(FeedbackReportParam param)
        {
            if (ModelState.IsValid)
            {
                return new JsonNetResult
                {
                    Data = _feedbackReportService.GetFeedbackReportList(param)
                };
            }

            return new JsonNetResult();
        }
    }
}
