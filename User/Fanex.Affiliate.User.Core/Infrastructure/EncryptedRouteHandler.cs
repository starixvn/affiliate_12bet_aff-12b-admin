using System.Web;
using System.Web.Routing;

namespace Fanex.Affiliate.User.Core.Infrastructure
{
    public class EncryptedRouteHandler : IRouteHandler
    {
        public IHttpHandler GetHttpHandler(RequestContext requestContext)
            => new EncryptedHandler(requestContext);
    }
}
