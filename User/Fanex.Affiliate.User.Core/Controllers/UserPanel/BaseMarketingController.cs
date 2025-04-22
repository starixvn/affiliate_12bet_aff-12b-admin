namespace Fanex.Affiliate.User.Core.Controllers.UserPanel
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;
    using BusinessService;
    using Constants;
    using CoreService;
    using DTO;
    using DTO.Auth;
    using DTO.Systems;
    using Enum;
    using Fanex.Affiliate.User.Core.DTO.UserPanel.MediaGallery;
    using Fanex.Affiliate.User.Core.MetalModels.UserPanel;
    using Fanex.Affiliate.User.Core.ViewModels.UserPanel.Marketing;
    using Filters;
    using Resources;
    using Utilities;

    [CustomAuthorize]
    public class BaseMarketingController : BaseController
    {
        protected readonly IMarketingService MarketingService;
        protected readonly IAppSetting AppSetting;
        protected readonly IAccountService AccountService;

        public BaseMarketingController(
            ISessionService sessionService,
            IAppSetting appSetting,
            ISystemService systemsService,
            IMarketingService marketingService,
            IAccountService accountService)
            : base(sessionService, systemsService)
        {
            MarketingService = marketingService;
            AppSetting = appSetting;
            AccountService = accountService;
        }

        protected IDictionary<string, LanguageLinkViewModel> GetLinkForMemberList(QrCodeParam param)
            => GetLinkList(param, AppSetting.TextLinkTemplate);

        protected IDictionary<string, LanguageLinkViewModel> GetLinkForSubAffList(QrCodeParam param)
            => GetLinkList(param, AppSetting.SubAffTextLinkTemplate);

        private IDictionary<string, LanguageLinkViewModel> GetLinkList(QrCodeParam param, string textLinkTemplate)
        {
            var selectedDomain = GetDomain(param.SelectedDomainId, param.SelectedTargetCountry, param.SelectedLinkType);

            if (selectedDomain == null)
            {
                return new Dictionary<string, LanguageLinkViewModel>();
            }

            var userSession = SessionService.UserSession;

            var linkGenerationParam = new LinkGeneratingParam
            {
                AffId = userSession.AffId,
                AffCode = userSession.AffCode,
                TextLinkTemplate = textLinkTemplate,
                PreferredLanguage = userSession.PreferredLanguage,
                SelectedLanguage = param.SelectedLanguage,
                SelectedDomain = selectedDomain,
                SelectedPage = param.SelectedPage,
                SelectedChannel = param.SelectedChannel,
                SelectedTargetCountry = param.SelectedTargetCountry
            };

            return MarketingService.GenerateLinkList(linkGenerationParam);
        }

        private CommonSearchFormViewModel BuildCommonData(
            string targetCountryCode,
            AffiliateLinkType targetIdOrType,
            bool isChangeAffTypeLink,
            bool isMediaGallery,
            bool isDefaultLoading = false,
            string currentLang = null)
        {
            var userSession = SessionService.UserSession;
            var isInternal = SessionService.IsInternal;

            var targetCountries = isDefaultLoading
                ? SystemService.GetSystemTargetCountries()
                : Enumerable.Empty<TargetCountry>();

            var targetLanguages = isChangeAffTypeLink
                ? Enumerable.Empty<Language>()
                : SystemService.GetLanguageByTargetCountry(targetCountryCode);

            var languageCode = GetTargetLanguageCode(isChangeAffTypeLink, currentLang, targetLanguages);

            var pages = MarketingService.GetLandingPageList(new LandingPageParam
            {
                CommGroupName = userSession.GroupName,
                IsInternal = isInternal,
                LinkType = targetIdOrType,
                TargetCountryCode = targetCountryCode,
                Language = languageCode
            });
            var categoryList = GetCategoryList(isChangeAffTypeLink, isMediaGallery, targetIdOrType, isInternal);
            var sizeList = GetSizeList(targetCountryCode, isMediaGallery, userSession, isInternal);
            var domains = GetDomainListByTargetCountry(targetCountryCode, targetIdOrType);
            var selectedDomain = domains.FirstOrDefault();

            return new CommonSearchFormViewModel
            {
                TargetCountries = SystemHelper.GetTargetCountryList(targetCountries, targetCountryCode),
                Languages = SystemHelper.GetTargetLanguageList(targetLanguages, languageCode),
                Domains = SystemHelper.GetDomainList(domains, selectedDomain?.DomainId),
                SitePages = SystemHelper.GetPageList(pages),
                CategoryList = categoryList,
                SizeList = sizeList
            };
        }

        private static string GetTargetLanguageCode(bool isChangeAffTypeLink, string currentLang, IEnumerable<Language> targetLanguages)
        {
            return isChangeAffTypeLink
                ? currentLang
                : targetLanguages.FirstOrDefault()?.Code ?? SystemLanguage.DefaultLanguageCode;
        }

        private IEnumerable<SelectListItem> GetCategoryList(bool isChangeAffTypeLink, bool isMediaGallery, AffiliateLinkType type, bool isInternal)
        {
            if (!isChangeAffTypeLink && !isMediaGallery)
            {
                return Enumerable.Empty<SelectListItem>();
            }

            return MarketingService.GetCategoryList(true, type, isInternal)
                .Where(c => string.IsNullOrEmpty(c.SubCategory))
                .Select(c => new SelectListItem
                {
                    Value = c.Category,
                    Text = Lang.ResourceManager.GetString(RegexHelper.Replace(c.Category, @"\s+", "")) ?? c.Category
                });
        }

        private IEnumerable<SelectListItem> GetSizeList(string targetCountryCode, bool isMediaGallery, UserSession userSession, bool isInternal)
        {
            if (!isMediaGallery)
            {
                return Enumerable.Empty<SelectListItem>();
            }

            return MarketingService.GetSizeList(new SizeParam
                {
                    TargetCountryCode = targetCountryCode,
                    AffType = userSession.AffType, 
                    Language = userSession.PreferredLanguage
                }, isInternal)
                .Select(i => new SelectListItem { Value = i.Size, Text = i.Size });
        }


        protected CommonSearchFormViewModel GetLinkFormViewModel(
            string targetCountryCode, 
            bool isMediaGallery = false, 
            AffiliateLinkType linkType = AffiliateLinkType.AffiliateLink)
        {
            var linkTypes = new List<SelectListItem>
            {
                new SelectListItem
                {
                    Value = linkType.ToString(),
                    Text = linkType == AffiliateLinkType.AffiliateLink ? Lang.AffilateLink : Lang.SubAffiliateLink
                }
            };

            var commonData = BuildCommonData(targetCountryCode, linkType, false, isMediaGallery, true);

            return new CommonSearchFormViewModel
            {
                LinkTypes = linkTypes,
                TargetCountries = commonData.TargetCountries,
                Languages = commonData.Languages,
                Domains = commonData.Domains,
                SitePages = commonData.SitePages,
                CategoryList = commonData.CategoryList,
                SizeList = commonData.SizeList
            };
        }

        protected ChangedFilterFormViewModel GetChangedFilter(SizeParam param, bool isChangeAffTypeLink, bool isMediaGallery)
        {
            var commonData = BuildCommonData(param.TargetCountryCode, param.TargetId, isChangeAffTypeLink, isMediaGallery, currentLang: param.Language);

            return new ChangedFilterFormViewModel
            {
                Domains = commonData.Domains,
                SitePages = commonData.SitePages,
                Languages = commonData.Languages,
                CategoryList = commonData.CategoryList,
                SizeList = commonData.SizeList
            };
        }

        protected Domain GetDomain(int? domainId, string targetCountryCode, AffiliateLinkType linkType)
            => GetDomainListByTargetCountry(targetCountryCode, linkType)
                .FirstOrDefault(d => d.DomainId == domainId);

        private IEnumerable<Domain> GetDomainListByTargetCountry(string targetCountryCode, AffiliateLinkType linkType = AffiliateLinkType.AffiliateLink)
        {
            var userSession = SessionService.UserSession;
            var isInternal = SessionService.IsInternal;

            var param = new AvailableDomainParam
            {
                TargetCountryCode = targetCountryCode,
                AffId = userSession.AffId,
                LinkType = linkType,
                IsInternal = isInternal
            };

            return MarketingService.GetAvailableDomainList(param);
        }
    }
}