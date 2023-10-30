using Fanex.Affiliate.Admin.Core.Attributes;
using Fanex.Affiliate.Admin.Core.Constants;
using Fanex.Affiliate.Admin.Core.Extensions;
using Fanex.Affiliate.Admin.Core.MetaModels.Report;
using Fanex.Affiliate.Admin.Core.Services;
using Fanex.Affiliate.Admin.Core.Services.Common;
using Fanex.Affiliate.Admin.Core.Services.Report;
using Fanex.Affiliate.Admin.Core.Utilities;
using Fanex.Affiliate.Admin.Core.ViewModels;
using System.Web.Mvc;

namespace Fanex.Affiliate.Admin.Core.Controllers
{
    [FunctionAuthorize(FunctionName = Function.ReportMenu)]
    public class ReportController : ReadOnlySessionBaseController
    {
        private readonly ISystemService _commonSettingService;

        private readonly IAffiliateReportService _affiliateReportService;

        public ReportController(
            ISessionService sessionService,
            ISystemService commonSettingService,
            IAffiliateReportService affiliateReportService) : base(sessionService)
        {
            _commonSettingService = commonSettingService;
            _affiliateReportService = affiliateReportService;
        }


        [HttpGet]
        public ActionResult Index()
        {
            var viewModel = new AffiliateReportViewModel
            {
                ProductList = _commonSettingService.GetProductList(),
                LanguageList = _commonSettingService.GetInChangeMarkets()
            };

            return View(viewModel);
        }


        [HttpPost]
        [ValidateAntiForgeryHeader]
        [UpdateUnauthorizedProperty]
        public ActionResult GetMainData(GetReportMainDataViewModel model)
        {
            var param = BuildParamFromModel(model);
            return new JsonNetResult
            {
                Data = _affiliateReportService.GetMainReportData(param)
            };
        }

        [HttpPost]
        [ValidateAntiForgeryHeader]
        [UpdateUnauthorizedProperty]
        public ActionResult GetDetailData(GetReportMainDataViewModel model)
        {
            var param = BuildParamFromModel(model);
            return new JsonNetResult
            {
                Data = _affiliateReportService.GetDetailReportData(param)
            };
        }

        [HttpGet]
        public ActionResult GetAffiliateList(string query)
        {
            return Json(_affiliateReportService.SearchAffiliateList(query.Trim()), JsonRequestBehavior.AllowGet);
        }

        private AffiliateReportMainParam BuildParamFromModel(GetReportMainDataViewModel model)
            => new AffiliateReportMainParam
            {
                FromDate = model.FromDate,
                IsExportExcel = model.IsExportExcel,
                IsGetAll = model.IsGetAll,
                IsGetMonthly = model.IsGetMonthly,
                PageIndex = model.PageIndex,
                PageSize = model.PageSize,
                PreferredLanguages = SystemHelper.BuildAllowQueryLanguages(model.PreferredLanguage, _commonSettingService.GetInChangeMarkets()),
                ProductIds = model.ProductIds,
                SortDirectionType = model.SortDirectionType,
                SortType = model.SortType,
                ToDate = model.ToDate,
                UserName = model.UserName?.Trim()
            };
    }
}
