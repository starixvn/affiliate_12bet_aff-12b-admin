namespace Fanex.Affiliate.User.Core.Controllers.UserPanel
{
    using Constants;
    using Fanex.Affiliate.User.Core.BusinessService;
    using Fanex.Affiliate.User.Core.CoreService;
    using Fanex.Affiliate.User.Core.DTO.UserPanel.MediaGallery;
    using Fanex.Affiliate.User.Core.MetalModels.UserPanel;
    using Fanex.Affiliate.User.Core.Utilities;
    using Filters;
    using System.Collections.Generic;
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
        public ActionResult GetFiltersByCountry(SizeParam param, bool shouldGetSize)
        {
            var domainList = MarketingService.GetDomainListByTargetCountry(param.TargetCountry, SessionService.IsInternal);
            var languageList = SystemService.GetSystemTargetLanguagesByCountry(param.TargetCountry);

            var targetLanguageCode = languageList.FirstOrDefault()?.LanguageCode ?? SystemLanguage.DefaultLanguageCode;
            var pageList = MarketingService.GetSpecialLandingPageList(param.TargetCountry, SessionService.UserSession.GroupName, targetLanguageCode, SessionService.IsInternal);

            param.Language = targetLanguageCode;
            var sizeList = shouldGetSize ? MarketingService.GetSizeList(param, SessionService.IsInternal) : new List<SizeType>();

            return Json(
                new {
                    domainList,
                    languageList = SystemHelper.GetTargetLanguageList(languageList),
                    pageList,
                    sizeList
                }, JsonRequestBehavior.AllowGet);
        }
    }
}