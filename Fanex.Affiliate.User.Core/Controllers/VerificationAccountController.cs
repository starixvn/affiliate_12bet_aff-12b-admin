using System;
using System.Web.Mvc;
using System.Web.UI;
using Fanex.Affiliate.User.Core.BusinessService;
using Fanex.Affiliate.User.Core.Constants;
using Fanex.Affiliate.User.Core.CoreService;
using Fanex.Affiliate.User.Core.DTO;
using Fanex.Affiliate.User.Core.MetalModels;
using Fanex.Affiliate.User.Core.MetalModels.Email;
using Fanex.Affiliate.User.Core.Repository.UserPanel;
using Fanex.Affiliate.User.Core.Resources;
using Fanex.Affiliate.User.Core.Utilities;

namespace Fanex.Affiliate.User.Core.Controllers
{
    public class VerificationAccountController : AuthBaseController
    {
        private readonly ITwoFactorAuthService _twoFactorAuthService;
        private readonly IAccountRepository _accountRepository;
        private readonly IEmailService _mailService;

        public VerificationAccountController(
            ISessionService sessionService,
            IAppSetting appSetting,
            ILanguageService languageService,
            ICaptchaService captchaService,
            ITwoFactorAuthService twoFactorAuthService,
            IAccountRepository accountRepository,
            IAuthService authService,
            IEmailService mailService) : base(sessionService, languageService, appSetting, captchaService, authService)
        {
            _twoFactorAuthService = twoFactorAuthService;
            _accountRepository = accountRepository;
            _mailService = mailService;
        }

        [HttpGet]
        public ActionResult Index()
        {
            if (SessionService.VerificationSession == null)
            {
                return Redirect("/Home");
            }
            var verificationAccount = SessionService.VerificationSession;
            var userInfo = _accountRepository.GetUserInfoById(verificationAccount.AffId, false);
            var email = ConvertEmail(userInfo.Email);
            return View("~/Views/VerificationAccount/Index.cshtml", model: email);
        }

        [HttpGet]
        [OutputCache(NoStore = true, Duration = 0, Location = OutputCacheLocation.None, VaryByParam = "*")]
        public ActionResult CaptchaImage(byte type = 0)
        {
            var captcha = Generator.GetRandomString(4);
            SessionService.VerificationSession.Captcha = captcha;
            return File(CaptchaService.GenerateImageBuffer(captcha, 100, 28, true), "image/png");
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        [AllowAnonymous]
        public ActionResult SendCode()
        {
            var countDown = Session["previousTime"] == null ? DateTime.Now.TimeOfDay.TotalMilliseconds : (double)Session["previousTime"];
            if (Session["previousTime"] == null || DateTime.Now.TimeOfDay.TotalMilliseconds - countDown > 60000)
            {
                var verificationSession = SessionService.VerificationSession;
                verificationSession.Counter = new Random().Next(int.MinValue, int.MaxValue);

                var userInfo = _accountRepository.GetUserInfoById(verificationSession.AffId, false);
                var otp = _twoFactorAuthService.GenerateHotp(verificationSession.AffId.ToString(), verificationSession.Counter);

                var verificationAccountEmailParam = new VerificationAccountEmailParam
                {
                    User = userInfo,
                    MailContact = AppSetting.MailContact,
                    MailTo = userInfo.Email,
                    RootDomain = SiteDomainHelper.GetRootUrl(this),
                    Code = otp,
                    Ip = RequestHelper.GetClientIPAddress()
                };

                var userLang = SystemLanguage.IsValid(userInfo.PreferredLanguage) ? userInfo.PreferredLanguage : SystemLanguage.DefaultLanguageCode;
                LanguageService.SetCulture(userLang);
                _mailService.SendVerificationEmail(verificationAccountEmailParam);
                LanguageService.SetCulture(SessionService.CurrentLanguage);
                Session["previousTime"] = DateTime.Now.TimeOfDay.TotalMilliseconds;

                return Json(new ResultInfo
                {
                    IsOk = true,
                    Message = Lang.SendCodeSuccessful
                });
            }

            return Json(new ResultInfo
            {
                IsOk = false,
                Message = Lang.SendCodeFailed
            });
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        [AllowAnonymous]
        public ActionResult VerifyAccount(VerificationAccountParam param)
        {
            var requestValidtionResult = ValidateLoginRequest(param.Captcha);
            if (!requestValidtionResult.IsOk)
            {
                return Json(requestValidtionResult);
            }

            var verificationSession = SessionService.VerificationSession;
            var isValidOtp = _twoFactorAuthService.VerifyHotp(verificationSession.AffId.ToString(), verificationSession.Counter, param.Code);

            if (isValidOtp)
            {
                var userParam = new UserLoginParam
                {
                    LoginInfo = param.LoginInfo,
                    LoginIp = Request.UserHostAddress,
                    Language = SessionService.CurrentLanguage
                };

                var loginResult = AuthService.GetLoginResult(userParam, false);
                var userInfo = loginResult.UserInfo;
                if (userInfo != null && verificationSession.AffId == userInfo.AffId)
                {
                    if (!IsValidDomain(userInfo))
                    {
                        return Json(GetErrorResult(Lang.NotAllowOnDomain));
                    }

                    SessionService.VerificationSession = loginResult.VerificationSession;

                    if (loginResult.Result.IsOk)
                    {
                        AssignLoginInfoAndSetAuth(userInfo, userParam.LoginIp);

                        return Json(loginResult.Result);
                    }
                }
            }

            return Json(GetErrorResult(Lang.ValidationVerificationAccount));
        }

        private static string ConvertEmail(string email)
        {
            var parts = email.Split('@');
            var nameEmail = parts[0];
            if (nameEmail.Length > 8)
            {
                var step = nameEmail.Length - 4;
                var item = new string('•', step);
                nameEmail = nameEmail.Remove(1, step);
                nameEmail = nameEmail.Insert(1, item);
            }
            else
            {
                var step = nameEmail.Length - 1;
                var item = new string('•', step);
                nameEmail = nameEmail.Remove(0, step);
                nameEmail = nameEmail.Insert(0, item);
            }
            var newEmail = nameEmail + "@" + parts[1];
            return newEmail;
        }
    }
}