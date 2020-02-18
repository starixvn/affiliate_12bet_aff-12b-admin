namespace Fanex.Affiliate.User.Core.Controllers.UserPanel
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;
    using Constants;
    using DTO.UserPanel;
    using Extensions;
    using Fanex.Affiliate.User.Core.BusinessService;
    using Fanex.Affiliate.User.Core.CoreService;
    using Fanex.Affiliate.User.Core.Repository.UserPanel;
    using Fanex.Affiliate.User.Core.Utilities;
    using Filters;
    using MetalModels.UserPanel;

    [CustomAuthorize]
    public class OldMarketingController : BaseController
    {
        private readonly IOldMarketingService _oldMarketingService;
        private readonly IAppSetting _appSetting;
        public OldMarketingController(ISessionService sessionService, IAppSetting appSetting,
            ILanguageService languageService,
            IOldMarketingService oldMarketingService) : base(sessionService, languageService)
        {

            _oldMarketingService = oldMarketingService;
            _appSetting = appSetting;
        }

        [HttpGet]
        [AjaxOnly]
        public ActionResult GetSubCategory(string category)
        {
            var type = "CategoryMedia" + category;
            var subCategoryList = _oldMarketingService.GetBannerTypeList(SessionService.IsInternal)
                                                    .Where(t => t.Type.MatchWith(type));

            return Json(subCategoryList, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        [AjaxOnly]
        public ActionResult GetBanner(OldMarketingBannerParam bannerParam)
        {
            var userSession = SessionService.UserSession;
            var selectedDomain = _oldMarketingService.GetDomainList(userSession.AffId, SessionService.IsInternal)
                                                    .FirstOrDefault(d => d.DomainId == bannerParam.SelectedDomainId);
            if (!string.IsNullOrEmpty(bannerParam.SubCategory))
            {
                bannerParam.Category = bannerParam.SubCategory;
            }

            var bannerSettingKey = userSession.PreferredLanguage.Replace("-", string.Empty).ToUpperInvariant();

            bannerParam.AffId = userSession.AffId;
            bannerParam.AffType = userSession.AffType;
            bannerParam.SelectedDomain = selectedDomain;
            bannerParam.TextLinkTemplate = _appSetting.TextLinkTemplate;
            bannerParam.BannerDomain = _appSetting.GetValue($"BannerDomain{bannerSettingKey}") ??
                _appSetting.BannerDomain;
            bannerParam.Language = SystemLanguage.LanguageList.FirstOrDefault(l => l.Code.MatchWith(bannerParam.Language))?.EnglishName;

            var bannerList = GetBannerList(bannerParam);
            ViewBag.TotalItem = bannerParam.Nrows;

            return View("~/Views/UserPanel/Marketing/MarketingPartial/_BannerList.cshtml", bannerList);
        }

        [HttpGet]
        [AjaxOnly]
        public ActionResult GetText(OldMarketingTextParam textParam)
        {
            var userSession = SessionService.UserSession;
            var selectedDomain = _oldMarketingService.GetDomainList(userSession.AffId, SessionService.IsInternal)
                                                    .FirstOrDefault(d => d.DomainId == textParam.SelectedDomainId);

            textParam.AffId = userSession.AffId;
            textParam.SelectedDomain = selectedDomain;
            textParam.TextLinkTemplate = _appSetting.TextLinkTemplate;
            textParam.ScriptUrl = _appSetting.MediaScriptUrl;
            textParam.Language = SystemLanguage.LanguageList.FirstOrDefault(l => l.Code.MatchWith(textParam.Language))?.EnglishName;

            var textList = GetTextList(textParam);
            ViewBag.TotalItem = textParam.Nrows;

            return View("~/Views/UserPanel/Marketing/MarketingPartial/_TextList.cshtml", textList);
        }

        [HttpGet]
        [AjaxOnly]
        public ActionResult GenerateLink(string selectedLanguage, int selectedDomainId, string selectedPage)
        {
            var userSession = SessionService.UserSession;
            var selectedDomain = _oldMarketingService.GetDomainList(userSession.AffId, SessionService.IsInternal)
                                                    .FirstOrDefault(d => d.DomainId == selectedDomainId);
            var bannerTypeList = _oldMarketingService.GetBannerTypeList(SessionService.IsInternal);
            var languageList = string.IsNullOrEmpty(selectedLanguage) ?
                OldMarketingHelper.GetLanguageList(bannerTypeList, userSession.PreferredLanguage) : null;

            var textLinkList = GetTextLinkList(selectedLanguage, selectedPage, selectedDomain, languageList);

            return View("~/Views/UserPanel/Marketing/MarketingPartial/_TextLink.cshtml", textLinkList);
        }

        private IEnumerable<MarketingItem> GetBannerList(OldMarketingBannerParam bannerParam)
        {
            if (bannerParam.SelectedDomain == null)
            {
                return new List<MarketingItem>();
            }

            return _oldMarketingService.GetBannerList(bannerParam);
        }

        private IEnumerable<MarketingItem> GetTextList(OldMarketingTextParam textParam)
        {
            if (textParam.SelectedDomain == null)
            {
                return new List<MarketingItem>();
            }

            return _oldMarketingService.GetTextList(textParam);
        }

        private IDictionary<string, string> GetTextLinkList(string selectedLanguage, string selectedPage, Domain selectedDomain, IEnumerable<SelectListItem> languageList)
        {
            if (selectedDomain == null)
            {
                return new Dictionary<string, string>();
            }

            var userSession = SessionService.UserSession;

            var linkGenerationParam = new LinkGenerationParam
            {
                AffId = userSession.AffId,
                TextLinkTemplate = _appSetting.TextLinkTemplate,
                PreferredLanguage = userSession.PreferredLanguage,
                SelectedLanguage = selectedLanguage,
                SelectedDomain = selectedDomain,
                SelectedPage = selectedPage,
                LanguageList = languageList
            };

            return _oldMarketingService.GenerateLinkList(linkGenerationParam);
        }
    }
}
