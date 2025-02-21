namespace Fanex.Affiliate.User.Core.Controllers
{
    using CoreService;
    using Fanex.Affiliate.User.Core.BusinessService;
    using Fanex.Affiliate.User.Core.Filters;
    using System.Web.Mvc;

    public class ProductController : BaseController
    {
        public ProductController(ISessionService sessionService, ISystemService systemsService)
            : base(sessionService, systemsService)
        {
        }

        [HttpGet]
        [LanguageRouteChecking]
        public ActionResult Index()
        {
            return View("~/Views/Product/Index.cshtml", model: SessionService.CurrentLanguage);
        }
    }
}