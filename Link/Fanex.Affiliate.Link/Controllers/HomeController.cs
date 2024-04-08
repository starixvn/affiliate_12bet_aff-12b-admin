using System.Configuration;

namespace Fanex.Affiliate.Link.Controllers
{
    using System;
    using System.Globalization;
    using System.Linq;
    using System.Web.Mvc;
    using Commands;
    using Helpers;
    using ONEbook.UM.AspNetMVC.Filters;
    using Services.Impl;
    using Services.Interface;

    /// <summary>
    /// Home controller for affiliate link.
    /// </summary>
    [CheckUMFilter]
    public class HomeController : Controller
    {
        private const string SIGN_UP_PAGE_CODE = "signup";
        private const string MALAYSIA_COUNTRY_CODE = "MY";
        private const string SPECIAL_PAGE_CODE = "12sports";
        private const string OLD_MKT_SIGNUP_PAGE_CODE = "inrsignup";
        private const string NEW_MKT_SIGNUP_PAGE_CODE = "mktsignup";
        private readonly string[] SpecialSubPageCodes = new [] { "cricket", "virtualsports" };

        private readonly string[] SpecialAffIds;
        private readonly string[] MarketingForCommGroups;
        private readonly string[] MarketingLandingPages;
        private readonly string[] ODIWorldCupLandingPages;
        private readonly string[] SupportedODIWorldCupLanguages;
        #region Services

        private readonly ITrackingSystemService _trackingSystemService;
        private readonly IAffiliateService _affiliateService;

        #endregion Services

        #region Constructor

        public HomeController()
        {
            _trackingSystemService = new TrackingSystemService();
            _affiliateService = new AffiliateService();
            SpecialAffIds = ConfigurationManager.AppSettings["SpecialAffIds"].ToString().Split(',');
            MarketingForCommGroups = ConfigurationManager.AppSettings["MarketingForCommGroups"].ToString().Split(',');
            MarketingLandingPages = ConfigurationManager.AppSettings["MarketingLandingPages"].ToString().Split(',');
            ODIWorldCupLandingPages = ConfigurationManager.AppSettings["ODIWorldCupLandingPages"].ToString().Split(',');
            SupportedODIWorldCupLanguages = ConfigurationManager.AppSettings["SupportedODIWorldCupLanguages"].ToString().Split(',');
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

        public ActionResult Link(int affId, string pageCode, string lang)
        {
            var isSpecialAff = IsSpecialAffiliate(affId, pageCode);

            if (isSpecialAff)
            {
                var defaultLanguageStatistic = ConfigurationManager.AppSettings["DefaultLanguageStatistic"].ToString();
                return InsertAffiliate(affId, pageCode, defaultLanguageStatistic, MALAYSIA_COUNTRY_CODE, true, lang);
            }

            var targetCountryCode = _affiliateService.GetTargetCountryCodeByLanguage(lang);

            if (targetCountryCode is null)
            {
                return IvalidLink();
            }

            return InsertAffiliate(affId, pageCode, lang, targetCountryCode);
        }

        public ActionResult CommonLink(int affId, string pageCode, string lang, string targetCountry)
        {
            //Check "{affId}/{pageCodePart1}/{pageCodePart2}/{lang}" case
            if (IsMatching(pageCode, SPECIAL_PAGE_CODE) && SpecialSubPageCodes.Any(s => IsMatching(s, lang)))
            {
                return Link(affId, pageCode: $"{pageCode}/{lang}", lang: targetCountry);
            }

            var targetCountryCode = _affiliateService.GetTargetCountryCodeByLanguage(lang);

            if (targetCountryCode is null || !IsMatching(targetCountry.ToUpperInvariant(), targetCountryCode))
            {
                return IvalidLink();
            }

            return InsertAffiliate(affId, pageCode, lang, targetCountry);
        }

        public ActionResult SpecialPageCodeLink(int affId, string pageCodePart1, string pageCodePart2, string lang, string targetCountry)
        {
            return InsertAffiliate(affId, $"{pageCodePart1}/{pageCodePart2}", lang, targetCountry);
        }

        private ActionResult InsertAffiliate(int affId, string pageCode, string lang, string targetCountry, bool isSpecialAff = false, string replacedLang = null)
        {

            if (Request.Url != null)
            {
                var affDomain = GetFullAffDomain(Request.Url.ToString(), affId);
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
                    AffiliateID = affId,
                    PageCode = IsMatching(pageCode, OLD_MKT_SIGNUP_PAGE_CODE) ? NEW_MKT_SIGNUP_PAGE_CODE : pageCode,
                    LanguageCode = lang,
                    TargetCountryCode = targetCountry.ToUpperInvariant(),
                    DeviceType = GetDeviceType(),
                    IP = Common.GetIPAddressOfClient(),
                    Browser = Request.Browser.Browser,
                    URLReferrer = isSpecialAff ? urlReferrer?.Replace($"/{replacedLang}/", $"/{lang}/") : urlReferrer,
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

                };

                var redirectUrl = _trackingSystemService.InsertStatistic(command);

                if (!string.IsNullOrEmpty(redirectUrl))
                {
                    if (isSpecialAff)
                    {
                        var customRedirectUrl = redirectUrl.Replace($"/{lang}/", "/");

                        return Redirect(customRedirectUrl);
                    }

                    var isMarketingLandingPage = MarketingLandingPages.Any(l => IsMatching(l, pageCode));
                    var isODIWorldCupLandingPage = ODIWorldCupLandingPages.Any(l => IsMatching(l, pageCode));

                    if (!(isMarketingLandingPage || isODIWorldCupLandingPage))
                    {
                        return Redirect(redirectUrl);
                    }

                    if (isMarketingLandingPage && IsValidAffMarketing(affId))
                    {
                        return Redirect(redirectUrl);
                    }

                    if (isODIWorldCupLandingPage && IsValidODIWorldCupLink(lang))
                    {
                        return Redirect(redirectUrl);
                    }
                }
            }

            return IvalidLink();
        }

