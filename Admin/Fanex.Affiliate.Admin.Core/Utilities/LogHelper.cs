using Fanex.Logging;
using System;
using System.Data.SqlClient;
using System.Text;
using System.Web;

namespace Fanex.Affiliate.Admin.Core.Utilities
{
    public static class LogHelper
    {
        public static void WriteError(Exception exception, string additionalMessage = null)
        {
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

        private static StringBuilder BuildMessage(string message)
        {
            var request = HttpContext.Current.Request;
            var builder = new StringBuilder(message).AppendLine();
            builder.AppendLine("DOMAIN: " + request.Headers["Host"])
                   .AppendLine("FORM: " + string.Join("~", request.Form))
                   .AppendLine("Url: " + request.RawUrl)
                   .AppendLine("Referrer: " + request.UrlReferrer)
                   .AppendLine("QUERYSTRING: " + string.Join("~", request.QueryString))
                   .AppendLine("Browser: " + request.Browser.Type)
                   .AppendLine("BrowserVer: " + request.Browser.Version)
                   .AppendLine("Agent: " + request.UserAgent);

            builder.AppendLine("UserName: " + HttpContext.Current.Session["UserName"]);

            return builder;
        }
    }
}
