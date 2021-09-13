using Fanex.Affiliate.Admin.Core.Attributes;
using Fanex.Affiliate.Admin.Core.Constants;
using Fanex.Affiliate.Admin.Core.DTO;
using Fanex.Affiliate.Admin.Core.DTO.ProcessingRequest;
using Fanex.Affiliate.Admin.Core.Extensions;
using Fanex.Affiliate.Admin.Core.MetaModels.ProcessingRequest;
using Fanex.Affiliate.Admin.Core.Services;
using Fanex.Affiliate.Admin.Core.Services.Common;
using Fanex.Affiliate.Admin.Core.Services.ProcessingRequest;
using Fanex.Affiliate.Admin.Core.Utilities;
using System;
using System.Linq;
using System.Web.Mvc;

namespace Fanex.Affiliate.Admin.Core.Controllers.Affiliate
{
    [FunctionAuthorize(FunctionName = Function.ProcessingRequest)]
    public class ProcessingRequestController : ReadOnlySessionBaseController
    {
        private readonly ILanguageService _languageService;
        private readonly IProcessingRequestService _processingRequestService;

        public ProcessingRequestController(
            IProcessingRequestService processingRequestService,
            ILanguageService languageService,
            ISessionService sessionService) : base(sessionService)
        {
            _languageService = languageService;
            _processingRequestService = processingRequestService;
        }

        [HttpGet]
        [AllowByPassFunctionAuthorize]
        public ActionResult Index()
        {
            var languageList = _languageService.GetLanguageList();
            return View("~/Areas/Portal/Views/Affiliate/ProcessingRequest.cshtml", languageList);
        }

        [HttpPost]
        [ValidateAntiForgeryHeader]
        [FunctionAuthorize(FunctionName = Function.ProcessingRequest)]
        public ActionResult GetProcessingRequestList(ProcessingRequestSelParam param)
        {
            if (ModelState.IsValid)
            {
                var supportLanguages = _languageService.GetInChangeMarkets();
                var queryLangs = LanguageHelper.BuildAllowQueryLanguages(null, supportLanguages);
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
                Data = new ResultInfo
                {
                    Code = 500,
                    Message = string.Join("---", msg)
                }
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
                            Code = 403,
                            Message = "Not have permission"
                        }
                    };
            }
            var msg = ModelState.Values.SelectMany(e => e.Errors)
                .Select(e => e.ErrorMessage);
            return new JsonNetResult
            {
                Data = new ResultInfo
                {
                    Code = 500,
                    Message = string.Join("---", msg)
                }
            };
        }

        private bool CheckHasUpdatePermission(ProcessingRequestDetailInfo currentRequestDetail)
        {
            var claims = SessionService.UserClaim;
            var hasManagerRole = claims.HasClaim(Function.ProcessingRequestManagementRole, Function.WritePermission);
            var hasMarketingRole = claims.HasClaim(Function.ProcessingRequestMarketingRole, Function.WritePermission);
            return hasManagerRole || (hasMarketingRole && string.Equals(currentRequestDetail.Status, ProcessingRequestStatus.DRAFT));
        }
    }
}
