using System;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.SessionState;
using Fanex.Affiliate.User.Core.BusinessService;
using Fanex.Affiliate.User.Core.DTO.VerificationAccount;
using Fanex.Affiliate.User.Core.Resources;
using Fanex.Affiliate.User.Core.Utilities;
using Newtonsoft.Json;

namespace Fanex.Affiliate.User.Core.Infrastructure
{
    public class EncryptedHandler : IHttpHandler, IRequiresSessionState
    {
        private readonly RequestContext _requestContext;

        public EncryptedHandler(RequestContext requestContext)
        {
            _requestContext = requestContext;
        }

        public bool IsReusable
        {
            get
            {
                return true;
            }
        }

        public ICryptographyService CryptographyService { get; set; }

        public IAppSetting AppSetting { get; set; }

        public void ProcessRequest(HttpContext context)
        {
            try
            {
                var encryptedQuery = _requestContext.RouteData.GetRequiredString("encryptedQuery")?.Trim();
                var parseQueryResult = TryParseQueryString(encryptedQuery);

                if (!parseQueryResult.IsValid)
                {
                    var response = _requestContext.HttpContext.Response;
                    response.StatusCode = (int)HttpStatusCode.BadRequest;
                    response.ContentType = "application/json";
                    response.Write(JsonConvert.SerializeObject(
                        new { IsOk = false, IsExpired = true, Message = Error.General }
                    ));

                    return;
                }

                var controller = ControllerBuilder.Current.GetControllerFactory()
                                .CreateController(_requestContext, parseQueryResult.ControllerName);
                if (controller != null)
                {
                    _requestContext.RouteData.Values["action"] = parseQueryResult.ActionName;
                    controller.Execute(_requestContext);
                }
            }
            catch (Exception)
            {
                SetNotFoundResponse();
            }
        }

        private void SetNotFoundResponse()
        {
            _requestContext.HttpContext.Response.StatusCode = (int)HttpStatusCode.NotFound;
        }

        private QueryStringResult TryParseQueryString(string encryptedQuery)
        {
            if (string.IsNullOrEmpty(encryptedQuery))
            {
                return new QueryStringResult { IsValid = false };
            }

            var queryString = DecryptQueryString(encryptedQuery);
            var paramValues = HttpUtility.ParseQueryString(queryString);

            var timeQuery = _requestContext.HttpContext.Request.Params["t"]?.Trim();
            var timeToken = paramValues["t"]?.Trim();
            var expirationByMinute = int.Parse(paramValues["e"]?.Trim());

            if (!string.Equals(timeQuery, timeToken) || IsExpired(timeToken, expirationByMinute))
            {
                return new QueryStringResult { IsValid = false };
            }

            var controllerName = paramValues["controller"]?.Trim();
            var actionName = paramValues["action"]?.Trim();

            return new QueryStringResult
            {
                IsValid = !string.IsNullOrEmpty(controllerName) && !string.IsNullOrEmpty(actionName),
                ControllerName = controllerName,
                ActionName = actionName
            };
        }

        private string DecryptQueryString(string encryptedQuery)
        {
            var cryptographyService = CryptographyService ?? DependencyResolver.Current.GetService<ICryptographyService>();
            var appSetting = AppSetting ?? DependencyResolver.Current.GetService<IAppSetting>();

            var decodedString = ConverterUtils.Base64Decode(encryptedQuery);

            return cryptographyService.RsaDecrypt(appSetting.RsaPrivateKey, decodedString);
        }

        private static bool IsExpired(string timeTickString, int expirationByMinute)
        {
            var minDateTime = DateTime.Now.AddMinutes(expirationByMinute * -1);
            var timeTicks = Convert.ToInt64(timeTickString);
            var dateTimeRequest = new DateTime(timeTicks, DateTimeKind.Local);

            return DateTime.Compare(dateTimeRequest, minDateTime) < 0;
        }
    }
}