namespace Fanex.Affiliate.User.Core.Controllers.UserPanel
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;
    using Constants;
    using DTO;
    using DTO.UserPanel;
    using Extensions;
    using Fanex.Affiliate.User.Core.BusinessService;
    using Fanex.Affiliate.User.Core.CoreService;
    using Fanex.Affiliate.User.Core.Repository.UserPanel;
    using Fanex.Affiliate.User.Core.Utilities;
    using Filters;
    using MetalModels.UserPanel;
    using ONEbook.UM.Filters;

    [CustomAuthorize]
    public class MarketingController : BaseController
    {

        private readonly IMarketingService _marketingService;
        private readonly IAppSetting _appSetting;
        public MarketingController(ISessionService sessionService, IAppSetting appSetting,
            ILanguageService languageService,
            IMarketingService marketingService,
            IAccountRepository accountRepository) : base(sessionService, languageService)
        {

            _marketingService = marketingService;
            _appSetting = appSetting;
        }

        [HttpGet]
        [AjaxOnly]
        public ActionResult GetSubCategory(string category)
        {
            var type = "CategoryMedia" + category;
            var subCategoryList = _marketingService.GetBannerTypeList(SessionService.IsInternal)
                                                    .Where(t => t.Type.MatchWith(type));

            return Json(subCategoryList, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        [AjaxOnly]
        public ActionResult GetBanner(MarketingBannerParam bannerParam)
        {
            var userSession = SessionService.UserSession;
            var selectedDomain = _marketingService.GetDomainList(userSession.AffId, SessionService.IsInternal)
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
        public ActionResult GetText(MarketingTextParam textParam)
        {
            var userSession = SessionService.UserSession;
            var selectedDomain = _marketingService.GetDomainList(userSession.AffId, SessionService.IsInternal)
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
            var selectedDomain = _marketingService.GetDomainList(userSession.AffId, SessionService.IsInternal)
                                                    .FirstOrDefault(d => d.DomainId == selectedDomainId);
            var bannerTypeList = _marketingService.GetBannerTypeList(SessionService.IsInternal);
            var languageList = string.IsNullOrEmpty(selectedLanguage) ? GetLanguageList(bannerTypeList) : null;

            var textLinkList = GetTextLinkList(selectedLanguage, selectedPage, selectedDomain, languageList);

            return View("~/Views/UserPanel/Marketing/MarketingPartial/_TextLink.cshtml", textLinkList);
        }

        private IEnumerable<MarketingItem> GetBannerList(MarketingBannerParam bannerParam)
        {
            if (bannerParam.SelectedDomain == null)
            {
                return new List<MarketingItem>();
            }

            return _marketingService.GetBannerList(bannerParam);
        }

        private IEnumerable<MarketingItem> GetTextList(MarketingTextParam textParam)
        {
            if (textParam.SelectedDomain == null)
            {
                return new List<MarketingItem>();
            }

            return _marketingService.GetTextList(textParam);
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

            return _marketingService.GenerateLinkList(linkGenerationParam);
        }
        private IEnumerable<SelectListItem> GetLanguageList(IEnumerable<BannerType> bannerTypeList)
        {
            var languageByType = bannerTypeList.Where(t => t.Type.MatchWith(BannerTypeName.Language));

            var dataList = new List<SelectListItem>();
            foreach (var item in languageByType)
            {
                var language = SystemLanguage.LanguageList.FirstOrDefault(l => l.EnglishName.MatchWith(item.Value));
                if (language != null)
                {
                    dataList.Add(new SelectListItem
                    {
                        Value = language.Code,
                        Text = SystemLanguage.GetLanguageText(item.Value),
                        Selected = language.Code.MatchWith(SessionService.UserSession.PreferredLanguage)
                    });
                }
            }

            return dataList;
        }
    }
}