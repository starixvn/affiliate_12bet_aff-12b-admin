using System;
using System.Linq;
using System.Web.Mvc;
using System.Web.Security;
using System.Web.UI;
using Fanex.Affiliate.User.Core.BusinessService;
using Fanex.Affiliate.User.Core.CoreService;
using Fanex.Affiliate.User.Core.DTO;
using Fanex.Affiliate.User.Core.Filters;
using Fanex.Affiliate.User.Core.MetalModels;
using Fanex.Affiliate.User.Core.Resources;
using Fanex.Affiliate.User.Core.Utilities;
using ONEbook.UM.Filters;

namespace Fanex.Affiliate.User.Core.Controllers
{
    public class AuthController : BaseController
    {
        private readonly IAuthService _authService;
        private readonly ICaptchaService _captchaService;
        private readonly IAppSetting _appSetting;
        public AuthController(
            ISessionService sessionService,
            IAppSetting appSetting,
            ILanguageService languageService,
            IAuthService authService,
            ICaptchaService captchaService) : base(sessionService, languageService)

        {
            _authService = authService;
            _captchaService = captchaService;
            _appSetting = appSetting;
        }

        [ChildActionOnly]
        [HttpGet]
        [AllowAnonymous]
        public ActionResult AuthenticationForm(string v)
        {
            if (!User.Identity.IsAuthenticated || SessionService.UserSession == null)
            {
                var keyString = Generator.GetRandomString(32);
                var ivString = Generator.GetRandomString(16);

                ViewBag.KeyString = keyString;
                ViewBag.IVString = ivString;
            }

            return View();
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        [AllowAnonymous]
        public ActionResult SignIn(UserLoginParam userParam)
        {
            if (!Request.IsAjaxRequest())
            {
                return Json(GetErrorResult(Lang.ErrAuthorized));
            }

            if (!ModelState.IsValid)
            {
                return Json(new ResultInfo
                {
                    IsOk = false,
                    Message = string.Join(", ", ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage))

                });
            }

            if (!_captchaService.IsValid(userParam.ValidationKey, SessionService.CaptchaCode, Request.UserHostAddress))
            {
                return Json(new ResultInfo
                {
                    IsOk = false,
                    Message = Lang.ValidationCode

                });
            }
            userParam.LoginIp = Request.UserHostAddress;
            userParam.Language = SessionService.CurrentLanguage;
            var loginResult = _authService.GetLoginResult(userParam, false);

            if (loginResult.UserInfo != null &&
                        !_appSetting.AllowedAffiliateTypes.Any(t => t.Equals(loginResult.UserInfo.AffType)))
            {
                return Json(GetErrorResult(Lang.NotAllowOnDomain));
            }

            if (loginResult.Result.IsOk)
            {
                var user = loginResult.UserInfo;
                var onUserIdParam = GetOnUserIdParam(user.AffId, userParam.LoginIp);
                user.OnUserId = _authService.GetOnUserId(onUserIdParam);

                SessionService.AssignSessions(user);
                FormsAuthentication.SetAuthCookie(user.UserName, false);
            }

            return Json(loginResult.Result);
        }

        [HttpGet]
        public ActionResult SignOut()
        {
            DoSignOut();

            return Redirect("~/Home");
        }

        [HttpGet]
        public ActionResult AdminLogin(InternalLoginParam loginParam)
        {
            if (ModelState.IsValid)
            {
                var adminSessionId = _authService.GetSessionId(loginParam.AccOnId);
                var hashedPassword = Generator.GenerateSHA512String(adminSessionId + loginParam.ChalengeStr);
                if (string.CompareOrdinal(loginParam.AccKey, hashedPassword) == 0)
                {
                    var clientIpAddress = Request.UserHostAddress;
                    var userLoginParam = new UserLoginParam
                    {
                        UserName = loginParam.CustName,
                        LoginIp = clientIpAddress,
                        Language = SessionService.CurrentLanguage
                    };

                    SessionService.IsInternal = true;
                    userLoginParam.IsInternal = true;
                    var loginResult = _authService.GetLoginResult(userLoginParam, true);
                    var user = loginResult.UserInfo;

                    if (loginResult.Result.IsOk && _appSetting.AllowedAffiliateTypes.Any(t => t.Equals(user.AffType)))
                    {
                        var adminId = loginParam.AccId * -1;
                        var onUserIdParam = GetOnUserIdParam(user.AffId, clientIpAddress, adminId);
                        user.OnUserId = _authService.GetOnUserId(onUserIdParam);

                        SessionService.AssignSessions(user);
                        FormsAuthentication.SetAuthCookie(user.UserName, false);
                    }
                }
                else
                {
                    return Redirect(Url.Action("PageNotFound", "Notify"));
                }
            }

            return Redirect(Url.Content("~/UserPanel/Main/#AccountOverView"));
        }

        [HttpGet]
        [OutputCache(NoStore = true, Duration = 0, Location = OutputCacheLocation.None)]
        [CustomAuthorize]
        public ActionResult ReCheckin()
        {
            var onUserId = SessionService.UserSession.OnUserId;
            var isKickOut = onUserId > 0 && _authService.Recheckin(SessionService.UserSession.OnUserId,
                SessionService.IsInternal);

            if (isKickOut)
            {
                DoSignOut();
            }

            return Json(new
            {
                IsKickOut = isKickOut,
                Message = isKickOut ? Lang.Msgkickout : string.Empty
            }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        [OutputCache(NoStore = true, Duration = 0, Location = OutputCacheLocation.None, VaryByParam = "*")]
        public ActionResult CaptchaImage(byte type = 0)
        {
            var captcha = new Random().Next(1000, 9999).ToString();
            if (type == 0)
            {
                SessionService.CaptchaCode = captcha;
            }
            else
            {
                SessionService.SignUpCaptchaCode = captcha;
            }

            return File(_captchaService.GenerateImageBuffer(captcha, 65, 26), "image/png");
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

        private ResultInfo GetErrorResult(string message)
        {
            return new ResultInfo
            {
                IsOk = false,
                Message = message
            };
        }

        private void DoSignOut()
        {
            Session.Clear();
            FormsAuthentication.SignOut();
        }
    }
}