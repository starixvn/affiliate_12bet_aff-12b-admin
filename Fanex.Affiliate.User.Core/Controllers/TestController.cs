namespace Fanex.Affiliate.User.Core.Controllers
{
    using System.Web.Mvc;
    using Utilities;

    public class TestController : Controller
    {
        private readonly IEmailBuilder _emailBuilder;

        public TestController()
        {
            _emailBuilder = new EmailBuilder();
        }

        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Email(TestCriteria criteria)
        {
            var viewPath = string.Format("~/Views/Shared/Email/{0}/Content.{1}.{2}", criteria.Function, criteria.Language, criteria.Format);
            var path = Server.MapPath(viewPath);
            dynamic model = new
            {
                RootUrl = SiteDomainHelper.GetRootUrl(Request.Url),
                RecoverPwdURL = Url.Action("Recovery", "Test"),
                CustId = 0,
                ValidationCode = "1234",
                Language = criteria.Language,
                FirstName = "Walter",
                UserName = "aawalter",
                MailContact = "mailcontact@gmail.com"
            };

            ViewBag.Data = _emailBuilder.BuildTemplateContent(path, model);
            return View();
        }
    }

    public class TestCriteria
    {
        public string Language { get; set; }

        public string Function { get; set; }

        public string Format { get; set; }
    }
}