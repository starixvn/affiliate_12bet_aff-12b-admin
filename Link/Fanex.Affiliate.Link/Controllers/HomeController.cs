using System.Configuration;

namespace Fanex.Affiliate.Link.Controllers
{
    using System;
    using System.Globalization;
    using System.Web.Mvc;
    using Criterias;
    using Helpers;
    using Services.Impl;
    using Services.Interface;

    /// <summary>
    /// Home controller for affiliate link.
    /// </summary>
    public class HomeController : Controller
    {
        #region Services

        private readonly ITrackingSystemService _trackingSystemService;

        #endregion Services

        #region Constructor

        public HomeController()
        {
            _trackingSystemService = new TrackingSystemService();
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

        public ActionResult Link(int affId, string pageId, string lang)
        {
            return InsertAffiliate(affId, pageId, lang);
        }

        public ActionResult OldLink(int affId, string pageId, string lang, string deviceType)
        {
            return InsertAffiliate(affId, pageId, lang);
        }

        private ActionResult InsertAffiliate(int affId, string pageId, string lang)
        {
            if (Request.Url != null)
            {
                var affDomain = GetFullAffDomain(Request.Url.ToString(), affId);
                var urlReferrer = Request.UrlReferrer != null ? Request.UrlReferrer.AbsoluteUri : null;
                var domainReferrer = Request.UrlReferrer != null ? Request.UrlReferrer.Host : null;

                if (string.IsNullOrEmpty(urlReferrer) && !string.IsNullOrEmpty(Request["ref"]))
                {
                    urlReferrer = Request["ref"];
                    var referrerUri = new Uri(urlReferrer);
                    domainReferrer = string.IsNullOrEmpty(referrerUri.Host) ? null : referrerUri.Host;
                }

                var criteria = new TrackingSystemCriteria
                {
                    AffDomain = affDomain,
                    AffiliateID = affId,
                    PageCode = pageId,
                    LanguageCode = lang,
                    DeviceType = GetDeviceType(),
                    IP = Common.GetIPAddressOfClient(),
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
                    Country = Common.ResolveCountry() != null
                                      ? Common.ResolveCountry().EnglishName
                                      : CultureInfo.CurrentCulture.ToString(),
                        
                };

                var redirectUrl = _trackingSystemService.InsertStatistic(criteria);

                if (!string.IsNullOrEmpty(redirectUrl))
                {
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
    }
}