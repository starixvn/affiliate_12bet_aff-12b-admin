using Fanex.Affiliate.Admin.Core.Attributes;
using Fanex.Affiliate.Admin.Core.Extensions;
using Fanex.Affiliate.Admin.Core.Services.Common;
using Fanex.Affiliate.Admin.Core.Repository.MediaGallery;
using Fanex.Affiliate.Admin.Core.Services.UserPanel;
using ONEbook.UM.AspNetMVC.Filters;
using System.Web.Mvc;

namespace Fanex.Affiliate.Admin.Core.Controllers
{
    [CustomAuthorize]
    [CheckUMFilter]
    public class CommonSettingController : Controller
    {
        private readonly ICommonSettingService _commonSettingService;
        private readonly ILandingPageRepository _landingPageRepository;

        public CommonSettingController(
            ICommonSettingService commonSettingService,
            ILandingPageRepository landingPageRepository
            )
        {
            _commonSettingService = commonSettingService;
            _landingPageRepository = landingPageRepository;
        }

        [HttpPost]
        [ValidateAntiForgeryHeader]
        public ActionResult GetProductList()
        {
            return new JsonNetResult
            {
                Data = _commonSettingService.GetProductList()
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
        public ActionResult GetCountryList()
        {
            return new JsonNetResult
            {
                Data = _commonSettingService.GetCountryList()
            };
        }
    }
}