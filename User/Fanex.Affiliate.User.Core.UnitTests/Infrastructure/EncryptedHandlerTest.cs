using Fanex.Affiliate.User.Core.Infrastructure;
using Fanex.Affiliate.User.Core.Utilities;
using NSubstitute;
using System.Web.Routing;

namespace Fanex.Affiliate.User.Core.UnitTests.Infrastructure
{
    public class EncryptedHandlerTest
    {
        private readonly RequestContext RequestContext;

        public EncryptedHandlerTest()
        {
            RequestContext = Substitute.For<RequestContext>();
        }



        private EncryptedHandler CreateHandler()
        {
            var hanlder = new EncryptedHandler(RequestContext);
            hanlder.AppSetting = Substitute.For<IAppSetting>();
            return hanlder;
        }
    }
}
