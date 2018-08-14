using System;
using System.Web.Mvc;
using Fanex.Affiliate.User.Core.BusinessService;
using Fanex.Affiliate.User.Core.Constants;
using Fanex.Affiliate.User.Core.CoreService;
using Fanex.Affiliate.User.Core.Utilities;
using Fanex.Affiliate.User.Core.ViewModels;

namespace Fanex.Affiliate.User.Core.Controllers
{
    public class HomeController : BaseController
    {
        private readonly ILanguageService _languageService;

        public HomeController(
            ISessionService sessionService,
            IAppSetting appSetting,
            ILanguageService languageService)
            : base(sessionService, appSetting)
        {
            _languageService = languageService;
        }

        [HttpGet]
        public ActionResult Index()
        {
            var isAuthenticated = Request.IsAuthenticated && SessionService.UserSession != null;

            var viewModel = new HomeViewModel
            {
                IsAuthenticated = isAuthenticated,
                SignUpLink = isAuthenticated ? Url.Content("~/UserPanel/Main/#AccountOverView") : Url.Action("Index", "SignUp"),
                BannerLink = isAuthenticated ? Url.Content("~/UserPanel/Main/#MediaGallery") : "javascript:;",
                Promotion12BetLink = GetPromotionLink()
            };

            return View(viewModel);
        }

        [HttpGet]
        public ActionResult CaptchaImage()
        {
            var captcha = new Random().Next(1000, 9999).ToString();
            SessionService.CaptchaCode = captcha;

            return File(CaptchaHelper.GenerateImageBuffer(captcha, 65, 26), "image/png");
        }

        [HttpPost]
        public ActionResult ChangeLanguage(string languageCode)
        {
            var code = string.IsNullOrEmpty(languageCode) ? SystemLanguage.DefaultLanguageCode : languageCode;
            SessionService.CurrentLanguage = code;
            _languageService.SetCulture(code);

            return Json(true);
        }

        private string GetPromotionLink()
        {
            var segments = Request.Url.Host.Split('.');
            var domainName = segments.Length > 1 ? segments[segments.Length - 2] : string.Empty;

            return string.Format(AppSetting.Promotion12BetLink, domainName).Replace("..", ".");
        }
    }
}