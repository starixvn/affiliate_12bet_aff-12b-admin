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
        const int InvalidUrlAddress = 1001;
        const int RecoverPasswordFailed = 1002;
        private readonly IAccountRepository _accountRepository;
        private readonly ICryptographyService _cryptographyService;
        private readonly IEmailService _mailService;
        private readonly IAppSetting _appSetting;

        public PasswordController(
            ISessionService sessionService,
            IAppSetting appSetting,
            IAccountRepository accountRepository,
            ICryptographyService cryptographyService,
            ILanguageService languageService,
            IEmailService mailService)
            : base(sessionService, languageService)
        {
            _accountRepository = accountRepository;
            _cryptographyService = cryptographyService;
            _mailService = mailService;
            _appSetting = appSetting;
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
                return Json(new ResultInfo
                {
                    IsOk = false,
                    Message = Lang.ErrEmailRequire
                });
            }

            var code = Generator.GetRandomString(10);
            var param = new UserInfoByEmailParam
            {
                Email = email.Trim(),
                Ip = RequestHelper.GetClientIPAddress(),
                ValidCode = code,
                Language = SessionService.CurrentLanguage
            };

            var contactUsForAnyReasonText = Lang.Foranyreasonyou.Replace("{0}", Url.Content($"~/{ControllerName.ContactUs}"));
            var result = new ResultInfo
            {
                IsOk = true,
                Message = $"{Lang.Yourlogindetail}<br>{Lang.Pleasecheckyour}<br>{contactUsForAnyReasonText}"
            };
            var userInfo = _accountRepository.GetUserInfoByEmail(param);

            if (param.ErrCode != 0)
            {
                result.IsOk = false;
                result.Message = Common.GetErrorMessage("ForgotPassword", param.ErrCode)
                                    .Replace("{0}", Url.Content($"~/{ControllerName.ContactUs}"));
            }
            else
            {
                var recoverEmailParam = new RecoverPasswordEmailParam
                {
                    User = userInfo,
                    MailContact = _appSetting.MailContact,
                    MailTo = param.Email,
                    RootDomain = SiteDomainHelper.GetRootUrl(this),
                    Code = code
                };

                var userLang = SystemLanguage.IsValid(userInfo.PreferredLanguage) ? userInfo.PreferredLanguage : SystemLanguage.DefaultLanguageCode;
                LanguageService.SetCulture(userLang);
                _mailService.SendRecoverPasswordEmail(recoverEmailParam);
                LanguageService.SetCulture(SessionService.CurrentLanguage);
            }

            return Json(result);
        }

        [HttpGet]
        public ActionResult RecoverPassword(string vid, int uid = 0)
        {
            if (uid == 0 || string.IsNullOrEmpty(vid))
            {
                return Redirect(Url.Content("~/Password/RecoverPasswordResult?isSuccessful=false"));
            }
            var newPassword = Generator.GetRandomString(10);
            var param = new PasswordRecoveryParam
            {
                AffId = uid,
                ValidCode = vid,
                NewPassword = CryptographyHelper.Encrypt(newPassword),
                Ip = Request.UserHostAddress,
                Language = SessionService.CurrentLanguage
            };

            var userInfo = _accountRepository.RecoverPassword(param);

            var isSuccessful = param.ErrCode == 0;

            if (isSuccessful)
            {
                var recoverEmailParam = new NewPasswordEmailParam
                {
                    User = userInfo,
                    RootDomain = SiteDomainHelper.GetRootUrl(this),
                    NewPassword = newPassword,
                    MailTo = userInfo.Email,
                    MailContact = _appSetting.MailContact
                };

                var userLang = SystemLanguage.IsValid(userInfo.PreferredLanguage) ? userInfo.PreferredLanguage : SystemLanguage.DefaultLanguageCode;
                LanguageService.SetCulture(userLang);
                _mailService.SendNewPasswordEmail(recoverEmailParam);
                LanguageService.SetCulture(SessionService.CurrentLanguage);

                return Redirect(Url.Content("~/Password/RecoverPasswordResult?isSuccessful=true"));
            }

            return Redirect(Url.Content($"~/Password/RecoverPasswordResult?isSuccessful={isSuccessful}&errCode={param.ErrCode}"));
        }

        public ActionResult RecoverPasswordResult(bool isSuccessful = false, int errCode = 0)
        {
            var result = new ResultInfo
            {
                IsOk = false,
                Message = Common.GetErrorMessage("RecoverPassword", errCode == RecoverPasswordFailed ? RecoverPasswordFailed : InvalidUrlAddress)
                                    .Replace("{0}", Url.Content("~/ContactUs"))
            };

            if (isSuccessful)
            {
                result.IsOk = true;
                result.Message = $"{Lang.Yourlogindetail}<br>{Lang.Pleasecheckyour}<br>{Lang.Foranyreasonyou.Replace("{0}", Url.Content("~/ContactUs"))}";
            }

            return View(result);
        }

        [CustomAuthorize]
        [HttpGet]
        public ActionResult Update()
        {
            ViewBag.PublicKey = _appSetting.RsaPublicKey;

            return View(new UpdatePasswordParam());
        }

        [CustomAuthorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update(UpdatePasswordParam param)
        {
            var resultInfo = new ResultInfo { IsOk = false };

            if (SessionService.IsInternal)
            {
                resultInfo.Message = Lang.ErrAuthorized;
            }
            else if (ModelState.IsValid)
            {
                param.NewPassword = _cryptographyService.EncryptPassword(_appSetting.RsaPrivateKey, param.NewPassword);
                var confirmedNewPassword = _cryptographyService.EncryptPassword(_appSetting.RsaPrivateKey, param.ConfirmNewPassword);

                if (param.NewPassword != confirmedNewPassword)
                {
                    return Json(new ResultInfo
                    {
                        IsOk = false,
                        Message = Lang.ReenterNewPassw
                    });
                }

                param.OldPassword = _cryptographyService.EncryptPassword(_appSetting.RsaPrivateKey, param.OldPassword);

                param.AffId = SessionService.UserSession.AffId;
                param.Ip = RequestHelper.GetClientIPAddress();
                param.Language = SessionService.CurrentLanguage;

                var updateResult = _accountRepository.UpdatePassword(param);
                resultInfo.IsOk = updateResult.IsOk;
                resultInfo.Message = updateResult.IsOk ? Lang.Passwordchanged : updateResult.Message;
            }
            else
            {
                resultInfo.Message = string.Join("<br>", ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage));
            }

            return Json(resultInfo);
        }
    }
}