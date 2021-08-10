using Fanex.Affiliate.Admin.Core.Attributes;
using Fanex.Affiliate.Admin.Core.Constants;
using Fanex.Affiliate.Admin.Core.Services.Common;
using System.Web.Mvc;

namespace Fanex.Affiliate.Admin.Core.Controllers.Affiliate
{
    [FunctionAuthorize(FunctionName = Function.ProcessingRequest)]
    public class ProcessingRequestController : ReadOnlySessionBaseController
    {
        public ProcessingRequestController(
            ISessionService sessionService) : base(sessionService)
        {
        }

        [HttpGet]
        [AllowByPassFunctionAuthorize]
        public ActionResult Index()
        {
            return View();
        }
    }
}
