using System.Linq;
using System.Web.Mvc;
using Fanex.Affiliate.Admin.Core.Attributes;
using Fanex.Affiliate.Admin.Core.Constants;
using Fanex.Affiliate.Admin.Core.DTO;
using Fanex.Affiliate.Admin.Core.DTO.CommissionGroup;
using Fanex.Affiliate.Admin.Core.Extensions;
using Fanex.Affiliate.Admin.Core.MetaModels.CommissionGroup;
using Fanex.Affiliate.Admin.Core.Repository;
using Fanex.Affiliate.Admin.Core.Services;
using Fanex.Affiliate.Admin.Core.Services.CommGroup;
using Fanex.Affiliate.Admin.Core.Services.Common;
using Fanex.Affiliate.Admin.Core.Utilities.CommGroup;
using Fanex.Affiliate.Admin.Core.ViewModels;

namespace Fanex.Affiliate.Admin.Core.Controllers
{
    [FunctionAuthorize(FunctionName = Function.CommissionGroup)]
    public class CommissionGroupController : BaseController
    {
        private readonly ICommGroupCommonService _commGroupCommonService;

        private readonly ICommissionGroupService _commissionGroupService;

        private readonly IProductRepository _productRepository;

        public CommissionGroupController(
            ISessionService sessionService,
            ICommissionGroupService commissionService,
            IProductRepository productRepository,
            ICommGroupCommonService commGroupCommonService) : base(sessionService)
        {
            _commissionGroupService = commissionService;
            _productRepository = productRepository;
            _commGroupCommonService = commGroupCommonService;
        }

