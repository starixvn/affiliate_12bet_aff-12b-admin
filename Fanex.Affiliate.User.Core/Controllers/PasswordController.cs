namespace Fanex.Affiliate.User.Core.Controllers
{
    using System.Linq;
    using System.Web.Mvc;
    using BusinessService;
    using Constants;
    using CoreService;
    using DTO;
    using Filters;
    using MetalModels;
    using MetalModels.Email;
    using Repository.UserPanel;
    using Resources;
    using Utilities;

    public class PasswordController : BaseController
    {
        private readonly IAccountRepository _accountRepository;
        private readonly ICookieService _cookieService;
        private readonly ICryptographyService _cryptographyService;
        private readonly IEmailService _mailService;
        private readonly ILanguageService _languageService;

        public PasswordController(
            ISessionService sessionService,
            IAppSetting appSetting,
            IAccountRepository accountRepository,
            ICookieService cookieService,
            ICryptographyService cryptographyService,
            ILanguageService languageService,
            IEmailService mailService)
            : base(sessionService, appSetting)
        {
            _accountRepository = accountRepository;
            _cookieService = cookieService;
            _cryptographyService = cryptographyService;
            _languageService = languageService;
            _mailService = mailService;
        }

        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Forgot(string email)
        {
            if (string.IsNullOrEmpty(email))
            {
                TempData[TempDataKey.SendForgotPasswordEmailResult] = new ResultInfo
                {
                    IsOk = false,
                    Message = Lang.ErrEmailRequire
                };

                return RedirectToAction("Index");
            }

            var code = Generator.GetRandomString(10);
            var param = new UserInfoByEmailParam
            {
                Email = email.Trim(),
                Ip = RequestHelper.GetClientIPAddress(),
                ValidCode = code
            };

            var result = new ResultInfo
            {
                IsOk = true,
                Message = $"{Lang.Yourlogindetail}<br>{Lang.Pleasecheckyour}<br>{Lang.Foranyreasonyou.Replace("{0}", Url.Action("Index", "ContactUs"))}"
            };
            var userInfo = _accountRepository.GetUserInfoByEmail(param);

            if (param.ErrCode != 0)
            {
                result.IsOk = false;
                result.Message = Common.GetErrorMessage("ForgotPassword", param.ErrCode)
                                    .Replace("{0}", Url.Action("Index", "ContactUs"));
            }
            else
            {
                var recoverEmailParam = new RecoverPasswordEmailParam
                {
                    User = userInfo,
                    MailContact = AppSetting.MailContact,
                    MailTo = param.Email,
                    RootDomain = SiteDomainHelper.GetRootUrl(this),
                    Code = code
                };

                _languageService.SetCulture(userInfo.PreferredLanguage ?? SystemLanguage.DefaultLanguageCode);
                _mailService.SendRecoverPasswordEmail(recoverEmailParam);
                _languageService.SetCulture(SessionService.CurrentLanguage);
            }

            TempData[TempDataKey.SendForgotPasswordEmailResult] = result;

            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult RecoverPassword(int uid, string vid)
        {
            var newPassword = Generator.GetRandomString(10);
            var param = new PasswordRecoveryParam
            {
                AffId = uid,
                ValidCode = vid,
                NewPassword = CryptographyHelper.Encrypt(newPassword)
            };

            var userInfo = _accountRepository.RecoverPassword(param);
            var result = new ResultInfo
            {
                IsOk = true,
                Message = $"{Lang.Yourlogindetail}<br>{Lang.Pleasecheckyour}<br>{Lang.Foranyreasonyou.Replace("{0}", Url.Action("Index", "ContactUs"))}"
            };

            if (param.ErrCode != 0)
            {
                result.IsOk = false;
                result.Message = Common.GetErrorMessage("RecoverPassword", param.ErrCode)
                                    .Replace("{0}", Url.Action("Index", "ContactUs"));
            }
            else
            {
                var recoverEmailParam = new NewPasswordEmailParam
                {
                    User = userInfo,
                    RootDomain = SiteDomainHelper.GetRootUrl(this),
                    NewPassword = newPassword,
                    MailTo = userInfo.Email,
                    MailContact = AppSetting.MailContact
                };

                _languageService.SetCulture(userInfo.PreferredLanguage ?? SystemLanguage.DefaultLanguageCode);
                _mailService.SendNewPasswordEmail(recoverEmailParam);
                _languageService.SetCulture(SessionService.CurrentLanguage);
            }

            TempData[TempDataKey.SendForgotPasswordEmailResult] = result;

            return RedirectToAction("Index");
        }

        [HttpGet]
        [Authenticate]
        public ActionResult Update()
        {
            var keyString = Generator.GetRandomString(32);
            var ivString = Generator.GetRandomString(16);

            ViewBag.KeyString = keyString;
            ViewBag.IVString = ivString;

            _cookieService.SetCookie(CookieName.EncryptionKey, keyString);
            _cookieService.SetCookie(CookieName.InitializationVector, ivString);

            return View();
        }

        [HttpPost]
        [Authenticate]
        [ValidateAntiForgeryToken]
        public ActionResult Update(UpdatePasswordParam param)
        {
            const string ResultKey = "UpdatePasswordResult";
            var resultInfo = new ResultInfo { IsOk = false };

            if (SessionService.IsInternal)
            {
                resultInfo.Message = Lang.ErrAuthorized;
            }
            else if (ModelState.IsValid)
            {
                param.AffId = SessionService.UserSession.AffId;
                param.Ip = RequestHelper.GetClientIPAddress();
                param.Language = SessionService.CurrentLanguage;
                param.OldPassword = _cryptographyService.EncryptPassword(param.OldPassword);
                param.NewPassword = _cryptographyService.EncryptPassword(param.NewPassword);

                var updateResult = _accountRepository.UpdatePassword(param);
                resultInfo.IsOk = updateResult.IsOk;
                resultInfo.Message = updateResult.IsOk ? Lang.Passwordchanged : updateResult.Message;
            }
            else
            {
                resultInfo.Message = string.Join("<br />", ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage));
            }

            TempData[ResultKey] = resultInfo;

            return RedirectToAction("Update");
        }
    }
}