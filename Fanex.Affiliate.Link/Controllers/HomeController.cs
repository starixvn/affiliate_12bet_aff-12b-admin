// ----------------------------------------------------------------------
// <copyright file="HomeController.cs" company="Nexcel Solutions Vietnam">
//     Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// ------------------------------------------------------------------------
// <history>
//   <change who="Long.Luu" date="2013/08/05">Created</change>
//   <change who="" date=""></change>
// </history>

using System.Configuration;

namespace Fanex.Affiliate.Link.Controllers
{
    using System;
    using System.Globalization;
    using System.Web.Mvc;
    using Criterias;
    using Fanex.Affiliate.Utilities;
    using Services.Impl;
    using Services.Interface;

    /// <summary>
    /// Home controller for affiliate link.
    /// </summary>
    public class HomeController : Controller
    {
        #region Const

        #endregion Const

        #region Services
        /// <summary>
        /// Define Payment service.
        /// </summary>
        private readonly ITrackingSystemService _trackingSystemService;

        #endregion Services

        #region Constructor
        /// <summary>
        /// Initializes a new instance of the HomeController class.
        /// </summary>
        public HomeController()
        {
            _trackingSystemService = new TrackingSystemService();
        }
        #endregion Constructor

        /// <summary>
        /// Main controller of this class.
        /// </summary>
        /// <returns>Action result.</returns>
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Action Error page.
        /// </summary>
        /// <returns>Action result.</returns>
        [HttpGet]
        public ActionResult Error()
        {
            return View();
        }

        /// <summary>
        /// Action page not found.
        /// </summary>
        /// <returns>Action result.</returns>
        [HttpGet]
        public ActionResult PageNotFound()
        {
            return View();
        }

        /// <summary>
        /// Affiliate link for desktop/laptop.
        /// </summary>
        /// <param name="affId">Affiliate ID.</param>
        /// <param name="pageId">Page identification.</param>
        /// <param name="lang">Language ID.</param>
        /// <returns>Action result.</returns>
        public ActionResult Link(int affId, string pageId, string lang)
        {
            return InsertAffiliate(affId, pageId, lang, string.Empty);
        }

        /// <summary>
        /// Affiliate link for mobile.
        /// </summary>
        /// <param name="affId">Affiliate ID.</param>
        /// <param name="pageId">Page identification.</param>
        /// <param name="lang">Language ID.</param>
        /// <param name="deviceType">Device Type.</param>
        /// <returns>Action result.</returns>
        public ActionResult LinkMobile(int affId, string pageId, string lang, string deviceType)
        {
            return InsertAffiliate(affId, pageId, lang, deviceType);
        }

        /// <summary>
        /// Insert Affiliate Link.
        /// </summary>
        /// <param name="affId">Affiliate ID.</param>
        /// <param name="pageId">Page identification.</param>
        /// <param name="lang">Language ID.</param>
        /// <param name="deviceType">Device Type.</param>
        /// <returns>Action result.</returns>
        private ActionResult InsertAffiliate(int affId, string pageId, string lang, string deviceType)
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
                        DeviceType = GetDeviceType(deviceType),
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
                                      : CultureInfo.CurrentCulture.ToString()
                    };

                var redirectUrl = _trackingSystemService.InsertStatistic(criteria);

                if (!string.IsNullOrEmpty(redirectUrl))
                {
                    return Redirect(redirectUrl);
                }

                ViewBag.Result = "This affiliate does not exists. Please contact administrator!";
            }

            return View("Link");
        }

        /// <summary>
        /// Get full affiliate domain from affiliate link.
        /// </summary>
        /// <param name="url">Affiliate link.</param>
        /// <param name="affId">Affiliate Id.</param>
        /// <returns>Full affiliate domain.</returns>
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

        /// <summary>
        /// Convert into device type.
        /// </summary>
        /// <param name="deviceType">Device Type string.</param>
        /// <returns>Actual device type.</returns>
        private byte GetDeviceType(string deviceType)
        {
            if (string.IsNullOrEmpty(deviceType) || !deviceType.Equals("mobile", StringComparison.OrdinalIgnoreCase))
            {
                return 0;
            }

            return 1;
        }
    }
}
