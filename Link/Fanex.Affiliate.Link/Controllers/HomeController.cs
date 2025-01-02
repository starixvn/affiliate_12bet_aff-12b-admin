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
        private const string OLD_MKT_SIGNUP_PAGE_CODE = "inrsignup";
        private const string NEW_MKT_SIGNUP_PAGE_CODE = "mktsignup";

        private static readonly string[] MarketingForCommGroups = ConfigurationManager.AppSettings["MarketingForCommGroups"].ToString().Split(',');
        private static readonly string[] MarketingLandingPages = ConfigurationManager.AppSettings["MarketingLandingPages"].ToString().Split(',');
        private static readonly bool IsRemovePortFromUrl = IsMatching(ConfigurationManager.AppSettings["RemovePortFromUrl"].ToString(), "1");
        private static readonly bool IsLocal = IsMatching(ConfigurationManager.AppSettings["Environment"].ToString(), LOCAL_ENV);

        private readonly ITrackingSystemService _trackingSystemService;
        private readonly IGeneralAffiliateInfoService _generalAffiliateInfoDataAccess;

        private const string BENGALI_SHORTED_CODE = "bn";
        private const string BENGALI_COUNTRY_CODE = "bd";

        private const string LinkIsInvalidMessage = "This link does not exists. Please contact administrator!";
        private const string LinkIsIncorrectMessage = "This link is incorrect. Please contact administrator!";

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
                var affDomain = GetFullAffDomain(GetOriginalUrl(), affId.ToString());
                var affInfo = _generalAffiliateInfoDataAccess.GetGeneralAffiliateInfo(affId: affId);

                if (string.IsNullOrEmpty(affInfo.AffCode))
                {
                    ViewBag.Result = LinkIsInvalidMessage;

                    return View("Link");
                }

                return InsertAffiliate(affInfo.AffCode, pageCode, lang, affDomain);
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

        public ActionResult Link(string pageCode, string affCode, string lang = null)
        {
            try
            {
                var affDomain = GetFullAffDomain(GetOriginalUrl(), pageCode);

                return InsertAffiliate(affCode, pageCode, lang, affDomain);
            }
            catch (Exception ex)
            {
                LogHelper.WriteError(ex);
                ViewBag.Result = LinkIsIncorrectMessage;
                return View("Link");
            }
        }

        public ActionResult SpecialPageCodeLink(string pageCodePart1, string pageCodePart2, string affCode, string lang = null)
        {
            return Link($"{pageCodePart1}/{pageCodePart2}", affCode, lang);
        }

        private ActionResult InsertAffiliate(string affCode, string pageCode, string lang, string affDomain)
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

                var command = new TrackingSystemCommand
                {
                    AffDomain = affDomain,
                    AffCode = affCode,
                    PageCode = IsMatching(pageCode, OLD_MKT_SIGNUP_PAGE_CODE) ? NEW_MKT_SIGNUP_PAGE_CODE : pageCode,
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
                HandleFakeSpecificDomainOnLocal(command);

                var redirectUrl = _trackingSystemService.InsertStatistic(command);

                if (!string.IsNullOrEmpty(redirectUrl))
                {

                    var isMarketingLandingPage = Array.Exists(MarketingLandingPages, l => IsMatching(l, pageCode));

                    if ((isMarketingLandingPage && IsValidAffMarketing(affCode)) || !isMarketingLandingPage)
                    {
                        var result = HandleRedirectUrl(redirectUrl, pageCode, command.LanguageCode, affCode);

                        return Redirect(result);
                    }
                }

                ViewBag.Result = LinkIsInvalidMessage;
            }

            return View("Link");
        }

        private static void HandleFakeSpecificDomainOnLocal(TrackingSystemCommand param)
        {
            if (IsLocal && SpecificDomain.FakeSpecificDomainByAffOnLocal.TryGetValue(param.AffCode, out string fakeInfo))
            {
                var infos = fakeInfo.Split(',');
                var fakeDomain = infos[0];

                if (param.AffDomain.Contains(fakeDomain))
                {
                    var realDomain = infos[1];
                    param.AffDomain = param.AffDomain.Replace(fakeDomain, realDomain);
                }
            }
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

        private static string HandleRedirectUrl(string redirectUrl, string pageCode, string languageCode, string affCode)
        {
            var customLanguageCode = languageCode;
            if (IsMatching(BENGALI_SHORTED_CODE, languageCode))
            {
                customLanguageCode = BENGALI_COUNTRY_CODE;
            }

            var redirectPattern = ConfigurationManager.AppSettings[$"Redirect_{pageCode.ToLowerInvariant()}_pattern"];

            if (!string.IsNullOrEmpty(redirectPattern))
            {
                var redirectUri = new Uri(redirectUrl);
                var pathAndQueryString = redirectPattern.Replace("#language#", customLanguageCode).Replace("#affCode#", affCode);
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