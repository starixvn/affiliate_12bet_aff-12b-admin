namespace Fanex.Affiliate.User.Core.Controllers
{
    using System.Web.Mvc;
    using CoreService;
    using Utilities;
    using ViewModels;

    public class ProductController : BaseController
    {
        public ProductController(ISessionService sessionService, IAppSetting appSetting)
            : base(sessionService, appSetting)
        {
        }

        [HttpGet]
        public ActionResult Index()
        {
            var viewModel = new ProductPageViewModel
            {
                CurrentLanguage = SessionService.CurrentLanguage,
                IsAuthenticated = HttpContext.Request.IsAuthenticated && SessionService.UserSession != null
            };

            return View("~/Views/Product/Index.cshtml", viewModel);
        }
    }
}