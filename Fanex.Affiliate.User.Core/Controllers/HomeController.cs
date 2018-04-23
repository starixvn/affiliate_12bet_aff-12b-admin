using System;
using System.Web.Mvc;
using Fanex.Affiliate.User.Core.BusinessService;
using Fanex.Affiliate.User.Core.Constants;
using Fanex.Affiliate.User.Core.CoreService;
using Fanex.Affiliate.User.Core.Utilities;

namespace Fanex.Affiliate.User.Core.Controllers
{
    public class HomeController : BaseController
    {
        private readonly ILanguageService _languageService;

        private readonly ICookieService _cookieService;

        public HomeController(
            ISessionService sessionService,
            IAppSetting appSetting,
            ILanguageService languageService,
            ICookieService cookieService)
            : base(sessionService, appSetting)
        {
            _languageService = languageService;
            _cookieService = cookieService;
        }

        public ActionResult Index()
        {
            if (SessionService.UserSession == null)
            {
                var keyString = Generator.GetRandomString(32);
                var ivString = Generator.GetRandomString(16);

                ViewBag.KeyString = keyString;
                ViewBag.IVString = ivString;

                _cookieService.SetCookie(CookieName.EncryptionKey, keyString);
                _cookieService.SetCookie(CookieName.InitializationVector, ivString);
            }

            return View();
        }

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