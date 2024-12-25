namespace Fanex.Affiliate.User.Core.Controllers.UserPanel
{
    using System;
    using System.Web.Mvc;
    using BusinessService;
    using CoreService;
    using Fanex.Affiliate.User.Core.ViewModels.UserPanel;
    using Filters;
    using MetalModels.UserPanel;

    [CustomAuthorize]
    public class RevenueReportController : BaseController
    {
        private readonly ICommissionReportService _commissionReportService;

        public RevenueReportController(
            ISessionService sessionService,
            ISystemService systemsService,
            ICommissionReportService commissionReportService
            ) : base(sessionService, systemsService)
        {
            _commissionReportService = commissionReportService;
        }

        [HttpGet]
        [AjaxOnly]
        public ActionResult Index(int? baseCurrency)
        {
            ViewBag.BaseCurrencies = SystemService.GetBaseCurrencyList();
            ViewBag.DefaultBaseCurrency = baseCurrency ?? SessionService.UserSession.BaseCurrency;

            return View("~/Views/UserPanel/CommissionReport/Index.cshtml", SessionService.UserSession.IsMaxSubLevel);
        }

        [HttpPost]
        [AjaxOnly]
        public ActionResult Index(int year, int baseCurrency = 0)
        {
            var currency = baseCurrency == 0 ? SessionService.UserSession.BaseCurrency : baseCurrency;
            var viewModel = year >= 2009 && year <= DateTime.Now.Year
                ? _commissionReportService.GenerateCommissionReportData(new CommissionReportParam
                {
                    AffId = SessionService.UserSession.AffId, 
                    WinLostDate = new DateTime(year, 1, 1), 
                    BaseCurrency = currency
                })
                : new CommissionReportDataViewModel();

            return Json(viewModel);
        }
    }
}