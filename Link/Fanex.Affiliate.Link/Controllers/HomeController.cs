using System.Configuration;

namespace Fanex.Affiliate.Link.Controllers
{
    using Commands;
    using Fanex.Affiliate.Link.Constants;
    using Fanex.Affiliate.Link.Extensions;
    using Helpers;
    using ONEbook.UM.AspNetMVC.Filters;
    using Services.Impl;
    using Services.Interface;
    using System;
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
        private readonly IGeneralAffiliateInfoService _generalAffiliateInfoDataAccess;

        private const string LinkIsInvalidMessage = "This link does not exists. Please contact administrator!";
        private const string LinkIsIncorrectMessage = "This link is in an incorrect format. Please contact administrator!";

        #endregion Services

        #region Constructor

        public HomeController()
        {
            _trackingSystemService = new TrackingSystemService();
            _generalAffiliateInfoDataAccess = new GeneralAffiliateInfoService();
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
                var affInfo = _generalAffiliateInfoDataAccess.GetGeneralAffiliateInfo(affId: affId);

                if (!string.IsNullOrEmpty(affInfo.AffCode) && !string.IsNullOrEmpty(lang))
                {
                    var affDomain = GetFullAffDomain(GetOriginalUrl(), affId.ToString());
                    var isValidDomain = ValidateTrackingV1MediaLink(affId, affDomain, affInfo.PreferredLanguage);

                    if (isValidDomain)
                    {
                        return InsertAffiliate(affInfo.AffCode, pageCode, lang, affDomain, affId);
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
                var affInfo = _generalAffiliateInfoDataAccess.GetGeneralAffiliateInfo(affCode: affCode);

                if (affInfo.AffId == 0 || string.IsNullOrEmpty(lang))
                {
                    ViewBag.Result = LinkIsInvalidMessage;

                    return View("Link");
                }

                var affDomain = GetFullAffDomain(GetOriginalUrl(), pageCode);

                return InsertAffiliate(affCode, pageCode, lang, affDomain, affInfo.AffId, channel);
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

        private ActionResult InsertAffiliate(string affCode, string pageCode, string lang, string affDomain, int affId = 0, string channel = null)
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
                    AffMediaLink = GetOriginalUrl()
                };
                HandleFakeSpecificDomainOnLocal(affId, command);

                var redirectUrl = _trackingSystemService.InsertStatistic(command);

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

        private bool ValidateTrackingV1MediaLink(int affId, string affDomain, string affLanguage)
        {
            var affMediaLink = GetOriginalUrl();

            var checkingCommand = new CheckMarketingLinkCommand
            {
                AffId = affId,
                AffDomain = affDomain,
                LanguageCode = affLanguage,
                AffMediaLink = affMediaLink
            };

            var checkMarketingLinkResult = _trackingSystemService.GetTrackingMarketingLinkInfo(checkingCommand);

            if(checkMarketingLinkResult.IsValidDomain && !checkMarketingLinkResult.IsExistedTracking)
            {
                var insertCommand = new ToggleMarketingLinkCommand
                {
                    AffId = affId,
                    LanguageCode = affLanguage,
                    AffMediaLink = affMediaLink
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
            var commGroup = _generalAffiliateInfoDataAccess.GetCommGroupByAffCode(affCode);

            return Array.Exists(MarketingForCommGroups, c => IsMatching(c, commGroup.GroupName));
        }

        private static string GetFullAffDomain(string url, string endPointOfAffDomain)
        {
            if (IsRemovePortFromUrl)
            {
                var uri = new Uri(url);
                url = url.Replace(":" + uri.Port, string.Empty);
            }

            const string slashString = "://";
            var pos1 = url.IndexOf(slashString, 0, StringComparison.OrdinalIgnoreCase);
            var pos2 = url.IndexOf("/" + endPointOfAffDomain, 0, StringComparison.OrdinalIgnoreCase);

            return url.Substring(pos1 + slashString.Length, pos2 - (pos1 + slashString.Length));
        }

        private string GetOriginalUrl()
        {
            var host = Request.Headers["Host"] ?? Request.Url.Host;
            var scheme = GetOriginalProtocol();
            var path = Request.Url.AbsolutePath;
            return $"{scheme}://{host}{path}";
        }

        private static byte GetDeviceType()
        {
            return (byte)(Common.IsMobile() ? 1 : 0);
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

        private string GetOriginalProtocol()
        {
            var refLink = Request["Referer"];
            var url = string.IsNullOrWhiteSpace(refLink) ? Request.Url : new Uri(refLink);
            var protos = Request.ServerVariables["HTTP_X_FORWARDED_PROTO"]?.Split(',');
            var origProto = protos != null && protos.Length > 0 ? protos[0].Trim() : url.Scheme;
            return origProto;
        }

        private static bool IsMatching(string value, string strToMatch)
        {
            return string.Equals(value, strToMatch, StringComparison.OrdinalIgnoreCase);
        }
    }
}