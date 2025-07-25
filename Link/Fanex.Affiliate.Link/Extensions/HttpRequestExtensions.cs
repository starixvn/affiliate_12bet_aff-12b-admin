using Fanex.Affiliate.Link.Constants;
using Fanex.Affiliate.Link.Helpers;
using System;
using System.Linq;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Http;

namespace Fanex.Affiliate.Link.Extensions
{
    public static class HttpRequestExtensions
    {
        private const string IPv4Regex = "^(([0-9]|[1-9][0-9]|1[0-9]{2}|2[0-4][0-9]|25[0-5])\\.){3}([0-9]|[1-9][0-9]|1[0-9]{2}|2[0-4][0-9]|25[0-5])$";

        public static string GetClientIP(this HttpRequest request)
        {
            var xActualIP = request.Headers[HeaderKey.HTTP_X_ACTUAL_IP].ToString();
            var xForwardedFor = !string.IsNullOrWhiteSpace(xActualIP) ? xActualIP : request.Headers[HeaderKey.HTTP_X_FORWARDED_FOR].ToString();

            var clientValidIP = GetValidIPAddress(xForwardedFor);

            if (string.IsNullOrWhiteSpace(clientValidIP))
            {
                clientValidIP = request.HttpContext.Connection.RemoteIpAddress?.ToString();
            }

            return clientValidIP?.Equals("::1") == true ? "127.0.0.1" : clientValidIP;
        }

        private static string GetValidIPAddress(string ips)
        {
            if (string.IsNullOrEmpty(ips))
            {
                return null;
            }

            var ipList = ips.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries).Select(i => i.Trim())
                        .Where(i => RegexHelper.IsMatch(i, IPv4Regex, RegexOptions.None));

            return ipList.FirstOrDefault();
        }
    }
}