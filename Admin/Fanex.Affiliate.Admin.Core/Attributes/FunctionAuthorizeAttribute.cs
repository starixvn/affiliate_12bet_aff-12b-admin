using Fanex.Affiliate.Admin.Core.Constants;
using Fanex.Affiliate.Admin.Core.Services.Common;
using System;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Web.Mvc;

namespace Fanex.Affiliate.Admin.Core.Attributes
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true)]
    public sealed class FunctionAuthorizeAttribute : ActionFilterAttribute
    {
        public string FunctionName { get; set; }

        public bool IsHost { get; set; }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var context = filterContext.HttpContext;

            if (!string.IsNullOrEmpty(FunctionName))
            {
                var claims = filterContext.HttpContext.User as ClaimsPrincipal;
                var claimValue = IsHost ? "IsHost" : String.Empty;
                var hasPerimission = false;
                if (claims != null)
                {
                    hasPerimission = IsHost ? claims.HasClaim(FunctionName, claimValue) : claims.HasClaim(c => c.Type == FunctionName);
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