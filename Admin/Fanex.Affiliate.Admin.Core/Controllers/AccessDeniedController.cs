using Fanex.Affiliate.Admin.Core.Attributes;
using ONEbook.UM.Filters;
using System.Web.Mvc;

namespace Fanex.Affiliate.Admin.Core.Controllers
{
    [CheckUMFilter]
    [CustomAuthorize]
    public class AccessDeniedController: Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }
    }
}
