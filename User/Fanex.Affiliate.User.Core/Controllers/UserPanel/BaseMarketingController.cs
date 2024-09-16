using Fanex.Affiliate.User.Core.BusinessService;
using Fanex.Affiliate.User.Core.Constants;
using Fanex.Affiliate.User.Core.CoreService;
using Fanex.Affiliate.User.Core.DTO.UserPanel.MediaGallery;
using Fanex.Affiliate.User.Core.Filters;
using Fanex.Affiliate.User.Core.Utilities;
using Fanex.Affiliate.User.Core.ViewModels.UserPanel.Marketing;
using System.Collections.Generic;
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

            var userSession = SessionService.UserSession;
            var isInternal = SessionService.IsInternal;
            var targetCountryCode = MarketingService.GetTargetCountryCodeByAff(userSession.AffId);
            var targetLanguageListByCountry = SystemService.GetSystemTargetLanguagesByCountry(targetCountryCode);
            var targetLanguageCode = targetLanguageListByCountry.FirstOrDefault()?.LanguageCode ?? SystemLanguage.DefaultLanguageCode;
            var domainList = GetDomainListByTargetCountry(targetCountryCode, isInternal);

            var viewModel = new LinkFormViewModel
            {
                SelectedTargetCountry = targetCountryCode,
                SelectedLanguage = targetLanguageCode,
                SelectedDomain = domainList.FirstOrDefault(),
                TargetCountryList = SystemHelper.GetTargetCountryList(targetCountryList, targetCountryCode),
                LanguageList = SystemHelper.GetTargetLanguageList(targetLanguageListByCountry, targetLanguageCode),
                DomainList = domainList.Select(i => new SelectListItem { Value = i.DomainId.ToString(), Text = i.DomainName }),
                MemberSitePageList = MarketingService.GetLandingPageList(targetCountryCode, userSession.GroupName, isInternal)
                .Select(i => new SelectListItem { Value = i.PageCode, Text = i.PageName })
            };

            return viewModel;
        }

        protected IEnumerable<Domain> GetDomainListByTargetCountry(string targetCountryCode, bool isInternal)
        {
            var userSession = SessionService.UserSession;
            return MarketingService.GetDomainListByTargetCountry(targetCountryCode, userSession.AffId, isInternal);
        }
    }
}
