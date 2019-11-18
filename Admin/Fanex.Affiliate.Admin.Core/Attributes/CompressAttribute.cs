using System;
using System.IO.Compression;
using System.Web.Mvc;

namespace Fanex.Affiliate.Admin.Core.Attributes
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true)]
    public class CompressAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var encodingsAccepted = filterContext.HttpContext.Request.Headers["Accept-Encoding"];
            if (string.IsNullOrEmpty(encodingsAccepted))
            {
                return;
            }

            encodingsAccepted = encodingsAccepted.ToLowerInvariant();
            if (!encodingsAccepted.Contains("deflate"))
            {
                return;
            }

            var response = filterContext.HttpContext.Response;

            response.AppendHeader("Content-Encoding", "deflate");
            response.Filter = new DeflateStream(response.Filter, CompressionMode.Compress);
        }
    }
}
