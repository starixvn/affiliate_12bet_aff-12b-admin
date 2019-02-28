namespace Fanex.Affiliate.User.Core.Controllers
{
    using System.Web.Mvc;
    using CoreService;
    using Fanex.Affiliate.User.Core.BusinessService;
    using ViewModels;

    public class ProductController : BaseController
    {
        public ProductController(ISessionService sessionService, ILanguageService languageService)
            : base(sessionService, languageService)
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