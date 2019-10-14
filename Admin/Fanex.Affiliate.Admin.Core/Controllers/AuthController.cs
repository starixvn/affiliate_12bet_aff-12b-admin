using System.Web.Mvc;
using Fanex.Affiliate.Admin.Core.Repository;
using Fanex.Affiliate.Admin.Core.Resources;
using Fanex.Affiliate.Admin.Core.Services.Common;
using ONEbook.UM.Filters;
using Fanex.Affiliate.Admin.Core.Extensions;
using Fanex.Affiliate.Admin.Core.MetaModels.Login;
using Fanex.Affiliate.Admin.Core.Attributes;
using Fanex.Affiliate.Admin.Core.DTO.Common;
using System.Web.Security;
using System.Web;
using System;
using System.Collections.Generic;
using Fanex.Affiliate.Admin.Core.Utilities;
using System.Linq;
using Fanex.Affiliate.Admin.Core.Configurations;

namespace Fanex.Affiliate.Admin.Core.Controllers
{
    [CheckUMFilter]
    public class AuthController : Controller
    {
        private readonly IAuthRepository _authRepository;
        private readonly ISessionService _sessionService;
        private readonly IAppSetting _appSetting;

        public AuthController(
            ISessionService sessionService,
            IAuthRepository authRepository,
            IAppSetting appSetting)
        {
            _sessionService = sessionService;
            _authRepository = authRepository;
            _appSetting = appSetting;
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
                isKickedOut = _authRepository.ReCheckin(_sessionService.OnUserId);
            }
            return Json(new { IsKickedOut = isKickedOut, Message = isKickedOut ? Lang.MsgKickout : string.Empty }, JsonRequestBehavior.AllowGet);
        }

        [ValidateAntiForgeryHeader]
        [HttpPost]
        public ActionResult Login(LoginParam param)
        {
            param.UserHostName = HttpContext.Request.UserHostName;
            param.Ip = Request.UserHostAddress;

            var result = _authRepository.Login(param);

            try
            {
                var permissionList = param.UserId > 0 ? _authRepository.GetPermissionList(param.UserId) : new List<UserPermission>();

                if (result.IsSuccessful && param.UserId > 0)
                {
                    if (permissionList.Any())
                    {
                        var authTicket = new FormsAuthenticationTicket(param.UserName, true, 20);
                        var encryptedTicket = FormsAuthentication.Encrypt(authTicket);
                        var cookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket)
                        {
                            HttpOnly = true,
                            Secure = FormsAuthentication.RequireSSL,
                            Path = FormsAuthentication.FormsCookiePath,
                            Domain = FormsAuthentication.CookieDomain,
                            Expires = authTicket.Expiration
                        };

                        _sessionService.UserId = param.UserId;
                        _sessionService.UserPermissionList = permissionList;
                        var onUserId = _authRepository.LoginOnUser(param, HttpContext.Session.SessionID, _appSetting.ServerId);
                        _sessionService.OnUserId = onUserId;

                        HttpContext.Response.SetCookie(cookie);

                        result.Message = "OK";
                    }
                    else
                    {
                        result.Message = "Your account is not configured to access any functions! Please contact your administrator";
                    }
                }
            }
            catch (Exception ex)
            {
                LogHelper.WriteError(ex, param.UserName);

                result.Message = "UM"; //Common.IsUnderMaintenance() ? "UM" : ex.Message;
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
            context.Response.ContentType = "text/html";
            _authRepository.Logout(_sessionService.OnUserId);
            FormsAuthentication.SignOut();
            context.Session.Clear();
            context.Session.Abandon();

            FormsAuthentication.RedirectToLoginPage();

            return new EmptyResult();
        }
    }
}
