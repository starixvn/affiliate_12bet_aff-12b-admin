using Fanex.Affiliate.Admin.Core.Attributes;
using Fanex.Affiliate.Admin.Core.Constants;
using Fanex.Affiliate.Admin.Core.DTO;
using Fanex.Affiliate.Admin.Core.Extensions;
using Fanex.Affiliate.Admin.Core.MetaModels.CommissionGroup;
using Fanex.Affiliate.Admin.Core.Resources;
using Fanex.Affiliate.Admin.Core.Services.CommGroup;
using Fanex.Affiliate.Admin.Core.Services.Common;
using Fanex.Affiliate.Admin.Core.Utilities.CommGroup;
using Fanex.Affiliate.Admin.Core.ViewModels;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Fanex.Affiliate.Admin.Core.Controllers.Affiliate
{
    [FunctionAuthorize(FunctionName = Function.Affiliate)]
    public class RevenueGroupController : ReadOnlySessionBaseController
    {
        private const int MaxLength = 100;

        private readonly ICommGroupCommonService _commGroupCommonService;

        private readonly ICommissionGroupService _commissionGroupService;

        private readonly ISystemService _systemService;

        private readonly ICommGroupValidation _commGroupValidation;

        public RevenueGroupController(
            ISessionService sessionService,
            ICommissionGroupService commissionService,
            ISystemService systemService,
            ICommGroupCommonService commGroupCommonService,
            ICommGroupValidation commGroupValidation) : base(sessionService)
        {
            _commissionGroupService = commissionService;
            _systemService = systemService;
            _commGroupCommonService = commGroupCommonService;
            _commGroupValidation = commGroupValidation;
        }

        [HttpGet]
        [FunctionAuthorize(FunctionName = Function.RevenueGroup)]
        public ActionResult Index()
        {
            var subMenu = new List<SubMenu>
            {
                new SubMenu { Name = Lang.AffiliateInfo, Url = Url.Content("~/Portal/Affiliate"), FunctionName = Function.Affiliate },
                new SubMenu { Name = Lang.PaymentRequest, Url = Url.Content("~/Portal/PaymentRequest"), FunctionName = Function.Payment },
                new SubMenu { Name = Lang.RevenueGroup, Url = Url.Content("~/Portal/RevenueGroup"), FunctionName = Function.RevenueGroup, IsActive = true },
                new SubMenu { Name = Lang.EmailHistory, Url = Url.Content("~/Portal/EmailHistory"), FunctionName = Function.EmailHistory },
                new SubMenu { Name = Lang.MarketingAccount, Url = Url.Content("~/Portal/MarketingAccount"), FunctionName = Function.Affiliate }
            };
            var productList = _systemService.GetProductList();

            var viewModel = new CommissionGroupViewModel
            {
                ProductList = productList,
                DeductionList = _commGroupCommonService.GetDeductionList(),
                SubMenuList = subMenu
            };

            return View("~/Areas/Portal/Views/Affiliate/CommissionGroup.cshtml", viewModel);
        }

        [FunctionAuthorize(FunctionName = Function.RevenueGroup)]
        [HttpPost]
        [ValidateAntiForgeryHeader]
        public ActionResult GetCommissionGroupList(GetCommissionGroupParam param)
        {
            var viewModel = new GetCommissionGroupViewModel
            {
                CommissionSettingList = _commissionGroupService.GetCommissionGroupList(param),
                TotalRows = param.Nrows
            };

            return GenerateJsonNetResult(viewModel);
        }

        [FunctionAuthorize(FunctionName = Function.RevenueGroup)]
        [HttpPost]
        [ValidateAntiForgeryHeader]
        public ActionResult GetCommissionGroup(GetCommissionGroupParam param)
        {
            if (param.GroupId >= Validation.MinId)
            {
                var commissionGroupInfo = _commissionGroupService.GetCommissionGroupList(param).FirstOrDefault();
                if (commissionGroupInfo != null)
                {
                    var groupName = commissionGroupInfo.GroupName;
                    var description = commissionGroupInfo.Description;

                    commissionGroupInfo.GroupName = Server.HtmlDecode(groupName);
                    commissionGroupInfo.Description = Server.HtmlDecode(description);

                    return new JsonNetResult
                    {
                        Data = commissionGroupInfo
                    };
                }
            }

            return GenerateErrResult(CommGroupErrorMessage.InvalidGroupId);
        }

        [FunctionAuthorize(FunctionName = Function.RevenueGroup, IsHost = true)]
        [HttpPost]
        [ValidateAntiForgeryHeader]
        public ActionResult CreateCommissionGroup(CommGroupSettingParam commGroup)
        {
            if (!ModelState.IsValid)
            {
                return GenerateModelStateErrResult();
            }

            var groupNameValidation = ValidateGroupName(commGroup.GroupName);
            var result = _commGroupValidation.ValidateCommGroup(commGroup);

            if (groupNameValidation.IsSuccessful && result.IsSuccessful)
            {
                var createParam = new CreateUpdateCommissionGroupParam
                {
                    UserId = SessionService.UserId,
                    Ip = Request.GetClientIP(),
                    GroupName = Server.HtmlEncode(commGroup.GroupName.Trim()),
                    Description = Server.HtmlEncode(commGroup.Description?.Trim()),
                    TierSettingList = commGroup.TierSettingList,
                    PlatformFeeList = commGroup.PlatformFeeList,
                    DepositFee = commGroup.DepositFee,
                    WithdrawalFee = commGroup.WithdrawalFee,
                    SubAffCommRate = commGroup.SubAffCommRate,
                    QmTurnover = commGroup.QmTurnover,
                    IsActivePlayerSum = commGroup.IsActivePlayerSum
                };

                return GenerateJsonNetResult(_commissionGroupService.CreateCommissionGroup(createParam));
            }

            return GenerateErrResult(groupNameValidation.Message + result.Message);
        }

        [FunctionAuthorize(FunctionName = Function.RevenueGroup, IsHost = true)]
        [HttpPost]
        [ValidateAntiForgeryHeader]
        public ActionResult UpdateCommissionGroup(CommGroupSettingParam commGroup)
        {
            if (!ModelState.IsValid || commGroup.GroupId < Validation.MinId)
            {
                return GenerateModelStateErrResult();
            }

            var groupNameValidation = ValidateGroupName(commGroup.GroupName);
            var result = _commGroupValidation.ValidateCommGroup(commGroup);

            if (groupNameValidation.IsSuccessful && result.IsSuccessful)
            {
                var updateParam = new CreateUpdateCommissionGroupParam
                {
                    UserId = SessionService.UserId,
                    GroupId = commGroup.GroupId,
                    GroupName = Server.HtmlEncode(commGroup.GroupName.Trim()),
                    Ip = Request.GetClientIP(),
                    Description = Server.HtmlEncode(commGroup.Description?.Trim()),
                    TierSettingList = commGroup.TierSettingList,
                    PlatformFeeList = commGroup.PlatformFeeList,
                    DepositFee = commGroup.DepositFee,
                    WithdrawalFee = commGroup.WithdrawalFee,
                    SubAffCommRate = commGroup.SubAffCommRate,
                    QmTurnover = commGroup.QmTurnover,
                    IsActivePlayerSum = commGroup.IsActivePlayerSum
                };

                return GenerateJsonNetResult(_commissionGroupService.UpdateCommissionGroup(updateParam));
            }

            return GenerateErrResult(groupNameValidation.Message + result.Message);
        }

        [FunctionAuthorize(FunctionName = Function.RevenueGroup, IsHost = true)]
        [HttpPost]
        [ValidateAntiForgeryHeader]
        public ActionResult DeleteCommissionGroup(DeleteCommissionGroupParam param)
        {
            if (!ModelState.IsValid)
            {
                return GenerateModelStateErrResult();
            }

            AssignUserInfoParam(param);

            return GenerateJsonNetResult(_commissionGroupService.DeleteCommissionGroup(param));
        }

        private static ResultInfo ValidateGroupName(string groupName)
        {
            if (string.IsNullOrEmpty(groupName) || groupName.Length > MaxLength)
            {
                return ResultInfo.GenerateErr(CommGroupErrorMessage.InvalidGroupName);
            }

            return ResultInfo.Success;
        }
    }
}