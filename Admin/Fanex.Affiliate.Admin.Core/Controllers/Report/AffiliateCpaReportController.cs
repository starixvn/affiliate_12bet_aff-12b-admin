using Fanex.Affiliate.Admin.Core.Attributes;
using Fanex.Affiliate.Admin.Core.Constants;
using Fanex.Affiliate.Admin.Core.DTO;
using Fanex.Affiliate.Admin.Core.DTO.Report;
using Fanex.Affiliate.Admin.Core.MetaModels.Report;
using Fanex.Affiliate.Admin.Core.Services.Common;
using Fanex.Affiliate.Admin.Core.Services.Report;
using Fanex.Affiliate.Admin.Core.ViewModels.Report;
using System.Web.Mvc;
using Fanex.Affiliate.Admin.Core.Extensions;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Fanex.Affiliate.Admin.Core.Controllers.Report
{
    [FunctionAuthorize(FunctionName = Function.AffReport)]
    public class AffiliateCpaReportController : ReadOnlySessionBaseController
    {
        private readonly ISystemService _systemService;
        private readonly IAffiliateCpaReportService _affiliateCpaReportService;
        private readonly JsonSerializerSettings _jsonSettings = new JsonSerializerSettings
        {
            ContractResolver = new CamelCasePropertyNamesContractResolver()
        };

        public AffiliateCpaReportController(
            ISessionService sessionService,
            ISystemService systemService,
            IAffiliateCpaReportService affiliateCpaReportService) : base(sessionService)
        {
            _systemService = systemService;
            _affiliateCpaReportService = affiliateCpaReportService;
        }


        [HttpGet]
        public ActionResult Index()
        {
            var viewModel = new ReportCommonViewModel
            {
                ProductList = _systemService.GetProductList(),
                BaseCurrencyList = _systemService.GetBaseCurrencies()
            };

            return View("~/Areas/Portal/Views/Report/AffiliateCpaReport.cshtml", viewModel);
        }


        [HttpPost]
        [ValidateAntiForgeryHeader]
        public ActionResult GetData(AffiliateCpaRequest request)
        {
            if (!ModelState.IsValid)
            {
                return GenerateModelStateErrResult();
            }

            return new JsonNetResult(_jsonSettings)
            {
                Data = _affiliateCpaReportService.GetData(request)
            };
        }
    }
}
