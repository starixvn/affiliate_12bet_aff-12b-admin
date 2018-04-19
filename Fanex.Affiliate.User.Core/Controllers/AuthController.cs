using System.Linq;
using System.Web.Mvc;
using System.Web.Security;
using Fanex.Affiliate.User.Core.Constants;
using Fanex.Affiliate.User.Core.CoreService;
using Fanex.Affiliate.User.Core.DTO;
using Fanex.Affiliate.User.Core.DTO.Auth;
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
            if (!Request.IsAjaxRequest())
            {
                return Json(GetErrorResult(Lang.ErrAuthorized));
            }

            var result = GetParamValidationResult(userParam);
            if (result.IsOk)
            {
                var clientIpAddress = RequestHelper.GetClientIPAddress();
                userParam.ServerId = AppSetting.ServerId;
                userParam.LoginIP = clientIpAddress;
                userParam.Language = SessionService.CurrentLanguage;

                var user = _authRepository.GetUserInfo(userParam);

                result.IsOk = userParam.ErrorCode == 0;
                if (result.IsOk)
                {
                    string encryptedPassword = EncryptPassword(userParam.Password);

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
                        return Json(GetErrorResult(loginResultParam.ErrorMessage));
                    }

                    if (!AppSetting.AllowedAffiliateTypes.Any(t => t.Equals(user.AffType.ToString())))
                    {
                        return Json(GetErrorResult(Lang.NotAllowOnDomain));
                    }

                    AssignUserSession(clientIpAddress, user);
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

        private void AssignUserSession(string clientIpAddress, AuthorizedUserInfo user)
        {
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
        }

        private static string EncryptPassword(string inputPassword)
        {
            var encryptionKey = CookiesHelper.GetCookies(CookieName.EncryptionKey);
            var iv = CookiesHelper.GetCookies(CookieName.InitializationVector);

            var plainPassword = CryptographyHelper.DecryptWithAES(inputPassword, encryptionKey, iv);

            return CryptographyHelper.Encrypt(plainPassword);
        }

        private JResult GetParamValidationResult(UserLoginParam userLoginParam)
        {
            if (string.IsNullOrEmpty(userLoginParam.UserName))
            {
                return GetErrorResult(Lang.ErrmsgEntryUsername);
            }

            if (string.IsNullOrEmpty(userLoginParam.Password))
            {
                return GetErrorResult(Lang.ErrmsgEntryPassword);
            }

            if (!AppSetting.IsProduction && !userLoginParam.ValidationKey.Equals(SessionService.CaptchaCode))
            {
                return GetErrorResult(Lang.ValidationCode);
            }

            return new JResult { IsOk = true };
        }

        private JResult GetErrorResult(string message)
        {
            return new JResult
            {
                IsOk = false,
                Message = message
            };
        }
    }
}