using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Routing;
using Fanex.Affiliate.User.Core.CoreService;

namespace Fanex.Affiliate.User.Core.Filters
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = false, AllowMultiple = true)]
    public sealed class CustomAuthorizeAttribute: AuthorizeAttribute
    {
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            var sessionService = DependencyResolver.Current.GetService<ISessionService>();

            var skipAuthorization = filterContext.ActionDescriptor.IsDefined(typeof(AllowAnonymousAttribute), true)
            || filterContext.ActionDescriptor.ControllerDescriptor.IsDefined(typeof(AllowAnonymousAttribute), true);
            var context = filterContext.RequestContext.HttpContext;

            if (!skipAuthorization)
            {
                var authorized = sessionService.UserSession != null && context.Request.IsAuthenticated
                    && context.User.Identity.IsAuthenticated;

                if(!authorized)
                {
                    if (context.Request.IsAjaxRequest())
                    {
                        context.Response.StatusCode = (int)HttpStatusCode.Forbidden;
                        context.Response.ContentType = "application/json";
                        filterContext.Result = new JsonResult
                        {
                            Data = new { ErrCode = HttpStatusCode.Forbidden, ErrMsg = "UnAuthorized" },
                            JsonRequestBehavior = JsonRequestBehavior.AllowGet
                        };
                    }else
                    {
                        var routeValue = new
                        {
                            controller = "Home",
                            action = "Index",
                        };

                        filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(routeValue));
                    }

                    context.Session.Clear();
                    context.Response.Cookies.Clear();

                }
                else
                {
                    base.OnAuthorization(filterContext);
                }
            }
           
        }
    }
}
