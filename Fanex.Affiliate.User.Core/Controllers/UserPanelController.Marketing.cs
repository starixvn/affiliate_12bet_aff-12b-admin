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
            return View();
        }

        [HttpGet]
        [AjaxOnly]
        public ActionResult TextBox()
        {
            return View();
        }

        [ChildActionOnly]
        public ActionResult SearchForm(bool isBannerPage)
        {
            var bannerTypeList = _marketingService.GetBannerTypeList(SessionService.IsInternal);

            var viewModel = new SearchFormViewModel
            {
                CategoryList = GetDataByType(bannerTypeList, BannerTypeName.Category),
                LanguageList = GetLanguageList(bannerTypeList),
                CurrencyList = GetDataByType(bannerTypeList, BannerTypeName.Currency)
            };

            if (isBannerPage)
            {
                viewModel.ImageTypeList = GetDataByType(bannerTypeList, BannerTypeName.ImageType);
                viewModel.SizeList = GetDataByType(bannerTypeList, BannerTypeName.Size);
            }

            return PartialView("~/Views/UserPanel/MarketingPartial/_Search.cshtml", viewModel);
        }

        [ChildActionOnly]
        public ActionResult LinkForm()
        {
            var memberSitePage = new MemberSitePage(true);

            var viewModel = new LinkFormViewModel
            {
                DomainList = _marketingService.GetDomainList(SessionService.UserSession.AffId, SessionService.IsInternal),
                MemberSitePageList = memberSitePage.PageList.Select(p => new SelectListItem { Value = p, Text = p })
            };

            ViewBag.PreferredLanguage = SessionService.UserSession.PreferredLanguage;

            return PartialView("~/Views/UserPanel/MarketingPartial/_Link.cshtml", viewModel);
        }

        [ChildActionOnly]
        public ActionResult TextLink(string selectedLanguage, int selectedDomain, string selectedPage)
        {
            var affId = SessionService.UserSession.AffId;
            var isInternal = SessionService.IsInternal;

            var linkGenerationParam = new LinkGenerationParam
            {
                AffId = affId,
                TextLinkTemplate = AppSetting.TextLinkTemplate,
                PreferredLanguage = SessionService.UserSession.PreferredLanguage,
                SelectedLanguage = selectedLanguage,
                SelectedDomain = _marketingService.GetDomainList(affId, isInternal).FirstOrDefault(p => p.DomainId == selectedDomain),
                SelectedPage = selectedPage
            };

            var bannerTypeList = _marketingService.GetBannerTypeList(isInternal);
            linkGenerationParam.LanguageList = bannerTypeList.Where(p => string.Compare(p.Type, BannerTypeName.Language, true) == 0);

            var linkList = _marketingService.GenerateLinkList(linkGenerationParam);

            return PartialView("~/Views/UserPanel/MarketingPartial/_TextLink.cshtml", linkList);
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