using System.Configuration;

namespace Fanex.Affiliate.Link.Controllers
{
    using System;
    using System.Globalization;
    using System.Text.RegularExpressions;
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
        public ActionResult Link(int affId, string pageId, string lang, string deviceType)
        {
            return InsertAffiliate(affId, pageId, lang);
        }

        /// <summary>
        /// Insert Affiliate Link.
        /// </summary>
        /// <param name="affId">Affiliate ID.</param>
        /// <param name="pageId">Page identification.</param>
        /// <param name="lang">Language ID.</param>
        /// <param name="deviceType">Device Type.</param>
        /// <returns>Action result.</returns>
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
                                      : CultureInfo.CurrentCulture.ToString()
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
        private byte GetDeviceType()
        {
            return (byte)(IsMobile() ? 1 : 0);
        }

        private bool IsMobile()
        {
            if (Request.Browser.IsMobileDevice)
            {
                return true;
            }

            var u = Request.ServerVariables["HTTP_USER_AGENT"];
            var b = new Regex(@"(android|bb\d+|meego).+mobile|avantgo|bada\/|blackberry|blazer|compal|elaine|fennec|hiptop|iemobile|ip(hone|od)|iris|kindle|lge |maemo|midp|mmp|mobile.+firefox|netfront|opera m(ob|in)i|palm( os)?|phone|p(ixi|re)\/|plucker|pocket|psp|series(4|6)0|symbian|treo|up\.(browser|link)|vodafone|wap|windows ce|xda|xiino", RegexOptions.IgnoreCase | RegexOptions.Multiline);
            var v = new Regex(@"1207|6310|6590|3gso|4thp|50[1-6]i|770s|802s|a wa|abac|ac(er|oo|s\-)|ai(ko|rn)|al(av|ca|co)|amoi|an(ex|ny|yw)|aptu|ar(ch|go)|as(te|us)|attw|au(di|\-m|r |s )|avan|be(ck|ll|nq)|bi(lb|rd)|bl(ac|az)|br(e|v)w|bumb|bw\-(n|u)|c55\/|capi|ccwa|cdm\-|cell|chtm|cldc|cmd\-|co(mp|nd)|craw|da(it|ll|ng)|dbte|dc\-s|devi|dica|dmob|do(c|p)o|ds(12|\-d)|el(49|ai)|em(l2|ul)|er(ic|k0)|esl8|ez([4-7]0|os|wa|ze)|fetc|fly(\-|_)|g1 u|g560|gene|gf\-5|g\-mo|go(\.w|od)|gr(ad|un)|haie|hcit|hd\-(m|p|t)|hei\-|hi(pt|ta)|hp( i|ip)|hs\-c|ht(c(\-| |_|a|g|p|s|t)|tp)|hu(aw|tc)|i\-(20|go|ma)|i230|iac( |\-|\/)|ibro|idea|ig01|ikom|im1k|inno|ipaq|iris|ja(t|v)a|jbro|jemu|jigs|kddi|keji|kgt( |\/)|klon|kpt |kwc\-|kyo(c|k)|le(no|xi)|lg( g|\/(k|l|u)|50|54|\-[a-w])|libw|lynx|m1\-w|m3ga|m50\/|ma(te|ui|xo)|mc(01|21|ca)|m\-cr|me(rc|ri)|mi(o8|oa|ts)|mmef|mo(01|02|bi|de|do|t(\-| |o|v)|zz)|mt(50|p1|v )|mwbp|mywa|n10[0-2]|n20[2-3]|n30(0|2)|n50(0|2|5)|n7(0(0|1)|10)|ne((c|m)\-|on|tf|wf|wg|wt)|nok(6|i)|nzph|o2im|op(ti|wv)|oran|owg1|p800|pan(a|d|t)|pdxg|pg(13|\-([1-8]|c))|phil|pire|pl(ay|uc)|pn\-2|po(ck|rt|se)|prox|psio|pt\-g|qa\-a|qc(07|12|21|32|60|\-[2-7]|i\-)|qtek|r380|r600|raks|rim9|ro(ve|zo)|s55\/|sa(ge|ma|mm|ms|ny|va)|sc(01|h\-|oo|p\-)|sdk\/|se(c(\-|0|1)|47|mc|nd|ri)|sgh\-|shar|sie(\-|m)|sk\-0|sl(45|id)|sm(al|ar|b3|it|t5)|so(ft|ny)|sp(01|h\-|v\-|v )|sy(01|mb)|t2(18|50)|t6(00|10|18)|ta(gt|lk)|tcl\-|tdg\-|tel(i|m)|tim\-|t\-mo|to(pl|sh)|ts(70|m\-|m3|m5)|tx\-9|up(\.b|g1|si)|utst|v400|v750|veri|vi(rg|te)|vk(40|5[0-3]|\-v)|vm40|voda|vulc|vx(52|53|60|61|70|80|81|83|85|98)|w3c(\-| )|webc|whit|wi(g |nc|nw)|wmlb|wonu|x700|yas\-|your|zeto|zte\-", RegexOptions.IgnoreCase | RegexOptions.Multiline);

            return (b.IsMatch(u) || v.IsMatch(u.Substring(0, 4)));
        }
    }
}