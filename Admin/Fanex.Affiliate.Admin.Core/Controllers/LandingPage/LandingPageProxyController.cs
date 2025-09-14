using System;
using System.Configuration;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Fanex.Affiliate.Admin.Core.Attributes;
using Fanex.Affiliate.Admin.Core.Constants;
using Fanex.Affiliate.Admin.Core.Utilities;

namespace Fanex.Affiliate.Admin.Core.Controllers.LandingPage
{
    /// <summary>
    /// Proxy controller to serve landing page content from same origin
    /// This bypasses CSP restrictions by serving external content from our domain
    /// </summary>
    [FunctionAuthorize(FunctionName = Function.LandingPage)]
    public class LandingPageProxyController : BaseController
    {
        private const int MaxContentSizeBytes = 10 * 1024 * 1024; // 10MB limit
        private const int RequestTimeoutSeconds = 30;
        
        private static readonly HttpClient _httpClient = new HttpClient(new HttpClientHandler
        {
            UseCookies = false // Prevent cookie sharing between requests
        })
        {
            Timeout = TimeSpan.FromSeconds(RequestTimeoutSeconds),
            MaxResponseContentBufferSize = MaxContentSizeBytes
        };

        // Cache for allowed domains to avoid repeated config reads
        private static readonly Lazy<string[]> _allowedDomains = new Lazy<string[]>(GetAllowedDomains);

        static LandingPageProxyController()
        {
            // Configure HttpClient with proper headers
            _httpClient.DefaultRequestHeaders.Add("User-Agent", "LandingPageProxy/1.0");
            _httpClient.DefaultRequestHeaders.Add("Accept", "text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8");
            _httpClient.DefaultRequestHeaders.Add("Accept-Language", "en-US,en;q=0.5");
            _httpClient.DefaultRequestHeaders.Add("Accept-Encoding", "gzip, deflate");
            _httpClient.DefaultRequestHeaders.Add("DNT", "1");
            _httpClient.DefaultRequestHeaders.Add("Connection", "keep-alive");
            _httpClient.DefaultRequestHeaders.Add("Upgrade-Insecure-Requests", "1");
        }
        /// <summary>
        /// Proxy endpoint to fetch and serve landing page content
        /// </summary>
        /// <param name="url">The landing page URL to proxy</param>
        /// <returns>The landing page content served from our domain</returns>
        [HttpGet]
        [ValidateInput(false)] // Allow HTML content in URL parameter
        public async Task<ActionResult> ProxyLandingPage(string url)
        {
            // Input validation
            if (string.IsNullOrWhiteSpace(url))
            {
                return new HttpStatusCodeResult(400, "URL parameter is required");
            }

            // URL decode the parameter
            url = HttpUtility.UrlDecode(url);

            try
            {
                // Validate that the URL is from an allowed landing page domain
                if (!IsAllowedLandingPageUrl(url))
                {
                    LogHelper.WriteError(new InvalidOperationException($"Proxy request denied for URL: {url}"));
                    return new HttpStatusCodeResult(403, "URL not allowed for proxying");
                }

                // Create cancellation token with timeout
                using (var cts = new CancellationTokenSource(TimeSpan.FromSeconds(25)))
                {
                    // Fetch the content from the landing page
                    using (var response = await _httpClient.GetAsync(new Uri(url), cts.Token))
                    {
                        if (!response.IsSuccessStatusCode)
                        {
                            LogHelper.WriteError(new HttpRequestException($"Proxy request failed for URL: {url}, Status: {response.StatusCode}, Reason: {response.ReasonPhrase}"));
                            return new HttpStatusCodeResult((int)response.StatusCode, $"Failed to fetch landing page: {response.ReasonPhrase}");
                        }

                        // Check content length
                        if (response.Content.Headers.ContentLength > MaxContentSizeBytes)
                        {
                            return new HttpStatusCodeResult(413, "Content too large");
                        }

                        var content = await response.Content.ReadAsStringAsync();
                        var contentType = response.Content.Headers.ContentType?.ToString() ?? "text/html";

                        // Only process HTML content
                        if (contentType.Contains("text/html"))
                        {
                            // Modify the content to fix relative URLs and make them absolute
                            content = FixRelativeUrls(content, url);
                            
                            // Remove potentially problematic scripts and elements
                            content = SanitizeContent(content);
                        }

                        // Set appropriate security headers
                        SetSecurityHeaders();

                        return Content(content, contentType, Encoding.UTF8);
                    }
                }
            }
            catch (OperationCanceledException ex)
            {
                LogHelper.WriteError(ex, $"Proxy request timeout for URL: {url}");
                return new HttpStatusCodeResult(408, "Request timeout");
            }
            catch (HttpRequestException ex)
            {
                LogHelper.WriteError(ex, $"HTTP error proxying URL: {url}");
                return new HttpStatusCodeResult(502, "Bad gateway - unable to fetch content");
            }
            catch (Exception ex)
            {
                LogHelper.WriteError(ex, $"Error proxying URL: {url}");
                return new HttpStatusCodeResult(500, "Internal server error");
            }
        }

