namespace Fanex.Affiliate.User.Core.Controllers
{
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

        public ContactUsController(
            ISessionService sessionService,
            IAppSetting appSetting,
            IContactService contactService,
            IAccountRepository accountRepository)
            : base(sessionService, appSetting)
        {
            _contactService = contactService;
            _accountRepository = accountRepository;
        }

        [HttpGet]
        public ActionResult Index()
        {
            if (SessionService.UserSession == null)
            {
                return View();
            }

            var userInfo = _accountRepository.GetUserInfoById(SessionService.UserSession.AffId, SessionService.IsInternal);

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
            else if (!string.Equals(SessionService.CaptchaCode, param.Captcha))
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
                    MailTo = AppSetting.MailContact,
                    MailCc = AppSetting.MailCc
                };

                result = _contactService.SendRequest(param, emailParam);
            }

            return Json(result);
        }
    }
}