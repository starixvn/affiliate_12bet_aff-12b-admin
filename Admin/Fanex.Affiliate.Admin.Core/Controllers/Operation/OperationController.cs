namespace Fanex.Affiliate.Admin.Core.Controllers.Operation
{
    using System.Web.Mvc;
    using Fanex.Affiliate.Admin.Core.Attributes;
    using Fanex.Affiliate.Admin.Core.Constants;
    using Fanex.Affiliate.Admin.Core.MetaModels.Operation;
    using Fanex.Affiliate.Admin.Core.Services.Common;
    using Fanex.Affiliate.Admin.Core.Services.Operation;

    [FunctionAuthorize(FunctionName = Function.RevenueGroup)] // TODO: Need Review
    public class OperationController : ReadOnlySessionBaseController
    {
        private readonly IOperationService _operationService;

        public OperationController(
            ISessionService sessionService, 
            IOperationService operationService) : base(sessionService)
        {
            _operationService = operationService;
        }

        [HttpGet]
        public ActionResult Index()
        {
            var dealTypes = _operationService.GetDealTypes();
            return View("~/Areas/Portal/Views/Operation/Deal.cshtml", dealTypes);
        }

        [HttpPost]
        [ValidateAntiForgeryHeader]
        public ActionResult GetDeals(DealParam param)
        {
            if(ModelState.IsValid)
            {
                return GenerateJsonNetResult(_operationService.GetDeals(param));
            }

            return GenerateModelStateErrResult();
        }
    }
}