        /// <summary>
        /// Gets allowed domains from configuration (cached)
        /// </summary>
        /// <returns>Array of allowed domain hosts</returns>
        private static string[] GetAllowedDomains()
        {
            var domains = new System.Collections.Generic.List<string>();

            try
            {
                var landingPageUrl = ConfigurationManager.AppSettings["LandingPageUrl"];
                if (!string.IsNullOrEmpty(landingPageUrl))
                {
                    var landingUri = new Uri(landingPageUrl);
                    domains.Add(landingUri.Host.ToLowerInvariant());
                }

                var additionalFrameSources = ConfigurationManager.AppSettings["CSP.AdditionalFrameSources"];
                
                if (string.IsNullOrEmpty(additionalFrameSources))
                {
                    return domains.ToArray();
                }

                var additionalSources = additionalFrameSources.Split(new[] { ' ', ',' }, StringSplitOptions.RemoveEmptyEntries);
                foreach (var source in additionalSources)
                {
                    if (!string.IsNullOrEmpty(source) && (source.StartsWith("http://") || source.StartsWith("https://")))
                    {
                        var sourceUri = new Uri(source);
                        domains.Add(sourceUri.Host.ToLowerInvariant());
                    }
                }
            }
            catch (Exception ex)
            {
                LogHelper.WriteError(ex, "Error loading allowed domains from configuration");
            }

            return domains.ToArray();
        }

        /// <summary>
        /// Validates that the URL is from an allowed landing page domain
        /// </summary>
        /// <param name="url">URL to validate</param>
        /// <returns>True if URL is allowed, false otherwise</returns>
        private static bool IsAllowedLandingPageUrl(string url)
        {
            if (string.IsNullOrWhiteSpace(url))
            {
                return false;
            }

            try
            {
                // Basic URL format validation
                if (!Uri.TryCreate(url, UriKind.Absolute, out var uri))
                {
                    return false;
                }

                // Only allow HTTP/HTTPS
                if (uri.Scheme != Uri.UriSchemeHttp && uri.Scheme != Uri.UriSchemeHttps)
                {
                    return false;
                }

                // Check against allowed domains
                var allowedDomains = _allowedDomains.Value;
                var requestHost = uri.Host.ToLowerInvariant();

                return Array.Exists(allowedDomains, domain => 
                    string.Equals(domain, requestHost, StringComparison.OrdinalIgnoreCase));
            }
            catch (Exception ex)
            {
                LogHelper.WriteError(ex, $"Error validating URL: {url}");
                return false;
            }
        }

        /// <summary>
        /// Sets security headers for the proxied content
        /// </summary>
        private void SetSecurityHeaders()
        {
            // Remove potentially problematic headers
            Response.Headers.Remove("X-Frame-Options");
            Response.Headers.Remove("Content-Security-Policy");
            
            // Set cache control headers
            Response.Headers.Remove("Cache-Control");
            Response.Headers.Remove("Pragma");
            Response.Headers.Remove("Expires");
            
            Response.Headers.Add("Cache-Control", "no-cache, no-store, must-revalidate, private");
            Response.Headers.Add("Pragma", "no-cache");
            Response.Headers.Add("Expires", "0");
            
            // Add security headers
            Response.Headers.Add("X-Content-Type-Options", "nosniff");
            Response.Headers.Add("X-XSS-Protection", "1; mode=block");
            Response.Headers.Add("Referrer-Policy", "strict-origin-when-cross-origin");
        }

