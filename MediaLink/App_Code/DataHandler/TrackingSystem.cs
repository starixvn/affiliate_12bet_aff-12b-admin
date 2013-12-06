using System;
using System.Web;
using System.Collections;
using System.Configuration;
using DNAide.Data;
using DNAide.Core;
using System.Globalization;
using System.Collections.Generic;
/*
 * Class to handle Clicks Tracking
 * Author: Peter
 * Date : May 05,2009
 */
namespace DataHandler
{
    public class TrackingSystem
    {
        public static bool InsertStatistic(int affId, string trackingCode, string pageCode, string languageCode, HttpRequest request, out string redirectPage, out Int64 clickID)
        {
            Hashtable hsParam = new Hashtable();
            hsParam.Add("clickid", 0);
            hsParam.Add("affiliateid", affId);
            hsParam.Add("trackingcode", trackingCode);
            hsParam.Add("pagecode", pageCode);
            hsParam.Add("languagecode", languageCode);
            hsParam.Add("ip", GetIPAddressOfClient());
            hsParam.Add("browser", request.Browser.Browser);
            hsParam.Add("urlreferrer", request.UrlReferrer != null ? request.UrlReferrer.AbsoluteUri : null);
            hsParam.Add("domainreferrer", request.UrlReferrer != null ? request.UrlReferrer.Host : null);
            hsParam.Add("os", GetOSName(request.UserAgent));
            hsParam.Add("device", GetDeviceName(request.UserAgent));
            hsParam.Add("browserversion", request.Browser.Version);
            hsParam.Add("browserresolution", request.Browser.ScreenPixelsWidth.ToString() + "x" + request.Browser.ScreenPixelsHeight.ToString());
            hsParam.Add("browserlanguage", request.ServerVariables["HTTP_ACCEPT_LANGUAGE"]);
            hsParam.Add("useragent", request.UserAgent);
            hsParam.Add("country", ResolveCountry() != null ? ResolveCountry().EnglishName : System.Globalization.CultureInfo.CurrentCulture.ToString());

            var data = DbHelper.Execute("Aff_AffiliateInsClickStatistics_New", hsParam, DBType.AFFILIATE_12BET);
            int result = int.Parse(data.Outputs["errcode"].ToString());
            redirectPage = data.Outputs["redirectpage"].ToString();
            clickID = Int64.Parse(data.Outputs["clickid"].ToString());
            return result == 0 ? true : false;
        }



        public static string GetIPAddressOfClient()
        {
            string ip;
            ip = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            if (ip == null)
            {
                ip = HttpContext.Current.Request.UserHostAddress;
            }
            if (ip == "::1")
            {
                ip = "127.0.0.1";
            }
            return ip;
        }
        public static Int64 GetIPValue()
        {
            string ip = GetIPAddressOfClient();
            if (string.IsNullOrEmpty(ip) || ip == "127.0.0.1")
            {
                return 0;
            }
            else
            {
                IList<string> parts = GetStringIList(ip, ".");
                return 16777216 * int.Parse(parts[0]) + 65536 * int.Parse(parts[1]) + 256 * int.Parse(parts[2]) + int.Parse(parts[3]);
            }
        }
        public static IList<string> GetStringIList(string stringArray, string sep)
        {
            string[] separator = { sep };
            string[] result = stringArray.Split(separator, StringSplitOptions.RemoveEmptyEntries);

            IList<string> data = new List<string>();
            foreach (var item in result)
            {
                data.Add(item);
            }
            return data;
        }
        public static string GetKeyword(Uri urlReferrer)
        {
            if (urlReferrer != null)
            {
                System.Collections.Specialized.NameValueCollection parameters = HttpUtility.ParseQueryString(urlReferrer.Query);
                if (urlReferrer.Host.Contains("google.com") ||
                    urlReferrer.Host.Contains("bing.com") ||
                    urlReferrer.Host.Contains("ask.com"))
                {
                    return parameters["q"];
                }
                else if (urlReferrer.Host.Contains("yahoo.com"))
                {
                    return parameters["p"];
                }
                else
                {
                    return string.Empty;
                }
            }
            else
            {
                return string.Empty;
            }
        }

        public static RegionInfo ResolveCountry()
        {
            CultureInfo culture = ResolveCulture();
            if (culture != null)
                return new RegionInfo(culture.LCID);

            return null;
        }

        public static CultureInfo ResolveCulture()
        {
            string[] languages = HttpContext.Current.Request.UserLanguages;

            if (languages == null || languages.Length == 0)
                return null;

            try
            {
                string language = languages[0].ToLowerInvariant().Trim();
                return CultureInfo.CreateSpecificCulture(language);
            }
            catch (ArgumentException)
            {
                return null;
            }
        }

        public static string GetDeviceName(string userAgent)
        {
            string device = "Computer/Laptop";

            if (userAgent.Contains("iPhone"))
            {
                device = "iPhone";
            }
            if (userAgent.Contains("iPad"))
            {
                device = "iPad";
            }
            if (userAgent.Contains("iPod"))
            {
                device = "iPod";
            }

            if (userAgent.Contains("BlackBerry"))
            {
                device = "BlackBerry";
            }

            if (userAgent.Contains("HTC"))
            {
                device = "HTC";
            }

            if (userAgent.Contains("Motorola"))
            {
                device = "Motorola";
            }

            if (userAgent.Contains("Nokia"))
            {
                device = "Nokia";
            }

            return device;
        }

        public static string GetOSName(string userAgent)
        {
            string os = string.Empty;
            //microsoft
            if (userAgent.Contains("Windows NT 6.1"))
            {
                os = "Windows 7";
            }
            else if (userAgent.Contains("Windows NT 6.0"))
            {
                os = "Windows Vista";
            }
            else if (userAgent.Contains("Windows NT 5.2"))
            {
                os = "Windows Server 2003";
            }
            else if (userAgent.Contains("Windows NT 5.1"))
            {
                os = "Windows XP";
            }
            else if (userAgent.Contains("Windows NT 5.0"))
            {
                os = "Windows 2000";
            }
            else if (userAgent.Contains("Windows NT 4.1"))
            {
                os = "Windows 98";
            }
            else if (userAgent.Contains("Windows"))
            {
                os = "Windows";
            }
            //apple
            else if (userAgent.Contains("iPhone") ||
                        userAgent.Contains("iPad") ||
                        userAgent.Contains("iPod"))
            {
                os = "iOS";
            }
            else if (userAgent.Contains("Mac OS"))
            {
                os = "Mac OS";
            }
            //RIM
            else if (userAgent.Contains("BlackBerry"))
            {
                os = "BlackBerry";
            }
            //Google
            else if (userAgent.Contains("Android"))
            {
                os = "Android";
            }
            //Linux
            else if (userAgent.Contains("Linux"))
            {
                os = "Linux";
            }
            else if (userAgent.Contains("Ubuntu"))
            {
                os = "Ubuntu";
            }
            else
                os = "unknown";
            return os;

        }
    }
 }

