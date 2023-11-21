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
            ISystemService systemService,
            IMarketingService marketingService
            ) : base(sessionService, systemService)
        {
            MarketingService = marketingService;
            AppSetting = appSetting;
        }

        protected LinkFormViewModel GetLinkFormViewModel()
        {
            var targetCountryList = SystemService.GetSystemTargetCountries();
            var targetLanguageList = SystemService.GetSystemTargetLanguages();

            var userSession = SessionService.UserSession;
            var isInternal = SessionService.IsInternal;
            var targetLanguageCode = targetCountryList.FirstOrDefault(t => t.TargetCountryCode == userSession.TargetCountryCode)?.DefaultLanguageCode ?? userSession.PreferredLanguage;

            var viewModel = new LinkFormViewModel
            {
                TargetCountryList = SystemHelper.GetTargetCountryList(targetCountryList, userSession.TargetCountryCode),
                LanguageList = SystemHelper.GetTargetLanguageList(targetLanguageList, targetLanguageCode),
                DomainList = MarketingService.GetDomainList(userSession.AffId, isInternal),
                MemberSitePageList = MarketingService.GetSpecialLandingPageList(userSession.GroupName, userSession.TargetCountryCode, targetLanguageCode, isInternal)
                .Select(i => new SelectListItem { Value = i.PageCode, Text = i.PageName })
            };

            return viewModel;
        }
    }
}
