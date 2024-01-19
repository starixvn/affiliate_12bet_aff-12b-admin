namespace Fanex.Affiliate.User.Core.Controllers.UserPanel
{
    using Constants;
    using Fanex.Affiliate.User.Core.BusinessService;
    using Fanex.Affiliate.User.Core.CoreService;
    using Fanex.Affiliate.User.Core.Utilities;
    using Filters;
    using System.Linq;
    using System.Web.Mvc;

    public sealed class MarketingController : BaseMarketingController
    {
        public MarketingController(
            ISessionService sessionService,
            ISystemService systemService,
            IMarketingService marketingService
            ) : base(sessionService, systemService, marketingService)
        {
        }

        [HttpGet]
        [AjaxOnly(PermissionName = ClaimConstant.MediaGallery)]
        public ActionResult GetLinkTo(string targetCountry)
        {
            var targetLanguageListByCountry = SystemService.GetSystemTargetLanguagesByCountry(targetCountry);
            var targetLanguageCode = targetLanguageListByCountry.FirstOrDefault()?.LanguageCode ?? SystemLanguage.DefaultLanguageCode;

            var landingPages = MarketingService.GetSpecialLandingPageList(targetCountry, SessionService.UserSession.GroupName, targetLanguageCode, SessionService.IsInternal);

            return Json(landingPages, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        [AjaxOnly(PermissionName = ClaimConstant.MediaGallery)]
        public ActionResult GetAvailableDomains(string targetCountry)
        {
            var landingPages = MarketingService.GetDomainListByTargetCountry(targetCountry, SessionService.IsInternal);

            return Json(landingPages, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        [AjaxOnly(PermissionName = ClaimConstant.MediaGallery)]
        public ActionResult GetLanguagesByTargetCountry(string targetCountry)
        {
            var targetLanguageListByCountry = SystemService.GetSystemTargetLanguagesByCountry(targetCountry);

            return Json(targetLanguageListByCountry, JsonRequestBehavior.AllowGet);
        }
    }
}