using System.Configuration;

namespace Fanex.Affiliate.Link.Controllers
{
    using System;
    using System.Globalization;
    using System.Linq;
    using System.Web.Mvc;
    using Commands;
    using Helpers;
    using Services.Impl;
    using Services.Interface;

    /// <summary>
    /// Home controller for affiliate link.
    /// </summary>
    public class HomeController : Controller
    {
        private const string SIGN_UP_PAGE_CODE = "signup";
        private readonly string[] SpecialAffIds;
        #region Services

        private readonly ITrackingSystemService _trackingSystemService;

        #endregion Services

        #region Constructor

        public HomeController()
        {
            _trackingSystemService = new TrackingSystemService();
            SpecialAffIds = ConfigurationManager.AppSettings["SpecialAffIds"].ToString().Split(',');
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
                return InsertAffiliate(affId, pageCode, defaultLanguageStatistic, true);
            }

            return InsertAffiliate(affId, pageCode, lang);
        }

        public ActionResult NewLink(int affId, string pageCodePart1, string pageCodePart2, string lang)
        {
            return InsertAffiliate(affId, $"{pageCodePart1}/{pageCodePart2}", lang);
        }

        private ActionResult InsertAffiliate(int affId, string pageCode, string lang, bool isSpecialAff = false, string replacedLang = null)
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
                    PageCode = pageCode,
                    LanguageCode = lang,
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

                    return Redirect(redirectUrl);
                }

                ViewBag.Result = "This link does not exists. Please contact administrator!";
            }

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
            return SpecialAffIds.Contains(affId.ToString()) && string.Equals(SIGN_UP_PAGE_CODE, pageCode, StringComparison.OrdinalIgnoreCase);
        }
    }
}