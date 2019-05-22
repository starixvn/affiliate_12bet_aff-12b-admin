using System.Web.Mvc;
using Fanex.Affiliate.Admin.Core.Attributes;
using Fanex.Affiliate.Admin.Core.Extensions;
using Fanex.Affiliate.Admin.Core.Repository;
using Fanex.Affiliate.Admin.Core.Repository.MediaGallery;
using Fanex.Affiliate.Admin.Core.Repository.UserPanel;

namespace Fanex.Affiliate.Admin.Core.Controllers
{
    public class CommonSettingController : Controller
    {
        private readonly IProductRepository _productRepository;
        private readonly ILandingPageRepository _landingPageRepository;
        private readonly ITrackingStatisticsRepository _trackingStatisticsRepository;

        public CommonSettingController(
            IProductRepository productRepository,
            ILandingPageRepository landingPageRepository,
            ITrackingStatisticsRepository trackingStatisticsRepository
            )
        {
            _productRepository = productRepository;
            _landingPageRepository = landingPageRepository;
            _trackingStatisticsRepository = trackingStatisticsRepository;
        }

        [HttpPost]
        [ValidateAntiForgeryHeader]
        [Compress]
        public ActionResult GetProductList()
        {
            return new JsonNetResult
            {
                Data = _productRepository.GetProductList()
            };
        }

        [HttpPost]
        [ValidateAntiForgeryHeader]
        [Compress]
        public ActionResult GetPageList()
        {
            return new JsonNetResult
            {
                Data = _landingPageRepository.GetLandingPageList()
            };
        }

        [HttpPost]
        [ValidateAntiForgeryHeader]
        [Compress]
        public ActionResult GetTrackingCodeList(int affId)
        {
            return new JsonNetResult
            {
                Data = _trackingStatisticsRepository.GetTrackingCodeList(affId)
            };
        }
    }
}