using System.Configuration;

namespace Fanex.Affiliate.Link.Controllers
{
    using Commands;
    using Fanex.Affiliate.Link.Constants;
    using Fanex.Affiliate.Link.Extensions;
    using Fanex.Logging;
    using Helpers;
    using ONEbook.UM.AspNetMVC.Filters;
    using Services.Impl;
    using Services.Interface;
    using System;
    using System.Collections.Specialized;
    using System.Text;
    using System.Web.Mvc;

    /// <summary>
    /// Home controller for affiliate link.
    /// </summary>
    [CheckUMFilter]
    public class HomeController : Controller
    {
        #region Services
        private const string LOCAL_ENV = "LOCAL";

        private static readonly string[] MarketingForCommGroups = ConfigurationManager.AppSettings["MarketingForCommGroups"].ToString().Split(',');
        private static readonly string[] MarketingLandingPages = ConfigurationManager.AppSettings["MarketingLandingPages"].ToString().Split(',');
        private static readonly bool IsRemovePortFromUrl = IsMatching(ConfigurationManager.AppSettings["RemovePortFromUrl"].ToString(), "1");
        private static readonly bool IsLocal = IsMatching(ConfigurationManager.AppSettings["Environment"].ToString(), LOCAL_ENV);

        private readonly ITrackingSystemService _trackingSystemService;
        private readonly IAffInfoService _affInfoService;

        private const string LinkIsInvalidMessage = "This link does not exists. Please contact administrator!";
        private const string LinkIsIncorrectMessage = "This link is in an incorrect format. Please contact administrator!";

        #endregion Services

        #region Constructor

        public HomeController()
        {
            _trackingSystemService = new TrackingSystemService();
            _affInfoService = new AffInfoService();
        }

        #endregion Constructor

        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Error()
        {
            return View();
        }

        [HttpGet]
        public ActionResult PageNotFound()
        {
            return View();
        }

