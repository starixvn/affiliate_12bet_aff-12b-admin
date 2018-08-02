namespace Fanex.Affiliate.User.Core.Controllers.UserPanel
{
    using System;
    using System.Web.Mvc;
    using Fanex.Affiliate.User.Core.BusinessService;
    using Fanex.Affiliate.User.Core.CoreService;
    using Fanex.Affiliate.User.Core.ViewModels.UserPanel;
    using Filters;
    using ONEbook.UM.Filters;

    [CheckUMFilter]
    [CustomAuthorize]
    public class CommissionReportController : Controller
    {
        private readonly ICommissionReportService _commissionReportService;
        private readonly ISessionService _sessionService;

        public CommissionReportController(ICommissionReportService commissionReportService, ISessionService sessionService)
        {
            _commissionReportService = commissionReportService;
            _sessionService = sessionService;
        }


        [HttpGet]
        [AjaxOnly]
        public ActionResult Index()
        {
            return View("~/Views/UserPanel/CommissionReport/index.cshtml");
        }

        [HttpPost]
        [AjaxOnly]
        public ActionResult Index(int year)
        {
            var viewModel = year >= 2009 && year <= DateTime.Now.Year
                ? _commissionReportService.GenerateCommissionReportData(
                _sessionService.UserSession.AffId,
                new DateTime(year, 1, 1)
                ) : new CommissionReportViewModel();

            return Json(viewModel);
        }
    }
}