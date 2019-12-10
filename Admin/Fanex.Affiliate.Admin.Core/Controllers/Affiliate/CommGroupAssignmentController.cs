using System.Linq;
using System.Web.Mvc;
using Fanex.Affiliate.Admin.Core.Attributes;
using Fanex.Affiliate.Admin.Core.Constants;
using Fanex.Affiliate.Admin.Core.DTO;
using Fanex.Affiliate.Admin.Core.DTO.Affiliate;
using Fanex.Affiliate.Admin.Core.DTO.CommissionGroup;
using Fanex.Affiliate.Admin.Core.DTO.Email;
using Fanex.Affiliate.Admin.Core.Extensions;
using Fanex.Affiliate.Admin.Core.MetaModels.CommissionGroup;
using Fanex.Affiliate.Admin.Core.Repository;
using Fanex.Affiliate.Admin.Core.Services;
using Fanex.Affiliate.Admin.Core.Services.CommGroup;
using Fanex.Affiliate.Admin.Core.Services.Common;
using Fanex.Affiliate.Admin.Core.Utilities;
using Fanex.Affiliate.Admin.Core.Utilities.CommGroup;
using Fanex.Affiliate.Admin.Core.ViewModels;

namespace Fanex.Affiliate.Admin.Core.Controllers.Affiliate
{
    [FunctionAuthorize(FunctionName = Function.CommissionGroup)]
    public class CommGroupAssignmentController : BaseController
    {
        private const string PrivateGroupName = "[None]";

        private readonly ICommGroupCommonService _commGroupCommonService;

        private readonly ICommGroupAssignmentService _commGroupAssignmentService;

        private readonly IAffiliateService _affiliateService;

        private readonly IProductRepository _productRepository;

        public CommGroupAssignmentController(
            ISessionService sessionService,
            IProductRepository productRepository,
            IAffiliateService affiliateService,
            ICommGroupAssignmentService commGroupAssignmentService,
            ICommGroupCommonService commGroupCommonService) : base(sessionService)
        {
            _affiliateService = affiliateService;
            _commGroupAssignmentService = commGroupAssignmentService;
            _productRepository = productRepository;
            _commGroupCommonService = commGroupCommonService;
        }

        [HttpGet]
        public ActionResult AssignGroup(int affId)
        {
            var viewModel = new AssignNormalGroupViewModel();

            if (affId >= Validation.MinId)
            {
                var param = new GetGroupListParam { AffId = affId };
                var commGroupList = _commGroupAssignmentService.GetNormalCommGroupList(param);

                viewModel = new AssignNormalGroupViewModel
                {
                    CommGroupList = commGroupList,
                    SelectedGroup = param.GroupId,
                    AffInfo = _affiliateService.GetAffiliateBasicInfo(affId)
                };
            }

            return View("~/Areas/Portal/Views/Affiliate/AssignGroup.cshtml", viewModel);
        }

        [FunctionAuthorize(FunctionName = Function.CommissionGroup, IsHost = true)]
        [HttpPost]
        [ValidateAntiForgeryHeader]
        public ActionResult AssignGroup(AssignGroupParam assignParam, AffiliateBasicInfo affInfo)
        {
            if (!ModelState.IsValid || assignParam.GroupId < Validation.MinId)
            {
                return CreateErrorResult();
            }

            assignParam.UserId = SessionService.UserId;
            assignParam.Ip = Request.UserHostAddress;
            assignParam.AffId = affInfo.AffId;

            var emailParam = new AffInformationEmail
            {
                RootUrl = SiteDomainHelper.GetRootUrl(this)
            };

            return new JsonNetResult
            {
                Data = _commGroupAssignmentService.AssignGroup(assignParam, affInfo, emailParam)
            };
        }

        [HttpGet]
        public ActionResult AssignPrivateGroup(int affId)
        {
            var viewModel = affId >= Validation.MinId ?
                new AssignPrivateGroupViewModel
                {
                    ProductList = _productRepository.GetProductList(),
                    DeductionList = _commGroupCommonService.GetDeductionList(),
                    AffInfo = _affiliateService.GetAffiliateBasicInfo(affId)
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
                return CreateErrorResult();
            }

            var platformFeeValidation = CommGroupValidationHelper.ValidatePlatformFeeList(_productRepository, commGroup.PlatformFeeList);
            if (platformFeeValidation.IsSuccessful)
            {
                var assignParam = new CreateUpdateCommissionGroupParam
                {
                    UserId = SessionService.UserId,
                    GroupId = affInfo.AffId,
                    GroupName = affInfo.UserName,
                    Description = PrivateGroupName,
                    QmTurnover = commGroup.QmTurnover,
                    TierSettingList = commGroup.TierSettingList,
                    PlatformFeeList = commGroup.PlatformFeeList,
                    OperationalFee = commGroup.OperationalFee,
                    BankingFee = commGroup.BankingFee,
                    ExchangeWinLossShare = commGroup.ExchangeWinLossShare,
                    ExchangeCommissionShare = commGroup.ExchangeCommissionShare,
                    Ip = Request.UserHostAddress
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
                Data = new ResultInfo
                {
                    Code = Code.GeneralErrorCode,
                    Message = platformFeeValidation.Message
                }
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
    }
}