        public ActionResult OldLink(int affId, string pageCode, string lang = null)
        {
            try
            {
                var affInfo = _affInfoService.GetAffInfoByAffId(affId);

                if (!string.IsNullOrEmpty(affInfo.AffCode) && !string.IsNullOrEmpty(lang))
                {
                    var affMediaLinkWithoutProtocol = GetAffMediaLinkWithoutProtocol();
                    var affDomain = GetFullAffDomain(affMediaLinkWithoutProtocol, affId.ToString());
                    var isValidDomain = ValidateTrackingV1MediaLink(affId, affDomain, lang, affMediaLinkWithoutProtocol);

                    if (isValidDomain)
                    {
                        return InsertAffiliate(affInfo.AffCode, pageCode, lang, affDomain, affMediaLinkWithoutProtocol, affId);
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

        public ActionResult SpecialPageCodeOldLink(int affId, string pageCodePart1, string pageCodePart2, string lang = null)
        {
            return OldLink(affId, $"{pageCodePart1}/{pageCodePart2}", lang);
        }

        public ActionResult Link(string pageCode, string affCode, string lang = null, string channel = null)
        {
            try
            {
                var affInfo = _affInfoService.GetAffInfoByAffCode(affCode);

                if (affInfo.AffId == 0 || string.IsNullOrEmpty(lang))
                {
                    ViewBag.Result = LinkIsInvalidMessage;

                    return View("Link");
                }

                var affMediaLinkWithoutProtocol = GetAffMediaLinkWithoutProtocol();
                var affDomain = GetFullAffDomain(affMediaLinkWithoutProtocol, pageCode);

                return InsertAffiliate(affCode, pageCode, lang, affDomain, affMediaLinkWithoutProtocol, affInfo.AffId, channel);
            }
            catch (Exception ex)
            {
                LogHelper.WriteError(ex);
                ViewBag.Result = LinkIsIncorrectMessage;
                return View("Link");
            }
        }

        public ActionResult SpecialPageCodeLink(string pageCodePart1, string pageCodePart2, string affCode, string lang = null, string channel = null)
        {
            return Link($"{pageCodePart1}/{pageCodePart2}", affCode, lang, channel);
        }

        private ActionResult InsertAffiliate(string affCode, string pageCode, string lang, string affDomain,
            string affMediaLinkWithoutProtocol, int affId = 0, string channel = null)
        {
            if (Request.Url != null)
            {
                var urlReferrer = Request.UrlReferrer?.AbsoluteUri;
                var domainReferrer = Request.UrlReferrer?.Host;

                if (string.IsNullOrEmpty(urlReferrer) && !string.IsNullOrEmpty(Request["ref"]))
                {
                    urlReferrer = Request["ref"];
                    domainReferrer = Common.GetDomainReferrer(urlReferrer);
                }

                var pageCodeOnV2 = GetSupportedPageCodeOnV2(pageCode);
                var command = new TrackingSystemCommand
                {
                    AffDomain = affDomain,
                    AffCode = affCode,
                    PageCode = pageCodeOnV2,
                    ChannelCode = channel,
                    LanguageCode = lang,
                    DeviceType = GetDeviceType(),
                    IP = Request.GetClientIP(),
                    Browser = Request.Browser.Browser,
                    URLReferrer = urlReferrer,
                    DomainReferrer = domainReferrer,
                    OS = Common.GetOsName(Request.UserAgent),
                    Device = Common.GetDeviceName(Request.UserAgent),
                    BrowserVersion = Request.Browser.Version,
                    BrowserResolution = Request.Browser.ScreenPixelsWidth + "x" + Request.Browser.ScreenPixelsHeight,
                    BrowserLanguage = string.IsNullOrEmpty(Request.ServerVariables["HTTP_ACCEPT_LANGUAGE"])
                                              ? string.Empty
                                              : Request.ServerVariables["HTTP_ACCEPT_LANGUAGE"],
                    UserAgent = Request.UserAgent,
                    Country = Common.GetCountry(),
                    AffMediaLink = affMediaLinkWithoutProtocol
                };
                HandleFakeSpecificDomainOnLocal(affId, command);

                var redirectUrl = _trackingSystemService.InsertStatistic(command);

                LogService.Info(GetAllHeader(Request.ServerVariables, command));

                if (!string.IsNullOrEmpty(redirectUrl))
                {
                    var isMarketingLandingPage = Array.Exists(MarketingLandingPages, l => IsMatching(l, pageCode));

                    if ((isMarketingLandingPage && IsValidAffMarketing(affCode)) || !isMarketingLandingPage)
                    {
                        var result = HandleRedirectUrl(redirectUrl, pageCodeOnV2, command.LanguageCode);

                        return Redirect(result);
                    }
                }

                ViewBag.Result = LinkIsInvalidMessage;
            }

            return View("Link");
        }

        private string GetAllHeader(NameValueCollection headers, TrackingSystemCommand command)
        {
            var builder = new StringBuilder().AppendLine($"Link Go Info: {Request.Url}").AppendLine();
            builder.AppendLine("REQUEST INFO")
                   .AppendLine("RawUrl: " + Request.RawUrl)
                   .AppendLine("AffMediaLinkWithoutProtocol: " + command.AffMediaLink)
                   .AppendLine("RedirectPage: " + command.RedirectPage)
                   .AppendLine("AffDomain: " + command.AffDomain)
                   .AppendLine("AffCode: " + command.AffCode)
                   .AppendLine("PageCode: " + command.PageCode)
                   .AppendLine("ChannelCode: " + command.ChannelCode)
                   .AppendLine("LanguageCode: " + command.LanguageCode)
                   .AppendLine("DeviceType: " + command.DeviceType)
                   .AppendLine("IP: " + command.IP)
                   .AppendLine("Browser: " + command.Browser)
                   .AppendLine("Country: " + command.Country)
                   .AppendLine("Referrer: " + command.URLReferrer)
                   .AppendLine("DomainReferrer: " + command.DomainReferrer);

            foreach (var key in headers.AllKeys)
            {
                builder.AppendLine($"{key}: {headers[key]}");
            }

            return builder.ToString();
        }

        private bool ValidateTrackingV1MediaLink(int affId, string affDomain, string lang, string affMediaLinkWithoutProtocol)
        {
            var marketInfo = _trackingSystemService.GetMarketInfo(lang);

            if (string.IsNullOrEmpty(marketInfo.MarketId))
            {
                return false;
            }

            var customDomain = affDomain;
            var affMediaLink = affMediaLinkWithoutProtocol;

            if (IsLocal && SpecificDomain.FakeSpecificDomainByAffOnLocal.TryGetValue(affId.ToString(), out string fakeInfo))
            {
                var infos = fakeInfo.Split(',');
                var fakeDomain = infos[0];

                if (affDomain.Contains(fakeDomain))
                {
                    var realDomain = infos[1];
                    customDomain = affDomain.Replace(fakeDomain, realDomain);
                    affMediaLink = affMediaLinkWithoutProtocol.Replace(fakeDomain, realDomain);
                }
            }

            var checkingCommand = new CheckMarketingLinkCommand
            {
                AffId = affId,
                AffDomain = customDomain,
                LanguageCode = marketInfo.MarketId,
                AffMediaLink = affMediaLink
            };

            var checkMarketingLinkResult = _trackingSystemService.GetTrackingMarketingLinkInfo(checkingCommand);

            if(checkMarketingLinkResult.IsValidDomain && !checkMarketingLinkResult.IsExistedTracking)
            {
                var insertCommand = new ToggleMarketingLinkCommand
                {
                    AffId = affId,
                    LanguageCode = marketInfo.MarketId,
                    AffMediaLink = $"https://{affMediaLinkWithoutProtocol}"
            };

                _trackingSystemService.ToggleMarketingLink(insertCommand);
            }

            return checkMarketingLinkResult.IsValidDomain;
        }

        private static void HandleFakeSpecificDomainOnLocal(int affId, TrackingSystemCommand param)
        {
            if (IsLocal && SpecificDomain.FakeSpecificDomainByAffOnLocal.TryGetValue(affId.ToString(), out string fakeInfo))
            {
                var infos = fakeInfo.Split(',');
                var fakeDomain = infos[0];

                if (param.AffDomain.Contains(fakeDomain))
                {
                    var realDomain = infos[1];
                    param.AffDomain = param.AffDomain.Replace(fakeDomain, realDomain);
                    param.AffMediaLink = param.AffMediaLink.Replace(fakeDomain, realDomain);
                }
            }
        }

        private static string GetSupportedPageCodeOnV2(string pageCode)
        {
            if(LandingPage.SupportedPageCodeOnV1.TryGetValue(pageCode.ToLowerInvariant(), out string pageCodeOnV2))
            {
                return pageCodeOnV2;
            }

            return pageCode;
        }

        private bool IsValidAffMarketing(string affCode)
        {
            var commGroup = _affInfoService.GetCommGroupByAffCode(affCode);

            return Array.Exists(MarketingForCommGroups, c => IsMatching(c, commGroup.GroupName));
        }

        private static string GetFullAffDomain(string urlWithoutProtocol, string endPointOfAffDomain)
        {
            if (IsRemovePortFromUrl)
            {
                var uri = new Uri($"https://{urlWithoutProtocol}");
                urlWithoutProtocol = urlWithoutProtocol.Replace(":" + uri.Port, string.Empty);
            }

            var pos = urlWithoutProtocol.IndexOf("/" + endPointOfAffDomain, 0, StringComparison.OrdinalIgnoreCase);

            return urlWithoutProtocol.Substring(0, pos);
        }

        private string GetAffMediaLinkWithoutProtocol()
        {
            var host = Request.Headers["Host"] ?? Request.Url.Host;
            var path = Request.Url.AbsolutePath;
            return $"{host}{path}";
        }

        private static string GetSupportedLanguageCodeOnMemberSiteV2(string languageCode)
        {
            if (Language.SupportedLanguageCodeOnV1.TryGetValue(languageCode.ToLowerInvariant(), out string languageCodeOnV2))
            {
                return languageCodeOnV2;
            }

            return languageCode;
        }

        private static string HandleRedirectUrl(string redirectUrl, string pageCode, string languageCode)
        {
            var customLanguageCode = GetSupportedLanguageCodeOnMemberSiteV2(languageCode);
            var redirectPattern = ConfigurationManager.AppSettings[$"Redirect_{pageCode.ToLowerInvariant()}_pattern"];

            if (!string.IsNullOrEmpty(redirectPattern))
            {
                var redirectUri = new Uri(redirectUrl);
                var pathAndQueryString = redirectPattern.Replace("#language#", customLanguageCode).Replace("#queryString#", redirectUri.Query);
                return $"{redirectUri.Scheme}://{redirectUri.Host}{pathAndQueryString}";
            }

            return redirectUrl.Replace($"/{languageCode}/", $"/{customLanguageCode}/");
        }

        private static byte GetDeviceType()
        {
            return (byte)(Common.IsMobile() ? 1 : 0);
        }

        private static bool IsMatching(string value, string strToMatch)
        {
            return string.Equals(value, strToMatch, StringComparison.OrdinalIgnoreCase);
        }
    }
}