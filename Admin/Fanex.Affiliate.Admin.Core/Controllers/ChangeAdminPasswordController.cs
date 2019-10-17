using System.Web.Mvc;

namespace Fanex.Affiliate.Admin.Core.Controllers
{
    public class ChangeAdminPasswordController: Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }
    }
}
