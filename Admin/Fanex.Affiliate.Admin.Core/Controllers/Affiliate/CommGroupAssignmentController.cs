using Fanex.Affiliate.Admin.Core.Attributes;
using Fanex.Affiliate.Admin.Core.Constants;
using Fanex.Affiliate.Admin.Core.DTO.Affiliate;
using Fanex.Affiliate.Admin.Core.DTO.Email;
using Fanex.Affiliate.Admin.Core.Extensions;
using Fanex.Affiliate.Admin.Core.MetaModels.CommissionGroup;
using Fanex.Affiliate.Admin.Core.Services.CommGroup;
using Fanex.Affiliate.Admin.Core.Services.Common;
using Fanex.Affiliate.Admin.Core.Utilities;
using Fanex.Affiliate.Admin.Core.ViewModels;
using System.Web.Mvc;

namespace Fanex.Affiliate.Admin.Core.Controllers.Affiliate
{
    [MultiFunctionAuthorize(FunctionName = Function.AssignCommGroup, FunctionNameOption = Function.ManagePendingAff)]
    public class CommGroupAssignmentController : ReadOnlySessionBaseController
    {
        private readonly ICommGroupAssignmentService _commGroupAssignmentService;

        public CommGroupAssignmentController(
            ISessionService sessionService,
            ICommGroupAssignmentService commGroupAssignmentService) : base(sessionService)
        {
            _commGroupAssignmentService = commGroupAssignmentService;
        }

        [HttpGet]
        public ActionResult AssignGroup(int affId)
        {
            if (!_commGroupAssignmentService.HasReadFunctionOrManagePendingAffPermission(affId, Function.AssignCommGroup))
            {
                return Redirect("~/Portal/AccessDenied");
            }

            var affInfo = _commGroupAssignmentService.GetAffiliateBasicInfo(affId);
            var hasHostAssignCommGroupPermission = _commGroupAssignmentService.HasHostFunctionPermission(Function.AssignCommGroup);
            var hasHostManagePendingAffPermission = _commGroupAssignmentService.HasHostManagePendingAffPermission(affId, affInfo.Status);
            var param = new GetGroupListParam { AffId = affId };
            var commGroupList = _commGroupAssignmentService.GetNormalCommGroupList(param);

            var viewModel = new AssignNormalGroupViewModel
            {
                CommGroupList = (hasHostManagePendingAffPermission && !hasHostAssignCommGroupPermission) ? ManageCommGroup.GetManageCommGroups(commGroupList) : commGroupList,
                SelectedGroup = param.GroupId,
                HasCommGroupVisible = _commGroupAssignmentService.HasReadFunctionPermission(Function.CommissionGroup),
                AffInfo = affInfo
            };

            return View("~/Areas/Portal/Views/Affiliate/AssignGroup.cshtml", viewModel);
        }

        [MultiFunctionAuthorize(FunctionName = Function.AssignCommGroup, FunctionNameOption = Function.ManagePendingAff, IsHost = true)]
        [HttpPost]
        [ValidateAntiForgeryHeader]
        public ActionResult AssignGroup(AssignGroupParam assignParam, AffiliateBasicInfo affInfo)
        {
            if (!ModelState.IsValid || assignParam.GroupId < Validation.MinId)
            {
                return ModelState.GenerateErrorResult();
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
    }
}