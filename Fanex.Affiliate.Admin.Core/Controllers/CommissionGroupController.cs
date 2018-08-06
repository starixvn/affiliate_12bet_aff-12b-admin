using System.Web.Mvc;
using Fanex.Affiliate.Admin.Core.MetaModels;
using Fanex.Affiliate.Admin.Core.Services;
using Fanex.Affiliate.Admin.Core.ViewModels;

namespace Fanex.Affiliate.Admin.Core.Controllers
{
    public class CommissionGroupController : Controller
    {
        private readonly ICommissionService _commissionService;

        public CommissionGroupController(ICommissionService commissionService)
        {
            _commissionService = commissionService;
        }

        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult GetCommissionSettingList(CommissionGroupParam param)
        {
            var viewModel = new CommissionGroupViewModel
            {
                CommissionSettingList = _commissionService.GetCommissionSettingList(param),
                TotalRows = param.Nrows
            };

            return Json(viewModel);
        }
    }
}