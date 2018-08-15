using System.Linq;
using System.Web.Mvc;
using Fanex.Affiliate.Admin.Core.Attributes;
using Fanex.Affiliate.Admin.Core.Constants;
using Fanex.Affiliate.Admin.Core.DTO.Affiliate;
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
        private const string PrivateGroupName = "[None]";

        private readonly ICommissionGroupService _commissionGroupService;

        private readonly IProductRepository _productRepository;

        private readonly IAffiliateService _affiliateService;

        public CommissionGroupController(
            ISessionService sessionService,
            ICommissionGroupService commissionService,
            IProductRepository productRepository,
            IAffiliateService affiliateService) : base(sessionService)
        {
            _commissionGroupService = commissionService;
            _productRepository = productRepository;
            _affiliateService = affiliateService;
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
        public ActionResult AssignGroup(int affId)
        {
            var param = new GetGroupListParam { AffId = affId };
            var commGroupList = _commissionGroupService.GetNormalCommGroupList(param);

            var viewModel = new AssignNormalGroupViewModel
            {
                CommGroupList = commGroupList,
                SelectedGroup = param.GroupId,
                AffInfo = _affiliateService.GetAffiliateBasicInfo(affId)
            };

            return View("~/Areas/Portal/Views/CommissionGroup/AssignGroup.cshtml", viewModel);
        }

        [HttpPost]
        public ActionResult AssignGroup(AssignGroupParam assignParam, AffiliateBasicInfo affInfo)
        {
            assignParam.UserId = SessionService.UserId;
            assignParam.Ip = Request.UserHostAddress;
            assignParam.AffId = affInfo.AffId;

            var result = _commissionGroupService.AssignGroup(assignParam, affInfo);

            return Json(new { assignParam, affInfo, result });
        }

        [HttpGet]
        public ActionResult AssignPrivateGroup(int affId)
        {
            var viewModel = new AssignPrivateGroupViewModel
            {
                ProductList = _productRepository.GetProductList(),
                DeductionList = _commissionGroupService.GetDeductionList(),
                AffInfo = _affiliateService.GetAffiliateBasicInfo(affId)
            };

            return View("~/Areas/Portal/Views/CommissionGroup/AssignPrivateGroup.cshtml", viewModel);
        }

        [HttpPost]
        public ActionResult AssignPrivateGroup(CommissionGroupSetting commGroup, AffiliateBasicInfo affInfo)
        {
            var assignParam = new CreateUpdateCommissionGroupParam
            {
                UserId = SessionService.UserId,
                GroupId = affInfo.AffId,
                GroupName = PrivateGroupName,
                Description = PrivateGroupName,
                QmTurnover = commGroup.QmTurnover,
                TierSettingList = commGroup.TierSettingList,
                PlatformFeeList = commGroup.PlatformFeeList,
                OperationalFee = commGroup.OperationalFee,
                BankingFee = commGroup.BankingFee,
                Ip = Request.UserHostAddress
            };

            var result = _commissionGroupService.AssignPrivateGroup(assignParam, affInfo);

            return Json(new { assignParam, affInfo, result });
        }
    }
}