using System;
using System.Data.SqlClient;
using System.Text;
using System.Web;
using Fanex.Logging;

namespace AffiliateUser.Business.Helper
{
    public static class LogHelper
    {
        public static void WriteError(Exception exception, string additionalMessage = null)
        {
            var request = HttpContext.Current.Request;
            var content = BuildMessage(exception.Message);
            if (!string.IsNullOrWhiteSpace(additionalMessage))
            {
                content.AppendLine(additionalMessage);
            }

            content.AppendLine("SOURCE: " + exception.Source)
                   .AppendLine("TARGETSITE: " + exception.TargetSite)
                   .AppendLine("STACKTRACE: " + exception.StackTrace);

            var sqlException = exception as SqlException;
            if (sqlException != null)
            {
                content.AppendLine("Procedure: " + sqlException.Procedure)
                    .AppendLine("Line Number: " + sqlException.LineNumber)
                    .AppendLine("Server: " + sqlException.Server);
            }

            LogService.Error(content.ToString(), exception);
        }

        public static void WriteError(string message)
        {
            var content = BuildMessage(message);
            LogService.Error(content.ToString());
        }

        private static StringBuilder BuildMessage(string message)
        {
            var request = HttpContext.Current.Request;
            var builder = new StringBuilder(message).AppendLine();
            builder.AppendLine("DOMAIN: " + request.Headers["Host"])
                   .AppendLine("FORM: " + string.Join("~", request.Form))
                   .AppendLine("IP: " + Common.GetIPAddress())
                   .AppendLine("Url: " + request.RawUrl)
                   .AppendLine("Referrer: " + request.UrlReferrer)
                   .AppendLine("QUERYSTRING: " + string.Join("~", request.QueryString))
                   .AppendLine("Browser: " + request.Browser.Type.ToString())
                   .AppendLine("BrowserVer: " + request.Browser.Version.ToString())
                   .AppendLine("Agent: " + request.UserAgent.ToString());

            builder.AppendLine("UserName: " + AppSettings.Session.UserName);

            return builder;
        }
    }
}