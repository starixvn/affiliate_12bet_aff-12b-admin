namespace Fanex.Affiliate.User.Core.Controllers
{
    using System;
    using System.Linq;
    using System.Web.Mvc;
    using BusinessService;
    using CoreService;
    using DTO;
    using MetalModels;
    using MetalModels.Email;
    using Resources;
    using Utilities;

    public class SignupController : BaseController
    {
        private readonly IAccountService _accountService;

        public SignupController(
            ISessionService sessionService,
            IAppSetting appSetting,
            IAccountService accountService) : base(sessionService, appSetting)
        {
            _accountService = accountService;
        }

        [HttpGet]
        public ActionResult Index()
        {
            return View(new UserSignUpParam());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(UserSignUpParam param)
        {
            var result = new ResultInfo { IsOk = false };
            if (!SessionService.CaptchaCode.Equals(param.Captcha))
            {
                result.Message = Lang.ValidationCode;
            }
            else if (ModelState.IsValid)
            {
                var signUpParam = new UserSignUpParam
                {
                    FirstName = Server.HtmlEncode(param.FirstName),
                    LastName = Server.HtmlEncode(param.LastName),
                    Email = Server.HtmlEncode(param.Email),
                    Phone = Server.HtmlEncode(param.Phone),
                    MobilePhone = Server.HtmlEncode(param.MobilePhone),
                    Address = Server.HtmlEncode(param.Address),
                    City = Server.HtmlEncode(param.City),
                    Postal = Server.HtmlEncode(param.Postal),
                    Country = Server.HtmlEncode(param.Country),
                    SiteUrl = Server.HtmlEncode(param.SiteUrl),
                    MoreSiteUrls = Server.HtmlEncode(param.MoreSiteUrls),
                    NoSiteDesc = Server.HtmlEncode(param.NoSiteDesc),
                    PreferredLanguage = Server.HtmlEncode(param.PreferredLanguage),
                    Question = Server.HtmlEncode(param.Question),
                    Answer = Server.HtmlEncode(param.Answer),
                    Player_UserName = Server.HtmlEncode(param.Player_UserName),
                    Gender = param.Gender,
                    State = param.State,
                    Language = SessionService.CurrentLanguage,
                    EmailFormat = false,
                    AffType = Convert.ToByte(AppSetting.AllowedAffiliateTypes.FirstOrDefault()),
                    Ip = Request.UserHostAddress,
                    IsLocal = AppSetting.IsLocal
                };

                var emailParam = new EmailBaseParam
                {
                    MailCc = AppSetting.MailCc,
                    MailContact = AppSetting.MailContact,
                    MailBcc = AppSetting.MailBcc,
                    RootDomain = SiteDomainHelper.GetRootUrl(this),
                    MailTo = AppSetting.MailContact
                };

                result = _accountService.CreateAffiliate(signUpParam, emailParam);
            }

            return Json(result);
        }
    }
}