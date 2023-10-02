using Autofac;
using Autofac.Integration.Mvc;
using Fanex.Affiliate.User.Core.CoreService;
using Fanex.Affiliate.User.Core.UnitTests.TestHelper;
using Fanex.Affiliate.User.Core.Utilities;
using System.Web;
using System.Web.Mvc;
using Xunit;

namespace Fanex.Affiliate.User.Core.UnitTests.Utilities
{
    public class ClaimUtitlityTest
    {
        [Theory]
        [InlineData(false)]
        [InlineData(true)]
        public void HasClaim_WhenCall_ReturnCorrect(bool isNull)
        {
            MockingHelper.SetUpEnviroment("test", false);
            if (isNull)
            {
                HttpContext.Current.User = null;
            }

            var hasClaim = ClaimUtitlity.HasClaim("test");

            Assert.False(hasClaim);
        }

        [Theory]
        [InlineData(false)]
        [InlineData(true)]
        public void HasClaimOnlyInternalLogin_WhenCall_ReturnCorrect(bool isInternal)
        {
            MockingHelper.SetUpEnviroment("test", false);
            var sessionService = MockingHelper.GetSessionService();
            sessionService.IsInternal = isInternal;

            var builder = new ContainerBuilder();
            builder.RegisterInstance(sessionService).As<ISessionService>();
            var container = builder.Build();

            var scopeProvider = new SimpleLifetimeScopeProvider(container);
            var resolver = new AutofacDependencyResolver(container, scopeProvider);
            DependencyResolver.SetResolver(resolver);

            if (isInternal)
            {
                HttpContext.Current.User = null;
            }

            var hasClaim = ClaimUtitlity.HasClaimOnlyInternalLogin("test");

            Assert.Equal(isInternal, hasClaim);
            scopeProvider.EndLifetimeScope();
        }

        [Theory]
        [InlineData(false)]
        [InlineData(true)]
        public void HasWriteClaim_WhenCall_ReturnFalse(bool isNull)
        {
            MockingHelper.SetUpEnviroment("test", true);
            if (isNull)
            {
                HttpContext.Current.User = null;
            }

            var hasClaim = ClaimUtitlity.HasWriteClaim("test");

            Assert.False(hasClaim);
        }
    }
}
