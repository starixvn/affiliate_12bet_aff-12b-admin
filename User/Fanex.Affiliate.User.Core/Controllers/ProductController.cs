namespace Fanex.Affiliate.User.Core.Controllers
{
    using CoreService;
    using Fanex.Affiliate.User.Core.BusinessService;
    using System.Web.Mvc;
    using ViewModels;

    public class ProductController : BaseController
    {
        public ProductController(ISessionService sessionService, ISystemService languageService)
            : base(sessionService, languageService)
        {
        }

        [HttpGet]
        public ActionResult Index()
        {
            var viewModel = new ProPageViewModel
            {
                CurrentLanguage = SessionService.CurrentLanguage,
                IsAuthenticated = HttpContext.Request.IsAuthenticated && SessionService.UserSession != null
            };

            return View("~/Views/Product/Index.cshtml", viewModel);
        }
    }
}