using System;
using System.Web.Mvc;
using System.Web.UI;
using Fanex.Affiliate.User.Core.BusinessService;
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
        private readonly IAccountRepository _accountRepository;
        private readonly IVerificationService _verificationService;

        public VerificationAccountController(
            ISessionService sessionService,
            IAppSetting appSetting,
            ILanguageService languageService,
            ICaptchaService captchaService,
            IAccountRepository accountRepository,
            IAuthService authService,
            IVerificationService verificationService) : base(sessionService, languageService, appSetting, captchaService, authService)
        {
            _accountRepository = accountRepository;
            _verificationService = verificationService;
        }

        [HttpGet]
        public ActionResult Index()
        {
            var verificationSession = SessionService.VerificationSession;
            if (verificationSession == null)
            {
                return Redirect(Url.Content("~/Home"));
            }

            var userInfo = _accountRepository.GetUserInfoById(verificationSession.AffId, false);
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
        public ActionResult SendCode()
        {
            var verificationSession = SessionService.VerificationSession;
            var sendCodeParam = new SendCodeParam
            {
                AffId = verificationSession.AffId,
                SentCodeTime = verificationSession.SentCodeTime,
                CurrentLanguage = SessionService.CurrentLanguage,
                VerificationEmailParam = new VerificationAccountEmailParam
                {
                    MailContact = AppSetting.MailContact,
                    RootDomain = SiteDomainHelper.GetRootUrl(this, AppSetting),
                    Ip = RequestHelper.GetClientIPAddress()
                }
            };

            var result = _verificationService.SendCode(sendCodeParam);
            if (result.IsOk)
            {
                verificationSession.Counter = result.NewCounter;
                verificationSession.SentCodeTime = GetNewSentCodeTime();
            }

            return Json(new ResultInfo
            {
                IsOk = result.IsOk,
                Message = result.Message
            });
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult VerifyAccount(VerificationAccountParam param)
        {
            var verificationSession = SessionService.VerificationSession;
            var requestValidtionResult = ValidateLoginRequest(param.Captcha, verificationSession.Captcha);
            if (!requestValidtionResult.IsOk)
            {
                return Json(requestValidtionResult);
            }

            if (_verificationService.IsValidOtp(verificationSession.AffId.ToString(), verificationSession.Counter, param.Code))
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
                    if (!IsValidDomain(userInfo.AffType))
                    {
                        return Json(GetErrorResult(Lang.NotAllowOnDomain));
                    }

                    if (loginResult.Result.IsOk)
                    {
                        SessionService.VerificationSession = null;
                        AssignLoginInfoAndSetAuth(userInfo, userParam.LoginIp);

                        return Json(loginResult.Result);
                    }
                }
            }

            return Json(GetErrorResult(Lang.ValidationVerificationAccount));
        }

        private static string ConvertEmail(string email)
        {
            var defaultLength = 8;
            var showedCharNumber = 1;
            var substrings = email.Split('@');
            var emailName = substrings[0];
            var emailTail = substrings[1];
            var newEmailName = string.Empty;

            if (emailName.Length > defaultLength)
            {
                showedCharNumber = 4;
                newEmailName = ReplaceEmailName(emailName, showedCharNumber, 1);
            }
            else
            {
                newEmailName = ReplaceEmailName(emailName, showedCharNumber, 0);
            }

            return newEmailName + "@" + emailTail;
        }

        private static string ReplaceEmailName (string emailName, int numberCharShow, int indexStart)
        {
            var hidCharNumber = emailName.Length - numberCharShow;
            var stringReplace = new string('•', hidCharNumber);
            var newEmailName = emailName.Remove(indexStart, hidCharNumber);
            newEmailName = newEmailName.Insert(indexStart, stringReplace);
            return newEmailName;
        }
        private static long GetNewSentCodeTime()
        {
            var now = DateTime.Now;

            return new DateTime(now.Year, now.Month, now.Day, now.Hour, now.Minute, now.Second).Ticks;
        }
    }
}