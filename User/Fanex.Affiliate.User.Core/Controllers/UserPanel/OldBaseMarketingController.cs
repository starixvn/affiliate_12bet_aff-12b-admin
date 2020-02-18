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
    public class OldBaseMarketingController : BaseController
    {
        protected readonly IOldMarketingService _oldMarketingService;
        protected readonly IAppSetting _appSetting;

        public OldBaseMarketingController(ISessionService sessionService, IAppSetting appSetting,
            ILanguageService languageService,
             IOldMarketingService oldMarketingService)
            : base(sessionService, languageService)
        {
            _oldMarketingService = oldMarketingService;
            _appSetting = appSetting;
        }
        protected SearchFormViewModel GetSearchFormViewModel(bool isMediaPage)
        {
            var bannerTypeList = _oldMarketingService.GetBannerTypeList(SessionService.IsInternal);

            var categoryType = isMediaPage ? BannerTypeName.CategoryMedia : BannerTypeName.Category;
            var viewModel = new SearchFormViewModel
            {
                CategoryList = GetDataByType(bannerTypeList, categoryType),
                LanguageList = OldMarketingHelper.GetLanguageList(bannerTypeList, SessionService.UserSession.PreferredLanguage),
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
                DomainList = _oldMarketingService.GetDomainList(userSession.AffId, isInternal),
                MemberSitePageList = _oldMarketingService.GetLandingPageList(isInternal)
            };

            return viewModel;
        }

        protected IEnumerable<SelectListItem> GetDataByType(IEnumerable<OldBannerType> bannerTypeList, string type)
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
