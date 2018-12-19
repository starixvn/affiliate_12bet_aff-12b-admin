using System.Web.Mvc;
using System.Web.UI;
using Fanex.Affiliate.User.Core.BusinessService;
using Fanex.Affiliate.User.Core.CoreService;
using Fanex.Affiliate.User.Core.Utilities;

namespace Fanex.Affiliate.User.Core.Controllers
{
    public class VerificationAccountController : BaseController
    {
        private readonly ICaptchaService _captchaService;

        private readonly ITwoFactorAuthService _twoFactorAuthService;

        public VerificationAccountController(
            ISessionService sessionService,
            ILanguageService languageService,
            ICaptchaService captchaService,
            ITwoFactorAuthService twoFactorAuthService) : base(sessionService, languageService)
        {
            _captchaService = captchaService;
            _twoFactorAuthService = twoFactorAuthService;
        }

        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [OutputCache(NoStore = true, Duration = 0, Location = OutputCacheLocation.None, VaryByParam = "*")]
        public ActionResult CaptchaImage(byte type = 0)
        {
            var captcha = Generator.GetRandomString(4);

            return File(_captchaService.GenerateImageBuffer(captcha, 100, 28, true), "image/png");
        }
    }
}
