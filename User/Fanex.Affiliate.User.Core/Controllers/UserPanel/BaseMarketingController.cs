using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Fanex.Affiliate.User.Core.BusinessService;
using Fanex.Affiliate.User.Core.CoreService;
using Fanex.Affiliate.User.Core.DTO.UserPanel;
using Fanex.Affiliate.User.Core.Filters;
using Fanex.Affiliate.User.Core.MetalModels.UserPanel;
using Fanex.Affiliate.User.Core.Resources;
using Fanex.Affiliate.User.Core.Utilities;
using Fanex.Affiliate.User.Core.ViewModels.UserPanel.Marketing;

namespace Fanex.Affiliate.User.Core.Controllers.UserPanel
{
    [CustomAuthorize]
    public class BaseMarketingController : BaseController
    {
        protected readonly IMarketingService MarketingService;
        protected readonly IAppSetting AppSetting;

        public BaseMarketingController(ISessionService sessionService, IAppSetting appSetting,
            ILanguageService languageService,
            IMarketingService marketingService)
            : base(sessionService, languageService)
        {
            MarketingService = marketingService;
            AppSetting = appSetting;
        }
        protected SearchFormViewModel GetSearchFormViewModel(bool isMediaPage)
        {
            var categoryList = MarketingService.GetCategoryList(isMediaPage, SessionService.IsInternal).Where(c => c.CategoryId == c.ParentCategoryId);
            var languageList = MarketingService.GetLanguageList(SessionService.IsInternal);
            var currencyList = MarketingService.GetCurrencyList(SessionService.IsInternal);
            var userSession = SessionService.UserSession;
            var viewModel = new SearchFormViewModel
            {
                CategoryList = categoryList.Select(i => new SelectListItem { Value = i.Category, Text = Lang.ResourceManager.GetString(i.Category.Trim()) ?? i.Category }),
                LanguageList = MarketingHelper.GetLanguageList(languageList, userSession.PreferredLanguage),
                CurrencyList = currencyList.Select(i => new SelectListItem { Value = i.Currency, Text = Lang.ResourceManager.GetString(i.Currency.Trim()) ?? i.Currency })
            };

            if (isMediaPage)
            {
                var sizeList = MarketingService.GetSizeList(new SizeParam { AffType = userSession.AffType, Language = userSession.PreferredLanguage }, SessionService.IsInternal);
                var bannerTypeList = MarketingService.GetBannerTypeList(SessionService.IsInternal);

                viewModel.ImageTypeList = bannerTypeList.Select(i => new SelectListItem { Value = i.Type, Text = Lang.ResourceManager.GetString(i.Type.Trim()) ?? i.Type });
                viewModel.SizeList = sizeList.Select(i => new SelectListItem { Value = i.Size, Text = i.Size });
            }

            return viewModel;
        }

        protected LinkFormViewModel GetLinkFormViewModel()
        {
            var userSession = SessionService.UserSession;
            var isInternal = SessionService.IsInternal;

            var viewModel = new LinkFormViewModel
            {
                DomainList = MarketingService.GetDomainList(userSession.AffId, isInternal),
                MemberSitePageList = MarketingService.GetLandingPageList(isInternal)
            };

            return viewModel;
        }

        protected IDictionary<string, string> GetTextLinkList(string selectedLanguage, string selectedPage,
            Domain selectedDomain, IEnumerable<SelectListItem> languageList)
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

            return MarketingService.GenerateLinkList(linkGenerationParam);
        }
    }
}
