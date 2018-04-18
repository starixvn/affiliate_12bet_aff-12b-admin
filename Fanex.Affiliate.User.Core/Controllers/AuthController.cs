using System.Linq;
using System.Web.Mvc;
using System.Web.Security;
using Fanex.Affiliate.User.Core.Constants;
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
                userParam.Language = SessionService.CurrentLanguage;

                var user = _authRepository.GetUserInfo(userParam);

                result.IsOk = userParam.ErrorCode == 0;
                if (result.IsOk)
                {
                    string encryptedPassword = EncryptPassword(userParam);

                    var loginResultParam = new LoginResultParam
                    {
                        AffId = user.AffId,
                        ServerId = AppSetting.ServerId,
                        LoginIp = clientIpAddress,
                        SessionId = HttpContext.Session.SessionID,
                        Language = SessionService.CurrentLanguage,
                        IsSuccessful = encryptedPassword.Equals(user.Password)
                    };

                    _authRepository.UpdateLoginResult(loginResultParam);
                    if (loginResultParam.ErrorCode != 0)
                    {
                        result.IsOk = false;
                        result.Message = loginResultParam.ErrorMessage;
                        return Json(result);
                    }

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

                    CookiesHelper.RemoveCookie(CookieName.EncryptionKey);
                    CookiesHelper.RemoveCookie(CookieName.InitializationVector);
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
            CookiesHelper.ClearAllCookies();
            Session.Clear();
            FormsAuthentication.SignOut();

            return RedirectToAction("Index", "Home");
        }

        private static string EncryptPassword(UserLoginParam userParam)
        {
            var encryptionKey = CookiesHelper.GetCookies(CookieName.EncryptionKey);
            var iv = CookiesHelper.GetCookies(CookieName.InitializationVector);

            var plainPassword = CryptographyHelper.DecryptWithAES(userParam.Password, encryptionKey, iv);

            return CryptographyHelper.Encrypt(plainPassword);
        }
    }
}