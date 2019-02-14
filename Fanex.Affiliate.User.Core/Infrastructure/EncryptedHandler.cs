using System;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.SessionState;
using Fanex.Affiliate.User.Core.BusinessService;
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

        public ICryptographyService CryptographyService { get; set; }

        public IAppSetting AppSetting { get; set; }

        public void ProcessRequest(HttpContext context)
        {
            try
            {
                var cryptographyService = CryptographyService ?? DependencyResolver.Current.GetService<ICryptographyService>();
                var appSetting = AppSetting ?? DependencyResolver.Current.GetService<IAppSetting>();

                var encryptedUrl = _requestContext.RouteData.GetRequiredString("encryptedUrl");
                if (string.IsNullOrEmpty(encryptedUrl?.Trim()))
                {
                    _requestContext.HttpContext.Response.StatusCode = (int)HttpStatusCode.NotFound;
                    return;
                }

                var decodedString = ConverterUtils.Base64Decode(encryptedUrl);
                var queryString = cryptographyService.RsaDecrypt(appSetting.RsaPrivateKey, decodedString);
                var paramValues = HttpUtility.ParseQueryString(queryString);
                var controllerName = paramValues["controller"]?.Trim();
                var actionName = paramValues["action"]?.Trim();
                if (!string.IsNullOrEmpty(controllerName) && !string.IsNullOrEmpty(actionName))
                {
                    var controller = ControllerBuilder.Current.GetControllerFactory().CreateController(_requestContext, controllerName);
                    if (controller != null)
                    {
                        _requestContext.RouteData.Values["action"] = actionName;
                        controller.Execute(_requestContext);
                    }
                }
                else
                {
                    _requestContext.HttpContext.Response.StatusCode = (int)HttpStatusCode.NotFound;
                }
            }
            catch (Exception ex)
            {
                _requestContext.HttpContext.Response.StatusCode = (int)HttpStatusCode.NotFound;
            }
        }
    }
}