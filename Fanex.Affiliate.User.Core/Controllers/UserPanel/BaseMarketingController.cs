using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Fanex.Affiliate.User.Core.BusinessService;
using Fanex.Affiliate.User.Core.Constants;
using Fanex.Affiliate.User.Core.CoreService;
using Fanex.Affiliate.User.Core.DTO.UserPanel;
using Fanex.Affiliate.User.Core.Extensions;
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
            var bannerTypeList = MarketingService.GetBannerTypeList(SessionService.IsInternal);

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

        protected IEnumerable<SelectListItem> GetLanguageList(IEnumerable<BannerType> bannerTypeList)
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
                        Text = SystemLanguage.GetLanguageText(language.DisplayName),
                        Selected = language.Code.MatchWith(SessionService.UserSession.PreferredLanguage)
                    });
                }
            }

            return dataList;
        }

        protected IEnumerable<SelectListItem> GetDataByType(IEnumerable<BannerType> bannerTypeList, string type)
        {
            return bannerTypeList.Where(p => p.Type.MatchWith(type))
                .Select(i => new SelectListItem { Value = i.Value, Text = Lang.ResourceManager.GetString(i.Value.Trim()) ?? i.Value });
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
