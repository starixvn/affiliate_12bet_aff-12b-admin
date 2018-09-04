using System.Web.Mvc;
using Fanex.Affiliate.Admin.Core.Attributes;
using Fanex.Affiliate.Admin.Core.Constants;
using Fanex.Affiliate.Admin.Core.MetaModels.Report;
using Fanex.Affiliate.Admin.Core.Repository;
using Fanex.Affiliate.Admin.Core.Services.Common;
using Fanex.Affiliate.Admin.Core.Services.Report;
using Fanex.Affiliate.Admin.Core.ViewModels;

namespace Fanex.Affiliate.Admin.Core.Controllers
{
    [FunctionAuthorize(FunctionName = Function.AffReport)]
    public class AffiliateReportController : BaseController
    {
        private readonly IProductRepository _productRepository;

        private readonly IAffiliateReportService _affiliateReportService;

        public AffiliateReportController(
            ISessionService sessionService,
            IProductRepository productRepository,
            IAffiliateReportService affiliateReportService) : base(sessionService)
        {
            _productRepository = productRepository;
            _affiliateReportService = affiliateReportService;
        }

        public ActionResult Index()
        {
            var viewModel = new AffiliateReportViewModel
            {
                ProductList = _productRepository.GetProductList()
            };

            return View("~/Areas/Portal/Views/AffiliateReport/Index.cshtml", viewModel);
        }

        [HttpPost]
        public ActionResult GetMainData(AffiliateReportMainParam param)
        {
            return Json(_affiliateReportService.GetMainReportData(param));
        }
    }
}
