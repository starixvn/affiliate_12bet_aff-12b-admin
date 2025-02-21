namespace Fanex.Affiliate.User.Core.Controllers
{
    using CoreService;
    using Fanex.Affiliate.User.Core.BusinessService;
    using Fanex.Affiliate.User.Core.Filters;
    using System.Web.Mvc;

    public class FaqController : BaseController
    {

        public FaqController(ISessionService sessionService, ISystemService systemsService)
            : base(sessionService, systemsService)
        {
        }

        [HttpGet]
        [LanguageRouteChecking]
        public ActionResult Index()
        {
            return View("~/Views/Faq/Index.cshtml", model: SessionService.CurrentLanguage);
        }
    }
}