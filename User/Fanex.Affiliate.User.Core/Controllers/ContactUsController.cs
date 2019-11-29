namespace Fanex.Affiliate.User.Core.Controllers
{
    using System.Linq;
    using System.Web.Mvc;
    using BusinessService;
    using CoreService;
    using DTO;
    using MetalModels;
    using MetalModels.Email;
    using Repository.UserPanel;
    using Resources;
    using Utilities;


    public class ContactUsController : BaseController
    {
        private readonly IContactService _contactService;

        private readonly IAccountRepository _accountRepository;

        private readonly ICaptchaService _captchaService;
        private readonly IAppSetting _appSetting;
        public ContactUsController(
            ISessionService sessionService,
            IAppSetting appSetting,
            ILanguageService languageService,
            IContactService contactService,
            IAccountRepository accountRepository,
            ICaptchaService captchaService)
            : base(sessionService, languageService)
        {
            _contactService = contactService;
            _accountRepository = accountRepository;
            _captchaService = captchaService;
            _appSetting = appSetting;
        }

        [HttpGet]
        public ActionResult Index()
        {
            if (SessionService.UserSession == null)
            {
                return View();
            }

            var model = _accountRepository.GetUserInfoById(SessionService.UserSession.AffId, SessionService.IsInternal);

            var userInfo = new UserInfo
            {
                AffId = model.AffId,
                FirstName = Server.HtmlDecode(model.FirstName),
                LastName = Server.HtmlDecode(model.LastName),
                Email = Server.HtmlDecode(model.Email),
                MailOption = model.MailOption,
                PreferredLanguage = model.PreferredLanguage,
                SecretKey = model.SecretKey,
                UserName = model.UserName
            };

            return View(userInfo);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(ContactUsParam param)
        {
            var result = new ResultInfo { IsOk = false };

            if (SessionService != null && SessionService.IsInternal)
            {
                result.Message = Lang.ErrAuthorized;
            }
            else if (!_captchaService.IsValid(param.Captcha, SessionService.SignUpCaptchaCode, Request.UserHostAddress))
            {
                result.Message = Lang.ValidationCode;
            }
            else if (ModelState.IsValid)
            {
                param.Sender = Server.HtmlEncode(param.Sender);
                param.Email = Server.HtmlEncode(param.Email);
                param.Affiliate = Server.HtmlEncode(param.Affiliate);
                param.Subject = Server.HtmlEncode(param.Subject);
                param.Content = Server.HtmlEncode(param.Content);
                param.Creator = SessionService.UserSession == null ? -1 : SessionService.UserSession.AffId;
                param.Ip = Request.UserHostAddress;
                param.SessionId = Session.SessionID;
                param.Browser = Request.Browser.Browser;

                var emailParam = new EmailBaseParam
                {
                    MailTo = _appSetting.MailContact,
                    MailCc = _appSetting.MailCc
                };

                result = _contactService.SendRequest(param, emailParam);
            }
            else
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage);
                result.Message = string.Join("<br>", errors);
            }

            return Json(result);
        }
    }
}