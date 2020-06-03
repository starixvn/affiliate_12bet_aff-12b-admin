using Fanex.Affiliate.User.Core.BusinessService;
using Fanex.Affiliate.User.Core.Controllers;
using Fanex.Affiliate.User.Core.CoreService;
using Fanex.Affiliate.User.Core.DTO.Auth;
using Fanex.Affiliate.User.Core.UnitTests.TestHelper;
using Fanex.Affiliate.User.Core.Utilities;
using Fanex.Affiliate.User.Core.ViewModels;
using NSubstitute;
using NSubstitute.ReturnsExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Xunit;

namespace Fanex.Affiliate.User.Core.UnitTests.Controllers
{
    public class AuthControllerTest
    {
        private readonly ISessionService SessionService;
        private readonly IAppSetting AppSetting;
        private readonly ILanguageService LanguageService;
        private readonly IAuthService AuthService;
        private readonly ICaptchaService CapchaService;

        public AuthControllerTest()
        {
            SessionService = NSubtituteMockingHelper.FakeSessionService();
            AppSetting = Substitute.For<IAppSetting>();
            LanguageService = NSubtituteMockingHelper.FakeLanguageService();
            AuthService = Substitute.For<IAuthService>();
            CapchaService = Substitute.For<ICaptchaService>();
        }

        [Fact]
        public void AuthenticationForm_WhenCallWithNoAuthenticationUser_SendPublicKey()
        {
            var controller = CreateController();
            NSubtituteMockingHelper.FakeController(controller);
            controller.HttpContext.User = Substitute.For<IPrincipal>();
            var identity = Substitute.For<IIdentity>();
            identity.IsAuthenticated.Returns(false);
            controller.HttpContext.User.Identity.Returns(identity);
            var key = "abcdxyz";
            AppSetting.RsaPublicKey.Returns(key);

            var result = controller.AuthenticationForm() as ViewResult;
            var model = result.Model as AuthenticationFormViewModel;

            Assert.Equal("~/Views/Auth/AuthenticationForm.cshtml", result.ViewName);
            Assert.Equal(key, model.PublicKey);
        }

        [Fact]
        public void AuthenticationForm_WhenCallWithNoSession_SendPublicKey()
        {
            var controller = CreateController();
            NSubtituteMockingHelper.FakeController(controller);
            controller.HttpContext.User = Substitute.For<IPrincipal>();
            var identity = Substitute.For<IIdentity>();
            identity.IsAuthenticated.Returns(true);
            controller.HttpContext.User.Identity.Returns(identity);
            SessionService.UserSession.ReturnsNull();
            var key = "abcdxyz";
            AppSetting.RsaPublicKey.Returns(key);

            var result = controller.AuthenticationForm() as ViewResult;
            var model = result.Model as AuthenticationFormViewModel;

            Assert.Equal("~/Views/Auth/AuthenticationForm.cshtml", result.ViewName);
            Assert.Equal(key, model.PublicKey);
        }

        [Fact]
        public void AuthenticationForm_WhenCallWithLoginUer_ReturnCoreectViewWithLoginUserName()
        {
            var controller = CreateController();
            NSubtituteMockingHelper.FakeController(controller);
            controller.HttpContext.User = Substitute.For<IPrincipal>();
            var identity = Substitute.For<IIdentity>();
            identity.IsAuthenticated.Returns(true);
            controller.HttpContext.User.Identity.Returns(identity);

            var result = controller.AuthenticationForm() as ViewResult;
            var model = result.Model as AuthenticationFormViewModel;

            Assert.Equal("~/Views/Auth/AuthenticationForm.cshtml", result.ViewName);
            Assert.Equal(SessionService.UserSession.UserName, model.UserName);
        }

        public AuthController CreateController()
            => new AuthController(SessionService, AppSetting, LanguageService, AuthService, CapchaService);
    }
}
