using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Routing;
using Fanex.Affiliate.Admin.Core.Services.Common;

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
                var permissionList = sessionService.UserPerissionList;
                var hasPerimission = permissionList.Any(i => i.IsHostedRequest == this.IsHost
                && i.FunctionName.Equals(this.FunctionName, StringComparison.OrdinalIgnoreCase));

                if (!hasPerimission)
                {
                    if (context.Request.IsAjaxRequest())
                    {
                        filterContext.HttpContext.Response.StatusCode = (int)HttpStatusCode.Forbidden;
                        filterContext.HttpContext.Response.ContentType = "application/json";
                        filterContext.Result = new JsonResult
                        {
                            Data = new { ErrCode = 403, ErrMsg = "Access Denied." },
                            JsonRequestBehavior = JsonRequestBehavior.AllowGet
                        };
                    }
                    else
                    {
                        context.Response.Redirect("~/AccessDenied.aspx");
                    }
                }
            }

            base.OnActionExecuting(filterContext);

        }
    }
}
