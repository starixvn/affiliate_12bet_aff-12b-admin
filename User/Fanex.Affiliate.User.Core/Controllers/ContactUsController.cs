namespace Fanex.Affiliate.User.Core.Controllers
{
    using System.Web.Mvc;
    using BusinessService;
    using CoreService;
    using Filters;

    public class ContactUsController : BaseController
    {
        public ContactUsController(
            ISessionService sessionService,
            ISystemService systemsService)
            : base(sessionService, systemsService)
        {
        }

        [HttpGet]
        [LanguageRouteChecking]
        public ActionResult Index()
        {
            ViewBag.CurrentLanguage = SessionService.CurrentLanguage;

            return View();
        }
    }
}