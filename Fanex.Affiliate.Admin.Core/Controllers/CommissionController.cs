using System.Web.Mvc;
using Fanex.Affiliate.Admin.Core.MetaModels;

namespace AffiliateAdmin.Areas.Portal.Controllers
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