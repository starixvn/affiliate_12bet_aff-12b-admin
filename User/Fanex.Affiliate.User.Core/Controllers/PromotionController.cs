namespace Fanex.Affiliate.User.Core.Controllers
{
    using CoreService;
    using Fanex.Affiliate.User.Core.BusinessService;
    using Fanex.Affiliate.User.Core.ViewModels;
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
            var viewModel = new ProPageViewModel
            {
                CurrentLanguage = SessionService.CurrentLanguage,
                IsAuthenticated = HttpContext.Request.IsAuthenticated && SessionService.UserSession != null
            };

            return View("~/Views/Promotion/Index.cshtml", viewModel);
        }
    }
}