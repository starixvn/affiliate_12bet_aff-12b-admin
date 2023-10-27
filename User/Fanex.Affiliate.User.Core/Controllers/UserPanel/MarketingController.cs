namespace Fanex.Affiliate.User.Core.Controllers.UserPanel
{
    using Constants;
    using Fanex.Affiliate.User.Core.BusinessService;
    using Fanex.Affiliate.User.Core.CoreService;
    using Fanex.Affiliate.User.Core.Utilities;
    using Filters;
    using System.Web.Mvc;

    public sealed class MarketingController : BaseMarketingController
    {
        public MarketingController(
            ISessionService sessionService,
            IAppSetting appSetting,
            ISystemService languageService,
            IMarketingService marketingService
            ) : base(sessionService, appSetting, languageService, marketingService)
        {
        }

        [HttpGet]
        [AjaxOnly(PermissionName = ClaimConstant.MediaGallery)]
        public ActionResult GetLinkTo(string targetCountry, string language)
        {
            var landingPages = MarketingService.GetSpecialLandingPageList(SessionService.UserSession.GroupName, targetCountry, language, SessionService.IsInternal);

            return Json(landingPages, JsonRequestBehavior.AllowGet);
        }
    }
}