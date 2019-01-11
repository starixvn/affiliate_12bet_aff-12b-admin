using System.Linq;
using System.Web.Mvc;
using System.Web.Security;
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
    public class VerificationAccountController : BaseController
    {
        private readonly ITwoFactorAuthService _twoFactorAuthService;
        private readonly IAccountRepository _accountRepository;
        private readonly IAuthService _authService;
        private readonly ICaptchaService _captchaService;
        private readonly IAppSetting _appSetting;
        private readonly IEmailService _mailService;

        public VerificationAccountController(
            ISessionService sessionService,
            IAppSetting appSetting,
            ILanguageService languageService,
            ICaptchaService captchaService,
            ITwoFactorAuthService twoFactorAuthService,
            IAccountRepository accountRepository,
            IAuthService authService,
            IEmailService mailService) : base(sessionService, languageService)
        {
            _twoFactorAuthService = twoFactorAuthService;
            _accountRepository = accountRepository;
            _authService = authService;
            _captchaService = captchaService;
            _appSetting = appSetting;
            _mailService = mailService;
        }

        [HttpGet]
        public ActionResult Index()
        {
            if(SessionService.VerificationAccount == null)
            {
                return Redirect("/Home");
            }
            var verificationAccount = SessionService.VerificationAccount;
            var userInfo = _accountRepository.GetUserInfoById(verificationAccount.AffId, false);
            var email = HandlingEmail(userInfo.Email);
            return View("~/Views/VerificationAccount/Index.cshtml", model: email);
        }

        [HttpGet]
        [OutputCache(NoStore = true, Duration = 0, Location = OutputCacheLocation.None, VaryByParam = "*")]
        public ActionResult CaptchaImage(byte type = 0)
        {
            var captcha = Generator.GetRandomString(4);
            SessionService.CaptchaString = captcha;
            return File(_captchaService.GenerateImageBuffer(captcha, 100, 28, true), "image/png");
        }


        [HttpPost]
        [AllowAnonymous]
        public ActionResult SendCode()
        {
            var verificationAccount = SessionService.VerificationAccount;
            var userInfo = _accountRepository.GetUserInfoById(verificationAccount.AffId, false);
            var secretKey = GetSecretKey(verificationAccount.AffId);
            var otp = _twoFactorAuthService.GenerateHotp(secretKey, verificationAccount.Counter);

            var verificationAccountEmailParam = new VerificationAccountEmailParam
            {
                User = userInfo,
                MailContact = _appSetting.MailContact,
                MailTo = "walter.tran@starixsoft.com",
                RootDomain = SiteDomainHelper.GetRootUrl(this),
                Code = otp,
                Ip = RequestHelper.GetClientIPAddress()
            };

            var userLang = SystemLanguage.IsValid(userInfo.PreferredLanguage) ? userInfo.PreferredLanguage : SystemLanguage.DefaultLanguageCode;
            LanguageService.SetCulture(userLang);
            _mailService.SendVerificationEmail(verificationAccountEmailParam);
            LanguageService.SetCulture(SessionService.CurrentLanguage);

            return Json(new ResultInfo
            {
                IsOk = true,
                Message = Lang.SendCodeSuccessful
            });
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        [AllowAnonymous]
        public ActionResult VerifyAccount(VerificationAccountParam param)
        {
            if (!Request.IsAjaxRequest())
            {
                return Json(new ResultInfo
                {
                    IsOk = false,
                    Message = Lang.ErrAuthorized
                });
            }

            if (!ModelState.IsValid)
            {
                return Json(new ResultInfo
                {
                    IsOk = false,
                    Message = string.Join(", ", ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage))
                });
            }

            if (!_captchaService.IsValid(param.Captcha, SessionService.CaptchaString, Request.UserHostAddress))
            {
                return Json(new ResultInfo
                {
                    IsOk = false,
                    Message = Lang.ValidationCaptcha
                });
            }

            var userParam = new UserLoginParam
            {
                LoginInfo = param.LoginInfo,
                LoginIp = Request.UserHostAddress,
                Language = SessionService.CurrentLanguage
            };

            var verificationAccount = SessionService.VerificationAccount;
            var userInfo = _accountRepository.GetUserInfoById(verificationAccount.AffId, false);

            var loginResult = _authService.VerifyAccount(userParam, userInfo.UserName);

            if (loginResult == null)
            {
                return Json(new ResultInfo
                {
                    IsOk = false,
                    Message = Lang.ValidationVerificationAccount
                });
            }

            if (loginResult.UserInfo != null &&
                        !_appSetting.AllowedAffiliateTypes.Any(t => t.Equals(loginResult.UserInfo.AffType)))
            {
                return Json(new ResultInfo
                {
                    IsOk = false,
                    Message = Lang.NotAllowOnDomain
                });
            }

            if (loginResult.Result.IsOk)
            {
                var secretKey = GetSecretKey(verificationAccount.AffId);
                var result = _twoFactorAuthService.VerifyHotp(secretKey, verificationAccount.Counter, param.Code);
                if (result)
                {
                    Session.Remove(userParam.UserName);

                    var user = loginResult.UserInfo;
                    var onUserIdParam = GetOnUserIdParam(user.AffId, userParam.LoginIp);
                    user.OnUserId = _authService.GetOnUserId(onUserIdParam);

                    SessionService.AssignSessions(user);
                    FormsAuthentication.SetAuthCookie(user.UserName, false);

                    return Json(loginResult.Result);
                }
            }

            return Json(new ResultInfo
            {
                IsOk = false,
                Message = Lang.ValidationVerificationAccount
            });
        }

        private OnUserIdParam GetOnUserIdParam(int affId, string clientIpAddress, int adminId = 0)
        {
            return new OnUserIdParam
            {
                CustId = affId,
                ServerId = _appSetting.ServerId,
                Ip = clientIpAddress,
                NetSessionId = HttpContext.Session.SessionID,
                SubAccId = adminId,
                IsInternal = SessionService.IsInternal
            };
        }

        private static string HandlingEmail(string email)
        {
            var parts = email.Split('@');
            var nameEmail = parts[0];
            if(nameEmail.Length > 8)
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

        private static string GetSecretKey(int affId)
        {
            var secretKey = affId.ToString().Replace('1','B').Replace('8','E').Replace('9','T');
            return secretKey;
        }
    }
}