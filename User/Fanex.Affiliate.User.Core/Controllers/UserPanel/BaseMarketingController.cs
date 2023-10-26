using Fanex.Affiliate.User.Core.BusinessService;
using Fanex.Affiliate.User.Core.Constants;
using Fanex.Affiliate.User.Core.CoreService;
using Fanex.Affiliate.User.Core.Filters;
using Fanex.Affiliate.User.Core.Utilities;
using Fanex.Affiliate.User.Core.ViewModels.UserPanel.Marketing;
using System.Linq;
using System.Web.Mvc;

namespace Fanex.Affiliate.User.Core.Controllers.UserPanel
{
    [CustomAuthorize(PermissionName = ClaimConstant.MediaGallery)]
    public class BaseMarketingController : BaseController
    {
        protected readonly IMarketingService MarketingService;
        protected readonly IAppSetting AppSetting;

        public BaseMarketingController(
            ISessionService sessionService,
            IAppSetting appSetting,
            ILanguageService languageService,
            IMarketingService marketingService
            ) : base(sessionService, languageService)
        {
            MarketingService = marketingService;
            AppSetting = appSetting;
        }

        protected LinkFormViewModel GetLinkFormViewModel()
        {
            var languageList = MarketingService.GetLanguageList();

            var userSession = SessionService.UserSession;
            var isInternal = SessionService.IsInternal;

            var viewModel = new LinkFormViewModel
            {
                TargetCountryList = MarketingHelper.GetLanguageList(languageList, userSession.PreferredLanguage),
                LanguageList = MarketingHelper.GetLanguageList(languageList, userSession.PreferredLanguage),
                DomainList = MarketingService.GetDomainList(userSession.AffId, isInternal),
                MemberSitePageList = MarketingService.GetSpecialLandingPageList(userSession.GroupName, userSession.TargetCountry, userSession.PreferredLanguage, isInternal)
                .Select(i => new SelectListItem { Value = i.PageCode, Text = i.PageName })
            };

            return viewModel;
        }
    }
}
