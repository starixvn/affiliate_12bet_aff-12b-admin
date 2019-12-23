namespace Fanex.Affiliate.User.Core.Controllers.UserPanel
{
    using System.Web.Mvc;
    using Fanex.Affiliate.User.Core.BusinessService;
    using Fanex.Affiliate.User.Core.CoreService;
    using Filters;

    [CustomAuthorize]
    public class AccountOverviewController : BaseController
    {
        public AccountOverviewController(ISessionService sessionService,
            ILanguageService languageService
            ) : base(sessionService, languageService)
        {
        }

        [HttpGet]
        [AjaxOnly]
        public ActionResult Index()
        {
            return View("~/Views/UserPanel/AccountOverView/Index.cshtml");
        }
    }
}