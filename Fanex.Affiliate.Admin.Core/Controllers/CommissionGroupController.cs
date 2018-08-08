using System.Web.Mvc;
using Fanex.Affiliate.Admin.Core.DTO.CommissionGroup;
using Fanex.Affiliate.Admin.Core.MetaModels;
using Fanex.Affiliate.Admin.Core.Repository;
using Fanex.Affiliate.Admin.Core.Services;
using Fanex.Affiliate.Admin.Core.ViewModels;

namespace Fanex.Affiliate.Admin.Core.Controllers
{
    public class CommissionGroupController : Controller
    {
        private readonly ICommissionService _commissionService;

        private readonly IProductRepository _productRepository;

        public CommissionGroupController(ICommissionService commissionService, IProductRepository productRepository)
        {
            _commissionService = commissionService;
            _productRepository = productRepository;
        }

        [HttpGet]
        public ActionResult Index()
        {
            var productList = _productRepository.GetProductList();

            return View("~/Areas/Portal/Views/CommissionGroup/Index.cshtml", productList);
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

        [HttpPost]
        public ActionResult InsertCommissionGroup(CommissionGroupSetting param)
        {
            return Json(param);
        }
    }
}