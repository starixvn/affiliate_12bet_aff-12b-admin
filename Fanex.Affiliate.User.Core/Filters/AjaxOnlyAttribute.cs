using System;
using System.Net;
using System.Web.Mvc;
using Fanex.Logging;

namespace Fanex.Affiliate.User.Core.Filters
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = false, AllowMultiple = true)]
    public sealed class AjaxOnlyAttribute : AuthorizeAttribute
    {
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            base.OnAuthorization(filterContext);
            try
            {
                if (!filterContext.HttpContext.Request.IsAjaxRequest())
                {
                    var response = filterContext.HttpContext.Response;
                    response.StatusCode = (int)HttpStatusCode.Unauthorized;
                    response.End();
                }
            }
            catch (Exception exception)
            {
                LogService.Error(exception.Message, exception);
                throw;
            }
        }
    }
}
