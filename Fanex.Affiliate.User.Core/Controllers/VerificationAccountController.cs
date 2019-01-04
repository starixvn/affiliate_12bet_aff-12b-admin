using System.Web.Mvc;
using System.Web.UI;
using Fanex.Affiliate.User.Core.BusinessService;
using Fanex.Affiliate.User.Core.CoreService;
using Fanex.Affiliate.User.Core.Utilities;
using Fanex.Affiliate.User.Core.MetalModels;
using Fanex.Affiliate.User.Core.Repository.UserPanel;
using Fanex.Affiliate.User.Core.MetalModels.Email;
using Fanex.Affiliate.User.Core.Constants;

namespace Fanex.Affiliate.User.Core.Controllers
{
    public class VerificationAccountController : BaseController
    {
        private readonly ICaptchaService _captchaService;

        private readonly ITwoFactorAuthService _twoFactorAuthService;
        private readonly IAccountRepository _accountRepository;
        private readonly IEmailService _mailService;
        private readonly IAppSetting _appSetting;

        public VerificationAccountController(
            ISessionService sessionService,
            ILanguageService languageService,
            ICaptchaService captchaService,
            ITwoFactorAuthService twoFactorAuthService,
            IAccountRepository accountRepository,
            IAppSetting appSetting,
            IEmailService mailService) : base(sessionService, languageService)
        {
            _captchaService = captchaService;
            _twoFactorAuthService = twoFactorAuthService;
            _accountRepository = accountRepository;
            _mailService = mailService;
            _appSetting = appSetting;
        }

        [HttpGet]
        public ActionResult Index(string userName)
        {
            //get email by userName
            string email = "meanuit@gmail.com";
            SendCode(email);
            return View();
        }

        [HttpGet]
        [OutputCache(NoStore = true, Duration = 0, Location = OutputCacheLocation.None, VaryByParam = "*")]
        public ActionResult CaptchaImage(byte type = 0)
        {
            var captcha = Generator.GetRandomString(4);

            return File(_captchaService.GenerateImageBuffer(captcha, 100, 28, true), "image/png");
        }

        public void SendCode(string email)
        {
            var secretKey = "GEZDGNBVGY3TQOJQGEZDGNBVGY3TQOJQ";
            var codeOtp = _twoFactorAuthService.GenerateHotp(secretKey);

            var param = new UserInfoByEmailParam
            {
                Email = email.Trim(),
                Ip = RequestHelper.GetClientIPAddress(),
                ValidCode = codeOtp,
                Language = SessionService.CurrentLanguage
            };
            var userInfo = _accountRepository.GetUserInfoByEmail(param);

            var verificationAccountEmailParam = new VerificationAccountEmailParam
            {
                User = userInfo,
                MailContact = _appSetting.MailContact,
                MailTo = param.Email,
                RootDomain = SiteDomainHelper.GetRootUrl(this),
                Code = codeOtp,
                Ip = param.Ip
            };

            var userLang = SystemLanguage.IsValid(userInfo.PreferredLanguage) ? userInfo.PreferredLanguage : SystemLanguage.DefaultLanguageCode;
            LanguageService.SetCulture(userLang);
            _mailService.SendHotpCodeEmail(verificationAccountEmailParam);
            LanguageService.SetCulture(SessionService.CurrentLanguage);
        }
    }
}
