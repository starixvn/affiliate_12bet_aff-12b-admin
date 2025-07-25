namespace Fanex.Affiliate.Link.Helpers
{
    using Fanex.Affiliate.Link.Constants;
    using Fanex.Affiliate.Link.DTOs;
    using Newtonsoft.Json;
    using SharpRaven.Data;
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Security.Cryptography;
    using System.Text;
    using System.Text.RegularExpressions;
    using System.Web;

    /// <summary>
    /// Common class.
    /// </summary>
    public static class Common
    {
        private const int ValidLength = 4;

        /// <summary>
        /// Contains all operating system mapping.
        /// </summary>
        private static readonly IReadOnlyDictionary<string, string> _operatingSystemMapping = new Dictionary<string, string>
        {
                { "Windows NT 6.1", "Windows 7" },
                { "Windows NT 6.0", "Windows Vista" },
                { "Windows NT 5.1", "Windows XP" },
                { "Windows NT 5.0", "Windows 2000" },
                { "Windows NT 4.1", "Windows 98" },
                { "iPhone", "iOS" },
                { "iPad", "iOS" },
                { "iPod", "iOS" },
                { "Mac OS", "Mac OS" },
                { "BlackBerry", "BlackBerry" },
                { "Android", "Android" },
                { "Linux", "Linux" },
                { "Ubuntu", "Ubuntu" }
        };

        /// <summary>
        /// Contains all device mapping.
        /// </summary>
        private static readonly IReadOnlyDictionary<string, string> _deviceMapping = new Dictionary<string, string>
        {
                { "iphone", "iPhone" },
                { "ipad", "iPad" },
                { "ipod", "iPod" },
                { "blackberry", "BlackBerry" },
                { "htc", "HTC" },
                { "motorola", "Motorola" },
                { "nokia", "Nokia" }
        };

        /// <summary>
        /// Get Country.
        /// </summary>
        /// <returns>Country English Name.</returns>
        public static string GetCountry()
        {
            try
            {
                var regionInfo = ResolveCountry();
                if (regionInfo != null)
                {
                    return regionInfo.EnglishName;
                }
            }
            catch
            {
                // ignore and fallback
            }

            try
            {
                return new RegionInfo(CultureInfo.CurrentCulture.Name).EnglishName;
            }
            catch
            {
                return "Unknown";
            }
        }

        /// <summary>
        /// Get region info.
        /// </summary>
        /// <returns>Region info.</returns>
        private static RegionInfo ResolveCountry()
        {
            var culture = ResolveCulture();

            //solving a culture issue
            if (culture != null && culture.LCID != 0x7F)
            {
                return new RegionInfo(culture.Name);
            }
            return null;
        }

        /// <summary>
        /// Get culture info.
        /// </summary>
        /// <returns>Culture info.</returns>
        private static CultureInfo ResolveCulture()
        {
            return CultureInfo.InvariantCulture;
        }

        /// <summary>
        /// Get original protocol.
        /// </summary>
        /// <returns>protocol</returns>
        public static string GetOriginalProtocol()
        {
            return "https";
        }

        /// <summary>
        /// Get device info.
        /// </summary>
        /// <param name="userAgent">User's info.</param>
        /// <returns>Device info.</returns>
        public static string GetDeviceName(string userAgent)
        {
            if (string.IsNullOrEmpty(userAgent))
            {
                return @"Computer/Laptop";
            }

            var deviceKey = _deviceMapping.Keys.FirstOrDefault(key => userAgent.ToLower().Contains(key));

            if (string.IsNullOrEmpty(deviceKey))
            {
                return IsMobile() ? "Mobile" : @"Computer/Laptop";
            }

            return _deviceMapping[deviceKey];
        }

        /// <summary>
        /// Get OS info.
        /// </summary>
        /// <param name="userAgent">User's info.</param>
        /// <returns>Operating system info.</returns>
        public static string GetOsName(string userAgent)
        {
            if (string.IsNullOrEmpty(userAgent))
            {
                return string.Empty;
            }

            var osKey = _operatingSystemMapping.Keys.FirstOrDefault(key => userAgent.Contains(key));

            return string.IsNullOrEmpty(osKey) ? string.Empty : _operatingSystemMapping[osKey];
        }

        /// <summary>
        /// Encrypt a string using SHA256.
        /// </summary>
        /// <param name="inputString">Input string.</param>
        /// <returns>Encrypted string.</returns>
        public static string EncryptUsingSHA256(string inputString)
        {
            var hashAlgorithm = new SHA256Managed();
            var hashData = hashAlgorithm.ComputeHash(Encoding.UTF8.GetBytes(inputString));

            return BitConverter.ToString(hashData).Replace("-", string.Empty).ToLower();
        }

        public static bool IsMobile()
        {
            return false; // Simplified for .NET Core - can be enhanced later
        }

        public static string GetDomainReferrer(string urlReferrer)
        {
            if (Uri.IsWellFormedUriString(urlReferrer, UriKind.Absolute))
            {
                var referrerUri = new Uri(urlReferrer);
                return string.IsNullOrEmpty(referrerUri.Host) ? null : referrerUri.Host;
            }

            return null;
        }

        /// <summary>
        /// Get browser name from user agent string.
        /// </summary>
        /// <param name="userAgent">User's user agent string.</param>
        /// <returns>Browser name.</returns>
        public static string GetBrowserName(string userAgent)
        {
            if (string.IsNullOrEmpty(userAgent))
            {
                return string.Empty;
            }

            userAgent = userAgent.ToLowerInvariant();

            if (userAgent.Contains("edg/") || userAgent.Contains("edge/"))
                return "Edge";
            if (userAgent.Contains("opr/") || userAgent.Contains("opera"))
                return "Opera";
            if (userAgent.Contains("chrome") && !userAgent.Contains("edg/") && !userAgent.Contains("opr/"))
                return "Chrome";
            if (userAgent.Contains("safari") && !userAgent.Contains("chrome"))
                return "Safari";
            if (userAgent.Contains("firefox"))
                return "Firefox";
            if (userAgent.Contains("msie") || userAgent.Contains("trident"))
                return "Internet Explorer";

            return "Other";
        }

        /// <summary>
        /// Get browser version from user agent string.
        /// </summary>
        /// <param name="userAgent">User's user agent string.</param>
        /// <returns>Browser version.</returns>
        public static string GetBrowserVersion(string userAgent)
        {
            if (string.IsNullOrEmpty(userAgent))
            {
                return string.Empty;
            }

            userAgent = userAgent.ToLowerInvariant();

            // Edge (Chromium-based and Legacy)
            var edgeMatch = System.Text.RegularExpressions.Regex.Match(userAgent, @"edg/([\d\.]+)");
            if (edgeMatch.Success)
            {
                return edgeMatch.Groups[1].Value;
            }
            edgeMatch = System.Text.RegularExpressions.Regex.Match(userAgent, @"edge/([\d\.]+)");
            if (edgeMatch.Success)
            {
                return edgeMatch.Groups[1].Value;
            }

            // Opera
            var operaMatch = System.Text.RegularExpressions.Regex.Match(userAgent, @"opr/([\d\.]+)");
            if (operaMatch.Success)
            {
                return operaMatch.Groups[1].Value;
            }
            operaMatch = System.Text.RegularExpressions.Regex.Match(userAgent, @"opera/([\d\.]+)");
            if (operaMatch.Success)
            {
                return operaMatch.Groups[1].Value;
            }

            // Chrome
            var chromeMatch = System.Text.RegularExpressions.Regex.Match(userAgent, @"chrome/([\d\.]+)");
            if (chromeMatch.Success && !userAgent.Contains("edg/") && !userAgent.Contains("opr/"))
            {
                return chromeMatch.Groups[1].Value;
            }

            // Safari (but not Chrome)
            if (userAgent.Contains("safari") && !userAgent.Contains("chrome"))
            {
                var safariMatch = System.Text.RegularExpressions.Regex.Match(userAgent, @"version/([\d\.]+)");
                if (safariMatch.Success)
                {
                    return safariMatch.Groups[1].Value;
                }
            }

            // Firefox
            var firefoxMatch = System.Text.RegularExpressions.Regex.Match(userAgent, @"firefox/([\d\.]+)");
            if (firefoxMatch.Success)
            {
                return firefoxMatch.Groups[1].Value;
            }

            // Internet Explorer
            var ieMatch = System.Text.RegularExpressions.Regex.Match(userAgent, @"msie ([\d\.]+)");
            if (ieMatch.Success)
            {
                return ieMatch.Groups[1].Value;
            }
            ieMatch = System.Text.RegularExpressions.Regex.Match(userAgent, @"rv:([\d\.]+)");
            if (ieMatch.Success && userAgent.Contains("trident"))
            {
                return ieMatch.Groups[1].Value;
            }

            return string.Empty;
        }
    }
}