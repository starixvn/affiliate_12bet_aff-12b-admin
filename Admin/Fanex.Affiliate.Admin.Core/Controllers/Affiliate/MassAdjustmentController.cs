using Fanex.Affiliate.Admin.Core.Attributes;
using Fanex.Affiliate.Admin.Core.Constants;
using Fanex.Affiliate.Admin.Core.Services.Common;
using Fanex.Affiliate.Admin.Core.Services.MassAdjustment;
using System.Web.Mvc;

namespace Fanex.Affiliate.Admin.Core.Controllers.Affiliate
{
    [FunctionAuthorize(FunctionName = Function.AffAdjustment)]
    public class MassAdjustmentController : ReadOnlySessionBaseController
    {
        private readonly IMassAdjustmentService _massAdjustmentService;

        public MassAdjustmentController(
            ISessionService sessionService,
            IMassAdjustmentService massAdjustmentService) : base(sessionService)
        {
            _massAdjustmentService = massAdjustmentService;
        }

        [HttpGet]
        [AllowByPassFunctionAuthorize]
        public ActionResult Index()
        {
            return View("~/Areas/Portal/Views/Affiliate/MassAdjustment.cshtml");
        }
    }
}
