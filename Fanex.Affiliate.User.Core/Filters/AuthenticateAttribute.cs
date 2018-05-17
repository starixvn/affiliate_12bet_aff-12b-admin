using System;
using System.Net;
using System.Web.Mvc;
using System.Web.Mvc.Filters;
using System.Web.Routing;
using System.Web.Security;
using Fanex.Affiliate.User.Core.CoreService;

namespace Fanex.Affiliate.User.Core.Filters
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = false, AllowMultiple = true)]
    public sealed class AuthenticateAttribute : FilterAttribute, IAuthenticationFilter
    {
        public void OnAuthentication(AuthenticationContext filterContext)
        {
            var sessionService = DependencyResolver.Current.GetService<ISessionService>();
            var user = filterContext.HttpContext.User;
            if (sessionService.UserSession == null || user == null || !user.Identity.IsAuthenticated)
            {
                if (filterContext.HttpContext.Request.IsAjaxRequest())
                {
                    var response = filterContext.HttpContext.Response;
                    response.StatusCode = (int)HttpStatusCode.Unauthorized;
                    response.ContentType = "application/json";
                    response.SuppressFormsAuthenticationRedirect = true;
                    response.End();

                    filterContext.Result = new JsonResult
                    {
                        Data = new { ErrCode = 403, ErrMsg = "Access Denied." },
                        JsonRequestBehavior = JsonRequestBehavior.AllowGet
                    };
                }
                else
                {
                    filterContext.HttpContext.Session.Clear();
                    FormsAuthentication.SignOut();

                    var routeValue = new
                    {
                        controller = "Home",
                        action = "Index"
                    };

                    filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(routeValue));
                }
            }
        }

        public void OnAuthenticationChallenge(AuthenticationChallengeContext filterContext)
        {
            // Method intentionally left empty.
        }
    }
}