        private ActionResult IvalidLink()
        {
            ViewBag.Result = "This link does not exists. Please contact administrator!";

            return View("Link");
        }

        private string GetFullAffDomain(string url, int affId)
        {
            if (byte.Parse(ConfigurationManager.AppSettings["RemovePortFromUrl"]) == 1)
            {
                var uri = new Uri(url);
                url = url.Replace(":" + uri.Port, string.Empty);
            }

            const string slashString = "://";
            var pos1 = url.IndexOf(slashString, 0, StringComparison.OrdinalIgnoreCase);
            var pos2 = url.IndexOf("/" + affId.ToString(CultureInfo.InvariantCulture), 0, StringComparison.OrdinalIgnoreCase);

            return url.Substring(pos1 + slashString.Length, pos2 - (pos1 + slashString.Length));
        }

        private byte GetDeviceType()
        {
            return (byte)(Common.IsMobile() ? 1 : 0);
        }

        private bool IsSpecialAffiliate(int affId, string pageCode)
        {  
            return SpecialAffIds.Contains(affId.ToString()) && IsMatching(SIGN_UP_PAGE_CODE, pageCode);
        }

        private bool IsValidODIWorldCupLink(string languageCode)
        {
            return !string.IsNullOrEmpty(languageCode) && SupportedODIWorldCupLanguages.Any(l => IsMatching(l, languageCode));
        }
        
        private bool IsValidAffMarketing(int affId)
        {
            var commGroup = _affiliateService.GetCommGroupByAffId(affId);

            return MarketingForCommGroups.Any(c => IsMatching(c, commGroup));
        }

        private bool IsMatching(string value, string strToMatch)
        {
            return string.Equals(value, strToMatch, StringComparison.OrdinalIgnoreCase);
        }
    }
}