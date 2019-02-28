namespace Fanex.Affiliate.User.Core.Controllers
{
    using System.Web.Mvc;
    using Fanex.Affiliate.User.Core.BusinessService;
    using Fanex.Affiliate.User.Core.CoreService;
    using Filters;
    using ViewModels.UserPanel;

    [CustomAuthorize]
    public class MainController : BaseController
    {
        public MainController(ISessionService sessionService,
            ILanguageService languageService) : base(sessionService, languageService)
        {

        }

        [HttpGet]
        public ActionResult Index()
        {
            var viewModel = new UserPanelMainViewModel
            {
                IsVisitedAccountOverviewPage = SessionService.UserSession != null && SessionService.UserSession.IsVisitedAccountOverViewPage,
                IsVisitedCommissionReportPage = SessionService.UserSession != null && SessionService.UserSession.IsVisitedCommissionReportPage
            };

            return View("~/Views/UserPanel/Main/index.cshtml", viewModel);
        }

        [HttpGet]
        public ActionResult CommissionNewsPopup()
        {
            return View("~/Views/UserPanel/Popup/Index.cshtml");
        }
    }
}