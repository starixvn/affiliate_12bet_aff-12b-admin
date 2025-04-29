namespace Fanex.Affiliate.User.Core.Controllers.UserPanel
{
    using BusinessService;
    using CoreService;
    using Enum;
    using Fanex.Affiliate.User.Core.DTO.Auth;
    using Fanex.Affiliate.User.Core.DTO.UserPanel.MediaGallery;
    using Fanex.Affiliate.User.Core.MetalModels.UserPanel;
    using Fanex.Affiliate.User.Core.ViewModels.UserPanel.Marketing;
    using Filters;
    using Resources;
    using System.Collections.Generic;
    using System.Linq;
    using Utilities;

    [CustomAuthorize]
    public class BaseMarketingController : BaseController
    {
        protected readonly IMarketingService MarketingService;
        protected readonly IAppSetting AppSetting;

        public BaseMarketingController(
            ISessionService sessionService,
            IAppSetting appSetting,
            ISystemService systemsService,
            IMarketingService marketingService)
            : base(sessionService, systemsService)
        {
            MarketingService = marketingService;
            AppSetting = appSetting;
        }

        protected IDictionary<string, LanguageLinkViewModel> GetLinkForMemberList(QrCodeParam param)
        {
            var selectedDomain = GetDomain(param.SelectedDomainId, param.SelectedLinkType);

            if (selectedDomain == null)
            {
                return new Dictionary<string, LanguageLinkViewModel>();
            }

            var userSession = SessionService.UserSession;

            var linkGenerationParam = new LinkGeneratingParam
            {
                AffId = userSession.AffId,
                AffCode = userSession.AffCode,
                TextLinkTemplate = AppSetting.TextLinkTemplate,
                PreferredLanguage = userSession.PreferredLanguage,
                SelectedLanguage = param.SelectedLanguage,
                SelectedDomain = selectedDomain,
                SelectedPage = param.SelectedPage,
                SelectedChannel = param.SelectedChannel,
            };

            return MarketingService.GenerateLinkList(linkGenerationParam);
        }

        protected IDictionary<string, LanguageLinkViewModel> GetLinkForSubAffList(QrCodeParam param)
        {
            var selectedDomain = GetDomain(param.SelectedDomainId, param.SelectedLinkType);

            if (selectedDomain == null)
            {
                return new Dictionary<string, LanguageLinkViewModel>();
            }

            var userSession = SessionService.UserSession;

            var linkGenerationParam = new LinkGeneratingParam
            {
                AffId = userSession.AffId,
                AffCode = userSession.AffCode,
                TextLinkTemplate = AppSetting.SubAffTextLinkTemplate,
                PreferredLanguage = userSession.PreferredLanguage,
                SelectedLanguage = param.SelectedLanguage,
                SelectedDomain = selectedDomain,
                SelectedPage = param.SelectedPage,
                SelectedChannel = param.SelectedChannel
            };

            return MarketingService.GenerateLinkList(linkGenerationParam);
        }

        protected RefLinkViewModel GetAffLinkTypeViewModel()
        {
            var userSession = SessionService.UserSession;
            var affId = userSession.AffId;
            var domains = MarketingService.GetAvailableDomainList(affId, AffiliateLinkType.AffiliateLink, SessionService.IsInternal);
            var pages = MarketingService.GetLandingPageList(userSession.GroupName, SessionService.IsInternal, language: userSession.PreferredLanguage);

            return new RefLinkViewModel
            {
                AffLinkType = AffiliateLinkType.AffiliateLink,
                AffTypeDisplayName = Lang.AffiliateLink,
                Domains = domains,
                SitePages = pages
            };
        }

        protected RefLinkViewModel GetSubAffLinkTypeViewModel()
        {
            var userSession = SessionService.UserSession;
            var affId = userSession.AffId;
            var domains = MarketingService.GetAvailableDomainList(affId, AffiliateLinkType.SubAffiliateLink, SessionService.IsInternal);
            var pages = MarketingService.GetLandingPageList(userSession.GroupName, SessionService.IsInternal, AffiliateLinkType.SubAffiliateLink, userSession.PreferredLanguage);

            return new RefLinkViewModel
            {
                AffLinkType = AffiliateLinkType.SubAffiliateLink,
                AffTypeDisplayName = Lang.SubAffiliateLink,
                Domains = domains,
                SitePages = pages
            };
        }

        private Domain GetDomain(int? domainId, AffiliateLinkType linkType)
            => MarketingService
            .GetAvailableDomainList(SessionService.UserSession.AffId, linkType, SessionService.IsInternal)
            .FirstOrDefault(d => d.DomainId == domainId);
    }
}