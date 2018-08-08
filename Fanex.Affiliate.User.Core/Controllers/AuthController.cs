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

namespace Fanex.Affiliate.User.Core.Controllers
{
    public class AuthController : BaseController
    {
        private readonly IAuthService _authService;
        private readonly ICookieService _cookieService;

        public AuthController(
            ISessionService sessionService,
            IAppSetting appSetting,
            ICookieService cookieService,
            IAuthService authService)
            : base(sessionService, appSetting)
        {
            _authService = authService;
            _cookieService = cookieService;
        }

        [ChildActionOnly]
        [HttpGet]
        public ActionResult AuthenticationForm()
        {
            if (SessionService.UserSession == null)
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
        public ActionResult SignIn(UserLoginParam userParam)
        {
            if (!Request.IsAjaxRequest())
            {
                return Json(GetErrorResult(Lang.ErrAuthorized));
            }

            var result = new ResultInfo();
            if (ModelState.IsValid)
            {
                if (IsValidCaptcha(userParam.ValidationKey))
                {
                    var clientIpAddress = Request.UserHostAddress;
                    userParam.LoginIp = clientIpAddress;
                    userParam.Language = SessionService.CurrentLanguage;

                    var loginResult = _authService.GetLoginResult(userParam, false);
                    result = loginResult.Result;

                    if (result.IsOk)
                    {
                        var user = loginResult.UserInfo;
                        if (!AppSetting.AllowedAffiliateTypes.Any(t => t.Equals(user.AffType)))
                        {
                            return Json(GetErrorResult(Lang.NotAllowOnDomain));
                        }

                        var onUserIdParam = GetOnUserIdParam(user.AffId, clientIpAddress);
                        user.OnUserId = _authService.GetOnUserId(onUserIdParam);

                        SessionService.AssignSessions(user);
                        FormsAuthentication.SetAuthCookie(user.UserName, false);
                    }
                }
                else
                {
                    result.Message = Lang.ValidationCode;
                }
            }
            else
            {
                result.Message = string.Join(", ", ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage));
            }

            return Json(result);
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

                    if (loginResult.Result.IsOk && AppSetting.AllowedAffiliateTypes.Any(t => t.Equals(user.AffType)))
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
        [OutputCache(NoStore = true, Duration = 0, Location = OutputCacheLocation.None, VaryByParam = "*")]
        [CustomAuthorize]
        public ActionResult ReCheckin()
        {
            var onUserId = SessionService.UserSession.OnUserId;
            var isKickOut = onUserId > 0 && _authService.Recheckin(SessionService.UserSession.OnUserId, SessionService.IsInternal);

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
                ServerId = AppSetting.ServerId,
                Ip = clientIpAddress,
                NetSessionId = HttpContext.Session.SessionID,
                SubAccId = adminId,
                IsInternal = SessionService.IsInternal
            };
        }

        private bool IsValidCaptcha(string validationKey)
        {
            return validationKey.Equals(SessionService.CaptchaCode);
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