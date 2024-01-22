using Fanex.Affiliate.User.Core.BusinessService;
using Fanex.Affiliate.User.Core.CoreService;
using Fanex.Affiliate.User.Core.DTO;
using Fanex.Affiliate.User.Core.DTO.VerificationAccount;
using Fanex.Affiliate.User.Core.Filters;
using Fanex.Affiliate.User.Core.MetalModels;
using Fanex.Affiliate.User.Core.Resources;
using Fanex.Affiliate.User.Core.Utilities;
using Fanex.Affiliate.User.Core.ViewModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using System.Web.Mvc;
using System.Web.UI;

namespace Fanex.Affiliate.User.Core.Controllers
{
    public class AuthController : AuthBaseController
    {
        private const int CaptchaImgWidth = 65;
        private const int CaptchaImgHeight = 26;

        public AuthController(
            ISessionService sessionService,
            IAppSetting appSetting,
            ISystemService languageService,
            IAuthService authService,
            ICaptchaService captchaService) : base(sessionService, languageService, appSetting, captchaService, authService)

        {
        }

        [ChildActionOnly]
        [HttpGet]
        [AllowAnonymous]
        public ActionResult AuthenticationForm()
        {
            var viewModel = new AuthenticationFormViewModel
            {
                IsAuthenticated = User.Identity.IsAuthenticated && SessionService.UserSession != null
            };

            if (viewModel.IsAuthenticated)
            {
                viewModel.UserName = SessionService.UserSession.UserName;
            }
            else
            {
                viewModel.PublicKey = AppSetting.RsaPublicKey;
            }

            return View("~/Views/Auth/AuthenticationForm.cshtml", viewModel);
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        [AllowAnonymous]
        public ActionResult SignIn([Bind(Include = "LoginInfo,ValidationKey")] UserLoginParam userParam)
        {
            if (SessionService.VerificationSession != null)
            {
                return Json(new LoginResultInfo { IsOk = false, ShouldVerifyAccount = true });
            }

            var requestValidation = ValidateLoginRequest(userParam.ValidationKey, SessionService.CaptchaCode);
            if (!requestValidation.IsOk)
            {
                return Json(requestValidation);
            }

            userParam.LoginIp = RequestHelper.GetClientIPAddress(Request);
            userParam.Language = SessionService.CurrentLanguage;
            userParam.UserAgent = Request.UserAgent;
            userParam.IsVerifyingAccount = false;
            userParam.LoginInfo = Uri.UnescapeDataString(userParam.LoginInfo);
            var loginResult = AuthService.GetLoginResult(userParam, false);

            if (loginResult.Result.ShouldVerifyAccount)
            {
                SessionService.VerificationSession = new VerificationSession { AffId = loginResult.UserInfo.AffId };

                return Json(loginResult.Result);
            }

            var userInfo = loginResult.UserInfo;
            if (userInfo != null)
            {
                if (!IsValidDomain(userInfo.AffType))
                {
                    return Json(GetErrorResult(Lang.NotAllowOnDomain));
                }

                if (loginResult.Result.IsOk)
                {
                    AssignLoginInfoAndSetAuth(userInfo, userParam.LoginIp);
                }
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
        public ActionResult AdminLogin(InternalLoginModel loginParam)
        {
            if (ModelState.IsValid)
            {
                var admin = AuthService.GetInternalLoginInfo(loginParam.AccOnId);
                var hashedPassword = Generator.GenerateSHA512String(admin?.SesionId + loginParam.ChalengeStr);
                if (admin != null && string.Equals(loginParam.AccKey, hashedPassword))
                {
                    var internalClaims = AuthService.GetInternalLoginClaim(admin.UserId);
                    var clientIpAddress = RequestHelper.GetClientIPAddress(Request);
                    var userLoginParam = new UserLoginParam
                    {
                        UserName = loginParam.CustName,
                        LoginIp = clientIpAddress,
                        Language = SessionService.CurrentLanguage,
                        UserAgent = Request.UserAgent,
                        IsVerifyingAccount = false
                    };

                    SessionService.IsInternal = true;
                    var loginResult = AuthService.GetLoginResult(userLoginParam, true);
                    var user = loginResult.UserInfo;

                    var isValidUser = user != null && IsValidDomain(user.AffType);
                    if (loginResult.Result.IsOk && isValidUser)
                    {
                        user.UserId = admin.UserId;
                        var adminId = admin.UserId * -1;
                        AssignLoginInfoAndSetAuth(user, clientIpAddress, adminId);
                        AssignClaimForInternalLogin(internalClaims);
                    }
                    else
                    {
                        var message = new StringBuilder().AppendLine($"Internal Login on AccOnId {loginParam.AccOnId} was failed: {JsonConvert.SerializeObject(loginResult)}");
                        message.AppendLine($"Admin: {JsonConvert.SerializeObject(admin)}");
                        message.AppendLine($"UserLoginParam: {JsonConvert.SerializeObject(userLoginParam)}");
                        message.AppendLine($"IsValidDomain: {isValidUser}");
                        message.AppendLine($"AllowedAffiliateTypes: {JsonConvert.SerializeObject(AppSetting.AllowedAffiliateTypes)}");

                        LogHelper.WriteInfo(message.ToString());

                        return Redirect(Url.Content("~/Notify/PageNotFound"));
                    }
                }
                else
                {
                    var message = new StringBuilder().AppendLine($"Compare Internal Login on AccOnId {loginParam.AccOnId} was failed: {JsonConvert.SerializeObject(admin)}");
                    message.AppendLine($"AccKey: {loginParam.AccKey}");
                    message.AppendLine($"HashedPassword: {hashedPassword}");

                    LogHelper.WriteInfo(message.ToString());
                    return Redirect(Url.Content("~/Notify/PageNotFound"));
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
            var isKickOut = onUserId > 0 && AuthService.Recheckin(SessionService.UserSession.OnUserId,
                SessionService.IsInternal);

            if (isKickOut)
            {
                DoSignOut();

                return Json(new
                {
                    IsKickOut = true,
                    Message = Lang.Msgkickout
                }, JsonRequestBehavior.AllowGet);
            }

            return Json(new
            {
                IsKickOut = false,
                IsShowFeedback = IsValidAffToShowFeedback(),
                Message = string.Empty
            }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        [OutputCache(NoStore = true, Duration = 0, Location = OutputCacheLocation.None, VaryByParam = "*")]
        public ActionResult CaptchaImage(byte type = 0)
        {
            var captcha = CaptchaService.GenerateCaptcha();
            if (type == 0)
            {
                SessionService.CaptchaCode = captcha;
            }
            else
            {
                SessionService.SignUpCaptchaCode = captcha;
            }

            return File(CaptchaService.GenerateImageBuffer(captcha, CaptchaImgWidth, CaptchaImgHeight), "image/png");
        }

        private void DoSignOut()
        {
            Session.Clear();
            AuthService.SignOut();
        }

        private void AssignClaimForInternalLogin(IEnumerable<Claim> internalClaims)
        {
            var claimIdentites = new List<ClaimsIdentity>
            {
                new ClaimsIdentity(internalClaims)
            };

            SessionService.ClaimsIdentities = claimIdentites;
        }

        private bool IsValidAffToShowFeedback()
        {
            return !SessionService.IsOpenedFeedback && !AuthService.IsValidIdleTime(SessionService.LastestRequestTime) && AuthService.IsValidAff(SessionService.UserSession.AffId);
        }
    }
}