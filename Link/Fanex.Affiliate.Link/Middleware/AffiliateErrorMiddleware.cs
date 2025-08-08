using Fanex.Affiliate.Link.Constants;
using System.Text;

namespace Fanex.Affiliate.Link.Middleware
{
    /// <summary>
    /// Middleware that handles 404 responses for affiliate link patterns and provides detailed error messages.
    /// Analyzes the URL structure to determine why the route failed and gives specific feedback.
    /// </summary>
    public class AffiliateErrorMiddleware(RequestDelegate next, ILogger<AffiliateErrorMiddleware> logger)
    {
        private readonly RequestDelegate _next = next ?? throw new ArgumentNullException(nameof(next));
        private readonly ILogger<AffiliateErrorMiddleware> _logger = logger ?? throw new ArgumentNullException(nameof(logger));

        private const string Incorrect_Link_Format = "This link is in an incorrect format. Please contact administrator!";

        public async Task InvokeAsync(HttpContext context)
        {
            await _next(context);

            if (ShouldHandleError(context))
            {
                var customError = AnalyzeAffiliateUrlError(context.Request.Path.Value);
                if (!string.IsNullOrEmpty(customError))
                {
                    await HandleAffiliateError(context, customError);
                }
            }
        }

        private static bool ShouldHandleError(HttpContext context)
        {
            return context.Response.StatusCode == 404
                && !context.Response.HasStarted
                && !string.IsNullOrEmpty(context.Request.Path.Value)
                && context.Request.Path.Value.EndsWith("/index.html", StringComparison.OrdinalIgnoreCase);
        }

        private async Task HandleAffiliateError(HttpContext context, string customError)
        {
            var path = context.Request.Path.Value;
            _logger.LogWarning("Affiliate URL validation failed: {Path} - {Error}", path, customError);

            // Store the error information in HttpContext.Items 
            context.Items["CustomErrorMessage"] = customError;
            context.Items["OriginalPath"] = path;

            // Generate and return the error page HTML directly
            // This mimics what the Link view would show for error cases
            await RenderAffiliateErrorPage(context, customError);
        }

        private static async Task RenderAffiliateErrorPage(HttpContext context, string customError)
        {
            var html = $@"
                <!DOCTYPE html>
                <html>
                <head>
                    <meta charset=""utf-8"" />
                    <meta name=""viewport"" content=""width=device-width, initial-scale=1.0"" />
                    <title>12Bet Affiliate Link</title>
                </head>
                <body>
                    <div>
                        <h2>12Bet Affiliate Link</h2>
                        <strong>This link is in an incorrect format. Please contact administrator!</strong>
                        <p>{System.Net.WebUtility.HtmlEncode(customError)}</p>
                    </div>
                </body>
                </html>";

            context.Response.Clear();
            context.Response.StatusCode = 200;
            context.Response.ContentType = "text/html; charset=utf-8";

            await context.Response.WriteAsync(html, Encoding.UTF8);
        }

        private string AnalyzeAffiliateUrlError(string path)
        {
            try
            {
                var segments = ExtractPathSegments(path);

                if (segments.Length < 3)
                    return "Invalid affiliate link format. Minimum required: pageCode/affCode/language/index.html";

                return segments.Length switch
                {
                    5 => ValidateSpecialPageCodeChannelPattern(segments),
                    4 => ValidateFourSegmentPattern(segments),
                    3 => ValidateThreeSegmentPattern(segments),
                    _ => Incorrect_Link_Format
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error analyzing affiliate URL: {Path}", path);
                return Incorrect_Link_Format;
            }
        }

        private static string[] ExtractPathSegments(string path)
        {
            return path.TrimStart('/').TrimEnd('/')
                .Replace("/index.html", "", StringComparison.OrdinalIgnoreCase)
                .Split('/', StringSplitOptions.RemoveEmptyEntries);
        }

        private static string ValidateSpecialPageCodeChannelPattern(string[] segments)
        {
            var (pageCodePart1, pageCodePart2, affCode, lang, _) =
                (segments[0], segments[1], segments[2], segments[3], segments[4]);

            return ValidatePageCodePart(pageCodePart1, "part 1")
                ?? ValidatePageCodePart(pageCodePart2, "part 2")
                ?? ValidateNumeric(affCode, "affiliate code")
                ?? ValidateLanguage(lang)
                ?? $"Affiliate link format is correct, but the combination '{pageCodePart1}/{pageCodePart2}' with affiliate '{affCode}' in '{lang}' language may not be active.";
        }

        private static string ValidateFourSegmentPattern(string[] segments)
        {
            return RegexPatterns.Numeric().IsMatch(segments[0])
                ? ValidateSpecialPageCodeOldLinkPattern(segments)
                : ValidateChannelLinkPattern(segments);
        }

        private static string ValidateChannelLinkPattern(string[] segments)
        {
            var (pageCode, affCode, lang, _) = (segments[0], segments[1], segments[2], segments[3]);

            return ValidatePageCode(pageCode)
                ?? ValidateNumeric(affCode, "affiliate code")
                ?? ValidateLanguage(lang)
                ?? $"Affiliate link format is correct, but page '{pageCode}' with affiliate '{affCode}' may not be available in '{lang}' language.";
        }

        private static string ValidateSpecialPageCodeOldLinkPattern(string[] segments)
        {
            var (affId, pageCodePart1, pageCodePart2, lang) = (segments[0], segments[1], segments[2], segments[3]);

            return ValidateNumeric(affId, "affiliate ID")
                ?? ValidatePageCodePart(pageCodePart1, "part 1")
                ?? ValidatePageCodePart(pageCodePart2, "part 2")
                ?? ValidateLanguage(lang)
                ?? $"Legacy affiliate link format is correct, but affiliate ID '{affId}' with page '{pageCodePart1}/{pageCodePart2}' may not be active.";
        }

        private static string ValidateThreeSegmentPattern(string[] segments)
        {
            var (first, second, third) = (segments[0], segments[1], segments[2]);

            if (RegexPatterns.Numeric().IsMatch(first))
            {
                // Legacy pattern: affId/pageCode/lang
                return ValidatePageCode(second)
                    ?? ValidateLanguage(third)
                    ?? $"Legacy affiliate link format is correct, but affiliate ID '{first}' with page '{second}' may not be active.";
            }
            else
            {
                // Modern pattern: pageCode/affCode/lang
                return ValidatePageCode(first)
                    ?? ValidateNumeric(second, "affiliate code")
                    ?? ValidateLanguage(third)
                    ?? $"Affiliate link format is correct, but page '{first}' with affiliate '{second}' may not be available in '{third}' language.";
            }
        }

        // Validation helper methods
        private static string ValidatePageCode(string pageCode)
        {
            return !RegexPatterns.PageCode().IsMatch(pageCode)
                ? $"Invalid page code '{pageCode}'."
                : null;
        }

        private static string ValidatePageCodePart(string pageCodePart, string partName)
        {
            return !RegexPatterns.PageCode().IsMatch(pageCodePart)
                ? $"Invalid page code {partName} '{pageCodePart}'."
                : null;
        }

        private static string ValidateNumeric(string value, string fieldName)
        {
            return !RegexPatterns.Numeric().IsMatch(value)
                ? $"Invalid {fieldName} '{value}'."
                : null;
        }

        private static string ValidateLanguage(string lang)
        {
            return !RegexPatterns.Language.IsMatch(lang)
                ? $"Invalid language '{lang}'."
                : null;
        }
    }
}