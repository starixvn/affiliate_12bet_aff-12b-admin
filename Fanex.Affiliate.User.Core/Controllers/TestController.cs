namespace Fanex.Affiliate.User.Core.Controllers
{
    using System;
    using System.Web.Mvc;
    using Constants;
    using Utilities;

    public class TestController : Controller
    {
        private readonly IMailProcessor _mailProcessor;

        public TestController()
        {
            _mailProcessor = new MailProcessor();
        }

        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Preview(TestCriteria criteria)
        {
            ViewBag.Data = GetEmailContent(criteria);
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
            var language = criteria.Language;
            var function = criteria.Function;

            var viewPath = string.Format(TemplateString.EmailTemplatePath, function, language, criteria.Format);
            var path = Server.MapPath(viewPath);
            var model = GetModelByFunction(function, language);

            return model == null ? string.Empty : _mailProcessor.BuildTemplateContent(path, model);
        }

        private object GetModelByFunction(string functionName, string language)
        {
            switch (functionName)
            {
                case "ForgetPassword":
                    return GetForgetPasswordModel(language);

                case "PaymentCancel":
                case "PaymentRequest":
                    return GetPaymentModel(language);
            }

            return null;
        }

        private object GetForgetPasswordModel(string language)
        {
            return new
            {
                RootUrl = SiteDomainHelper.GetRootUrl(this),
                RecoverPwdURL = Url.Action("Recovery", "Test", null, Request.Url.Scheme),
                CustId = 0,
                ValidationCode = "1234",
                Language = language,
                FirstName = "Walter",
                UserName = "aawalter",
                MailContact = "mailcontact@gmail.com",
                ImageRootUrl = string.Format(TemplateString.EmailImageRootPath, SiteDomainHelper.GetRootUrl(this))
            };
        }

        private object GetPaymentModel(string language)
        {
            return new
            {
                RootUrl = SiteDomainHelper.GetRootUrl(this),
                Language = language,
                FirstName = "Walter",
                MailContact = "mailcontact@gmail.com",
                TransId = 10008751L,
                Date = DateTime.Now.Date.ToString("MM/dd/yyyy"),
                Amount = 989,
                ImageRootUrl = string.Format(TemplateString.EmailImageRootPath, SiteDomainHelper.GetRootUrl(this))
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