        /// <summary>
        /// Sanitizes HTML content to remove potentially problematic elements
        /// </summary>
        /// <param name="content">HTML content</param>
        /// <returns>Sanitized content</returns>
        private static string SanitizeContent(string content)
        {
            try
            {
                // Remove script tags that might interfere with iframe functionality
                content = Regex.Replace(content, @"<script[^>]*>.*?</script>", "", RegexOptions.IgnoreCase | RegexOptions.Singleline);
                
                // Remove meta refresh redirects
                content = Regex.Replace(content, @"<meta[^>]*http-equiv[^>]*refresh[^>]*>", "", RegexOptions.IgnoreCase);
                
                // Remove frame-busting JavaScript patterns
                content = Regex.Replace(content, @"if\s*\(\s*top\s*[!=]=\s*self\s*\)", "if(false)", RegexOptions.IgnoreCase);
                content = Regex.Replace(content, @"if\s*\(\s*window\s*[!=]=\s*top\s*\)", "if(false)", RegexOptions.IgnoreCase);
                content = Regex.Replace(content, @"if\s*\(\s*parent\s*[!=]=\s*window\s*\)", "if(false)", RegexOptions.IgnoreCase);
                content = Regex.Replace(content, @"top\.location\s*=", "//top.location=", RegexOptions.IgnoreCase);
                content = Regex.Replace(content, @"window\.top\.location", "//window.top.location", RegexOptions.IgnoreCase);
                content = Regex.Replace(content, @"parent\.location", "//parent.location", RegexOptions.IgnoreCase);

                return content;
            }
            catch (Exception ex)
            {
                LogHelper.WriteError(ex, "Error sanitizing content");
                return content; // Return original content if sanitization fails
            }
        }

        /// <summary>
        /// Fixes relative URLs in the content to make them absolute
        /// </summary>
        /// <param name="content">HTML content</param>
        /// <param name="baseUrl">Base URL for resolving relative URLs</param>
        /// <returns>Content with fixed URLs</returns>
        private static string FixRelativeUrls(string content, string baseUrl)
        {
            try
            {
                var uri = new Uri(baseUrl);
                var baseUri = $"{uri.Scheme}://{uri.Host}";
                var basePath = uri.GetLeftPart(UriPartial.Path);
                if (basePath.EndsWith("/"))
                {
                    basePath = basePath.TrimEnd('/');
                }

                // Use regex for more precise URL replacement
                // Fix src attributes
                content = Regex.Replace(content, @"src=""(/[^""]*)", $@"src=""{baseUri}$1", RegexOptions.IgnoreCase);
                content = Regex.Replace(content, @"src='(/[^']*)", $@"src='{baseUri}$1", RegexOptions.IgnoreCase);
                
                // Fix href attributes
                content = Regex.Replace(content, @"href=""(/[^""]*)", $@"href=""{baseUri}$1", RegexOptions.IgnoreCase);
                content = Regex.Replace(content, @"href='(/[^']*)", $@"href='{baseUri}$1", RegexOptions.IgnoreCase);
                
                // Fix action attributes
                content = Regex.Replace(content, @"action=""(/[^""]*)", $@"action=""{baseUri}$1", RegexOptions.IgnoreCase);
                content = Regex.Replace(content, @"action='(/[^']*)", $@"action='{baseUri}$1", RegexOptions.IgnoreCase);
                
                // Fix CSS url() references
                content = Regex.Replace(content, @"url\((['""]?)(/[^)]*)\1\)", $@"url($1{baseUri}$2$1)", RegexOptions.IgnoreCase);
                
                // Fix relative paths without leading slash (more complex)
                content = Regex.Replace(content, @"src=""(?!https?://|//|#|data:)([^""]*)", $@"src=""{basePath}/$1", RegexOptions.IgnoreCase);
                content = Regex.Replace(content, @"href=""(?!https?://|//|#|mailto:|tel:)([^""]*)", $@"href=""{basePath}/$1", RegexOptions.IgnoreCase);

                return content;
            }
            catch (Exception ex)
            {
                LogHelper.WriteError(ex, "Error fixing relative URLs");
                return content; // Return original content if URL fixing fails
            }
        }

    }
}
