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
            var verificationAccount = SessionService.VerificationAccount;
            var userInfo = _accountRepository.GetUserInfoById(verificationAccount.AffId, false);

            return View("~/Views/VerificationAccount/Index.cshtml", model: userInfo.Email);
        }

        [HttpGet]
        [OutputCache(NoStore = true, Duration = 0, Location = OutputCacheLocation.None, VaryByParam = "*")]
        public ActionResult CaptchaImage(byte type = 0)
        {
            var captcha = Generator.GetRandomString(4);
            SessionService.CaptchaString = captcha;
            return File(_captchaService.GenerateImageBuffer(captcha, 100, 28, true), "image/png");
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        [AllowAnonymous]
        public ActionResult SendCode()
        {
            var verificationAccount = SessionService.VerificationAccount;
            var userInfo = _accountRepository.GetUserInfoById(verificationAccount.AffId, false);
            var secretKey = verificationAccount.AffId.ToString();
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
                var result = _twoFactorAuthService.VerifyHotp(verificationAccount.AffId.ToString(), verificationAccount.Counter, param.Code);
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
    }
}