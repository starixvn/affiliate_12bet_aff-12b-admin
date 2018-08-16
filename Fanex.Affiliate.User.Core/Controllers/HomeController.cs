using System.Web.Mvc;
using Fanex.Affiliate.User.Core.BusinessService;
using Fanex.Affiliate.User.Core.Constants;
using Fanex.Affiliate.User.Core.CoreService;
using Fanex.Affiliate.User.Core.Utilities;
using Fanex.Affiliate.User.Core.ViewModels;
using ONEbook.UM.Filters;

namespace Fanex.Affiliate.User.Core.Controllers
{
    [CheckUMFilter]
    public class HomeController : Controller
    {
        private readonly ILanguageService _languageService;
        private readonly ISessionService _sessionService;
        private readonly IAppSetting _appSetting;

        public HomeController(
            ISessionService sessionService,
            IAppSetting appSetting,
            ILanguageService languageService)

        {
            _languageService = languageService;
            _sessionService = sessionService;
            _appSetting = appSetting;
        }

        [HttpGet]
        public ActionResult Index()
        {
            var isAuthenticated = Request.IsAuthenticated && _sessionService.UserSession != null;

            var viewModel = new HomeViewModel
            {
                IsAuthenticated = isAuthenticated,
                SignUpLink = isAuthenticated ? Url.Content("~/UserPanel/Main/#AccountOverView") : Url.Action("Index", "SignUp"),
                BannerLink = isAuthenticated ? Url.Content("~/UserPanel/Main/#MediaGallery") : "javascript:;",
                Promotion12BetLink = GetPromotionLink()
            };

            return View(viewModel);
        }


        [HttpPost]
        public ActionResult ChangeLanguage(string languageCode)
        {
            var code = string.IsNullOrEmpty(languageCode) ? SystemLanguage.DefaultLanguageCode : languageCode;
            _sessionService.CurrentLanguage = code;
            _languageService.SetCulture(code);

            return Json(true);
        }

        private string GetPromotionLink()
        {
            var segments = Request.Url.Host.Split('.');
            var domainName = segments.Length > 1 ? segments[segments.Length - 2] : string.Empty;

            return string.Format(_appSetting.Promotion12BetLink, domainName).Replace("..", ".");
        }
    }
}