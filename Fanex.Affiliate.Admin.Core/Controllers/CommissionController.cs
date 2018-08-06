using System.Web.Mvc;
using Fanex.Affiliate.Admin.Core.MetaModels;
using Fanex.Affiliate.Admin.Core.Services;

namespace Fanex.Affiliate.Admin.Core.Controllers
{
    public class CommissionController : Controller
    {
        private readonly ICommissionService _commissionService;

        public CommissionController(ICommissionService commissionService)
        {
            _commissionService = commissionService;
        }

        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult GetCommissionSettingList(CommissionSettingParam param)
        {
            return Json(_commissionService.GetCommissionSettingList(param));
        }
    }
}