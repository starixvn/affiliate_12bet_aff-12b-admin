namespace Fanex.Affiliate.Admin.Core.Controllers.Operation
{
    using System.Web.Mvc;
    using Fanex.Affiliate.Admin.Core.Attributes;
    using Fanex.Affiliate.Admin.Core.Constants;
    using Fanex.Affiliate.Admin.Core.Extensions;
    using Fanex.Affiliate.Admin.Core.MetaModels.Operation;
    using Fanex.Affiliate.Admin.Core.Services.Common;
    using Fanex.Affiliate.Admin.Core.Services.Operation;
    using Fanex.Affiliate.Admin.Core.ViewModels.Operation;

    [FunctionAuthorize(FunctionName = Function.RevenueGroup)] // TODO: Need Review
    public class OperationController : ReadOnlySessionBaseController
    {
        private readonly IOperationService _operationService;
        private readonly ISystemService _systemService;

        public OperationController(
            ISessionService sessionService,
            IOperationService operationService,
            ISystemService systemService) : base(sessionService)
        {
            _operationService = operationService;
            _systemService = systemService;
        }

        [HttpGet]
        public ActionResult Index()
        {
            var viewModel = new OperationViewModel
            {
                DealTypes = _operationService.GetDealTypes(),
                TargetCountries = _systemService.GetTargetCountryList(),
                Products = _systemService.GetProductList()
            };

            return View("~/Areas/Portal/Views/Operation/Deal.cshtml", viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryHeader]
        public ActionResult GetDeals(DealParam param)
        {
            if (ModelState.IsValid)
            {
                return GenerateJsonNetResult(_operationService.GetDeals(param));
            }

            return GenerateModelStateErrResult();
        }

        [FunctionAuthorize(FunctionName = Function.RevenueGroup, IsHost = true)]
        [HttpPost]
        [ValidateAntiForgeryHeader]
        public ActionResult AddCpaDeal(DealSettingParam param)
        {
            if (!ModelState.IsValid)
            {
                return GenerateModelStateErrResult();
            }

            param.UserId = SessionService.UserId;
            param.Ip = Request.GetClientIP();
            param.DealName = Server.HtmlDecode(param.DealName);
            param.Description = Server.HtmlDecode(param.Description);

            return GenerateJsonNetResult(_operationService.AddCpaDeal(param));
        }
    }
}
