using System;
using System.Net;
using System.Security.Claims;
using System.Web.Mvc;
using System.Web.Routing;
using Fanex.Affiliate.User.Core.CoreService;

namespace Fanex.Affiliate.User.Core.Filters
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = true, AllowMultiple = true)]
    public sealed class CustomAuthorizeAttribute : AuthorizeAttribute
    {
        public string PermissionName { get; set; }

        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            var sessionService = DependencyResolver.Current.GetService<ISessionService>();

            var skipAuthorization = filterContext.ActionDescriptor.IsDefined(typeof(AllowAnonymousAttribute), true)
            || filterContext.ActionDescriptor.ControllerDescriptor.IsDefined(typeof(AllowAnonymousAttribute), true);
            var context = filterContext.RequestContext.HttpContext;
            base.OnAuthorization(filterContext);
            if (!skipAuthorization)
            {
                var authorized = sessionService.UserSession != null && context.Request.IsAuthenticated
                    && context.User.Identity.IsAuthenticated;

                if (!authorized)
                {
                    HandleResponse(filterContext);

                    context.Session.Clear();
                    context.Response.Cookies.Clear();

                }
                else
                {
                    if(!string.IsNullOrEmpty(PermissionName))
                    {
                        ClaimPermission(filterContext, sessionService.IsInternal);
                    }
                }
            }
        }

        private void ClaimPermission(AuthorizationContext filterContext, bool isInternal)
        {
            var claims = filterContext.HttpContext.User as ClaimsPrincipal;
            var hasPermission = claims.HasClaim(c => c.Type == PermissionName);

            if(isInternal && !hasPermission)
            {
                HandleResponse(filterContext);
            }
        }

        private void HandleResponse(AuthorizationContext filterContext)
        {
            var context = filterContext.RequestContext.HttpContext;
            if (context.Request.IsAjaxRequest())
            {
                context.Response.StatusCode = (int)HttpStatusCode.Forbidden;
                context.Response.ContentType = "application/json";
                filterContext.Result = new JsonResult
                {
                    Data = new { ErrCode = HttpStatusCode.Forbidden, ErrMsg = "UnAuthorized" },
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet
                };
            }
            else
            {
                var routeValue = new
                {
                    controller = "Home",
                    action = "Index",
                };

                filterContext.Result = new RedirectToRouteResult("Default", new RouteValueDictionary(routeValue));
            }
        }
    }
}
