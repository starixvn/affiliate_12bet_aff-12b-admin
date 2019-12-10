using System.Web.Mvc;
using Fanex.Affiliate.Admin.Core.Attributes;
using Fanex.Affiliate.Admin.Core.Constants;
using Fanex.Affiliate.Admin.Core.Extensions;
using Fanex.Affiliate.Admin.Core.MetaModels.Report;
using Fanex.Affiliate.Admin.Core.Repository;
using Fanex.Affiliate.Admin.Core.Services;
using Fanex.Affiliate.Admin.Core.Services.Common;
using Fanex.Affiliate.Admin.Core.Services.Report;
using Fanex.Affiliate.Admin.Core.ViewModels;

namespace Fanex.Affiliate.Admin.Core.Controllers
{
    [FunctionAuthorize(FunctionName = Function.AffReport)]
    public class ReportController : BaseController
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
                LanguageList = _languageService.GetLanguageList()
            };

            return View(viewModel);
        }


        [HttpPost]
        [ValidateAntiForgeryHeader]
        public ActionResult GetMainData(AffiliateReportMainParam param)
        {
            return new JsonNetResult
            {
                Data = _affiliateReportService.GetMainReportData(param)
            };
        }

        [HttpPost]
        [ValidateAntiForgeryHeader]
        public ActionResult GetDetailData(AffiliateReportMainParam param)
        {
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
    }
}
