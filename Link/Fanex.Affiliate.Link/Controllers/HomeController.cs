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
    using Fanex.Affiliate.Link.Criterias;
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
        private readonly string[] _marketingForCommGroups;
        private readonly string[] _marketingPageCodeRegexPatterns;
        private readonly bool _isRemovePortFromUrl;

        private readonly ITrackingSystemService trackingSystemService;
        private readonly IAffInfoService affInfoService;
        private readonly IConfiguration configuration;
        private readonly IMessageQueueService messageQueueService;
        private readonly IMaintenanceService maintenanceService;

        private const string LinkIsInvalidMessage = "This link does not exist. Please contact administrator!";
        private const string LinkIsIncorrectMessage = "This link is in an incorrect format. Please contact administrator!";

        public HomeController(
            ITrackingSystemService trackingSystemService,
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

            _marketingForCommGroups = (this.configuration["AppSettings:MarketingForCommGroups"] ?? string.Empty)
                .Split(',', StringSplitOptions.RemoveEmptyEntries);
            _marketingPageCodeRegexPatterns = (this.configuration["AppSettings:MarketingPageCodeRegexPatterns"] ?? string.Empty)
                .Split(',', StringSplitOptions.RemoveEmptyEntries);
            _isRemovePortFromUrl = IsMatching(this.configuration["AppSettings:RemovePortFromUrl"] ?? "0", "1");
        }

        public IActionResult Index() => View();

        [HttpGet]
        public IActionResult Error() => View();

        [HttpGet]
        public IActionResult PageNotFound() => View();

        public IActionResult OldLink(int affId, string pageCode, string? lang = null)
        {
            try
            {
                var affInfo = affInfoService.GetAffInfoByAffId(affId);

                if (!string.IsNullOrEmpty(affInfo.AffCode) && !string.IsNullOrEmpty(lang))
                {
                    var affMediaLinkWithoutProtocol = GetAffMediaLinkWithoutProtocol();
                    var affDomain = GetFullAffDomain(affMediaLinkWithoutProtocol, affId.ToString());
                    var isValidDomain = ValidateTrackingV1MediaLink(affId, affDomain, lang, affMediaLinkWithoutProtocol);

                    if (isValidDomain)
                    {
                        return InsertAffiliate(affInfo.AffCode, pageCode, lang, affDomain, affMediaLinkWithoutProtocol);
                    }
                }

                ViewBag.Result = LinkIsInvalidMessage;
                return View("Link");
            }
            catch (Exception ex)
            {
                LogHelper.WriteError(ex);
                ViewBag.Result = LinkIsIncorrectMessage;
                return View("Link");
            }
        }

        public IActionResult SpecialPageCodeOldLink(int affId, string pageCodePart1, string pageCodePart2, string? lang = null)
        {
            return OldLink(affId, $"{pageCodePart1}/{pageCodePart2}", lang);
        }

        public async Task<IActionResult> Link(string pageCode, string affCode, string? lang = null, string? channel = null)
        {
            try
            {
                
                
                var affMediaLinkWithoutProtocol = GetAffMediaLinkWithoutProtocol();
                var affDomain = GetFullAffDomain(affMediaLinkWithoutProtocol, pageCode);

                if (IsUnderMaintenance())
                {
                    var masterInfoValidations = await affInfoService.GetMasterDataForValidateClicked();
                    var validateResult = IsValidLink(masterInfoValidations, pageCode, affCode, lang, channel, affDomain, affMediaLinkWithoutProtocol);
                    if (validateResult.IsValid)
                    {
                        // increase clickId
                        var clickId = await affInfoService.GetClickId();
                        var redirectLink = $"{validateResult.RedirectView}&clickId={clickId}";

                        //insert into queue
                        await InsertAffiliateToQueue(clickId, affCode, pageCode, lang, affDomain, affMediaLinkWithoutProtocol, channel, redirectLink);

                        return Redirect(redirectLink);
                    }
                    else
                    {
                        return View(validateResult.RedirectView);
                    }
                }
                else
                {
                    var criteria = new GetRedirectPageBySettingCriteria
                    {
                        AffCode = affCode,
                        AffDomain = affDomain,
                        PageCode = pageCode,
                        LanguageCode = lang
                    };
                    await trackingSystemService.GetRedirectLink(criteria);

                    if (criteria.ErrCode == 0)
                    {
                        // increase clickId
                        var clickId = await affInfoService.GetClickId();
                        var redirectLink = $"{criteria.RedirectPage}&clickId={clickId}";

                        //insert into queue
                        await InsertAffiliateToQueue(clickId, affCode, pageCode, lang, affDomain, affMediaLinkWithoutProtocol, channel, redirectLink);

                        return Redirect(redirectLink);
                    }
                    else
                    {
                        return View("Link");
                    }
                }
                


                //var affInfo = _affInfoService.GetAffInfoByAffCode(affCode);

                //if (affInfo.AffId == 0 || string.IsNullOrEmpty(lang))
                //{
                //    ViewBag.Result = LinkIsInvalidMessage;
                //    return View("Link");
                //}

                //var affMediaLinkWithoutProtocol = GetAffMediaLinkWithoutProtocol();
                //var affDomain = GetFullAffDomain(affMediaLinkWithoutProtocol, pageCode);

                //return InsertAffiliate(affCode, pageCode, lang, affDomain, affMediaLinkWithoutProtocol, channel);
            }
            catch (Exception ex)
            {
                LogHelper.WriteError(ex);
                ViewBag.Result = LinkIsIncorrectMessage;
                return View("Link");
            }
        }

       

        private bool IsUnderMaintenance()
        {
            var maintanceTime = maintenanceService.GetUnderMaintenanceTime();
            return maintanceTime.IsUnderMaintenanceTime;
        }

        private ValidateResult IsValidLink(IEnumerable<MasterInfoValidation> masterInfoValidations, string pageCode, string affCode, string lang, string channel, string affDomain, string affMediaLinkWithoutProtocol)
        {
            if (string.IsNullOrEmpty(affCode) || string.IsNullOrEmpty(lang))
            {
                ViewBag.Result = LinkIsInvalidMessage;
                return new ValidateResult { 
                    IsValid = false,
                    RedirectView = "Link"
                };
            }

            //var redirectDomain1 = masterInfoValidations.Where(m => m.AffCode == affCode).ToList();
            var masterInfoValidation = masterInfoValidations.FirstOrDefault(m => m.AffCode == affCode);
            
            if (masterInfoValidation == null)
            {
                ViewBag.Result = LinkIsInvalidMessage;
                return new ValidateResult
                {
                    IsValid = false,
                    RedirectView = "Link"
                };
            }


            var redirectSpecialDomain = masterInfoValidations.FirstOrDefault(m => m.AffCode == affCode && m.IsSpecific && m.AffDomain == affDomain);

            if (redirectSpecialDomain == null)
            {
                var redirectDomain = masterInfoValidations.FirstOrDefault(m => m.AffCode == affCode 
                    && !m.IsSpecific && m.AffDomain == affDomain && m.LanguageCode == lang);
                if (redirectDomain == null)
                {
                    ViewBag.Result = "This link does not exists. Please contact administrator!";
                    return new ValidateResult
                    {
                        IsValid = false,
                        RedirectView = "Link"
                    };
                }
                else
                {
                    //licensee domain
                    var licenseeDomain = $"{(redirectDomain.IsSSL ? "https" : "http")}://{redirectDomain.RedirectDomain}/{redirectDomain.LanguageLink}/{pageCode}?affCode={redirectDomain.AffCode}";
                    return new ValidateResult
                    {
                        IsValid = true,
                        RedirectView = licenseeDomain
                    };
                }
            }
            else
            {
                var specialDomain = $"{(redirectSpecialDomain.IsSSL ? "https" : "http")}://{redirectSpecialDomain.RedirectDomain}/{redirectSpecialDomain.LanguageLink}/{pageCode}?affCode={redirectSpecialDomain.AffCode}";
                return new ValidateResult
                {
                    IsValid = true,
                    RedirectView = specialDomain
                };
            }
        }

        public async Task<IActionResult> SpecialPageCodeLink(string pageCodePart1, string pageCodePart2, string affCode, string? lang = null, string? channel = null)
        {
            return await Link($"{pageCodePart1}/{pageCodePart2}", affCode, lang, channel);
        }

        private async Task InsertAffiliateToQueue(
            long clickId,
            string affCode,
            string pageCode,
            string lang,
            string affDomain,
            string affMediaLinkWithoutProtocol,
            string channel,
            string redirectLink)
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

            var trackingSystem = new TrackingSystem
            {
                ClickId = clickId,
                AffCode = affCode,
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
                LanguageCode = mappingRemovedLanguage,
                PageCode = pageCodeOnV2,
                RedirectURL = redirectLink,
                DeviceType = GetDeviceType(),
                AffDomain = affDomain,
                AffMediaLink = affMediaLinkWithoutProtocol,
                ChannelCode = channel ?? string.Empty,
                UpdatedTime = DateTime.Now,
            };

            await messageQueueService.PublishAsync(trackingSystem);
        }

        private IActionResult InsertAffiliate(
            string affCode,
            string pageCode,
            string lang,
            string affDomain,
            string affMediaLinkWithoutProtocol,
            string? channel = null)
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

            string redirectUrl = trackingSystemService.InsertStatistic(command);

            if (!string.IsNullOrEmpty(redirectUrl))
            {
                WriteLogInfo(command);

                bool isMarketingLandingPage = Array.Exists(
                    _marketingPageCodeRegexPatterns,
                    reg => RegexHelper.IsMatch(pageCode, reg, RegexOptions.IgnoreCase));

                string mappingPageCode = isMarketingLandingPage
                    ? (IsValidAffMarketing(affCode) ? pageCodeOnV2 : LandingPage.JOIN_PAGE_CODE)
                    : pageCodeOnV2;

                string result = HandleRedirectUrl(redirectUrl, pageCodeOnV2, command.LanguageCode, mappingPageCode);

                return Redirect(result);
            }

            ViewBag.Result = LinkIsInvalidMessage;
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

        private bool ValidateTrackingV1MediaLink(
            int affId,
            string affDomain,
            string lang,
            string affMediaLinkWithoutProtocol)
        {
            string mappingRemovedLanguage = GetMappingRemovedLanguageCode(lang);
            var mappingMarketInfo = trackingSystemService.GetMarketInfo(mappingRemovedLanguage);

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

            var checkMarketingLinkResult = trackingSystemService.GetTrackingMarketingLinkInfo(checkingCommand);

            if (checkMarketingLinkResult.IsValidDomain && !checkMarketingLinkResult.IsExistedTracking)
            {
                var insertCommand = new ToggleMarketingLinkCommand
                {
                    AffId = affId,
                    LanguageCode = mappingMarketInfo.MarketId,
                    AffMediaLink = $"https://{affMediaLinkWithoutProtocol}"
                };

                trackingSystemService.ToggleMarketingLink(insertCommand);
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
            return Array.Exists(_marketingForCommGroups, c => IsMatching(c, commGroup.GroupName));
        }

        private string GetFullAffDomain(string urlWithoutProtocol, string endPointOfAffDomain)
        {
            if (string.IsNullOrEmpty(urlWithoutProtocol) || string.IsNullOrEmpty(endPointOfAffDomain))
            {
                return string.Empty;
            }

            string resultUrl = urlWithoutProtocol;
            if (_isRemovePortFromUrl)
            {
                if (Uri.TryCreate($"https://{urlWithoutProtocol}", UriKind.Absolute, out var uri))
                {
                    resultUrl = urlWithoutProtocol.Replace($":{uri.Port}", string.Empty, StringComparison.Ordinal);
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
                    string pathAndQueryString = redirectPattern.Replace("#language#", customLanguageCode, StringComparison.OrdinalIgnoreCase);
                    return $"{redirectUri.Scheme}://{redirectUri.Host}{pathAndQueryString}{redirectUri.Query}";
                }
            }

            return redirectUrl
                .Replace($"/{pageCode}", $"/{mappingPageCode}", StringComparison.OrdinalIgnoreCase)
                .Replace($"/{languageCode}/", $"/{customLanguageCode}/", StringComparison.OrdinalIgnoreCase);
        }

        private static byte GetDeviceType()
        {
            return Common.IsMobile() ? (byte)1 : (byte)0;
        }

        private static bool IsMatching(string value, string strToMatch)
        {
            return string.Equals(value, strToMatch, StringComparison.OrdinalIgnoreCase);
        }
    }
}