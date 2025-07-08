namespace Fanex.Affiliate.Admin.Core.Controllers.Operation
{
    using System.Threading.Tasks;
    using System.Web.Mvc;
    using Fanex.Affiliate.Admin.Core.Attributes;
    using Fanex.Affiliate.Admin.Core.Constants;
    using Fanex.Affiliate.Admin.Core.DTO;
    using Fanex.Affiliate.Admin.Core.DTO.Email;
    using Fanex.Affiliate.Admin.Core.Extensions;
    using Fanex.Affiliate.Admin.Core.Services.Affiliate;
    using Fanex.Affiliate.Admin.Core.Services.Common;
    using Fanex.Affiliate.Admin.Core.Utilities;
    using MetaModels.Operation;
    using Services.Operation;
    using ViewModels.Operation;

    [FunctionAuthorize(FunctionName = Function.Deal)]
    public class DealAssignmentController : ReadOnlySessionBaseController
    {
        private readonly IAffiliateService _affiliateService;
        private readonly IDealAssignmentService _dealAssignmentService;
        private readonly IOperationService _operationService;

        public DealAssignmentController(
            ISessionService sessionService,
            IAffiliateService affiliateService,
            IDealAssignmentService dealAssignmentService,
            IOperationService operationService) : base(sessionService)
        {
            _affiliateService = affiliateService;
            _dealAssignmentService = dealAssignmentService;
            _operationService = operationService;
        }

        [HttpGet]
        public ActionResult AssignDeal(int affId)
        {
            var viewModel = new AssignDealViewModel();

            if (affId >= Validation.MinId)
            {
                var dealTypeList = _operationService.GetDealTypes();
                var param = new GetDealListParam { AffId = affId };
                var dealList = _dealAssignmentService.GetDealList(param);

                viewModel = new AssignDealViewModel
                {
                    DealList = dealList,
                    DealTypeList = dealTypeList,
                    AffInfo = _affiliateService.GetAffiliateBasicInfo(affId)
                };
            }

            return View("~/Areas/Portal/Views/Operation/AssignDeal.cshtml", viewModel);
        }

        [FunctionAuthorize(FunctionName = Function.Deal, IsHost = true)]
        [HttpPost]
        [ValidateAntiForgeryHeader]
        public async Task<ActionResult> AssignDeal(AssignDealParam assignParam, AffInfoParam affInfo)
        {
            if (!_dealAssignmentService.HasPermissionAssignOnAff(SessionService.UserId, affInfo.AffId))
            {
                return CreateAccessDeniedResult();
            }

            if (!ModelState.IsValid || assignParam.GroupId < Validation.MinId)
            {
                return GenerateModelStateErrResult();
            }

            assignParam.AffId = affInfo.AffId;
            AssignUserInfoParam(assignParam);

            var emailParam = new AffInformationEmail
            {
                RootUrl = SiteDomainHelper.GetRootUrl(this)
            };

            return GenerateJsonNetResult(await _dealAssignmentService.AssignDeal(assignParam, affInfo, emailParam));
        }

        private static ActionResult CreateAccessDeniedResult()
        {
            return new JsonNetResult
            {
                Data = new ResultInfo
                {
                    Code = ErrCode.ErrCodeAccessDenied,
                    Message = "You are not authorized to approve affiliate."
                }
            };
        }
    }
}