namespace Fanex.Affiliate.User.Core.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;
    using Constants;
    using DTO;
    using DTO.UserPanel;
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
                Language = null,
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
                                                    .Where(t => string.Compare(t.Type, type, true) == 0);

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
            return bannerTypeList.Where(p => string.Compare(p.Type, type, true) == 0)
                .Select(i => new SelectListItem { Value = i.Value, Text = i.Value });
        }

        private IEnumerable<SelectListItem> GetLanguageList(IEnumerable<BannerType> bannerTypeList)
        {
            var languageByType = bannerTypeList.Where(t => string.Compare(t.Type, BannerTypeName.Language, true) == 0);

            var dataList = new List<SelectListItem>();
            foreach (var item in languageByType)
            {
                var language = SystemLanguage.LanguageList.FirstOrDefault(l => string.Compare(item.Value, l.EnglishName, true) == 0);
                if (language != null)
                {
                    dataList.Add(new SelectListItem
                    {
                        Value = language.Code,
                        Text = language.NativeName,
                        Selected = string.Compare(language.Code, SessionService.UserSession.PreferredLanguage, true) == 0
                    });
                }
            }

            return dataList;
        }
    }
}