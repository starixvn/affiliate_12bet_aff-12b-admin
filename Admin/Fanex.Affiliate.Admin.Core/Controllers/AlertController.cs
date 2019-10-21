using Fanex.Affiliate.Admin.Core.MetaModels;
using System.Web.Mvc;

namespace Fanex.Affiliate.Admin.Core.Controllers
{
    public class AlertController: Controller
    {
        [HttpGet]
        public ActionResult Index(AlertParam param)
        {
            return View(param);
        }
    }
}
