using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Affiliate.ThirdPartyProduct.Startup))]
namespace Affiliate.ThirdPartyProduct
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
