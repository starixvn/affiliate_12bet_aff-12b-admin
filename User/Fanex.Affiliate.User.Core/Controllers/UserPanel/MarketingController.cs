namespace Fanex.Affiliate.User.Core.Controllers.UserPanel
{
    using Constants;
    using Fanex.Affiliate.User.Core.BusinessService;
    using Fanex.Affiliate.User.Core.CoreService;
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
        public ActionResult GetLinkTo(string targetCountry, string language)
        {
            var landingPages = MarketingService.GetSpecialLandingPageList(targetCountry, SessionService.UserSession.GroupName, language, SessionService.IsInternal);

            return Json(landingPages, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        [AjaxOnly(PermissionName = ClaimConstant.MediaGallery)]
        public ActionResult GetFiltersByCountry(string targetCountry)
        {
            var domainList = MarketingService.GetDomainListByTargetCountry(targetCountry, SessionService.IsInternal);
            var languageList = SystemService.GetSystemTargetLanguagesByCountry(targetCountry);

            var targetLanguageCode = languageList.FirstOrDefault()?.LanguageCode ?? SystemLanguage.DefaultLanguageCode;
            var pageList = MarketingService.GetSpecialLandingPageList(targetCountry, SessionService.UserSession.GroupName, targetLanguageCode, SessionService.IsInternal);

            return Json(new { domainList, languageList, pageList }, JsonRequestBehavior.AllowGet);
        }
    }
}