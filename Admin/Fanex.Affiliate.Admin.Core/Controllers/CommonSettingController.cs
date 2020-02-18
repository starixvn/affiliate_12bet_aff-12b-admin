using System.Web.Mvc;
using Fanex.Affiliate.Admin.Core.Attributes;
using Fanex.Affiliate.Admin.Core.Extensions;
using Fanex.Affiliate.Admin.Core.Repository;
using Fanex.Affiliate.Admin.Core.Repository.MediaGallery;
using Fanex.Affiliate.Admin.Core.Services.UserPanel;
using ONEbook.UM.Filters;
using Fanex.Affiliate.Admin.Core.Constants;

namespace Fanex.Affiliate.Admin.Core.Controllers
{
    [CustomAuthorize]
    [CheckUMFilter]
    [FunctionAuthorize(FunctionName = Function.Affiliate)]
    public class CommonSettingController : Controller
    {
        private readonly IProductRepository _productRepository;
        private readonly ILandingPageRepository _landingPageRepository;
        private readonly ITrackingStatisticsService _trackingStatisticsService;

        public CommonSettingController(
            IProductRepository productRepository,
            ILandingPageRepository landingPageRepository,
            ITrackingStatisticsService trackingStatisticsService
            )
        {
            _productRepository = productRepository;
            _landingPageRepository = landingPageRepository;
            _trackingStatisticsService = trackingStatisticsService;
        }

        [HttpPost]
        [ValidateAntiForgeryHeader]
        public ActionResult GetProductList()
        {
            return new JsonNetResult
            {
                Data = _productRepository.GetProductList()
            };
        }

        [HttpPost]
        [ValidateAntiForgeryHeader]
        public ActionResult GetPageList()
        {
            return new JsonNetResult
            {
                Data = _landingPageRepository.GetLandingPageList()
            };
        }

        [HttpPost]
        [ValidateAntiForgeryHeader]
        public ActionResult GetTrackingCodeList(int affId)
        {
            return new JsonNetResult
            {
                Data = _trackingStatisticsService.GetTrackingCodeList(affId)
            };
        }
    }
}