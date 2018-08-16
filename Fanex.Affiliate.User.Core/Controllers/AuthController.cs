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
    [CheckUMFilter]
    public class AuthController : Controller
    {
        private readonly IAuthService _authService;
        private readonly ICookieService _cookieService;
        private readonly ISessionService _sessionService;
        private readonly IAppSetting _appSetting;

        public AuthController(
            ISessionService sessionService,
            IAppSetting appSetting,
            ICookieService cookieService,
            IAuthService authService)

        {
            _authService = authService;
            _cookieService = cookieService;
            _sessionService = sessionService;
            _appSetting = appSetting;
        }

        [ChildActionOnly]
        [HttpGet]
        [AllowAnonymous]
        public ActionResult AuthenticationForm()
        {
            if (_sessionService.UserSession == null)
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

            if (!IsValidCaptcha(userParam.ValidationKey))
            {
                return Json(new ResultInfo
                {
                    IsOk = false,
                    Message = Lang.ValidationCode

                });
            }
            userParam.LoginIp = Request.UserHostAddress;
            userParam.Language = _sessionService.CurrentLanguage;
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

                _sessionService.AssignSessions(user);
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
                        Language = _sessionService.CurrentLanguage
                    };

                    _sessionService.IsInternal = true;
                    userLoginParam.IsInternal = true;
                    var loginResult = _authService.GetLoginResult(userLoginParam, true);
                    var user = loginResult.UserInfo;

                    if (loginResult.Result.IsOk && _appSetting.AllowedAffiliateTypes.Any(t => t.Equals(user.AffType)))
                    {
                        var adminId = loginParam.AccId * -1;
                        var onUserIdParam = GetOnUserIdParam(user.AffId, clientIpAddress, adminId);
                        user.OnUserId = _authService.GetOnUserId(onUserIdParam);

                        _sessionService.AssignSessions(user);
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
        [OutputCache(NoStore = true, Duration = 0, Location = OutputCacheLocation.None, VaryByParam = "*")]
        [CustomAuthorize]
        public ActionResult ReCheckin()
        {
            var onUserId = _sessionService.UserSession.OnUserId;
            var isKickOut = onUserId > 0 && _authService.Recheckin(_sessionService.UserSession.OnUserId, 
                _sessionService.IsInternal);

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

        private OnUserIdParam GetOnUserIdParam(int affId, string clientIpAddress, int adminId = 0)
        {
            return new OnUserIdParam
            {
                CustId = affId,
                ServerId = _appSetting.ServerId,
                Ip = clientIpAddress,
                NetSessionId = HttpContext.Session.SessionID,
                SubAccId = adminId,
                IsInternal = _sessionService.IsInternal
            };
        }

        private bool IsValidCaptcha(string validationKey)
        {
            return validationKey.Equals(_sessionService.CaptchaCode);
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
            _cookieService.ClearAllCookies();
            Session.Clear();
            FormsAuthentication.SignOut();
        }
    }
}