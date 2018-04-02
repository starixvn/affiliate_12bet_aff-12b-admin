namespace Fanex.Affiliate.User.Core.Controllers
{
    using System.Web.Mvc;

    public class TestController : Controller
    {
        [HttpGet]
        public ActionResult Email(TestCriteria criteria)
        {
            var data = string.Empty;

            ViewBag.Data = data;
            return View();
        }
    }

    public class TestCriteria
    {
        public string Language { get; set; }

        public string Function { get; set; }
    }

    public class TestService
    {
    }
}