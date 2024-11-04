using Fanex.Affiliate.Admin.Core.Attributes;
using Fanex.Affiliate.Admin.Core.Configurations;
using Fanex.Affiliate.Admin.Core.Constants;
using Fanex.Affiliate.Admin.Core.Extensions;
using Fanex.Affiliate.Admin.Core.MetaModels.Login;
using Fanex.Affiliate.Admin.Core.Resources;
using Fanex.Affiliate.Admin.Core.Services;
using Fanex.Affiliate.Admin.Core.Services.Common;
using ONEbook.UM.AspNetMVC.Filters;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Fanex.Affiliate.Admin.Core.Controllers
{
    [CheckUMFilter]
    public class AuthController : Controller
    {
        private readonly IAuthService _authService;
        private readonly ISessionService _sessionService;
        private readonly IAppSetting _appSetting;
        private readonly ISignInService _signInService;

        public AuthController(
            ISessionService sessionService,
            ISignInService signInService,
            IAuthService authService,
            IAppSetting appSetting)
        {
            _sessionService = sessionService;
            _authService = authService;
            _appSetting = appSetting;
            _signInService = signInService;
        }

        [HttpGet]
        public ActionResult RecheckIn()
        {
            return View("~/Areas/Portal/Views/Auth/RecheckIn.cshtml");
        }

        [HttpGet]
        [OutputCache(NoStore = true, Duration = 0)]
        public ActionResult Ping()
        {
            var isKickedOut = true;
            var isAuthorized = HttpContext.Request.IsAuthenticated && HttpContext.User.Identity.IsAuthenticated;
            if (isAuthorized && _sessionService.OnUserId > 0 && _sessionService.UserId > 0)
            {
                isKickedOut = _signInService.IsKickOut(_sessionService.OnUserId);
            }
            return Json(new { IsKickedOut = isKickedOut, Message = isKickedOut ? Lang.MsgKickout : string.Empty }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ValidateAntiForgeryHeader]
        public ActionResult Login(LoginParam param)
        {
            param.UserHostName = HttpContext.Request.UserHostName;
            param.Ip = Request.GetClientIP();

            var result = _signInService.VerifyAccount(param);

            if (result.IsSuccessful)
            {
                var claims = _authService.GetClaims(param.UserId);

                if (claims.Any())
                {
                    var authTicket = new FormsAuthenticationTicket(param.UserName, true, 20);
                    var encryptedTicket = FormsAuthentication.Encrypt(authTicket);
                    var cookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket)
                    {
                        HttpOnly = true,
                        Secure = FormsAuthentication.RequireSSL,
                        Path = FormsAuthentication.FormsCookiePath,
                        Domain = FormsAuthentication.CookieDomain,
                        Expires = authTicket.Expiration,
                    };

                    _sessionService.UserId = param.UserId;
                    _sessionService.ClaimIdentities = new List<ClaimsIdentity> { new ClaimsIdentity(claims) };
                    var trackId = _signInService.TrackLoginInfo(param, HttpContext.Session.SessionID, _appSetting.ServerId);
                    _sessionService.OnUserId = trackId;

                    HttpContext.ApplicationInstance.Context.Session["UserName"] = param.UserName;

                    HttpContext.Response.SetCookie(cookie);

                    result.Message = "OK";
                }
                else
                {
                    result.Code = Code.GeneralErrorCode;
                    result.Message = "Your account is not configured to access any functions! Please contact your administrator";
                }
            }

            return new JsonNetResult
            {
                Data = result
            };
        }

        [HttpGet]
        public ActionResult Logout()
        {
            var context = HttpContext;
            _signInService.ReleaseTracking(_sessionService.OnUserId);
            FormsAuthentication.SignOut();
            context.Session.Clear();
            context.Session.Abandon();
            return Redirect(FormsAuthentication.LoginUrl);
        }
    }
}
