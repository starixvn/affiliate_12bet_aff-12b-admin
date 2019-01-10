using System.Web.Mvc;
using System.Web.UI;
using Fanex.Affiliate.User.Core.BusinessService;
using Fanex.Affiliate.User.Core.CoreService;
using Fanex.Affiliate.User.Core.Utilities;
using Fanex.Affiliate.User.Core.MetalModels;
using Fanex.Affiliate.User.Core.Repository.UserPanel;
using Fanex.Affiliate.User.Core.MetalModels.Email;
using Fanex.Affiliate.User.Core.Constants;
using Fanex.Affiliate.User.Core.DTO;
using System.Linq;
using Fanex.Affiliate.User.Core.Resources;
using System.Web.Security;
using RestSharp.Extensions.MonoHttp;

namespace Fanex.Affiliate.User.Core.Controllers
{
    public class VerificationAccountController : BaseController
    {
        private readonly ITwoFactorAuthService _twoFactorAuthService;
        private readonly IAccountRepository _accountRepository;
        private readonly IEmailService _mailService;
        private readonly IAuthService _authService;
        private readonly ICaptchaService _captchaService;
        private readonly IAppSetting _appSetting;
        private readonly ICryptographyService _cryptographyService;

        public VerificationAccountController(
            ISessionService sessionService,
            IAppSetting appSetting,
            ILanguageService languageService,
            ICaptchaService captchaService,
            ITwoFactorAuthService twoFactorAuthService,
            IAccountRepository accountRepository,
            IEmailService mailService,
            IAuthService authService,
            ICryptographyService cryptographyService) : base(sessionService, languageService)
        {
            _twoFactorAuthService = twoFactorAuthService;
            _accountRepository = accountRepository;
            _mailService = mailService;
            _authService = authService;
            _captchaService = captchaService;
            _appSetting = appSetting;
            _cryptographyService = cryptographyService;
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
            SessionService.CaptchaString = captcha;
            return File(_captchaService.GenerateImageBuffer(captcha, 100, 28, true), "image/png");
        }

        public ActionResult SendCode()
        {
            try
            {
                var verificationAccount = SessionService.VerificationAccount;
                var codeOtp = _twoFactorAuthService.GenerateHotp(verificationAccount.SecretKey, verificationAccount.Counter);

                var param = new UserInfoByEmailParam
                {
                    Email = verificationAccount.Email,
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

                return Json(new ResultInfo
                {
                    IsOk = true,
                    Message = Lang.SendCodeSuccessful
                });
            }
            catch
            {
                return Json(new ResultInfo
                {
                    IsOk = false,
                    Message = Lang.SendCodeFailed
                });
            }
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        [AllowAnonymous]
        public ActionResult VerifyAccount(VerificationAccountParam param)
        {
            if (!Request.IsAjaxRequest())
            {
                return Json( new ResultInfo
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

            UserLoginParam userParam = new UserLoginParam
            {
                LoginInfo = param.LoginInfo,
                LoginIp = Request.UserHostAddress,
                Language = SessionService.CurrentLanguage
            };

            var loginResult = _authService.VerifyAccount(userParam, SessionService.VerificationAccount.UserName);

            if(loginResult == null)
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
                var verificationAccount = SessionService.VerificationAccount;
                var result = _twoFactorAuthService.VerifyHotp(verificationAccount.SecretKey, verificationAccount.Counter, param.Code);
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
