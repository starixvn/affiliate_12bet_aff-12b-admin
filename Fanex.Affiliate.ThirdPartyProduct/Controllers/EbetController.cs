using System.Web.Mvc;

namespace Fanex.Affiliate.ThirdPartyProduct.Controllers
{
    public class EbetController : Controller
    {
        public ActionResult Index()
        {
            return View("~/Areas/Portal/Views/ThirdPartyProduct/Ebet/Index.cshtml");
        }
    }
}
