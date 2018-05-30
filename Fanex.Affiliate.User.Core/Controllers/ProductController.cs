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
    }
}