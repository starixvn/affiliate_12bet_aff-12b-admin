namespace Fanex.Affiliate.User.Core.Controllers
{
    using System;
    using System.Web.Mvc;
    using BusinessService;
    using Constants;
    using Utilities;
    using ViewModels;

    public class TestController : Controller
    {
        private readonly IMailProcessor _mailProcessor;
        private readonly ILanguageService _languageService;

        public TestController(ILanguageService languageService)
        {
            _mailProcessor = new MailProcessor();
            _languageService = languageService;
        }

        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Preview(TestCriteria criteria)
        {
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
            var success = true;
            var message = string.Empty;

            try
            {
                var content = GetEmailContent(criteria);
                var subject = "Affilite 12Bet testing";

                _mailProcessor.Send(content, criteria.EmailAddress, subject);
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
                case "PaymentRequest":
                    return GetPaymentModel();

                case "NewPassword":
                    return GetRecoverPasswordModel();

                case "Signup":
                    return GetSignupModel();
            }

            return null;
        }

        private object GetForgetPasswordModel()
        {
            var domain = SiteDomainHelper.GetRootUrl(this);

            return new
            {
                RecoverPwdURL = domain + "Test/Recovery",
                CustId = 0,
                ValidationCode = "1234",
                FirstName = "Walter",
                UserName = "aawalter",
                MailContact = "mailcontact@gmail.com"
            };
        }

        private object GetPaymentModel()
        {
            return new
            {
                FirstName = "Walter",
                MailContact = "mailcontact@gmail.com",
                TransId = 10008751L,
                Date = DateTime.Now.Date.ToString("MM/dd/yyyy"),
                Amount = 989
            };
        }

        private object GetRecoverPasswordModel()
        {
            return new
            {
                FirstName = "Walter",
                MailContact = "mailcontact@gmail.com",
                UserName = "12BET1001",
                Password = "1234aa",
                Url = "http://affiliate.12ying.com/"
            };
        }

        private object GetSignupModel()
        {
            return new
            {
                FirstName = "Walter",
                MailContact = "mailcontact@gmail.com"
            };
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