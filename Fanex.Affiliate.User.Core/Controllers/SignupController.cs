namespace Fanex.Affiliate.User.Core.Controllers
{
    using System.Web.Mvc;

    public class SignupController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }
    }
}