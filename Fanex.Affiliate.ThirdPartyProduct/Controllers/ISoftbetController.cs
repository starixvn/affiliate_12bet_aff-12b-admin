using System.Web.Mvc;

namespace Fanex.Affiliate.ThirdPartyProduct.Controllers
{
    public class ISoftbetController : Controller
    {
        public ActionResult Index()
        {
            return View("~/Areas/Portal/Views/ThirdPartyProduct/ISoftbet/Index.cshtml");
        }
    }
}
