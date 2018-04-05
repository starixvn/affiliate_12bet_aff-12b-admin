namespace Fanex.Affiliate.User.Core.Controllers
{
    using System.Web.Mvc;

    public class ProductController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }
    }
}