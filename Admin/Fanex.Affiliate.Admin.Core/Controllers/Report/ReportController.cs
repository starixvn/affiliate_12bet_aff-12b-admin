using Fanex.Affiliate.Admin.Core.Attributes;
using Fanex.Affiliate.Admin.Core.Constants;
using Fanex.Affiliate.Admin.Core.MetaModels.CommissionGroup;
using Fanex.Affiliate.Admin.Core.MetaModels.Report;
using Fanex.Affiliate.Admin.Core.Services.CommGroup;
using Fanex.Affiliate.Admin.Core.Services.Common;
using Fanex.Affiliate.Admin.Core.Services.Report;
using Fanex.Affiliate.Admin.Core.ViewModels.Report;
using System.Web.Mvc;

namespace Fanex.Affiliate.Admin.Core.Controllers.Report
{
    [FunctionAuthorize(FunctionName = Function.AffReport)]
    public class ReportController : ReadOnlySessionBaseController
    {
        private readonly ISystemService _systemService;

        private readonly IAffiliateReportService _affiliateReportService;

        private readonly ICommGroupCommonService _commGroupCommonService;

        public ReportController(
            ISessionService sessionService,
            ISystemService systemService,
            IAffiliateReportService affiliateReportService,
            ICommGroupCommonService commGroupCommonService) : base(sessionService)
        {
            _systemService = systemService;
            _affiliateReportService = affiliateReportService;
            _commGroupCommonService = commGroupCommonService;
        }


        [HttpGet]
        [AllowByPassFunctionAuthorize]
        public ActionResult Index()
        {
            var viewModel = new AffiliateReportViewModel
            {
                ReportCutOffDate = _affiliateReportService.GetReportCutOffDate(),
                ProductList = _systemService.GetProductList(),
                LanguageList = _systemService.GetLanguageList(),
                CurrencyList = _systemService.GetCurrencies(),
                BaseCurrencyList = _systemService.GetBaseCurrencies(),
                PaymentCurrencyList = _systemService.GetPaymentCurrencies(),
                CommGroupList = _commGroupCommonService.GetNormalCommGroupList(new GetGroupListParam()),
            };

            return View(viewModel);
        }


        [HttpPost]
        [ValidateAntiForgeryHeader]
        public ActionResult GetMainData(AffiliateReportParam param)
        {
            param.PreferredLanguage = _systemService.ConvertSelectedLanguageCodes(param.PreferredLanguage);

            return GenerateJsonNetResult(_affiliateReportService.GetMainReportData(param));
        }

        [HttpPost]
        [ValidateAntiForgeryHeader]
        public ActionResult GetDetailData(AffiliateReportParam param)
        {
            return GenerateJsonNetResult(_affiliateReportService.GetDetailReportData(param));
        }
    }
}
