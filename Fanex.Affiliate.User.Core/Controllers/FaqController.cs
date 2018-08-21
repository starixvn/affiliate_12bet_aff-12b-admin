namespace Fanex.Affiliate.User.Core.Controllers
{
    using System.Web.Mvc;
    using ONEbook.UM.Filters;

    [CheckUMFilter]
    public class FaqController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [ChildActionOnly]
        [OutputCache(Duration = 86400, VaryByParam = "none", VaryByCustom = "language")]
        public ActionResult Content()
        {
            return PartialView("~/Views/Faq/_Content.cshtml");
        }
    }
}