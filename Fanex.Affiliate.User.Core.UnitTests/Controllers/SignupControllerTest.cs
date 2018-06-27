using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Fanex.Affiliate.User.Core.BusinessService;
using Fanex.Affiliate.User.Core.Controllers;
using Fanex.Affiliate.User.Core.CoreService;
using Fanex.Affiliate.User.Core.DTO;
using Fanex.Affiliate.User.Core.MetalModels;
using Fanex.Affiliate.User.Core.MetalModels.Email;
using Fanex.Affiliate.User.Core.Resources;
using Fanex.Affiliate.User.Core.UnitTests.TestHelper;
using Fanex.Affiliate.User.Core.Utilities;
using Moq;
using Newtonsoft.Json;
using Xunit;

namespace Fanex.Affiliate.User.Core.UnitTests.Controllers
{
    public class SignupControllerTest
    {
        [Fact]
        public void Index_WhenCallWithGet_ReturnView()
        {
            // Arrange
            var accountService = new Mock<IAccountService>();
            var controller = CreateController(accountService.Object);

            // Action
            var result = controller.Index() as ViewResult;

            // Assert
            Assert.Empty(result.ViewName);
        }

        [Fact]
        public void Index_WhenCallWithPost_ModelStateIsInvalid_DoNotCallService()
        {
            // Arrange
            var accountService = new Mock<IAccountService>();
            var controller = CreateController(accountService.Object);
            controller.ModelState.AddModelError("Error", "Error");

            // Action
            controller.Index(new UserSignUpParam());

            // Assert
            accountService.Verify(p => p.CreateAffiliate(It.IsAny<UserSignUpParam>(), It.IsAny<EmailBaseParam>()), Times.Never);
        }

        [Fact]
        public void Index_WhenCallWithPost_CaptchaIsInvalid_SetResultCorrectly()
        {
            // Arrange
            var accountService = new Mock<IAccountService>();
            var controller = CreateController(accountService.Object);
            var signUpParam = new UserSignUpParam { Captcha = "Invalid" };

            var expectedResult = new ResultInfo { IsOk = false, Message = Lang.ValidationCode };

            // Action
            var actionResult = controller.Index(signUpParam) as JsonResult;
            var result = actionResult.Data as ResultInfo;

            // Assert
            Assert.Equal(expectedResult.IsOk, result.IsOk);
            Assert.Equal(expectedResult.Message, result.Message);
        }

        [Fact]
        public void Index_WhenCallWithPost_ModelStateIsValid_CallServiceWithCorrectParam()
        {
            // Arrange
            UserSignUpParam passedParam = null;
            var accountService = new Mock<IAccountService>();
            accountService.Setup(p => p.CreateAffiliate(It.IsAny<UserSignUpParam>(), It.IsAny<EmailBaseParam>()))
                        .Callback<UserSignUpParam, EmailBaseParam>((p, e) => passedParam = p);

            var appSetting = Mock.Of<IAppSetting>(ap =>
            ap.AllowedAffiliateTypes == new List<string> { Constant.AffTypeAsian.ToString() } &&
            ap.IsLocal);

            var controller = CreateController(accountService.Object, appSetting: appSetting);

            var signUpParam = CreateParam();
            signUpParam.Captcha = Constant.Captcha;

            var expectedPassedParam = JsonConvert.SerializeObject(CreateExpectedParam());

            // Action
            controller.Index(signUpParam);
            var result = JsonConvert.SerializeObject(passedParam);

            // Assert
            Assert.Equal(expectedPassedParam, result);
        }

        [Fact]
        public void Index_WhenCallWithPost_ModelStateIsValid_ReturnResultCorrectly()
        {
            // Arrange
            var expectedResult = new ResultInfo { IsOk = true };
            var accountService = new Mock<IAccountService>();
            accountService.Setup(p => p.CreateAffiliate(It.IsAny<UserSignUpParam>(), It.IsAny<EmailBaseParam>())).Returns(expectedResult);

            var appSetting = Mock.Of<IAppSetting>(ap => ap.AllowedAffiliateTypes == new List<string> { Constant.AffTypeAsian.ToString() });

            var controller = CreateController(accountService.Object, appSetting: appSetting);
            controller.TempData = new TempDataDictionary();

            var signUpParam = CreateParam();
            signUpParam.Captcha = Constant.Captcha;

            // Action
            var actionResult = controller.Index(signUpParam) as JsonResult;
            var result = actionResult.Data as ResultInfo;

            // Assert
            Assert.Equal(expectedResult, result);
        }

