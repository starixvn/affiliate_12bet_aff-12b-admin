namespace Fanex.Affiliate.User.Core.Controllers
{
    using System.Web.Mvc;
    using ONEbook.UM.Filters;

    [CheckUMFilter]
    public class PromotionController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }
    }
}