using System;
using System.Data.SqlClient;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Fanex.Affiliate.User.Core.CoreService;
using Fanex.Logging;
using Newtonsoft.Json;

namespace Fanex.Affiliate.User.Core.Filters
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true)]
    public class GlobalErrorHandlerAttribute : FilterAttribute, IExceptionFilter
    {
        public void OnException(ExceptionContext filterContext)
        {
            var exception = filterContext.Exception;
            var httpException = exception as HttpException;
            if (httpException != null && httpException.GetHttpCode() == 404)
            {
                return;
            }

            var httpContext = filterContext.RequestContext.HttpContext;
            var message = BuildErrorMessage(exception, httpContext);

            LogService.Error(message.ToString());

            if (httpContext.Request.IsAjaxRequest())
            {
                httpContext.ClearError();

                var response = httpContext.Response;
                response.ContentType = "application/json";
                response.StatusCode = 500;
                response.Write(JsonConvert.SerializeObject(new
                {
                    Success = false,
                    exception.Message
                }));
            }
            else
            {
                var routeValue = new
                {
                    controller = "Notify",
                    action = "Error"
                };

                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(routeValue));
            }
        }

        private static StringBuilder BuildErrorMessage(Exception exception, HttpContextBase httpContext)
        {

            var httpRequest = httpContext.Request;
            var message = new StringBuilder()
                            .AppendLine($"Error:{exception.Message}")
                            .AppendLine($"Url: { httpRequest.RawUrl}")
                            .AppendLine($"UrlReferrer: {httpRequest.UrlReferrer} ")
                            .AppendLine($"QUERYSTRING: {httpRequest.QueryString}")
                            .AppendLine($"FORM: {string.Join("~", httpRequest.Form)}")
                            .AppendLine($"Browser:{httpRequest.Browser.Type}")
                            .AppendLine($"BrowserVer:{httpRequest.Browser.Version}")
                            .AppendLine($"Agent:{httpRequest.UserAgent}")
                            .AppendLine($"Source:{exception.Source}")
                            .AppendLine($"TargetSite:{exception.TargetSite}");

            var sessionService = DependencyResolver.Current.GetService<ISessionService>();
            if (sessionService != null)
            {
                message.AppendLine($"Current Language: {sessionService.CurrentLanguage}");
                message.AppendLine($"IsInternal: {sessionService.IsInternal}");

                if (sessionService.UserSession != null)
                {
                    message.AppendLine($"User Info: {JsonConvert.SerializeObject(sessionService.UserSession)}");
                }
            }

            var sqlException = exception as SqlException;
            if (sqlException != null)
            {
                message.AppendLine($"Procedure: { sqlException.Procedure}")
                    .AppendLine($"Line Number: {sqlException.LineNumber}")
                    .AppendLine($"Server: {sqlException.Server}");
            }

            message.AppendLine($"StackTrace: {exception.StackTrace}");
            message.AppendLine($"InnerException: {exception.InnerException}");

            return message;
        }
    }
}