        [Fact]
        public void Index_WhenCallWithPost_ModelStateIsValid_CallServiceWithCorrectEmailParam()
        {
            // Arrange
            EmailBaseParam passedEmailParam = null;
            var resultInfo = new ResultInfo { IsOk = true, Message = string.Empty };
            var accountService = new Mock<IAccountService>();
            accountService.Setup(p => p.CreateAffiliate(It.IsAny<UserSignUpParam>(), It.IsAny<EmailBaseParam>()))
                                .Returns(resultInfo)
                                .Callback<UserSignUpParam, EmailBaseParam>((u, e) => passedEmailParam = e);

            var appSetting = Mock.Of<IAppSetting>(ap =>
            ap.AllowedAffiliateTypes == new List<string> { Constant.AffTypeAsian.ToString() } &&
            ap.MailContact == Constant.DefaultMailContact &&
            ap.MailCc == Constant.DefaultMailCc &&
            ap.MailBcc == string.Empty);

            var controller = CreateController(accountService.Object, appSetting: appSetting);
            controller.TempData = new TempDataDictionary();

            var signUpParam = CreateParam();
            signUpParam.Captcha = Constant.Captcha;

            var expectedResult = JsonConvert.SerializeObject(new EmailBaseParam
            {
                MailContact = Constant.DefaultMailContact,
                MailCc = Constant.DefaultMailCc,
                MailBcc = string.Empty,
                MailTo = Constant.DefaultMailContact,
                RootDomain = Constant.DefaultRootDomain + "/"
            });

            // Action
            controller.Index(signUpParam);
            var result = JsonConvert.SerializeObject(passedEmailParam);

            // Assert
            Assert.Equal(expectedResult, result);
        }

        private SignupController CreateController(IAccountService service, ISessionService sessionService = null, IAppSetting appSetting = null)
        {
            sessionService = sessionService ?? MockingHelper.GetSessionService();
            appSetting = appSetting ?? Mock.Of<IAppSetting>();

            var controller = new SignupController(sessionService, appSetting, service);
            var request = MockingHelper.GetHttpRequest();
            var server = new Mock<HttpServerUtilityBase>();
            server.Setup(s => s.HtmlEncode(It.IsAny<string>())).Returns<string>(s => s);

            controller.ControllerContext = Mock.Of<ControllerContext>(ctx =>
                ctx.HttpContext.Request == request &&
                ctx.HttpContext.Server == server.Object
            );

            var context = Mock.Of<HttpContextBase>(ctx => ctx.Request == request);
            var requestContext = new RequestContext(context, new RouteData());
            controller.Url = new UrlHelper(requestContext);

            return controller;
        }

        private UserSignUpParam CreateParam()
        {
            return new UserSignUpParam
            {
                FirstName = "FirstName",
                LastName = "LastName",
                Email = "Email",
                Phone = "Phone",
                MobilePhone = "MobilePhone",
                Address = "Address",
                City = "City",
                Postal = "Postal",
                Country = "Country",
                SiteUrl = "SiteUrl",
                MoreSiteUrls = "MoreSiteUrls",
                NoSiteDesc = "NoSiteDesc",
                PreferredLanguage = "PreferredLanguage",
                Question = "Question",
                Answer = "Answer",
                Player_UserName = "Player_UserName",
                Gender = true,
                State = "State"
            };
        }

        private UserSignUpParam CreateExpectedParam()
        {
            var passedParam = CreateParam();

            passedParam.Language = Constant.DefaultLanguage;
            passedParam.AffType = Constant.AffTypeAsian;
            passedParam.Ip = "::1";
            passedParam.IsLocal = true;

            return passedParam;
        }
    }
}