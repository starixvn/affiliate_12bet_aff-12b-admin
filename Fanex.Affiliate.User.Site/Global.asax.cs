using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Script.Serialization;
using Fanex.Affiliate.User.Core.BusinessService;
using Fanex.Affiliate.User.Core.CoreService;
using Fanex.Affiliate.User.Core.Utilities;
using Fanex.Data;
using Fanex.Data.OldConnectionStringProvider;
using Fanex.Logging;

namespace Fanex.Affiliate.User.Site
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            IoCConfig.Register();
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            DbSettingProviderManager
              .StartNewSession()
              .Use(new OldConnectionStringProvider())
              .UseDefaultDbSettingProvider(
                resourcePath: @"~\App_Data\SpConfig\SpName.xml",
                ignoreRedundantParameters: false,
                enableWatching: true)
              .Run();
        }

        protected void Application_AcquireRequestState(object sender, EventArgs e)
        {
            var languageService = DependencyResolver.Current.GetService<ILanguageService>();
            var sessionService = DependencyResolver.Current.GetService<ISessionService>();

            languageService.SetCulture(sessionService.CurrentLanguage);
        }

        protected void Application_Error(object sender, EventArgs e)
        {
            var exception = Server.GetLastError();
            var httpException = exception as HttpException;
            if (httpException != null && httpException.GetHttpCode() == 404)
            {
                return;
            }

            var message = BuildErrorMessage(exception);

            LogService.Error(message.ToString());

            var appSetting = DependencyResolver.Current.GetService<IAppSetting>();

            if (appSetting.IsProduction && Request.RequestContext.HttpContext.Request.IsAjaxRequest())
            {
                Context.ClearError();
                Response.ContentType = "application/json";
                Response.StatusCode = 500;
                Response.Write(new JavaScriptSerializer().Serialize(new
                {
                    Success = false,
                    exception.Message,
                    ErrorCode = exception.GetHashCode(),
                    ErrorMessage = exception.Message
                }));
            }
        }

        private StringBuilder BuildErrorMessage(Exception exception)
        {
            var sessionService = DependencyResolver.Current.GetService<ISessionService>();

            var message = new StringBuilder()
                            .AppendLine($"Error:{exception.Message}")
                            .AppendLine($"Url: { Request.RawUrl}")
                            .AppendLine($"UrlReferrer: {Request.UrlReferrer} ")
                            .AppendLine($"QUERYSTRING: {Request.QueryString}")
                            .AppendLine($"FORM: {string.Join("~", Request.Form)}")
                            .AppendLine($"Browser:{Request.Browser.Type}")
                            .AppendLine($"BrowserVer:{Request.Browser.Version}")
                            .AppendLine($"Agent:{Request.UserAgent}")
                            .AppendLine($"Source:{exception.Source}")
                            .AppendLine($"TargetSite:{exception.TargetSite}");
            if (sessionService.UserSession != null)
            {
                message.AppendLine(new JavaScriptSerializer().Serialize(sessionService.UserSession));
            }
            else
            {
                var context = HttpContext.Current;
                message.AppendLine($"IdentityName: {context?.User?.Identity?.Name}");

                var session = context?.Session;

                if (session != null)
                {
                    message.AppendLine($"SessionId: {session.SessionID}");

                    var sessionKeyList = new List<string>();
                    foreach (var key in session.Keys)
                    {
                        sessionKeyList.Add(key.ToString());
                    }
                    message.AppendLine($"SessionKey: {string.Join(",", sessionKeyList)}");
                }

                var cookies = Request.Cookies;
                if (cookies != null)
                {
                    foreach (var key in cookies.AllKeys)
                    {
                        var cookie = cookies.Get(key);

                        if (cookie != null)
                        {
                            message.AppendLine($"CookieKey:{key} && CookieValue={cookie.Value}");
                        }
                    }
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