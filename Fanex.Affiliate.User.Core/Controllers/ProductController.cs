namespace Fanex.Affiliate.User.Core.Controllers
{
    using System.Web.Mvc;
    using CoreService;
    using Utilities;

    public class ProductController : BaseController
    {
        public ProductController(ISessionService sessionService, IAppSetting appSetting)
            : base(sessionService, appSetting)
        {
        }

        [HttpGet]
        public ActionResult Index()
        {
            var isAuthenticated = HttpContext.Request.IsAuthenticated && SessionService.UserSession != null;
            return View(isAuthenticated);
        }

        [HttpGet]
        [ChildActionOnly]
        [OutputCache(Duration = 86400, VaryByParam = "none", VaryByCustom = "language")]
        public ActionResult Content()
        {
            var viewName = $"~/Views/Product/_Content.{SessionService.CurrentLanguage}.cshtml";

            return PartialView(viewName);
        }
    }
}