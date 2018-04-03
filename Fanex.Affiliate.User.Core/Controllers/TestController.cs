namespace Fanex.Affiliate.User.Core.Controllers
{
    using System;
    using System.Web.Mvc;
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
            var viewPath = string.Format("~/Views/Shared/Email/{0}/Content.{1}.{2}", criteria.Function, criteria.Language, criteria.Format);
            var path = Server.MapPath(viewPath);
            dynamic model = new
            {
                RootUrl = SiteDomainHelper.GetRootUrl(this),
                RecoverPwdURL = Url.Action("Recovery", "Test", null, Request.Url.Scheme),
                CustId = 0,
                ValidationCode = "1234",
                Language = criteria.Language,
                FirstName = "Walter",
                UserName = "aawalter",
                MailContact = "mailcontact@gmail.com"
            };

            return _mailProcessor.BuildTemplateContent(path, model);
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