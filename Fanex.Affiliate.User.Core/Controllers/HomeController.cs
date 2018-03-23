using System.Web.Mvc;
using Fanex.Affiliate.User.Core.DTO;
using Fanex.Affiliate.User.Core.MetalModels;

namespace Fanex.Affiliate.User.Core.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Login(UserLogin user)
        {
            var result = new JResult
            {

            };

            return Json(result);
        }

        [HttpGet]
        public ActionResult FAQ()
        {
            return View();
        }

        [HttpGet]
        public ActionResult ContactUs()
        {
            return View();
        }

        [HttpGet]
        public ActionResult ProgramDetail()
        {
            return View();
        }
    }
}
