using System;
using System.Linq;
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
            ILanguageService languageService,
            ICookieService cookieService)
            : base(sessionService, appSetting)
        {
            _languageService = languageService;
        }

        [HttpGet]
        public ActionResult Index()
        {
            var isAuthenticated = HttpContext.Request.IsAuthenticated && SessionService.UserSession != null;

            var shortedLanguageCode = SystemLanguage.ShortedDefaultCode;
            if (AppSetting.IsEuroSite)
            {
                shortedLanguageCode = SystemLanguage.ShortedEuroCode;
            }
            else
            {
                shortedLanguageCode = SystemLanguage.LanguageList
                                        .FirstOrDefault(l => l.Code.Equals(SessionService.CurrentLanguage))?.ShortedCode ?? shortedLanguageCode;
            }

            var viewModel = new HomeViewModel
            {
                IsAuthenticated = isAuthenticated,
                SignUpLink = isAuthenticated ? Url.Action("Index", "UserPanel") + "/#AccountOverView" : Url.Action("Index", "SignUp"),
                BannerLink = isAuthenticated ? Url.Action("Index", "UserPanel") + "/#MediaGallery" : "javascript:;",
                Promotion12BetLink = AppSetting.Promotion12BetLink.Replace("{lang}", shortedLanguageCode)
            };

            return View(viewModel);
        }

        [HttpGet]
        public ActionResult CaptchaImage()
        {
            var captcha = new Random().Next(1000, 9999).ToString();
            SessionService.CaptchaCode = captcha;

            return File(CaptchaHelper.GenerateImageBuffer(captcha, 65, 26), "image/Jpeg");
        }

        [HttpPost]
        public ActionResult ChangeLanguage(string languageCode)
        {
            languageCode = string.IsNullOrEmpty(languageCode) ? SystemLanguage.DefaultLanguageCode : languageCode;
            SessionService.CurrentLanguage = languageCode;
            _languageService.SetCulture(languageCode);

            return Json(true);
        }
    }
}