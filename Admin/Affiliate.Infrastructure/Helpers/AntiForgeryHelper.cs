namespace Affiliate.Infrastructure.Helpers
{
    using System;
    using System.Security.Cryptography;
    using System.Text;
    using System.Web;
    using System.Web.Security;

    public static class AntiForgeryHelper
    {
        public const string AntiXsrfTokenKey = "__AntiCSRFToken";

        public static string GenerateXsrfFormToken(HttpContext context)
        {
            var requestCookie = context.Request.Cookies[AntiXsrfTokenKey];
            string resultFormToken;
            Guid requestCookieGuidValue;
            if (requestCookie != null && Guid.TryParse(requestCookie.Value, out requestCookieGuidValue))
            {
                resultFormToken = CreateFormToken(requestCookie.Value, context.User.Identity.Name);
            }
            else
            {
                var cookieToken = Guid.NewGuid().ToString("N");
                resultFormToken = CreateFormToken(cookieToken, context.User.Identity.Name);

                var responseCookie = new HttpCookie(AntiXsrfTokenKey)
                {
                    HttpOnly = true,
                    Value = cookieToken
                };

                if (FormsAuthentication.RequireSSL && context.Request.IsSecureConnection)
                {
                    responseCookie.Secure = true;
                }

                context.Response.Cookies.Set(responseCookie);
            }

            return resultFormToken;
        }

        public static void ValidateXsrfToken(HttpContext context)
        {
            if (context.Request.RequestType != "GET")
            {
                var requestCookie = context.Request.Cookies[AntiXsrfTokenKey];

                var cookieTokenValue = requestCookie != null ? requestCookie.Value : string.Empty;

                var expectedFormToken = CreateFormToken(cookieTokenValue, context.User.Identity.Name);

                if (expectedFormToken != context.Request["X-CSRF-Token"])
                {
                    throw new InvalidOperationException("Validation of Anti-XSRF token failed.");
                }
            }
        }

        private static string CreateFormToken(string guid, string userName)
        {
            using (SHA512 sha512 = SHA512.Create())
            {
                byte[] hash = sha512.ComputeHash(Encoding.Default.GetBytes(guid + userName));
                return BitConverter.ToString(hash).Replace("-", string.Empty);
            }
        }
    }
}