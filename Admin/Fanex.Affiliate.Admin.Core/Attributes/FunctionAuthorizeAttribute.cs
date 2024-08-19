using Fanex.Affiliate.Admin.Core.Constants;
using System;
using System.Net;
using System.Security.Claims;
using System.Security.Principal;
using System.Web.Mvc;

namespace Fanex.Affiliate.Admin.Core.Attributes
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true)]
    public sealed class FunctionAuthorizeAttribute : ActionFilterAttribute
    {
        public string FunctionName { get; set; }

        public string FunctionNameOption { get; set; }

        public bool IsHost { get; set; }

        public string ClaimValue => IsHost ? Function.WritePermission : string.Empty;

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var context = filterContext.HttpContext;
            var skipCheckAuthorization = filterContext.ActionDescriptor.IsDefined(typeof(AllowByPassFunctionAuthorizeAttribute), true);

            if (!(skipCheckAuthorization || string.IsNullOrEmpty(FunctionName)))
            {
                var hasPerimission = HasPerimission(filterContext.HttpContext.User);

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

        private bool HasPerimission(IPrincipal principal)
        {
            if (principal is ClaimsPrincipal claims)
            {
                if (string.IsNullOrEmpty(FunctionNameOption))
                {
                    return HasClaim(claims, FunctionName);
                }

                return HasClaim(claims, FunctionName) || HasClaim(claims, FunctionNameOption);
            }

            return false;
        }

        private bool HasClaim(ClaimsPrincipal claimsPrincipal, string funcName)
        {
            return IsHost ? claimsPrincipal.HasClaim(funcName, ClaimValue) : claimsPrincipal.HasClaim(c => c.Type == funcName);
        }
    }
}