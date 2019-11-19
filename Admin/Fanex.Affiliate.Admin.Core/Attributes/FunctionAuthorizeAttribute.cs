using System;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using Fanex.Affiliate.Admin.Core.Services.Common;
using Fanex.Affiliate.Admin.Core.Constants;

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
                var sessionService = DependencyResolver.Current.GetService<ISessionService>();
                var permissionList = sessionService.UserPermissionList;
                var hasPerimission = permissionList.Any(i =>
                i.FunctionName.Equals(this.FunctionName, StringComparison.OrdinalIgnoreCase)
                && (i.IsHost || i.IsHost == IsHost)
                );

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