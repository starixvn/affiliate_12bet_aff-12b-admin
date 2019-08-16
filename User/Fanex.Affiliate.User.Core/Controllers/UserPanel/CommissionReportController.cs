namespace Fanex.Affiliate.User.Core.Controllers.UserPanel
{
    using System;
    using System.Web.Mvc;
    using Fanex.Affiliate.User.Core.BusinessService;
    using Fanex.Affiliate.User.Core.CoreService;
    using Fanex.Affiliate.User.Core.ViewModels.UserPanel;
    using Filters;

    [CustomAuthorize]
    public class CommissionReportController : BaseController
    {
        private readonly ICommissionReportService _commissionReportService;

        public CommissionReportController(
            ISessionService sessionService,
            ILanguageService languageService,
            ICommissionReportService commissionReportService
          ) : base(sessionService, languageService)
        {
            _commissionReportService = commissionReportService;
        }


        [HttpGet]
        [AjaxOnly]
        public ActionResult Index()
        {
            return View("~/Views/UserPanel/CommissionReport/Index.cshtml");
        }

        [HttpPost]
        [AjaxOnly]
        public ActionResult Index(int year)
        {
            var viewModel = year >= 2009 && year <= DateTime.Now.Year
                ? _commissionReportService.GenerateCommissionReportData(
                SessionService.UserSession.AffId,
                new DateTime(year, 1, 1)
                ) : new CommissionReportDataViewModel();

            return Json(viewModel);
        }
    }
}