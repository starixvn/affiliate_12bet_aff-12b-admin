using Fanex.Affiliate.Admin.Core.Controllers.Domain;
using Fanex.Affiliate.Admin.Core.Services.Common;
using Fanex.Affiliate.Admin.Core.Services.Domain;
using Fanex.Affiliate.Admin.Core.UnitTests.TestHelper;
using Fanex.Affiliate.Admin.Core.ViewModels.Domain;
using Moq;
using System.Web;
using System.Web.Mvc;
using Xunit;

namespace Fanex.Affiliate.Admin.Core.UnitTests.Controllers.Domain
{
    public class MemberDomainControllerTest
    {
        [Fact]
        public void Index_WhenCall_RedirectToCommonIndex()
        {
            // Arrange
            var controller = CreateMemberDomainController();

            var urlHelperMock = new Mock<UrlHelper>();
            urlHelperMock.Setup(m => m.Content(It.IsAny<string>())).Returns("/Portal/MemberDomain");
            controller.Url = urlHelperMock.Object;

            // Action
            var viewResult = controller.Index() as ViewResult;
            var viewName = viewResult.ViewName;
            var model = viewResult.Model as DomainViewModel;

            // Assert
            Assert.Equal("~/Areas/Portal/Views/Domain/Index.cshtml", viewName);
            Assert.False(model.IsAffDomain);
        }

        private MemberDomainController CreateMemberDomainController(
            ISessionService sessionService = null,
            IDomainService domainService = null,
            ISystemService systemService = null)
        {
            var fakeSessionService = sessionService ?? MockingHelper.GetSessionService();
            var fakeDomainService = domainService ?? Mock.Of<IDomainService>();
            var fakeSystemService = systemService ?? Mock.Of<ISystemService>();

            var controller = new MemberDomainController(fakeSessionService, fakeDomainService, fakeSystemService);

            var request = MockingHelper.GetHttpRequest();
            var server = new Mock<HttpServerUtilityBase>();
            server.Setup(s => s.HtmlEncode(It.IsAny<string>())).Returns<string>(s => $"Encoded{s}");

            controller.ControllerContext = Mock.Of<ControllerContext>(ctx =>
                ctx.HttpContext.Request == request &&
                ctx.HttpContext.Server == server.Object &&
                ctx.HttpContext.Session.SessionID == "SessionId"
            );

            return controller;
        }
    }
}
