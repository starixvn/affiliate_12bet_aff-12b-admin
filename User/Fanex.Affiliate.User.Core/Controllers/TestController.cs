namespace Fanex.Affiliate.User.Core.Controllers
{
    using System;
    using System.Web.Mvc;
    using BusinessService;
    using Constants;
    using CoreService;
    using DTO;
    using Enum;
    using Filters;
    using MetalModels.Email;
    using ONEbook.UM.Filters;
    using Utilities;
    using ViewModels;

    [CheckUMFilter]
    [CustomAuthorize]
    public class TestController : BaseController
    {
        private readonly IMailProcessor _mailProcessor;
        private readonly ILanguageService _languageService;
        private readonly IAppSetting _appSetting;

        private const string MailContact = "walter.tran@starixsoft.com";

        public TestController(ISessionService sessionService,
            ILanguageService languageService, IAppSetting appSetting) : base(sessionService, languageService)
        {
            _mailProcessor = new MailProcessor();
            _languageService = languageService;
            _appSetting = appSetting;
        }

        [HttpGet]
        public ActionResult Index()
        {
            if (!IsValidUser())
            {
                return HttpNotFound();
            }

            return View();
        }

        [HttpGet]
        public ActionResult Preview(TestCriteria criteria)
        {
            if (!IsValidUser())
            {
                return HttpNotFound();
            }

            try
            {
                _languageService.SetCulture(criteria.Language);
                ViewBag.Data = GetEmailContent(criteria);
                _languageService.SetCulture("en-US");
            }
            catch (Exception ex)
            {
                ViewBag.Data = ex.Message + "\n" + ex.StackTrace;
            }

            return View();
        }

        [HttpPost]
        public ActionResult SendEmail(TestCriteria criteria)
        {
            if (!IsValidUser())
            {
                return HttpNotFound();
            }

            var success = true;
            var message = string.Empty;

            try
            {
                var content = GetEmailContent(criteria);
                const string Subject = "Affilite 12Bet testing";

                _mailProcessor.Send(content, criteria.EmailAddress, Subject);
            }
            catch (Exception ex)
            {
                success = false;
                message = ex.Message + "\n" + ex.StackTrace;
            }

            var result = new
            {
                Success = success,
                Message = message
            };

            return Json(result);
        }

        private string GetEmailContent(TestCriteria criteria)
        {
            var content = GetEmailTextContent(criteria);

            return criteria.Format == "txt" ? content : GetEmailHtmlContent(content, criteria.Language);
        }

        private string GetEmailTextContent(TestCriteria criteria)
        {
            var language = criteria.Language;
            var function = criteria.Function;

            var viewPath = string.Format(TemplateString.EmailTemplatePath, function, language);
            var physicalPath = Server.MapPath(viewPath);
            var model = GetModelByFunction(function);

            return model == null ? string.Empty : _mailProcessor.BuildTemplateContent(physicalPath, model);
        }

        private string GetEmailHtmlContent(string content, string language)
        {
            var domain = SiteDomainHelper.GetRootUrl(this);

            var viewModel = new EmailTemplateViewModel
            {
                RootUrl = domain,
                ImageRootUrl = string.Format(TemplateString.EmailImageRootPath, domain),
                Language = language,
                Content = content,
                Title = "This is title"
            };
            var viewPath = Server.MapPath("~/Views/Shared/Email/CommonLayout.cshtml");

            return _mailProcessor.BuildTemplateContent(viewPath, viewModel);
        }

        private object GetModelByFunction(string functionName)
        {
            switch (functionName)
            {
                case "RecoverPassword":
                    return GetForgetPasswordModel();

                case "PaymentCancel":
                    return GetPaymentModel(PaymentRequestType.Cancel);

                case "PaymentRequest":
                    return GetPaymentModel(PaymentRequestType.Create);

                case "NewPassword":
                    return GetRecoverPasswordModel();

                case "Signup":
                    return GetSignupModel();

                default:
                    return null;
            }
        }

        private object GetForgetPasswordModel()
        {
            return new RecoverPasswordEmailParam
            {
                User = GetUserInfo(),
                MailContact = MailContact,
                Code = "12345",
                RootDomain = SiteDomainHelper.GetRootUrl(this),
                MailTo = MailContact
            };
        }

        private object GetPaymentModel(PaymentRequestType type)
        {
            const long TranId = 1L;
            const decimal Amount = 989;

            return new PaymentEmailParam
            {
                User = GetUserInfo(),
                MailContact = MailContact,
                TransId = TranId,
                RequestDate = DateTime.Now.Date,
                Amount = Amount,
                RootDomain = SiteDomainHelper.GetRootUrl(this),
                MailTo = MailContact,
                PaymentType = type
            };
        }

        private object GetRecoverPasswordModel()
        {
            return new NewPasswordEmailParam
            {
                User = GetUserInfo(),
                MailContact = "mailcontact@gmail.com",
                NewPassword = "1234aa",
                RootDomain = "http://affiliate.12ying.com/",
                MailTo = MailContact,
            };
        }

        private object GetSignupModel()
        {
            return new EmailBaseParam
            {
                User = GetUserInfo(),
                MailContact = "mailcontact@gmail.com"
            };
        }

        private UserInfo GetUserInfo()
        {
            return new UserInfo
            {
                AffId = 1,
                FirstName = "walter fisrt name",
                UserName = "walter",
                Email = "walter.tran@starixsoft.com",
                LastName = "walter last name",
                MailOption = false,
                PreferredLanguage = "en-US"
            };
        }

        private bool IsValidUser()
        {
            return _appSetting.IsLocal || SessionService.IsInternal;
        }
    }

    public class TestCriteria
    {
        public string Language { get; set; }

        public string Function { get; set; }

        public string Format { get; set; }

        public string EmailAddress { get; set; }
    }
}