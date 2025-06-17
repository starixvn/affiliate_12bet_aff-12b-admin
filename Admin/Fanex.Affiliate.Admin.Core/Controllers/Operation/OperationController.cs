namespace Fanex.Affiliate.Admin.Core.Controllers.Operation
{
    using System.Web.Mvc;
    using Fanex.Affiliate.Admin.Core.Attributes;
    using Fanex.Affiliate.Admin.Core.Constants;
    using Fanex.Affiliate.Admin.Core.MetaModels.Operation;
    using Fanex.Affiliate.Admin.Core.Services.Common;
    using Fanex.Affiliate.Admin.Core.Services.Operation;
    using Fanex.Affiliate.Admin.Core.Utilities.Operation;
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
                Deductions = _operationService.GetDeductionList(),
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

        [HttpPost]
        [ValidateAntiForgeryHeader]
        public ActionResult GetDealDetail(int groupId)
        {
            if (ModelState.IsValid)
            {
                return GenerateJsonNetResult(_operationService.GetDealSettingDetail(groupId));
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

            var (isValid, message) = DealInfoValidator.Validate(param);
            if (!isValid)
            {
                return GenerateErrResult(message);
            }

            AssignUserInfoParam(param);
            param.DealName = Server.HtmlDecode(param.DealName);
            param.Description = Server.HtmlDecode(param.Description);

            return GenerateJsonNetResult(_operationService.AddCpaDeal(param));
        }

        [FunctionAuthorize(FunctionName = Function.RevenueGroup, IsHost = true)]
        [HttpPost]
        [ValidateAntiForgeryHeader]
        public ActionResult DeleteDeal(DeleteDealParam param)
        {
            if(!ModelState.IsValid)
            {
                return GenerateErrResult();
            }

            AssignUserInfoParam(param);

            return GenerateJsonNetResult(_operationService.DeleteDeal(param));
        }
    }
}