        [HttpGet]
        public ActionResult Index()
        {
            var viewModel = new CommissionGroupViewModel
            {
                ProductList = _productRepository.GetProductList(),
                DeductionList = _commGroupCommonService.GetDeductionList()
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

            return new JsonNetResult
            {
                Data = viewModel
            };
        }

        [HttpPost]
        [ValidateAntiForgeryHeader]
        public ActionResult GetCommissionGroup(GetCommissionGroupParam param)
        {
            if (param.GroupId >= Validation.MinId)
            {
                return new JsonNetResult
                {
                    Data = _commissionGroupService.GetCommissionGroupList(param).FirstOrDefault()
                };
            }

            return new JsonNetResult
            {
                Data = new ResultInfo
                {
                    Code = Code.GeneralErrorCode,
                    Message = CommGroupErrorMessage.InvalidGroupId
                }
            };
        }

        [FunctionAuthorize(FunctionName = Function.CommissionGroup, IsHost = true)]
        [HttpPost]
        [ValidateAntiForgeryHeader]
        public ActionResult CreateCommissionGroup(CommissionGroupSetting commGroup)
        {
            if (!ModelState.IsValid)
            {
                return CreateErrorResult();
            }

            var groupNameValidation = ValidateGroupName(commGroup.GroupName);
            var platformFeeValidation = CommGroupValidationHelper.ValidatePlatformFeeList(_productRepository, commGroup.PlatformFeeList);

            if (groupNameValidation.IsSuccessful && platformFeeValidation.IsSuccessful)
            {
                var createParam = new CreateUpdateCommissionGroupParam
                {
                    UserId = SessionService.UserId,
                    Ip = Request.UserHostAddress,
                    GroupName = Server.HtmlEncode(commGroup.GroupName?.Trim()),
                    Description = Server.HtmlEncode(commGroup.Description?.Trim()),
                    QmTurnover = commGroup.QmTurnover,
                    TierSettingList = commGroup.TierSettingList,
                    PlatformFeeList = commGroup.PlatformFeeList.Select(platformFee => EncodeDeductionSetting(platformFee)),
                    OperationalFee = EncodeDeductionSetting(commGroup.OperationalFee),
                    BankingFee = EncodeDeductionSetting(commGroup.BankingFee),
                    ExchangeWinLossShare = EncodeDeductionSetting(commGroup.ExchangeWinLossShare),
                    ExchangeCommissionShare = EncodeDeductionSetting(commGroup.ExchangeCommissionShare)
                };

                return new JsonNetResult
                {
                    Data = _commissionGroupService.CreateCommissionGroup(createParam)
                };
            }

            return new JsonNetResult
            {
                Data = new ResultInfo
                {
                    Code = Code.GeneralErrorCode,
                    Message = groupNameValidation.Message + platformFeeValidation.Message
                }
            };
        }

        private DeductionSetting EncodeDeductionSetting(DeductionSetting param)
        {
            return new DeductionSetting
            {
                GroupId = param.GroupId,
                DeductionId = param.DeductionId,
                DeductionName = Server.HtmlEncode(param.DeductionName),
                ProductIds = Server.HtmlEncode(param.ProductIds),
                DeductedPercentage = param.DeductedPercentage,
                NextDeductedPercentage = param.NextDeductedPercentage
            };
        }

        [FunctionAuthorize(FunctionName = Function.CommissionGroup, IsHost = true)]
        [HttpPost]
        [ValidateAntiForgeryHeader]
        public ActionResult UpdateCommissionGroup(CommissionGroupSetting commGroup)
        {
            if (!ModelState.IsValid || commGroup.GroupId < Validation.MinId)
            {
                return CreateErrorResult();
            }

            var groupNameValidation = ValidateGroupName(commGroup.GroupName);
            var platformFeeValidation = CommGroupValidationHelper.ValidatePlatformFeeList(_productRepository, commGroup.PlatformFeeList);

            if (groupNameValidation.IsSuccessful && platformFeeValidation.IsSuccessful)
            {
                var updateParam = new CreateUpdateCommissionGroupParam
                {
                    UserId = SessionService.UserId,
                    GroupId = commGroup.GroupId,
                    GroupName = Server.HtmlEncode(commGroup.GroupName?.Trim()),
                    Ip = Request.UserHostAddress,
                    Description = Server.HtmlEncode(commGroup.Description?.Trim()),
                    QmTurnover = commGroup.NextQmTurnover,
                    TierSettingList = commGroup.TierSettingList,
                    PlatformFeeList = commGroup.PlatformFeeList.Select(platformFee => EncodeDeductionSetting(platformFee)),
                    OperationalFee = EncodeDeductionSetting(commGroup.OperationalFee),
                    BankingFee = EncodeDeductionSetting(commGroup.BankingFee),
                    ExchangeWinLossShare = EncodeDeductionSetting(commGroup.ExchangeWinLossShare),
                    ExchangeCommissionShare = EncodeDeductionSetting(commGroup.ExchangeCommissionShare)
                };

                return new JsonNetResult
                {
                    Data = _commissionGroupService.UpdateCommissionGroup(updateParam)
                };
            }

            return new JsonNetResult
            {
                Data = new ResultInfo
            var validateGroupName = ValidateGroupName(param.GroupName);
            var result = new ResultInfo();

                {
                    Code = Code.GeneralErrorCode,
                    Message = groupNameValidation.Message + platformFeeValidation.Message
                }
            };
        }

        [FunctionAuthorize(FunctionName = Function.CommissionGroup, IsHost = true)]
        [HttpPost]
        [ValidateAntiForgeryHeader]
        public ActionResult DeleteCommissionGroup(DeleteCommissionGroupParam param)
        {
            if (!ModelState.IsValid)
            {
                return CreateErrorResult();
            }

            param.UserId = SessionService.UserId;
            param.Ip = Request.UserHostAddress;

            return new JsonNetResult
            {
                Data = _commissionGroupService.DeleteCommissionGroup(param)
            };
        }

        private ActionResult CreateErrorResult()
        {
            return new JsonNetResult
            {
                Data = new ResultInfo
                {
                    Code = Code.GeneralErrorCode,
                    Message = string.Join("<br />", ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage))
                }
            };
        }

        private static ResultInfo ValidateGroupName(string groupName)
                var affiliateBasicInfo = new AffiliateBasicInfo
                {
                    AffId = affInfo.AffId,
                    GroupId = affInfo.GroupId,
                    UserName = Server.HtmlEncode(affInfo.UserName),
                    FirstName = Server.HtmlEncode(affInfo.FirstName),
                    AffType = affInfo.AffType,
                    Email = Server.HtmlEncode(affInfo.Email),
                    MailOption = affInfo.MailOption,
                    PreferredLanguage = Server.HtmlEncode(affInfo.PreferredLanguage),
                    PlayerUsername = Server.HtmlEncode(affInfo.PlayerUsername)
                };

        {
            var result = new ResultInfo();
            if (string.IsNullOrEmpty(groupName) || groupName.Length > 100)
            {
                result.Code = Code.GeneralErrorCode;
                result.Message = CommGroupErrorMessage.InvalidGroupName;
                    PlatformFeeList = commGroup.PlatformFeeList.Select(platformFee => EncodeDeductionSetting(platformFee)),
                    OperationalFee = EncodeDeductionSetting(commGroup.OperationalFee),
                    BankingFee = EncodeDeductionSetting(commGroup.BankingFee),
                    ExchangeWinLossShare = EncodeDeductionSetting(commGroup.ExchangeWinLossShare),
                    ExchangeCommissionShare = EncodeDeductionSetting(commGroup.ExchangeCommissionShare),
            }

            return result;
        }
    }
}