using System.Web.Mvc;
using Fanex.Affiliate.Admin.Core.MetaModels;

namespace Fanex.Affiliate.Admin.Core.Controllers
{
    public class CommissionController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult GetCommissionSettingList(CommissionSettingParam param)
        {
            throw new System.NotSupportedException();
        }
    }
}