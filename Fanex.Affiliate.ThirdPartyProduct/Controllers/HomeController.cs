using System.Web.Mvc;

namespace Fanex.Affiliate.ThirdPartyProduct.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View("~/Areas/Portal/Views/ThirdPartyProduct/Home/Index.cshtml");
        }
    }
}
