using System;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace Fanex.Affiliate.ThirdPartyProduct.Attributes
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true)]
    public sealed class FunctionAuthorizeAttribute : AuthorizeAttribute
    {
        private const int ErrdCodeAccessDenied = 403;

        public bool IsHost { get; set; }

        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            base.OnAuthorization(filterContext);
            var context = filterContext.HttpContext;
            var allowedUser = ConfigurationManager.AppSettings["AllowedUser"] ?? string.Empty;

            if (!string.IsNullOrEmpty(allowedUser.Trim()))
            {
                var currentUser = context.User.Identity.Name;
                var userList = allowedUser.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                var hasPerimission = userList.Any(i => string.Equals(currentUser, i, StringComparison.InvariantCultureIgnoreCase));

                if (!hasPerimission)
                {
                    if (context.Request.IsAjaxRequest())
                    {
                        filterContext.HttpContext.Response.StatusCode = (int)HttpStatusCode.Forbidden;
                        filterContext.HttpContext.Response.ContentType = "application/json";
                        filterContext.Result = new JsonResult
                        {
                            Data = new { ErrCode = ErrdCodeAccessDenied, ErrMsg = "Access Denied." },
                            JsonRequestBehavior = JsonRequestBehavior.AllowGet
                        };
                    }
                    else
                    {
                        context.Response.Redirect("~/Portal/AccessDenied");
                    }
                }
            }
            else
            {
                context.Response.Redirect("~/Portal/AccessDenied");
            }

        }

    }
}
