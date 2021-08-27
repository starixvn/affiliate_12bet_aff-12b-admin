using Fanex.Affiliate.Admin.Core.Attributes;
using Fanex.Affiliate.Admin.Core.Constants;
using Fanex.Affiliate.Admin.Core.DTO;
using Fanex.Affiliate.Admin.Core.Extensions;
using Fanex.Affiliate.Admin.Core.MetaModels.ProcessingRequest;
using Fanex.Affiliate.Admin.Core.Services;
using Fanex.Affiliate.Admin.Core.Services.Common;
using Fanex.Affiliate.Admin.Core.Services.ProcessingRequest;
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
        public ActionResult GetProcessingRequestList(ProcessingRequestSelParam param)
        {
            if (ModelState.IsValid)
            {
                return new JsonNetResult
                {
                    Data = _processingRequestService.GetProcessingRequestList(param)
                };
            }

            return new JsonNetResult();
        }

        [HttpPost]
        [ValidateAntiForgeryHeader]
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
        public ActionResult DeleteProcessingRequest(ProcessingRequestDelParam param)
        {
            return new JsonNetResult
            {
                Data = _processingRequestService.DeleteProcessingRequest(param)
            };
        }

        [HttpPost]
        [ValidateAntiForgeryHeader]
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
        public ActionResult UpdateProcessingRequest(ProcessingRequestUpdParam param)
        {
            if (ModelState.IsValid)
            {
                return new JsonNetResult
                {
                    Data = _processingRequestService.UpdateProcessingRequest(param)
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
    }
}
