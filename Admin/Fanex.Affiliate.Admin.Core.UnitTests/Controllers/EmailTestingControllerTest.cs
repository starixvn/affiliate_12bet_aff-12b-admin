using Autofac;
using Autofac.Integration.Mvc;
using Fanex.Affiliate.Admin.Core.Configurations;
using Fanex.Affiliate.Admin.Core.Constants;
using Fanex.Affiliate.Admin.Core.Controllers;
using Fanex.Affiliate.Admin.Core.Services.Common;
using Fanex.Affiliate.Admin.Core.UnitTests.TestHelper;
using Fanex.Affiliate.Admin.Core.Utilities;
using Moq;
using System.Web;
using System.Web.Mvc;
using Xunit;

namespace Fanex.Affiliate.Admin.Core.UnitTests.Controllers
{
    public class EmailTestingControllerTest
    {
        private ILifetimeScopeProvider _scopeProvider;

        [Fact]
        public void Index_WhenCall_ReturnView()
        {
            // Arrange
            var controller = CreateEmailTestingController();

            // Action
            var viewResult = controller.Index() as ViewResult;
            var viewName = viewResult.ViewName;

            // Assert
            Assert.Equal("", viewName);
        }

        [Fact]
        public void Preview_WhenCallHasException_ReturnView()
        {
            // Arrange
            var controller = new EmailTestingController(MockingHelper.GetSessionService(), Mock.Of<IMailProcessor>(), Mock.Of<IAppSetting>(), Mock.Of<ISystemService>());

            // Action
            var viewResult = controller.Preview(string.Empty, string.Empty, string.Empty) as ViewResult;
            var viewName = viewResult.ViewName;

            // Assert
            Assert.Equal("", viewName);
        }

        [Theory]
        [InlineData(TemplateString.AffiliateDisapproved)]
        [InlineData(TemplateString.AffiliateApproved)]
        [InlineData(TemplateString.WithdrawalGuidance)]
        [InlineData(TemplateString.AffiliateInformation)]
        [InlineData(TemplateString.PaymentRequestApproved)]
        [InlineData(TemplateString.PaymentRequestRejected)]
        [InlineData("Default")]
        public void Preview_WhenCall_ReturnView(string template)
        {
            // Arrange
            var appSetting = new Mock<IAppSetting>();
            appSetting.Setup(a => a.Environment).Returns("UAT");
            appSetting.Setup(a => a.IsProduction).Returns(true);

            var builder = new ContainerBuilder();
            builder.RegisterInstance(appSetting.Object).As<IAppSetting>();
            var container = builder.Build();

            _scopeProvider = new SimpleLifetimeScopeProvider(container);
            var resolver = new AutofacDependencyResolver(container, _scopeProvider);
            DependencyResolver.SetResolver(resolver);

            var controller = CreateEmailTestingController();

            // Action
            var viewResult = controller.Preview(string.Empty, template, string.Empty) as ViewResult;
            var viewName = viewResult.ViewName;

            // Assert
            Assert.Equal("", viewName);
        }

        [Fact]
        public void SendEmail_WhenCallHasException_ReturnFalse()
        {
            // Arrange
            var controller = new EmailTestingController(MockingHelper.GetSessionService(), Mock.Of<IMailProcessor>(), Mock.Of<IAppSetting>(), Mock.Of<ISystemService>());

            // Action
            var jsonResult = controller.SendEmail(string.Empty, string.Empty, string.Empty, string.Empty) as JsonResult;
            var data = jsonResult.Data as dynamic;

            // Assert
            Assert.False(data.Success);
        }

        [Fact]
        public void SendEmail_WhenCall_ReturnTrue()
        {
            // Arrange
            var appSetting = new Mock<IAppSetting>();
            appSetting.Setup(a => a.Environment).Returns("UAT");
            appSetting.Setup(a => a.IsProduction).Returns(true);
            appSetting.Setup(a => a.EmailBackgroundUrl).Returns("https://google.com");

            var builder = new ContainerBuilder();
            builder.RegisterInstance(appSetting.Object).As<IAppSetting>();
            var container = builder.Build();

            _scopeProvider = new SimpleLifetimeScopeProvider(container);
            var resolver = new AutofacDependencyResolver(container, _scopeProvider);
            DependencyResolver.SetResolver(resolver);

            var controller = CreateEmailTestingController();

            // Action
            var jsonResult = controller.SendEmail(string.Empty, TemplateString.AffiliateApproved, "txt", string.Empty) as JsonResult;
            var data = jsonResult.Data as dynamic;

            // Assert
            Assert.True(data.Success);
        }

        private EmailTestingController CreateEmailTestingController(
            ISessionService sessionService = null,
            IMailProcessor mailProcessor = null,
            IAppSetting appSetting = null,
            ISystemService systemServive = null
            )
        {
            var fakeSessionService = sessionService ?? MockingHelper.GetSessionService();
            var fakeMailProcessor = mailProcessor ?? Mock.Of<IMailProcessor>();
            var fakeAppSetting = appSetting ?? Mock.Of<IAppSetting>();
            var fakesystemServive = systemServive ?? Mock.Of<ISystemService>();

            var controller = new EmailTestingController(fakeSessionService, fakeMailProcessor, fakeAppSetting, fakesystemServive);

            var request = MockingHelper.GetHttpRequest();
            var server = new Mock<HttpServerUtilityBase>();
            server.Setup(s => s.HtmlEncode(It.IsAny<string>())).Returns<string>(s => $"Encoded{s}");

            controller.ControllerContext = Mock.Of<ControllerContext>(ctx =>
                ctx.HttpContext.Request == request &&
                ctx.HttpContext.Server == server.Object &&
                ctx.HttpContext.Session.SessionID == "SessionId"
            );

            var expectedPath = "~/";
            var expectedContent = "/";
            var urlHelperMock = new Mock<UrlHelper>();
            urlHelperMock
                .Setup(m => m.Content(expectedPath))
                .Returns(expectedContent);

            controller.Url = urlHelperMock.Object;

            return controller;
        }
    }
}
