using Fanex.Affiliate.Admin.Core.Constants;
using System;
using System.Net;
using System.Security.Claims;
using System.Web.Mvc;

namespace Fanex.Affiliate.Admin.Core.Attributes
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true)]
    internal class ClaimAuthorizeAttribute : ActionFilterAttribute
    {
        public string ClaimName { get; set; }

        public string ClaimValue { get; set; }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var context = filterContext.HttpContext;
            var skipCheckAuthorization = filterContext.ActionDescriptor.IsDefined(typeof(AllowByPassFunctionAuthorizeAttribute), true);

            if (!(string.IsNullOrWhiteSpace(ClaimName) || skipCheckAuthorization))
            {
                var claims = filterContext.HttpContext.User as ClaimsPrincipal;
                var hasPerimission = false;
                if (claims != null)
                {
                    hasPerimission = claims.HasClaim(ClaimName, ClaimValue);
                }

                if (!hasPerimission)
                {
                    if (context.Request.IsAjaxRequest())
                    {
                        filterContext.HttpContext.Response.StatusCode = (int)HttpStatusCode.Forbidden;
                        filterContext.HttpContext.Response.ContentType = "application/json";
                        filterContext.Result = new JsonResult
                        {
                            Data = new { ErrCode = Code.ErrCodeAccessDenied, ErrMsg = "Access Denied." },
                            JsonRequestBehavior = JsonRequestBehavior.AllowGet
                        };
                    }
                    else
                    {
                        context.Response.Redirect("~/Portal/AccessDenied");
                    }
                }
            }

            base.OnActionExecuting(filterContext);
        }
    }
}
