namespace Fanex.Affiliate.User.Core.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;
    using Constants;
    using DTO;
    using DTO.UserPanel;
    using Extensions;
    using Filters;
    using MetalModels.UserPanel;
    using ViewModels.UserPanel.Marketing;

    public partial class UserPanelController
    {
        [HttpGet]
        [AjaxOnly]
        public ActionResult MediaGallery()
        {
            var userSession = SessionService.UserSession;

            var searchForm = GetSearchFormViewModel(true);
            var linkForm = GetLinkFormViewModel();

            var selectedLanguage = userSession.PreferredLanguage;
            var selectedDomain = linkForm.DomainList.FirstOrDefault();
            var selectedPage = linkForm.MemberSitePageList.First().Value;

            var bannerParam = new MarketingBannerParam
            {
                AffId = userSession.AffId,
                Language = SystemLanguage.LanguageList.FirstOrDefault(l => l.Code.MatchWith(selectedLanguage))?.EnglishName,
                AffType = userSession.AffType,
                SelectedDomain = selectedDomain,
                SelectedPage = selectedPage,
                TextLinkTemplate = AppSetting.TextLinkTemplate,
                BannerDomain = AppSetting.BannerDomain
            };

            var viewModel = new MarketingViewModel
            {
                SearchForm = searchForm,
                LinkForm = linkForm,
                TextLinkList = GetTextLinkList(selectedLanguage, selectedPage, selectedDomain, searchForm.LanguageList),
                MarketingDataList = GetBannerList(bannerParam)
            };

            ViewBag.TotalItem = bannerParam.Nrows;

            return View(viewModel);
        }

        [HttpGet]
        [AjaxOnly]
        public ActionResult TextBox()
        {
            var userSession = SessionService.UserSession;

            var searchForm = GetSearchFormViewModel(false);
            var linkForm = GetLinkFormViewModel();

            var selectedLanguage = userSession.PreferredLanguage;
            var selectedDomain = linkForm.DomainList.FirstOrDefault();
            var selectedPage = linkForm.MemberSitePageList.First().Value;

            var textParam = new MarketingTextParam
            {
                AffId = userSession.AffId,
                Language = SystemLanguage.LanguageList.FirstOrDefault(l => l.Code.MatchWith(selectedLanguage))?.EnglishName,
                SelectedDomain = selectedDomain,
                SelectedPage = selectedPage,
                ScriptUrl = AppSetting.MediaScriptUrl,
                TextLinkTemplate = AppSetting.TextLinkTemplate
            };

            var viewModel = new MarketingViewModel
            {
                SearchForm = searchForm,
                LinkForm = linkForm,
                TextLinkList = GetTextLinkList(selectedLanguage, selectedPage, selectedDomain, searchForm.LanguageList),
                MarketingDataList = GetTextList(textParam)
            };

            ViewBag.TotalItem = textParam.Nrows;

            return View(viewModel);
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

            bannerParam.AffId = userSession.AffId;
            bannerParam.AffType = userSession.AffType;
            bannerParam.SelectedDomain = selectedDomain;
            bannerParam.TextLinkTemplate = AppSetting.TextLinkTemplate;
            bannerParam.BannerDomain = AppSetting.BannerDomain;
            bannerParam.Language = SystemLanguage.LanguageList.FirstOrDefault(l => l.Code.MatchWith(bannerParam.Language))?.EnglishName;

            var bannerList = GetBannerList(bannerParam);
            ViewBag.TotalItem = bannerParam.Nrows;

            return View("~/Views/UserPanel/MarketingPartial/_BannerList.cshtml", bannerList);
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
            textParam.TextLinkTemplate = AppSetting.TextLinkTemplate;
            textParam.ScriptUrl = AppSetting.MediaScriptUrl;
            textParam.Language = SystemLanguage.LanguageList.FirstOrDefault(l => l.Code.MatchWith(textParam.Language))?.EnglishName;

            var textList = GetTextList(textParam);
            ViewBag.TotalItem = textParam.Nrows;

            return View("~/Views/UserPanel/MarketingPartial/_TextList.cshtml", textList);
        }

        [HttpGet]
        [AjaxOnly]
        public ActionResult GenerateLink(string selectedLanguage, int selectedDomainId, string selectedPage)
        {
            var userSession = SessionService.UserSession;
            var selectedDomain = _marketingService.GetDomainList(userSession.AffId, SessionService.IsInternal)
                                                    .FirstOrDefault(d => d.DomainId == selectedDomainId);
            var bannerTypeList = _marketingService.GetBannerTypeList(SessionService.IsInternal);
            var languageList = GetLanguageList(bannerTypeList);

            var textLinkList = GetTextLinkList(selectedLanguage, selectedPage, selectedDomain, languageList);

            return View("~/Views/UserPanel/MarketingPartial/_TextLink.cshtml", textLinkList);
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

        private SearchFormViewModel GetSearchFormViewModel(bool isMediaPage)
        {
            var bannerTypeList = _marketingService.GetBannerTypeList(SessionService.IsInternal);

            var categoryType = isMediaPage ? BannerTypeName.CategoryMedia : BannerTypeName.Category;
            var viewModel = new SearchFormViewModel
            {
                CategoryList = GetDataByType(bannerTypeList, categoryType),
                LanguageList = GetLanguageList(bannerTypeList),
                CurrencyList = GetDataByType(bannerTypeList, BannerTypeName.Currency)
            };

            if (isMediaPage)
            {
                viewModel.ImageTypeList = GetDataByType(bannerTypeList, BannerTypeName.ImageType);
                viewModel.SizeList = GetDataByType(bannerTypeList, BannerTypeName.Size);
            }

            return viewModel;
        }

        private LinkFormViewModel GetLinkFormViewModel()
        {
            var userSession = SessionService.UserSession;
            var isInternal = SessionService.IsInternal;

            var userHasCasino = _accountRepository.UserHasCasino(userSession.UserName, isInternal);
            var memberSitePage = new MemberSitePage(userHasCasino);

            var viewModel = new LinkFormViewModel
            {
                DomainList = _marketingService.GetDomainList(userSession.AffId, isInternal),
                MemberSitePageList = memberSitePage.PageList.Select(p => new SelectListItem { Value = p, Text = p })
            };

            return viewModel;
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
                TextLinkTemplate = AppSetting.TextLinkTemplate,
                PreferredLanguage = userSession.PreferredLanguage,
                SelectedLanguage = selectedLanguage,
                SelectedDomain = selectedDomain,
                SelectedPage = selectedPage,
                LanguageList = languageList
            };

            return _marketingService.GenerateLinkList(linkGenerationParam);
        }

        private IEnumerable<SelectListItem> GetDataByType(IEnumerable<BannerType> bannerTypeList, string type)
        {
            return bannerTypeList.Where(p => p.Type.MatchWith(type))
                .Select(i => new SelectListItem { Value = i.Value, Text = GetDisplayText(i.Value) });
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
                        Text = GetDisplayText(item.Value),
                        Selected = language.Code.MatchWith(SessionService.UserSession.PreferredLanguage)
                    });
                }
            }

            return dataList;
        }

        private static string GetDisplayText(string value)
        {
            switch (value.ToLower())
            {
                case "bonus": return Resources.Lang.Bonus;
                case "tennis": return Resources.Lang.Tennis;
                case "football": return Resources.Lang.Football;
                case "poker": return Resources.Lang.Poker;
                case "image": return Resources.Lang.Image;
                case "flash": return Resources.Lang.Flash;
                case "english": return Resources.Lang.English;
                case "chinese traditional": return Resources.Lang.ChineseTraditio;
                case "chinese simplified": return Resources.Lang.ChineseSimplifi;
                case "thai": return Resources.Lang.Thai;
                case "spanish": return Resources.Lang.Spanish;
                case "indonesian": return Resources.Lang.Indonesian;
                case "vietnamese": return Resources.Lang.Vietnamese;
                case "english (europe)": return Resources.Lang.EnglishEU;
                case "korean": return Resources.Lang.Korean;
                case "japanese": return Resources.Lang.Japanese;
                case "casino": return Resources.Lang.Casino;
                case "basketball": return Resources.Lang.Basketball;
                case "tennins": return Resources.Lang.Tennis;
                case "rmb": return Resources.Lang.RMB;
                case "myr": return Resources.Lang.MYR;
                case "thb": return Resources.Lang.THB;
                case "usd": return Resources.Lang.USD;
                case "eur": return Resources.Lang.EUR;
                case "gbp": return Resources.Lang.GBP;
                case "aud": return Resources.Lang.AUD;
                case "chf": return Resources.Lang.CHF;
                case "number game": return Resources.Lang.NumberGame;
                case "sportsbook": return Resources.Lang.Sportbook;
                case "racebook": return Resources.Lang.Racebook;
                case "sb bonus": return Resources.Lang.SBBonus;
                case "soccer": return Resources.Lang.Soccer;
                case "casino bonus": return Resources.Lang.CasinoBonus;
                case "baccarat": return Resources.Lang.Baccarat;
                case "blackjack": return Resources.Lang.Blackjack;
                case "slot": return Resources.Lang.Slot;
                case "roulette": return Resources.Lang.Roulette;
                case "sic bo": return Resources.Lang.Sicbo;

                default:
                    return value;
            }
        }
    }
}