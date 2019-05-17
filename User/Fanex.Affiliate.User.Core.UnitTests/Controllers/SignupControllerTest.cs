using System.Collections.Generic;
using System.Linq;
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
        public void Index_ModelStateIsInvalid_ReturnCorrectResult()
        {
            // Arrange
            const string ErrorMessage1 = "ErrorMessage1";
            const string ErrorMessage2 = "ErrorMessage2";
            var accountService = new Mock<IAccountService>();
            var controller = CreateController(accountService.Object);
            controller.ModelState.AddModelError("Error1", ErrorMessage1);
            controller.ModelState.AddModelError("Error2", ErrorMessage2);

            var contactAppList = CreateAppContactList();
            var expectedResult = new ResultInfo { IsOk = false, Message = $"{ErrorMessage1}<br>{ErrorMessage2}" };

            // Action
            var actionResult = controller.Index(new UserSignUpParam
            {
                Captcha = Constant.Captcha,
                ContactAppList = contactAppList
            }) as JsonResult;
            var result = actionResult.Data as ResultInfo;

            // Assert
            accountService.Verify(p => p.CreateAffiliate(It.IsAny<UserSignUpParam>(), It.IsAny<EmailBaseParam>()), Times.Never);
            Assert.Equal(expectedResult.IsOk, result.IsOk);
            Assert.Equal(expectedResult.Message, result.Message);
        }

        [Fact]
        public void Index_ContactAppListIsInvalid_ReturnCorrectResult()
        {
            // Arrange
            var accountService = new Mock<IAccountService>();
            var controller = CreateController(accountService.Object);
            var contactAppList = CreateAppContactList();
            contactAppList.ForEach(item => item.Value = null);

            // Action
            var actionResult = controller.Index(new UserSignUpParam { Captcha = Constant.Captcha, ContactAppList = contactAppList }) as JsonResult;
            var result = actionResult.Data as ResultInfo;

            // Assert
            accountService.Verify(p => p.CreateAffiliate(It.IsAny<UserSignUpParam>(), It.IsAny<EmailBaseParam>()), Times.Never);
            Assert.False(result.IsOk);
            Assert.Equal(Error.RequireIMAccount, result.Message);
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
            ap.AllowedAffiliateTypes == new List<string> { Constant.AffTypeAsian } &&
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

            var appSetting = Mock.Of<IAppSetting>(ap => ap.AllowedAffiliateTypes == new List<string> { Constant.AffTypeAsian });

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
        public void Index_ModelStateIsValid_CallServiceWithCorrectEmailParam()
        {
            // Arrange
            EmailBaseParam passedEmailParam = null;
            var resultInfo = new ResultInfo { IsOk = true, Message = string.Empty };
            var accountService = new Mock<IAccountService>();
            accountService.Setup(p => p.CreateAffiliate(It.IsAny<UserSignUpParam>(), It.IsAny<EmailBaseParam>()))
                                .Returns(resultInfo)
                                .Callback<UserSignUpParam, EmailBaseParam>((u, e) => passedEmailParam = e);

            var appSetting = Mock.Of<IAppSetting>(ap =>
            ap.AllowedAffiliateTypes == new List<string> { Constant.AffTypeAsian } &&
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

        private static SignupController CreateController(
            IAccountService accountService,
            ISessionService sessionService = null,
            IAppSetting appSetting = null,
            ICaptchaService captchaService = null,
            ILanguageService languageService = null,
            ISecurityQuestionService securityQuestionService = null)
        {
            sessionService = sessionService ?? MockingHelper.GetSessionService();
            sessionService.SignUpCaptchaCode = Constant.Captcha;
            appSetting = appSetting ?? Mock.Of<IAppSetting>();
            captchaService = captchaService ?? CreateDefaultCaptchaService();
            languageService = languageService ?? Mock.Of<ILanguageService>();
            securityQuestionService = securityQuestionService ?? Mock.Of<ISecurityQuestionService>();

            var controller = new SignupController(sessionService, appSetting, accountService, languageService, captchaService, securityQuestionService);
            var request = MockingHelper.GetHttpRequest();
            var server = new Mock<HttpServerUtilityBase>();
            server.Setup(s => s.HtmlEncode(It.IsAny<string>())).Returns<string>(s => $"Encoded{s}");

            controller.ControllerContext = Mock.Of<ControllerContext>(ctx =>
                ctx.HttpContext.Request == request &&
                ctx.HttpContext.Server == server.Object
            );

            var context = Mock.Of<HttpContextBase>(ctx => ctx.Request == request);
            var requestContext = new RequestContext(context, new RouteData());
            controller.Url = new UrlHelper(requestContext);

            return controller;
        }

        private static ICaptchaService CreateDefaultCaptchaService()
        {
            var mockCaptchaService = new Mock<ICaptchaService>();
            mockCaptchaService.Setup(p => p.IsValid(Constant.Captcha, Constant.Captcha, It.IsAny<string>()))
                .Returns(true);

            return mockCaptchaService.Object;
        }

        private UserSignUpParam CreateParam()
        {
            return new UserSignUpParam
            {
                FirstName = "FirstName",
                LastName = "LastName",
                Email = "Email",
                Phone = "Phone",
                Address = "Address",
                City = "City",
                Postal = "Postal",
                Country = "Country",
                SiteUrl = "SiteUrl",
                MoreSiteUrls = "MoreSiteUrls",
                NoSiteDesc = "NoSiteDesc",
                PreferredLanguage = "PreferredLanguage",
                QuestionId = 1,
                Answer = "Answer",
                Player_UserName = "Player_UserName",
                Gender = true,
                State = "State",
                ContactApps = "ContactApps",
                ContactAppList = CreateAppContactList()
            };
        }

        private UserSignUpParam CreateExpectedParam()
        {
            var appContactList = CreateAppContactList().Select(p => new ContactApp
            {
                Name = $"Encoded{p.Name}",
                Value = $"Encoded{p.Value}",
                Order = p.Order
            });

            return new UserSignUpParam
            {
                FirstName = "EncodedFirstName",
                LastName = "EncodedLastName",
                Email = "EncodedEmail",
                Phone = "EncodedPhone",
                Address = "EncodedAddress",
                City = "EncodedCity",
                Postal = "EncodedPostal",
                Country = "EncodedCountry",
                SiteUrl = "EncodedSiteUrl",
                MoreSiteUrls = "EncodedMoreSiteUrls",
                NoSiteDesc = "EncodedNoSiteDesc",
                PreferredLanguage = "EncodedPreferredLanguage",
                QuestionId = 1,
                Answer = "EncodedAnswer",
                Player_UserName = "EncodedPlayer_UserName",
                Gender = true,
                State = "EncodedState",
                Language = Constant.DefaultLanguage,
                AffType = Constant.AffTypeAsian,
                Ip = "::1",
                IsLocal = true,
                ContactApps = JsonConvert.SerializeObject(appContactList)
            };
        }

        private static List<ContactApp> CreateAppContactList()
        {
            return new List<ContactApp> {
                    new ContactApp
                    {
                        Name = "Skype",
                        Value = "skyper",
                        Order = 0
                    },
                    new ContactApp
                    {
                        Name = "Zalo",
                        Value = "Zalor",
                        Order = 1
                    }
                };
        }
    }
}