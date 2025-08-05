namespace Fanex.Affiliate.Link.Controllers
{
    using System;
    using System.Text;
    using System.Text.RegularExpressions;
    using System.Threading.Tasks;
    using Commands;
    using Fanex.Affiliate.Link.Constants;
    using Fanex.Affiliate.Link.Core.Models;
    using Fanex.Affiliate.Link.Core.Services.Interfaces;
    using Fanex.Affiliate.Link.DTOs;
    using Fanex.Affiliate.Link.Extensions;
    using Fanex.Affiliate.Link.Models;
    using Fanex.Logging;
    using Helpers;
    using Microsoft.AspNetCore.Mvc;
    using ONEbook.UM.Services;
    using Services.Interface;

    /// <summary>
    /// Home controller for affiliate link.
    /// </summary>
    public class HomeController : Controller
    {
        private readonly string[] marketingForCommGroups;
        private readonly string[] marketingPageCodeRegexPatterns;
        private readonly bool isRemovePortFromUrl;

        private readonly IClickTrackingEventService trackingSystemService;
        private readonly IAffInfoService affInfoService;
        private readonly IConfiguration configuration;
        private readonly IMessageQueueService messageQueueService;
        private readonly IMaintenanceService maintenanceService;
        private const string linkIsInvalidMessage = "This link does not exist. Please contact administrator!";
        private const string linkIsIncorrectMessage = "This link is in an incorrect format. Please contact administrator!";

        public HomeController(
            IClickTrackingEventService trackingSystemService,
            IAffInfoService affInfoService,
            IMessageQueueService messageQueueService,
            IMaintenanceService maintenanceService,
            IConfiguration configuration)
        {
            this.trackingSystemService = trackingSystemService ?? throw new ArgumentNullException(nameof(trackingSystemService));
            this.affInfoService = affInfoService ?? throw new ArgumentNullException(nameof(affInfoService));
            this.configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            this.messageQueueService = messageQueueService;
            this.maintenanceService = maintenanceService;
            marketingForCommGroups = (this.configuration["AppSettings:MarketingForCommGroups"] ?? string.Empty)
                .Split(',', StringSplitOptions.RemoveEmptyEntries);
            marketingPageCodeRegexPatterns = (this.configuration["AppSettings:MarketingPageCodeRegexPatterns"] ?? string.Empty)
                .Split(',', StringSplitOptions.RemoveEmptyEntries);
            isRemovePortFromUrl = this.configuration["AppSettings:RemovePortFromUrl"].MatchWith("1");
        }

        public IActionResult Index() => View();

        [HttpGet]
        public IActionResult Error() => View();

        [HttpGet]
        public IActionResult PageNotFound() => View();

        public async Task<IActionResult> OldLink(int affId, string pageCode, string lang = null)
        {
            try
            {
                var affInfo = affInfoService.GetAffInfoByAffId(affId);

                if (!string.IsNullOrEmpty(affInfo.AffCode) && !string.IsNullOrEmpty(lang))
                {
                    var affMediaLinkWithoutProtocol = GetAffMediaLinkWithoutProtocol();
                    var affDomain = GetFullAffDomain(affMediaLinkWithoutProtocol, affId.ToString());
                    var isValidDomain = await ValidateTrackingV1MediaLink(affId, affDomain, lang, affMediaLinkWithoutProtocol);

                    if (isValidDomain)
                    {
                        return await InsertAffiliate(affInfo.AffCode, pageCode, lang, affDomain, affMediaLinkWithoutProtocol);
                    }
                }

                ViewBag.Result = linkIsInvalidMessage;
                return View("Link");
            }
            catch (Exception ex)
            {
                LogHelper.WriteError(ex);
                ViewBag.Result = linkIsIncorrectMessage;
                return View("Link");
            }
        }

        public async Task<IActionResult> SpecialPageCodeOldLink(int affId, string pageCodePart1, string pageCodePart2, string lang = null)
        {
            return await OldLink(affId, $"{pageCodePart1}/{pageCodePart2}", lang);
        }

        public async Task<IActionResult> Link(LinkParams linkParams)
        
        {
            try
            {
                var clickTrackingEvent = InitClickTrackingEvent(linkParams);
                var validateResult = await ValidateLink(clickTrackingEvent);
                if (validateResult.IsValid)
                {
                    clickTrackingEvent.RedirectLink = validateResult.RedirectView;
                    return await ProcessLink(clickTrackingEvent);
                }
                else
                {
                    ViewBag.Result = validateResult.ErrorMessage;
                    return View("Link");
                }
            }
            catch (Exception ex)
            {
                LogHelper.WriteError(ex);
                ViewBag.Result = linkIsIncorrectMessage;
                return View("Link");
            }
        }

        private ClickTrackingEvent InitClickTrackingEvent(LinkParams linkParams)
        {
            var affMediaLinkWithoutProtocol = GetAffMediaLinkWithoutProtocol();
            var affDomain = GetFullAffDomain(affMediaLinkWithoutProtocol, linkParams.PageCode);

            string urlReferrer = Request.Headers["Referer"].ToString();
            string domainReferrer = !string.IsNullOrEmpty(urlReferrer) ? new Uri(urlReferrer).Host : string.Empty;

            if (string.IsNullOrEmpty(urlReferrer) && !string.IsNullOrEmpty(Request.Query["ref"].ToString()))
            {
                urlReferrer = Request.Query["ref"].ToString();
                domainReferrer = Common.GetDomainReferrer(urlReferrer) ?? string.Empty;
            }

            string pageCodeOnV2 = GetSupportedPageCodeOnV2(linkParams.PageCode);
            string mappingRemovedLanguage = GetMappingRemovedLanguageCode(linkParams.Lang);
            string userAgent = Request.Headers["User-Agent"].ToString();
            string acceptLanguage = Request.Headers["Accept-Language"].ToString();

            var clickTrackingEvent = new ClickTrackingEvent
            {
                PageCode = linkParams.PageCode,
                AffCode = linkParams.AffCode,
                LanguageCode = linkParams.Lang,
                ChannelCode = linkParams.Channel,
                AffDomain = affDomain,
                AffMediaLink = affMediaLinkWithoutProtocol,
                IP = Request.GetClientIP(),
                Browser = Common.GetBrowserName(userAgent),
                UrlReferrer = urlReferrer,
                DomainReferrer = domainReferrer,
                OS = Common.GetOsName(userAgent),
                Device = Common.GetDeviceName(userAgent),
                BrowserVersion = Common.GetBrowserVersion(userAgent),
                //// BrowserResolution = Request.Query["res"].ToString(), 
                BrowserLanguage = acceptLanguage,
                UserAgent = userAgent,
                Country = Common.GetCountry(),
                DeviceType = GetDeviceType(),
                UpdatedTime = DateTime.Now
            };

            return clickTrackingEvent;
        }

        private async Task<IActionResult> ProcessLink(ClickTrackingEvent clickTrackingEvent)
        {
            var clickId = await trackingSystemService.GetClickId();
            clickTrackingEvent.ClickId = clickId;
            clickTrackingEvent.RedirectLink = $"{clickTrackingEvent.RedirectLink}&clickId={clickId}";
            
            await messageQueueService.PublishAsync(clickTrackingEvent);

            var forwardQueryString = Request.QueryString.Value.ReplaceStr("?", "&");
            var redirectLink = $"{clickTrackingEvent.RedirectLink}{forwardQueryString}";
            return Redirect(redirectLink);
        }

        private bool IsUnderMaintenance()
        {
            var maintanceTime = maintenanceService.GetUnderMaintenanceTime();
            return maintanceTime.IsUnderMaintenanceTime;
        }

        private async Task<ValidateResult> ValidateLink(ClickTrackingEvent clickTrackingEvent)
        {
            ValidateResult validateResult = null;
            if (IsUnderMaintenance())
            {
                validateResult = await ValidateLinkFromCache(clickTrackingEvent);
            }
            else
            {
                validateResult = await trackingSystemService.ValidateLinkFromDatabase(clickTrackingEvent);
                if (validateResult.IsValid)
                {
                    validateResult.IsAffMarketing = IsValidAffMarketing(clickTrackingEvent.AffCode);
                }
            }

            if (validateResult.IsValid)
            {
                bool isMarketingLandingPage = Array.Exists(
                 marketingPageCodeRegexPatterns,
                   reg => RegexHelper.IsMatch(clickTrackingEvent.PageCode, reg, RegexOptions.IgnoreCase));

                string mappingPageCode = isMarketingLandingPage
                    ? (validateResult.IsAffMarketing ? clickTrackingEvent.PageCode : LandingPage.JOIN_PAGE_CODE)
                    : clickTrackingEvent.PageCode;

                validateResult.RedirectView = HandleRedirectUrl(validateResult.RedirectView, clickTrackingEvent.PageCode, clickTrackingEvent.LanguageCode, mappingPageCode);
            }

            return validateResult;
        }

        private async Task<ValidateResult> ValidateLinkFromCache(ClickTrackingEvent clickTrackingEvent)
        {
            var masterInfoValidation = await trackingSystemService.GetMasterDataForValidateClicked();
            if (string.IsNullOrEmpty(clickTrackingEvent.AffCode) || string.IsNullOrEmpty(clickTrackingEvent.LanguageCode))
            {
                return new ValidateResult { IsValid = false, ErrorMessage = linkIsInvalidMessage };
            }

            var validateResult = ValidateLinkSpecialDomain(clickTrackingEvent, masterInfoValidation);
            if (!validateResult.IsValid)
            {
                validateResult = ValidateLinkDomain(clickTrackingEvent, masterInfoValidation);
            }

            if (validateResult.IsValid)
            {
                var commGroup = masterInfoValidation.AffCommGroups.FirstOrDefault(m => m.AffCode == clickTrackingEvent.AffCode);
                validateResult.IsAffMarketing = Array.Exists(marketingForCommGroups, c => c.MatchWith(commGroup.GroupName));
            }

            return validateResult;
        }

        private ValidateResult ValidateLinkDomain(ClickTrackingEvent clickTrackingEvent, MasterInfoValidation masterInfoValidation)
        {
            var affCommGroup = masterInfoValidation.AffCommGroups.FirstOrDefault(m => m.AffCode == clickTrackingEvent.AffCode);
            if (affCommGroup == null)
            {
                return new ValidateResult { IsValid = false, ErrorMessage = linkIsInvalidMessage };
            }
            else
            {
                var domain = masterInfoValidation.DomainMappings.FirstOrDefault(m => m.AffDomain == clickTrackingEvent.AffDomain
                                        && m.LanguageCode == clickTrackingEvent.LanguageCode);
                if (domain == null)
                {
                    return new ValidateResult { IsValid = false, ErrorMessage = linkIsInvalidMessage };
                }
                else
                {
                    var licenseeDomain = $"{(domain.IsSSL ? "https" : "http")}://{domain.RedirectDomain}/{domain.LanguageLink}/{clickTrackingEvent.PageCode}?affCode={affCommGroup.AffCode}";
                    return new ValidateResult { IsValid = true, RedirectView = licenseeDomain };
                }
            }
        }

        private ValidateResult ValidateLinkSpecialDomain(ClickTrackingEvent clickTrackingEvent, MasterInfoValidation masterInfoValidation)
        {
            var domain = masterInfoValidation.SpecialDomainMappings.FirstOrDefault(m => m.AffCode == clickTrackingEvent.AffCode
                && m.AffDomain == clickTrackingEvent.AffDomain);
            if (domain == null)
            {
                return new ValidateResult { IsValid = false, ErrorMessage = linkIsInvalidMessage };
            }
            else
            {
                var languageLink = masterInfoValidation.LanguageReferences.FirstOrDefault(m => m.LanguageCode == clickTrackingEvent.LanguageCode);
                var specialDomain = $"{(domain.IsSSL ? "https" : "http")}://{domain.RedirectDomain}/{languageLink}/{clickTrackingEvent.PageCode}?affCode={domain.AffCode}";
                return new ValidateResult { IsValid = true, RedirectView = specialDomain };
            }
        }

        public async Task<IActionResult> SpecialPageCodeLink(string pageCodePart1, string pageCodePart2, string affCode, string lang = null, string channel = null)
        {
            var linkParams = new LinkParams
            { 
                PageCode = $"{pageCodePart1}/{pageCodePart2}",
                AffCode = affCode,
                Lang = lang,
                Channel = channel
            };

            return await Link(linkParams);
        }

        private async Task<IActionResult> InsertAffiliate(
            string affCode,
            string pageCode,
            string lang,
            string affDomain,
            string affMediaLinkWithoutProtocol,
            string channel = null)
        {
            string urlReferrer = Request.Headers["Referer"].ToString();
            string domainReferrer = !string.IsNullOrEmpty(urlReferrer) ? new Uri(urlReferrer).Host : string.Empty;

            if (string.IsNullOrEmpty(urlReferrer) && !string.IsNullOrEmpty(Request.Query["ref"].ToString()))
            {
                urlReferrer = Request.Query["ref"].ToString();
                domainReferrer = Common.GetDomainReferrer(urlReferrer) ?? string.Empty;
            }

            string pageCodeOnV2 = GetSupportedPageCodeOnV2(pageCode);
            string mappingRemovedLanguage = GetMappingRemovedLanguageCode(lang);
            string userAgent = Request.Headers["User-Agent"].ToString();
            string acceptLanguage = Request.Headers["Accept-Language"].ToString();

            var command = new TrackingSystemCommand
            {
                AffDomain = affDomain,
                AffCode = affCode,
                PageCode = pageCodeOnV2,
                ChannelCode = channel ?? string.Empty,
                LanguageCode = mappingRemovedLanguage,
                DeviceType = GetDeviceType(),
                IP = Request.GetClientIP(),
                Browser = Common.GetBrowserName(userAgent),
                URLReferrer = urlReferrer,
                DomainReferrer = domainReferrer,
                OS = Common.GetOsName(userAgent),
                Device = Common.GetDeviceName(userAgent),
                BrowserVersion = Common.GetBrowserVersion(userAgent),
                //// BrowserResolution = Request.Query["res"].ToString(), 
                BrowserLanguage = acceptLanguage,
                UserAgent = userAgent,
                Country = Common.GetCountry(),
                AffMediaLink = affMediaLinkWithoutProtocol,
            };

            string redirectUrl = await trackingSystemService.InsertStatistic(command);

            if (!string.IsNullOrEmpty(redirectUrl))
            {
                WriteLogInfo(command);

                bool isMarketingLandingPage = Array.Exists(
                    marketingPageCodeRegexPatterns,
                    reg => RegexHelper.IsMatch(pageCode, reg, RegexOptions.IgnoreCase));

                string mappingPageCode = isMarketingLandingPage
                    ? (IsValidAffMarketing(affCode) ? pageCodeOnV2 : LandingPage.JOIN_PAGE_CODE)
                    : pageCodeOnV2;

                var redirectUrlFinal = HandleRedirectUrl(redirectUrl, pageCodeOnV2, command.LanguageCode, mappingPageCode);

                var forwardQueryString = Request.QueryString.Value.ReplaceStr("?", "&");
                return Redirect($"{redirectUrlFinal}{forwardQueryString}");
            }

            ViewBag.Result = linkIsInvalidMessage;
            return View("Link");
        }

        private static void WriteLogInfo(TrackingSystemCommand command)
        {
            if (!command.RedirectPage.Contains(command.AffCode, StringComparison.OrdinalIgnoreCase)
                || command.RedirectPage.Contains("affMediaLinkId=&", StringComparison.OrdinalIgnoreCase)
                || string.IsNullOrEmpty(command.ChannelCode))
            {
                var builder = new StringBuilder()
                    .AppendLine($"Link Go Info: http://{command.AffMediaLink}")
                    .AppendLine()
                    .AppendLine("REQUEST INFO")
                    .AppendLine($"RedirectPage: {command.RedirectPage}")
                    .AppendLine($"AffDomain: {command.AffDomain}")
                    .AppendLine($"AffCode: {command.AffCode}")
                    .AppendLine($"PageCode: {command.PageCode}")
                    .AppendLine($"ChannelCode: {command.ChannelCode}")
                    .AppendLine($"LanguageCode: {command.LanguageCode}")
                    .AppendLine($"DeviceType: {command.DeviceType}")
                    .AppendLine($"IP: {command.IP}")
                    .AppendLine($"Browser: {command.Browser}")
                    .AppendLine($"Country: {command.Country}")
                    .AppendLine($"Referrer: {command.URLReferrer}")
                    .AppendLine($"DomainReferrer: {command.DomainReferrer}");

                LogService.Info(builder.ToString());
            }
        }

        private async Task<bool> ValidateTrackingV1MediaLink(
            int affId,
            string affDomain,
            string lang,
            string affMediaLinkWithoutProtocol)
        {
            string mappingRemovedLanguage = GetMappingRemovedLanguageCode(lang);
            var mappingMarketInfo = await trackingSystemService.GetMarketInfo(mappingRemovedLanguage);

            if (string.IsNullOrEmpty(mappingMarketInfo.MarketId))
            {
                return false;
            }

            var checkingCommand = new CheckMarketingLinkCommand
            {
                AffId = affId,
                AffDomain = affDomain,
                LanguageCode = mappingMarketInfo.MarketId,
                AffMediaLink = affMediaLinkWithoutProtocol
            };

            var checkMarketingLinkResult = await trackingSystemService.GetTrackingMarketingLinkInfo(checkingCommand);

            if (checkMarketingLinkResult.IsValidDomain && !checkMarketingLinkResult.IsExistedTracking)
            {
                var insertCommand = new ToggleMarketingLinkCommand
                {
                    AffId = affId,
                    LanguageCode = mappingMarketInfo.MarketId,
                    AffMediaLink = $"https://{affMediaLinkWithoutProtocol}"
                };

                await trackingSystemService.ToggleMarketingLink(insertCommand);
            }

            return checkMarketingLinkResult.IsValidDomain;
        }

        private static string GetSupportedPageCodeOnV2(string pageCode)
        {
            if (string.IsNullOrWhiteSpace(pageCode))
            {
                return string.Empty;
            }

            string pageCodeLower = pageCode.ToLowerInvariant();

            return LandingPage.SupportedPageCodeOnV1.TryGetValue(pageCodeLower, out string pageCodeOnV2)
                ? pageCodeOnV2
                : pageCodeLower;
        }

        private bool IsValidAffMarketing(string affCode)
        {
            var commGroup = affInfoService.GetCommGroupByAffCode(affCode);
            return Array.Exists(marketingForCommGroups, c => c.MatchWith(commGroup.GroupName));
        }

        private string GetFullAffDomain(string urlWithoutProtocol, string endPointOfAffDomain)
        {
            if (string.IsNullOrEmpty(urlWithoutProtocol) || string.IsNullOrEmpty(endPointOfAffDomain))
            {
                return string.Empty;
            }

            string resultUrl = urlWithoutProtocol;
            if (isRemovePortFromUrl)
            {
                if (Uri.TryCreate($"https://{urlWithoutProtocol}", UriKind.Absolute, out var uri))
                {
                    resultUrl = urlWithoutProtocol.ReplaceStr($":{uri.Port}", string.Empty, StringComparison.Ordinal);
                }
            }

            int pos = resultUrl.IndexOf($"/{endPointOfAffDomain}", StringComparison.OrdinalIgnoreCase);
            return pos > 0 ? resultUrl.Substring(0, pos) : resultUrl;
        }

        private string GetAffMediaLinkWithoutProtocol()
        {
            string host = Request.Headers["Host"].ToString();
            if (string.IsNullOrEmpty(host))
            {
                host = Request.Host.Value;
            }
            string path = Request.Path.Value ?? string.Empty;
            return $"{host}{path}";
        }

        private static string GetSupportedLanguageCodeOnMemberSiteV2(string languageCode)
        {
            if (string.IsNullOrWhiteSpace(languageCode))
            {
                return string.Empty;
            }

            return Language.SupportedLanguageCodes.TryGetValue(languageCode.ToLowerInvariant(), out string languageCodeOnV2)
                ? languageCodeOnV2
                : languageCode;
        }

        private static string GetMappingRemovedLanguageCode(string languageCode)
        {
            if (string.IsNullOrWhiteSpace(languageCode))
            {
                return string.Empty;
            }

            return Language.RemovedLanguageCodes.TryGetValue(languageCode.ToLowerInvariant(), out string mappingLanguageCode)
                ? mappingLanguageCode
                : languageCode;
        }

        private string HandleRedirectUrl(string redirectUrl, string pageCode, string languageCode, string mappingPageCode)
        {
            if (string.IsNullOrEmpty(redirectUrl) || string.IsNullOrEmpty(pageCode) || string.IsNullOrEmpty(languageCode))
            {
                return redirectUrl ?? string.Empty;
            }

            string customLanguageCode = GetSupportedLanguageCodeOnMemberSiteV2(languageCode);
            string redirectPattern = configuration[$"AppSettings:Redirect_{pageCode.ToLowerInvariant()}_pattern"];

            if (!string.IsNullOrEmpty(redirectPattern))
            {
                if (Uri.TryCreate(redirectUrl, UriKind.Absolute, out var redirectUri))
                {
                    string pathAndQueryString = redirectPattern.ReplaceStr("#language#", customLanguageCode);
                    return $"{redirectUri.Scheme}://{redirectUri.Host}{pathAndQueryString}{redirectUri.Query}";
                }
            }

            if (languageCode.MatchWith(Language.BENGALI_SHORTED_CODE))
            {
                return redirectUrl.ReplaceStr($"/{pageCode}", $"/{mappingPageCode}").ReplaceStr($"/bn/", $"/bd/");
            }

            return redirectUrl
                .ReplaceStr($"/{pageCode}", $"/{mappingPageCode}")
                .ReplaceStr($"/{languageCode}/", $"/{customLanguageCode}/");
        }

        private static byte GetDeviceType()
        {
            return Common.IsMobile() ? (byte)1 : (byte)0;
        }
    }
}