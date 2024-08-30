using Fanex.Affiliate.Admin.Core.Attributes;
using Fanex.Affiliate.Admin.Core.Constants;
using Fanex.Affiliate.Admin.Core.DTO;
using Fanex.Affiliate.Admin.Core.DTO.ProcessingRequest;
using Fanex.Affiliate.Admin.Core.Extensions;
using Fanex.Affiliate.Admin.Core.MetaModels.ProcessingRequest;
using Fanex.Affiliate.Admin.Core.Services.Common;
using Fanex.Affiliate.Admin.Core.Services.ProcessingRequest;
using Fanex.Affiliate.Admin.Core.Utilities;
using System.Linq;
using System.Web.Mvc;

namespace Fanex.Affiliate.Admin.Core.Controllers.Affiliate
{
    [FunctionAuthorize(FunctionName = Function.ProcessingRequest)]
    public class ProcessingRequestController : ReadOnlySessionBaseController
    {
        private readonly ISystemService _systemService;
        private readonly IProcessingRequestService _processingRequestService;

        public ProcessingRequestController(
            IProcessingRequestService processingRequestService,
            ISystemService systemService,
            ISessionService sessionService) : base(sessionService)
        {
            _systemService = systemService;
            _processingRequestService = processingRequestService;
        }

        [HttpGet]
        [AllowByPassFunctionAuthorize]
        public ActionResult Index(string userName)
        {
            var languageList = _systemService.GetTargetLanguageListByUser();
            ViewBag.Languages = languageList;
            ViewBag.UserName = userName;
            return View("~/Areas/Portal/Views/Affiliate/ProcessingRequest.cshtml");
        }

        [HttpPost]
        [ValidateAntiForgeryHeader]
        [FunctionAuthorize(FunctionName = Function.ProcessingRequest)]
        public ActionResult GetProcessingRequestList(ProcessingRequestSelParam param)
        {
            if (ModelState.IsValid)
            {
                var supportLanguages = _systemService.GetInChangeMarkets();
                var queryLangs = SystemHelper.BuildAllowQueryLanguages(null, supportLanguages);
                param.PreferredLanguages = queryLangs;
                return new JsonNetResult
                {
                    Data = _processingRequestService.GetProcessingRequestList(param)
                };
            }

            return new JsonNetResult();
        }

        [HttpPost]
        [ValidateAntiForgeryHeader]
        [FunctionAuthorize(FunctionName = Function.ProcessingRequest)]
        public ActionResult GetProcessingRequestDetail(int processId)
        {
            var result = new JsonNetResult
            {
                Data = _processingRequestService.GetProcessingRequestDetail(processId)
            };
            return result;
        }

        [HttpPost]
        [ValidateAntiForgeryHeader]
        [FunctionAuthorize(FunctionName = Function.ProcessingRequest, IsHost = true)]
        public ActionResult DeleteProcessingRequest(ProcessingRequestDelParam param)
        {
            return new JsonNetResult
            {
                Data = _processingRequestService.DeleteProcessingRequest(param)
            };
        }

        [HttpPost]
        [ValidateAntiForgeryHeader]
        [FunctionAuthorize(FunctionName = Function.ProcessingRequest, IsHost = true)]
        public ActionResult InsertProcessingRequest(ProcessingRequestInsParam param)
        {
            if (ModelState.IsValid)
            {
                return new JsonNetResult
                {
                    Data = _processingRequestService.AddProcessingRequest(param)
                };
            }
            var msg = ModelState.Values.SelectMany(e => e.Errors)
                .Select(e => e.ErrorMessage);
            return new JsonNetResult
            {
                Data = ResultInfo.GenerateErr(string.Join("---", msg))
            };
        }

        [HttpPost]
        [ValidateAntiForgeryHeader]
        [FunctionAuthorize(FunctionName = Function.ProcessingRequest, IsHost = true)]
        public ActionResult UpdateProcessingRequest(ProcessingRequestUpdParam param)
        {
            if (ModelState.IsValid)
            {
                var currentRequestDetail = _processingRequestService.GetProcessingRequestDetail(param.RequestId);
                return CheckHasUpdatePermission(currentRequestDetail)
                    ? new JsonNetResult
                    {
                        Data = _processingRequestService.UpdateProcessingRequest(param)
                    }
                    : new JsonNetResult
                    {
                        Data = new ResultInfo
                        {
                            Code = Code.ErrCodeAccessDenied,
                            Message = "Not have permission"
                        }
                    };
            }
            var msg = ModelState.Values.SelectMany(e => e.Errors)
                .Select(e => e.ErrorMessage);
            return new JsonNetResult
            {
                Data = ResultInfo.GenerateErr(string.Join("---", msg))
            };
        }

        private bool CheckHasUpdatePermission(ProcessingRequestDetailInfo currentRequestDetail)
        {
            var claims = SessionService.UserClaim;
            var hasManagerRole = claims.HasClaim(Function.ProcessingRequestManagementRole, Function.WritePermission);
            var hasMarketingRole = claims.HasClaim(Function.ProcessingRequestMarketingRole, Function.WritePermission);
            return hasManagerRole
                   || (hasMarketingRole && (string.Equals(currentRequestDetail.Status, ProcessingRequestStatus.DRAFT) || string.Equals(currentRequestDetail.Status, ProcessingRequestStatus.KIV)));
        }
    }
}
