using System.Web.Mvc;
using Fanex.Affiliate.Admin.Core.Attributes;
using Fanex.Affiliate.Admin.Core.Extensions;
using Fanex.Affiliate.Admin.Core.Repository;
using Fanex.Affiliate.Admin.Core.Services;
using Fanex.Affiliate.Admin.Core.Repository.MediaGallery;

namespace Fanex.Affiliate.Admin.Core.Controllers
{
    public class CommonSettingController : Controller
    {
        private readonly IProductRepository _productRepository;
        private readonly ILanguageService _languageService;
        private readonly ILandingPageRepository _landingPageRepository;

        public CommonSettingController(
            IProductRepository productRepository,
            ILanguageService languageService,
            ILandingPageRepository landingPageRepository
            )
        {
            _productRepository = productRepository;
            _languageService = languageService;
            _landingPageRepository = landingPageRepository;
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
        public ActionResult GetLanguageList()
        {
            return new JsonNetResult
            {
                Data = _languageService.GetLanguageList()
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
    }
}
