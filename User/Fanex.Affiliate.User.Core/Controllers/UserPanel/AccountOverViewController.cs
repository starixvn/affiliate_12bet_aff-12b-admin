namespace Fanex.Affiliate.User.Core.Controllers.UserPanel
{
    using System.Web.Mvc;
    using Fanex.Affiliate.User.Core.BusinessService;
    using Fanex.Affiliate.User.Core.CoreService;
    using Filters;
    using Repository.UserPanel;

    [CustomAuthorize]
    public class AccountOverviewController : BaseController
    {
        private readonly IAccountOverviewRepository _accountOverviewRepository;
        public AccountOverviewController(ISessionService sessionService,
            ILanguageService languageService,
            IAccountOverviewRepository accountOverviewRepository
            ) : base(sessionService, languageService)
        {
            _accountOverviewRepository = accountOverviewRepository;
        }

        [HttpGet]
        [AjaxOnly]
        public ActionResult Index()
        {
            var model = _accountOverviewRepository.GetLastLoginInfo(SessionService.UserSession.AffId);

            return View("~/Views/UserPanel/AccountOverView/Index.cshtml", model);
        }
    }
}