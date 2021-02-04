using Fanex.Affiliate.Admin.Core.Attributes;
using Fanex.Affiliate.Admin.Core.Constants;
using Fanex.Affiliate.Admin.Core.Extensions;
using Fanex.Affiliate.Admin.Core.MetaModels.Report;
using Fanex.Affiliate.Admin.Core.Repository;
using Fanex.Affiliate.Admin.Core.Services;
using Fanex.Affiliate.Admin.Core.Services.Common;
using Fanex.Affiliate.Admin.Core.Services.Report;
using Fanex.Affiliate.Admin.Core.Utilities;
using Fanex.Affiliate.Admin.Core.ViewModels;
using System.Web.Mvc;

namespace Fanex.Affiliate.Admin.Core.Controllers
{
    [FunctionAuthorize(FunctionName = Function.AffReport)]
    public class ReportController : ReadOnlySessionBaseController
    {
        private readonly IProductRepository _productRepository;

        private readonly ILanguageService _languageService;

        private readonly IAffiliateReportService _affiliateReportService;

        public ReportController(
            ISessionService sessionService,
            IProductRepository productRepository,
            ILanguageService languageService,
            IAffiliateReportService affiliateReportService) : base(sessionService)
        {
            _productRepository = productRepository;
            _languageService = languageService;
            _affiliateReportService = affiliateReportService;
        }


        [HttpGet]
        public ActionResult Index()
        {
            var viewModel = new AffiliateReportViewModel
            {
                ProductList = _productRepository.GetProductList(),
                LanguageList = _languageService.GetInChangeMarkets()
            };

            return View(viewModel);
        }


        [HttpPost]
        [ValidateAntiForgeryHeader]
        [ResetDefaultUnauthorizedProperty]
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
        [ResetDefaultUnauthorizedProperty]
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
                PageIndex = model.PageIndex,
                PageSize = model.PageSize,
                PreferredLanguages = LanguageHelper.BuildAllowQueryLanguages(model.PreferredLanguage, _languageService.GetInChangeMarkets()),
                ProductIds = model.ProductIds,
                SortDirectionType = model.SortDirectionType,
                SortType = model.SortType,
                ToDate = model.ToDate,
                UserName = model.UserName.Trim()
            };
    }
}
