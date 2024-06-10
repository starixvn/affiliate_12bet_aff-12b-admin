namespace Fanex.Affiliate.User.Core.Controllers
{
    using CoreService;
    using Fanex.Affiliate.User.Core.BusinessService;
    using System.Web.Mvc;

    public class ProductController : BaseController
    {
        public ProductController(ISessionService sessionService, ISystemService languageService)
            : base(sessionService, languageService)
        {
        }

        [HttpGet]
        public ActionResult Index()
        {
            return View("~/Views/Product/Index.cshtml", model: SessionService.CurrentLanguage);
        }
    }
}