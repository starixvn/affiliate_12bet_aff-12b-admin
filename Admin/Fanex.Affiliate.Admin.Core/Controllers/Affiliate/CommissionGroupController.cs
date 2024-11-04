using Fanex.Affiliate.Admin.Core.Attributes;
using Fanex.Affiliate.Admin.Core.Constants;
using Fanex.Affiliate.Admin.Core.DTO;
using Fanex.Affiliate.Admin.Core.DTO.Affiliate;
using Fanex.Affiliate.Admin.Core.DTO.CommissionGroup;
using Fanex.Affiliate.Admin.Core.DTO.Email;
using Fanex.Affiliate.Admin.Core.Extensions;
using Fanex.Affiliate.Admin.Core.MetaModels.CommissionGroup;
using Fanex.Affiliate.Admin.Core.Services;
using Fanex.Affiliate.Admin.Core.Services.CommGroup;
using Fanex.Affiliate.Admin.Core.Services.Common;
using Fanex.Affiliate.Admin.Core.Utilities;
using Fanex.Affiliate.Admin.Core.Utilities.CommGroup;
using Fanex.Affiliate.Admin.Core.ViewModels;
using System.Linq;
using System.Web.Mvc;

namespace Fanex.Affiliate.Admin.Core.Controllers
{
    [FunctionAuthorize(FunctionName = Function.CommissionGroup)]
    public class CommissionGroupController : ReadOnlySessionBaseController
    {
        private const int MaxLength = 100;

        private const string PrivateGroupName = "[None]";

        private readonly ICommissionGroupService _commissionGroupService;

        private readonly ICommGroupAssignmentService _commGroupAssignmentService;

        private readonly ISystemService _systemService;

        public CommissionGroupController(
            ISessionService sessionService,
            ICommissionGroupService commissionService,
            ISystemService systemService,
            ICommGroupAssignmentService commGroupAssignmentService) : base(sessionService)
        {
            _commissionGroupService = commissionService;
            _systemService = systemService;
            _commGroupAssignmentService = commGroupAssignmentService;
        }

        [HttpGet]
        [AllowByPassFunctionAuthorize]
        public ActionResult Index()
        {
            var viewModel = new CommissionGroupViewModel
            {
                ProductList = _systemService.GetProductList(),
                DeductionList = _commissionGroupService.GetDeductionList(),
            };

            return View("~/Areas/Portal/Views/Affiliate/Index.cshtml", viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryHeader]
        public ActionResult FetchInitState()
        {
            var viewModel = new CommissionGroupViewModel
            {
                ProductList = _systemService.GetProductList(),
                DeductionList = _commissionGroupService.GetDeductionList(),
            };

            return new JsonNetResult(viewModel);
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

            return new JsonNetResult
            {
                Data = viewModel
            };
        }

        [HttpPost]
        [ValidateAntiForgeryHeader]
        [AllowByPassFunctionAuthorize]
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

            return new JsonNetResult
            {
                Data = ResultInfo.GenerateErr(CommGroupErrorMessage.InvalidGroupId)
            };
        }

        [FunctionAuthorize(FunctionName = Function.CommissionGroup, IsHost = true)]
        [HttpPost]
        [ValidateAntiForgeryHeader]
        public ActionResult CreateCommissionGroup(CommissionGroupSetting commGroup)
        {
            if (!ModelState.IsValid)
            {
                return ModelState.GenerateErrorResult();
            }

            var groupNameValidation = ValidateGroupName(commGroup.GroupName);
            var platformFeeValidation = CommGroupValidationHelper.ValidatePlatformFeeList(_systemService, commGroup.PlatformFeeList);

            if (groupNameValidation.IsSuccessful && platformFeeValidation.IsSuccessful)
            {
                var createParam = new CreateUpdateCommissionGroupParam
                {
                    UserId = SessionService.UserId,
                    Ip = Request.GetClientIP(),
                    GroupName = Server.HtmlEncode(commGroup.GroupName.Trim()),
                    Description = Server.HtmlEncode(commGroup.Description?.Trim()),
                    QmTurnover = commGroup.QmTurnover,
                    IsApplyBonus = commGroup.IsApplyBonus,
                    TierSettingList = commGroup.TierSettingList,
                    FdmSettingList = commGroup.FdmSettingList,
                    PlatformFeeList = commGroup.PlatformFeeList,
                    OperationalFee = commGroup.OperationalFee,
                    BankingFee = commGroup.BankingFee
                };

                return new JsonNetResult
                {
                    Data = _commissionGroupService.CreateCommissionGroup(createParam)
                };
            }

            return new JsonNetResult
            {
                Data = ResultInfo.GenerateErr(groupNameValidation.Message + platformFeeValidation.Message)
            };
        }

