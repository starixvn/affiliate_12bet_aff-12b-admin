using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Affiliate.ThirdPartyProduct.TestRequest.Startup))]
namespace Affiliate.ThirdPartyProduct.TestRequest
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
