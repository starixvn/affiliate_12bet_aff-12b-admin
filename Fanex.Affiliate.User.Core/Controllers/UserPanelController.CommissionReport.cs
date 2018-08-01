namespace Fanex.Affiliate.User.Core.Controllers
{
    using System;
    using System.Web.Mvc;
    using Fanex.Affiliate.User.Core.ViewModels.UserPanel;
    using Filters;

    public partial class UserPanelController
    {
        [HttpGet]
        [AjaxOnly]
        public ActionResult CommissionReport()
        {
            return View();
        }

        [HttpPost]
        [AjaxOnly]
        public ActionResult CommissionReport(int year)
        {
            var viewModel = year >=2009 && year <= DateTime.Now.Year
                ? _commissionReportService.GenerateCommissionReportData(
                SessionService.UserSession.AffId,
                new DateTime(year, 1, 1)
                ):new CommissionReportViewModel();

            return Json(viewModel);
        }
    }
}