using System;
using System.Net;
using System.Security.Cryptography;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.SessionState;
using Fanex.Affiliate.User.Core.DTO.VerificationAccount;
using Fanex.Affiliate.User.Core.Utilities;

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

        public IAppSetting AppSetting { get; set; }

        public void ProcessRequest(HttpContext context)
        {
            try
            {
                var encryptedQuery = _requestContext.HttpContext.Request.QueryString["callInfo"]?.Trim();
                var parseQueryResult = TryParseQueryString(encryptedQuery);

                if (!parseQueryResult.IsValid && parseQueryResult.IsExpired)
                {
                    var response = _requestContext.HttpContext.Response;
                    var request = new HttpRequestWrapper(context.Request);
                    if (request.IsAjaxRequest())
                    {
                        var controller = ControllerBuilder.Current.GetControllerFactory()
                                    .CreateController(_requestContext, "Notify");
                        _requestContext.RouteData.Values["action"] = "CodeExpired";
                        controller.Execute(_requestContext);
                    }
                    else
                    {
                        response.Redirect(VirtualPathUtility.ToAbsolute("~/Notify/Error"));
                    }

                    return;
                }
                else if (!parseQueryResult.IsValid)
                {
                    SetNotFoundResponse();
                }
                else
                {
                    var controller = ControllerBuilder.Current.GetControllerFactory()
                                    .CreateController(_requestContext, parseQueryResult.ControllerName);
                    if (controller != null)
                    {
                        _requestContext.RouteData.Values["action"] = parseQueryResult.ActionName;
                        controller.Execute(_requestContext);
                    }
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
                return new QueryStringResult { IsValid = false, IsExpired = true };
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
            var appSetting = AppSetting ?? DependencyResolver.Current.GetService<IAppSetting>();

            var decodedString = ConverterUtils.Base64Decode(encryptedQuery);

            return SymmetricEncryptionUtility.Decrypt<AesManaged>(decodedString, appSetting.SymmetricPassword, appSetting.SymmetricSalt);
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