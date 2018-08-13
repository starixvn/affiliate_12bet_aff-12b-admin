using System.Linq;
using System.Web.Mvc;
using Fanex.Affiliate.Admin.Core.Attributes;
using Fanex.Affiliate.Admin.Core.Constants;
using Fanex.Affiliate.Admin.Core.DTO.CommissionGroup;
using Fanex.Affiliate.Admin.Core.MetaModels;
using Fanex.Affiliate.Admin.Core.MetaModels.CommissionGroup;
using Fanex.Affiliate.Admin.Core.Repository;
using Fanex.Affiliate.Admin.Core.Services;
using Fanex.Affiliate.Admin.Core.Services.Common;
using Fanex.Affiliate.Admin.Core.ViewModels;

namespace Fanex.Affiliate.Admin.Core.Controllers
{
    [FunctionAuthorize(FunctionName = Function.CommissionGroup)]
    public class CommissionGroupController : BaseController
    {
        private readonly ICommissionGroupService _commissionGroupService;

        private readonly IProductRepository _productRepository;

        public CommissionGroupController(
            ISessionService sessionService,
            ICommissionGroupService commissionService,
            IProductRepository productRepository) : base(sessionService)
        {
            _commissionGroupService = commissionService;
            _productRepository = productRepository;
        }

        [HttpGet]
        public ActionResult Index()
        {
            var viewModel = new CommissionGroupViewModel
            {
                ProductList = _productRepository.GetProductList(),
                DeductionList = _commissionGroupService.GetDeductionList()
            };

            return View("~/Areas/Portal/Views/CommissionGroup/Index.cshtml", viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryHeader]
        public ActionResult GetCommissionSettingList(CommissionGroupParam param)
        {
            var viewModel = new GetCommissionGroupViewModel
            {
                CommissionSettingList = _commissionGroupService.GetCommissionSettingList(param),
                TotalRows = param.Nrows
            };

            return Json(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryHeader]
        public ActionResult GetCommissionGroup(CommissionGroupParam param)
        {
            var commGroup = _commissionGroupService.GetCommissionSettingList(param).FirstOrDefault();

            return Json(commGroup);
        }

        [HttpPost]
        [ValidateAntiForgeryHeader]
        public ActionResult InsertCommissionGroup(CommissionGroupSetting commGroup)
        {
            var insertParam = new InsertCommissionGroupParam
            {
                UserId = SessionService.UserId,
                Ip = Request.UserHostAddress,
                GroupName = commGroup.GroupName,
                Description = commGroup.Description,
                QmTurnover = commGroup.QmTurnover,
                TierSettingList = commGroup.TierSettingList,
                PlatformFeeList = commGroup.PlatformFeeList,
                OperationalFee = commGroup.OperationalFee,
                BankingFee = commGroup.BankingFee
            };

            var result = _commissionGroupService.InsertCommissionGroup(insertParam);

            return Json(result);
        }

        [HttpPost]
        [ValidateAntiForgeryHeader]
        public ActionResult UpdateCommissionGroup(CommissionGroupSetting commGroup)
        {
            var insertParam = new InsertCommissionGroupParam
            {
                UserId = SessionService.UserId,
                GroupId = commGroup.GroupId,
                Ip = Request.UserHostAddress,
                Description = commGroup.Description,
                QmTurnover = commGroup.NextQmTurnover,
                TierSettingList = commGroup.TierSettingList,
                PlatformFeeList = commGroup.PlatformFeeList,
                OperationalFee = commGroup.OperationalFee,
                BankingFee = commGroup.BankingFee
            };

            var result = _commissionGroupService.UpdateCommissionGroup(insertParam);

            return Json(result);
        }

        [HttpPost]
        [ValidateAntiForgeryHeader]
        public ActionResult DeleteCommissionGroup(DeleteCommissionGroupParam param)
        {
            param.UserId = SessionService.UserId;
            param.Ip = Request.UserHostAddress;

            var result = _commissionGroupService.DeleteCommissionGroup(param);

            return Json(result);
        }
    }
}