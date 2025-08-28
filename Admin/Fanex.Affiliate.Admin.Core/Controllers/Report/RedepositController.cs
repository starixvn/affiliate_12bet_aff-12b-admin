namespace Fanex.Affiliate.Admin.Core.Controllers.Report
{
    using System.Web.Mvc;
    using Attributes;
    using Constants;
    using MetaModels.Report;
    using Services.Common;
    using Services.Report;

    [FunctionAuthorize(FunctionName = Function.AffReport)]
    public class RedepositController : ReadOnlySessionBaseController
    {
        private readonly IRedepositReportService _redepositService;

        public RedepositController(
            ISessionService sessionService,
            IRedepositReportService redepositService) : base(sessionService)
        {
            _redepositService = redepositService;
        }

        [HttpGet]
        public ActionResult Index()
        {
            return View("~/Areas/Portal/Views/Report/RedepositReport.cshtml");
        }

        [HttpPost]
        [ValidateAntiForgeryHeader]
        public ActionResult GetData(RedepositReportParam param)
        {
            return GenerateJsonNetResult(_redepositService.GetReportData(param));
        }
    }
}
