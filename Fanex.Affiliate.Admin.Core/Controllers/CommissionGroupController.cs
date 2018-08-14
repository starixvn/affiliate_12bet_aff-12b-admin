using System.Linq;
using System.Web.Mvc;
using Fanex.Affiliate.Admin.Core.Attributes;
using Fanex.Affiliate.Admin.Core.Constants;
using Fanex.Affiliate.Admin.Core.DTO.CommissionGroup;
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
        public ActionResult GetCommissionGroupList(GetCommissionGroupParam param)
        {
            var viewModel = new GetCommissionGroupViewModel
            {
                CommissionSettingList = _commissionGroupService.GetCommissionGroupList(param),
                TotalRows = param.Nrows
            };

            return Json(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryHeader]
        public ActionResult GetCommissionGroup(GetCommissionGroupParam param)
        {
            var commGroup = _commissionGroupService.GetCommissionGroupList(param).FirstOrDefault();

            return Json(commGroup);
        }

        [HttpPost]
        [ValidateAntiForgeryHeader]
        public ActionResult CreateCommissionGroup(CommissionGroupSetting commGroup)
        {
            var createParam = new CreateUpdateCommissionGroupParam
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

            var result = _commissionGroupService.CreateCommissionGroup(createParam);

            return Json(result);
        }

        [HttpPost]
        [ValidateAntiForgeryHeader]
        public ActionResult UpdateCommissionGroup(CommissionGroupSetting commGroup)
        {
            var updateParam = new CreateUpdateCommissionGroupParam
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

            var result = _commissionGroupService.UpdateCommissionGroup(updateParam);

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

        [HttpGet]
        public ActionResult AssignNormalGroup(int affId, string affName)
        {
            var param = new GetGroupListParam { AffId = affId };
            var commGroupList = _commissionGroupService.GetNormalCommGroupList(param);
            var viewModel = new AssignNormalGroupViewModel
            {
                CommGroupList = commGroupList,
                SelectedGroup = param.GroupId.HasValue ? param.GroupId.Value : -1,
                AffName = affName
            };

            return View("~/Areas/Portal/Views/CommissionGroup/AssignNormalGroup.cshtml", viewModel);
        }

        [HttpPost]
        public ActionResult AssignNormalGroup()
        {
            return Json(new { });
        }

        [HttpGet]
        public ActionResult AssignPrivateGroup()
        {
            var viewModel = new CommissionGroupViewModel
            {
                ProductList = _productRepository.GetProductList(),
                DeductionList = _commissionGroupService.GetDeductionList()
            };

            return View("~/Areas/Portal/Views/CommissionGroup/AssignPrivateGroup.cshtml", viewModel);
        }
    }
}