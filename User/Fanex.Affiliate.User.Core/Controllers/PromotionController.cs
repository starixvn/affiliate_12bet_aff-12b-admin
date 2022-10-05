namespace Fanex.Affiliate.User.Core.Controllers
{
    using CoreService;
    using Fanex.Affiliate.User.Core.BusinessService;
    using Fanex.Affiliate.User.Core.Constants;
    using Fanex.Affiliate.User.Core.Filters;
    using System.Web.Mvc;

    public class PromotionController : BaseController
    {

        public PromotionController(ISessionService sessionService, ILanguageService languageService)
            : base(sessionService, languageService)
        {
        }

        [HttpGet]
        public ActionResult Index()
        {
            return View("~/Views/Promotion/Index.cshtml", model: SystemLanguage.GetShortedCodeForTracking(SessionService.CurrentLanguage));
        }

        [HttpPost]
        [AjaxOnly]
        public ActionResult New()
        {
            return GetContent("New");
        }

        [HttpPost]
        [AjaxOnly]
        public ActionResult Existing()
        {
            return GetContent("Existing");
        }

        private ActionResult GetContent(string folder)
        {
            var isAuthenticated = Request.IsAuthenticated && SessionService.UserSession != null;

            return PartialView($"~/Views/Promotion/{folder}/_Content.{SessionService.CurrentLanguage}.cshtml", model: isAuthenticated);
        }
    }
}