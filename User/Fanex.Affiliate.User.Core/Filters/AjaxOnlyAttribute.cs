using System;
using System.Linq;
using System.Reflection;
using System.Web.Mvc;
using Fanex.Affiliate.User.Core.CoreService;

namespace Fanex.Affiliate.User.Core.Filters
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = false, AllowMultiple = true)]
    public sealed class AjaxOnlyAttribute : ActionMethodSelectorAttribute
    {
        public string PermissionName { get; set; }

        public override bool IsValidForRequest(ControllerContext controllerContext, MethodInfo methodInfo)
        {
            var sessionService = DependencyResolver.Current.GetService<ISessionService>();
            var hasPermission = true;

            if (!string.IsNullOrEmpty(PermissionName) && sessionService.IsInternal)
            {
                var claims = sessionService.ClaimsIdentities.FirstOrDefault();

                hasPermission = claims.HasClaim(c => c.Type == PermissionName);
            }

            return controllerContext.HttpContext.Request.IsAjaxRequest() && hasPermission;
        }
    }
}
