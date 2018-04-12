using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Fanex.Affiliate.User.Core.CoreService;
using Fanex.Affiliate.User.Core.DTO;
using Fanex.Affiliate.User.Core.MetalModels;
using Fanex.Affiliate.User.Core.Repository;
using Fanex.Affiliate.User.Core.Resources;
using Fanex.Affiliate.User.Core.Utilities;

namespace Fanex.Affiliate.User.Core.Controllers
{
    public class AuthController : BaseController
    {
        private readonly IAuthRepository _authRepository;

        public AuthController(IAuthRepository authRepository, ISessionService sessionService, IAppSetting appSetting)
            : base(sessionService, appSetting)
        {
            _authRepository = authRepository;
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult SignIn(UserLoginParam userParam)
        {
            var result = new JResult
            {
                IsOk = false,
                Message = string.Empty
            };

            if (!Request.IsAjaxRequest())
            {
                result.Message = Lang.ErrAuthorized;
                return Json(result);
            }

            if (string.IsNullOrEmpty(userParam.UserName))
            {
                result.Message = Lang.ErrmsgEntryUsername;
                return Json(result);
            }
            else if (string.IsNullOrEmpty(userParam.Password))
            {
                result.Message = Lang.ErrmsgEntryPassword;
                return Json(result);
            }
            else if (AppSetting.IsProduction && !userParam.ValidationKey.Equals(SessionService.CaptchaCode))
            {
                result.Message = Lang.ValidationCode;
                return Json(result);
            }
            else
            {
                var clientIpAddress = RequestHelper.GetClientIPAddress();
                userParam.ServerId = AppSetting.ServerId;
                userParam.LoginIP = clientIpAddress;

                var password = userParam.Password;
                userParam.Password = CryptographyHelper.Encrypt(password);

                var user = _authRepository.GetUserInfo(userParam);

                result.IsOk = userParam.ErrorCode == 0;
                if (result.IsOk)
                {
                    if (!AppSetting.AllowedAffiliateTypes.Any(t => t.Equals(user.AffType.ToString())))
                    {
                        result.IsOk = false;
                        result.Message = Lang.NotAllowOnDomain;
                        return Json(result);
                    }


                    SessionService.AssignSessions(user);

                    var onUserIdParam = new OnUserIdParam
                    {
                        CustId = user.AffId,
                        ServerId = AppSetting.ServerId,
                        Ip = clientIpAddress,
                        NetSessionId = HttpContext.Session.SessionID
                    };

                    var userSession = SessionService.UserSession;
                    userSession.OnUserId = _authRepository.GetOnUserId(onUserIdParam);
                    userSession.IsInternal = false;
                    FormsAuthentication.SetAuthCookie(user.UserName, false);
                }
                else
                {
                    result.Message = userParam.ErrorMessage;
                }
            }

            return Json(result);
        }

        [HttpGet]
        public ActionResult SignOut()
        {
            ClearCookies();
            Session.Clear();
            FormsAuthentication.SignOut();

            return RedirectToAction("Index", "Home");
        }

        private void ClearCookies()
        {
            var cache = Response.Cache;
            cache.SetNoStore();
            cache.SetExpires(DateTime.Now.AddMinutes(-1));
            cache.SetCacheability(HttpCacheability.NoCache);
            var cookies = Request.Cookies.AllKeys;
            foreach (string cookie in cookies)
            {
                var item = new HttpCookie(cookie)
                {
                    Expires = DateTime.Now.AddDays(-1)
                };
                Response.Cookies.Add(item);
            }
        }
    }
}