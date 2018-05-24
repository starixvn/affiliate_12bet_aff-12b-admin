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
                Language = selectedLanguage,
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

            return View(viewModel);
        }

        [HttpGet]
        [AjaxOnly]
        public ActionResult TextBox()
        {
            return View();
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

            var bannerList = GetBannerList(bannerParam);

            return View("~/Views/UserPanel/MarketingPartial/_BannerList.cshtml", bannerList);
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

        private SearchFormViewModel GetSearchFormViewModel(bool isMediaPage)
        {
            var bannerTypeList = _marketingService.GetBannerTypeList(SessionService.IsInternal);

            var viewModel = new SearchFormViewModel
            {
                CategoryList = GetDataByType(bannerTypeList, BannerTypeName.CategoryMedia),
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
            var memberSitePage = new MemberSitePage(true);

            var viewModel = new LinkFormViewModel
            {
                DomainList = _marketingService.GetDomainList(SessionService.UserSession.AffId, SessionService.IsInternal),
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
                .Select(i => new SelectListItem { Value = i.Value, Text = i.Value });
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
                        Text = language.NativeName,
                        Selected = language.Code.MatchWith(SessionService.UserSession.PreferredLanguage)
                    });
                }
            }

            return dataList;
        }
    }
}