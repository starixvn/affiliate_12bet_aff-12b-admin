using System;
using System.Configuration;
using System.Web.Mvc;
using Fanex.Affiliate.Admin.Core.Utilities;

namespace Fanex.Affiliate.Admin.Core.Attributes
{
    /// <summary>
    /// Action filter attribute to allow framing of landing page URLs in iframes
    /// by setting appropriate Content Security Policy headers
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true)]
    public class AllowLandingPageFrameAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            try
            {
                var response = filterContext.HttpContext.Response;
                var landingPageUrl = ConfigurationManager.AppSettings["LandingPageUrl"];
                
                if (!string.IsNullOrEmpty(landingPageUrl))
                {
                    // Extract the domain from the landing page URL for CSP
                    var uri = new Uri(landingPageUrl);
                    var landingDomain = $"{uri.Scheme}://{uri.Host}";
                    
                    // Set CSP header to allow framing landing page domain
                    // This is more specific than the global CSP policy
                    var cspPolicy = $"default-src 'self' blob: data:; " +
                                   $"frame-src 'self' {landingDomain} https:; " +
                                   $"script-src 'self' 'unsafe-inline' 'unsafe-eval' blob: data:; " +
                                   $"style-src 'self' 'unsafe-inline' blob: data:; " +
                                   $"img-src 'self' blob: data: https:; " +
                                   $"connect-src 'self' {landingDomain} https:;";
                    
                    // Remove existing CSP header if present
                    response.Headers.Remove("Content-Security-Policy");
                    
                    // Add the new CSP header
                    response.Headers.Add("Content-Security-Policy", cspPolicy);
                    
                    // Also add X-Frame-Options to allow framing from landing domain
                    response.Headers.Remove("X-Frame-Options");
                    response.Headers.Add("X-Frame-Options", "SAMEORIGIN");
                }
            }
            catch (Exception ex)
            {
                // Log error but don't break the application
                LogHelper.WriteError(ex);
            }
            
            base.OnActionExecuting(filterContext);
        }
    }
}
