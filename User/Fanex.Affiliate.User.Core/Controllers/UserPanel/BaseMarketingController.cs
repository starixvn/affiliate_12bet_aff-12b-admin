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

        public BaseMarketingController(
            ISessionService sessionService,
            ISystemService systemService,
            IMarketingService marketingService
            ) : base(sessionService, systemService)
        {
            MarketingService = marketingService;
        }

        protected LinkFormViewModel GetLinkFormViewModel()
        {
            var targetCountryList = SystemService.GetSystemTargetCountries();
            var targetLanguageList = SystemService.GetSystemTargetLanguages();

            var userSession = SessionService.UserSession;
            var isInternal = SessionService.IsInternal;
            var targetCountryCode = MarketingService.GetTargetCountryCodeByAff(userSession.AffId);
            var targetLanguageCode = targetCountryList.FirstOrDefault(t => t.TargetCountryCode == targetCountryCode)?.DefaultLanguageCode ?? SystemLanguage.DefaultLanguageCode;
            var domainList = MarketingService.GetDomainListByTargetCountry(targetCountryCode, isInternal);

            var viewModel = new LinkFormViewModel
            {
                SelectedTargetCountry = targetCountryCode,
                SelectedLanguage = targetLanguageCode,
                SelectedDomain = domainList.FirstOrDefault(),
                TargetCountryList = SystemHelper.GetTargetCountryList(targetCountryList, targetCountryCode),
                LanguageList = SystemHelper.GetTargetLanguageList(targetLanguageList, targetLanguageCode),
                DomainList = domainList.Select(i => new SelectListItem { Value = i.DomainId.ToString(), Text = i.DomainName }),
                MemberSitePageList = MarketingService.GetSpecialLandingPageList(targetCountryCode, userSession.GroupName, targetLanguageCode, isInternal)
                .Select(i => new SelectListItem { Value = i.PageCode, Text = i.PageName })
            };

            return viewModel;
        }
    }
}