        [FunctionAuthorize(FunctionName = Function.CommissionGroup, IsHost = true)]
        [HttpPost]
        [ValidateAntiForgeryHeader]
        public ActionResult UpdateCommissionGroup(CommissionGroupSetting commGroup)
        {
            if (!ModelState.IsValid || commGroup.GroupId < Validation.MinId)
            {
                return ModelState.GenerateErrorResult();
            }

            var groupNameValidation = ValidateGroupName(commGroup.GroupName);
            var platformFeeValidation = CommGroupValidationHelper.ValidatePlatformFeeList(_systemService, commGroup.PlatformFeeList);

            if (groupNameValidation.IsSuccessful && platformFeeValidation.IsSuccessful)
            {
                var updateParam = new CreateUpdateCommissionGroupParam
                {
                    UserId = SessionService.UserId,
                    GroupId = commGroup.GroupId,
                    GroupName = Server.HtmlEncode(commGroup.GroupName.Trim()),
                    Ip = Request.GetClientIP(),
                    Description = Server.HtmlEncode(commGroup.Description?.Trim()),
                    QmTurnover = commGroup.NextQmTurnover,
                    IsApplyBonus = commGroup.NextIsApplyBonus,
                    TierSettingList = commGroup.TierSettingList,
                    FdmSettingList = commGroup.FdmSettingList,
                    PlatformFeeList = commGroup.PlatformFeeList,
                    OperationalFee = commGroup.OperationalFee,
                    BankingFee = commGroup.BankingFee
                };

                return new JsonNetResult
                {
                    Data = _commissionGroupService.UpdateCommissionGroup(updateParam)
                };
            }

            return new JsonNetResult
            {
                Data = ResultInfo.GenerateErr(groupNameValidation.Message + platformFeeValidation.Message)
            };
        }

        [FunctionAuthorize(FunctionName = Function.CommissionGroup, IsHost = true)]
        [HttpPost]
        [ValidateAntiForgeryHeader]
        public ActionResult DeleteCommissionGroup(DeleteCommissionGroupParam param)
        {
            if (!ModelState.IsValid)
            {
                return ModelState.GenerateErrorResult();
            }

            AssignUserInfoParam(param);

            return new JsonNetResult
            {
                Data = _commissionGroupService.DeleteCommissionGroup(param)
            };
        }

        [HttpGet]
        public ActionResult AssignPrivateGroup(int affId)
        {
            var viewModel = affId >= Validation.MinId ?
                new AssignPrivateGroupViewModel
                {
                    ProductList = _systemService.GetProductList(),
                    DeductionList = _commissionGroupService.GetDeductionList(),
                    AffInfo = _commGroupAssignmentService.GetAffiliateBasicInfo(affId)
                } :
                new AssignPrivateGroupViewModel();

            return View("~/Areas/Portal/Views/Affiliate/AssignPrivateGroup.cshtml", viewModel);
        }

        [FunctionAuthorize(FunctionName = Function.CommissionGroup, IsHost = true)]
        [HttpPost]
        [ValidateAntiForgeryHeader]
        public ActionResult AssignPrivateGroup(CommissionGroupSetting commGroup, AffiliateBasicInfo affInfo)
        {
            if (!ModelState.IsValid)
            {
                return ModelState.GenerateErrorResult();
            }

            var platformFeeValidation = CommGroupValidationHelper.ValidatePlatformFeeList(_systemService, commGroup.PlatformFeeList);
            if (platformFeeValidation.IsSuccessful)
            {
                var assignParam = new CreateUpdateCommissionGroupParam
                {
                    UserId = SessionService.UserId,
                    GroupId = affInfo.AffId,
                    GroupName = affInfo.UserName,
                    Description = PrivateGroupName,
                    QmTurnover = commGroup.QmTurnover,
                    IsApplyBonus = commGroup.IsApplyBonus,
                    TierSettingList = commGroup.TierSettingList,
                    FdmSettingList = commGroup.FdmSettingList,
                    PlatformFeeList = commGroup.PlatformFeeList,
                    OperationalFee = commGroup.OperationalFee,
                    BankingFee = commGroup.BankingFee,
                    Ip = Request.GetClientIP()
                };

                var emailParam = new AffInformationEmail
                {
                    RootUrl = SiteDomainHelper.GetRootUrl(this)
                };

                return new JsonNetResult
                {
                    Data = _commGroupAssignmentService.AssignPrivateGroup(assignParam, affInfo, emailParam)
                };
            }

            return new JsonNetResult
            {
                Data = ResultInfo.GenerateErr(platformFeeValidation.Message)
            };
        }

        private static ResultInfo ValidateGroupName(string groupName)
        {
            if (string.IsNullOrEmpty(groupName) || groupName.Length > MaxLength)
            {
                return ResultInfo.GenerateErr(CommGroupErrorMessage.InvalidGroupName);
            }

            return new ResultInfo();
        }
    }
}