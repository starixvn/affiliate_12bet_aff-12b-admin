namespace Fanex.Affiliate.Link.Helpers
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Security.Cryptography;
    using System.Text;
    using System.Web;

    /// <summary>
    /// Common class.
    /// </summary>
    public static class Common
    {
        /// <summary>
        /// Contains all operating system mapping.
        /// </summary>
        private static IDictionary<string, string> _operatingSystemMapping = new Dictionary<string, string>
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
        private static IDictionary<string, string> _deviceMapping = new Dictionary<string, string>
        {
                { "iPhone", "iPhone" },
                { "iPad", "iPad" },
                { "iPod", "iPod" },
                { "BlackBerry", "BlackBerry" },
                { "HTC", "HTC" },
                { "Motorola", "Motorola" },
                { "Nokia", "Nokia" }
        };
        
        /// <summary>
        /// Get region info.
        /// </summary>
        /// <returns>Region info.</returns>
        public static RegionInfo ResolveCountry()
        {
            var culture = ResolveCulture();
            return culture != null ? new RegionInfo(culture.LCID) : null;
        }

        /// <summary>
        /// Get culture info.
        /// </summary>
        /// <returns>Culture info.</returns>
        public static CultureInfo ResolveCulture()
        {
            var languages = HttpContext.Current.Request.UserLanguages;

            if (languages == null || languages.Length == 0)
            {
                return null;
            }

            try
            {
                var language = languages[0].ToLowerInvariant().Trim();
                return CultureInfo.CreateSpecificCulture(language);
            }
            catch (ArgumentException)
            {
                return null;
            }
        }

        /// <summary>
        /// Get IP address from client request.
        /// </summary>
        /// <returns>String of IP address.</returns>
        public static string GetIPAddressOfClient()
        {
            string ips = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            string ip = ips == null ?
                HttpContext.Current.Request.UserHostAddress :
                ips.Split(',').First().Trim();

            if (ip == "::1")
            {
                ip = "127.0.0.1";
            }

            return ip;
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

            foreach (var key in _deviceMapping.Keys.Where(key => userAgent.Contains(key)))
            {
                return _deviceMapping[key];
            }

            return @"Computer/Laptop";
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

            foreach (var key in _operatingSystemMapping.Keys.Where(key => userAgent.Contains(key)))
            {
                return _operatingSystemMapping[key];
            }

            return string.Empty;
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
    }
}