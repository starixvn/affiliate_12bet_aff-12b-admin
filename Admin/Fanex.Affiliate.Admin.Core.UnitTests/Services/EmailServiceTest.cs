using Autofac;
using Autofac.Integration.Mvc;
using Fanex.Affiliate.Admin.Core.Configurations;
using Fanex.Affiliate.Admin.Core.DTO.Email;
using Fanex.Affiliate.Admin.Core.Services;
using Fanex.Affiliate.Admin.Core.Services.Common;
using Fanex.Affiliate.Admin.Core.UnitTests.TestHelper;
using Fanex.Affiliate.Admin.Core.Utilities;
using Moq;
using NSubstitute;
using System.IO;
using System.Web;
using System.Web.Mvc;
using Xunit;

namespace Fanex.Affiliate.Admin.Core.UnitTests.Services
{
    public class EmailServiceTest
    {
        private ILifetimeScopeProvider ScopeProvider;
        private readonly IMailProcessor MailProcessor;
        private readonly IAppSetting AppSetting;
        private readonly ICommonSettingService commonSettingService;

        public EmailServiceTest()
        {
            MailProcessor = Substitute.For<IMailProcessor>();
            AppSetting = Substitute.For<IAppSetting>();
            commonSettingService = Substitute.For<ICommonSettingService>();
        }

        [Fact]
        public void SendApprovedEmail_WhenCallWithIsHtml_SendMail()
        {
            //Arrange
            var appSetting = new Mock<IAppSetting>();
            appSetting.Setup(a => a.Environment).Returns("UAT");
            appSetting.Setup(a => a.IsProduction).Returns(true);

            var builder = new ContainerBuilder();
            builder.RegisterInstance(appSetting.Object).As<IAppSetting>();
            var container = builder.Build();

            ScopeProvider = new SimpleLifetimeScopeProvider(container);
            var resolver = new AutofacDependencyResolver(container, ScopeProvider);
            DependencyResolver.SetResolver(resolver);

            var emailParam = new AffInformationEmail
            {
                Language = "en-US",
                IsHtml = true
            };
            var mainContent = "mail content";
            MailProcessor.BuildTemplateContent(null, null, null).ReturnsForAnyArgs(mainContent);

            //Action
            var service = CreateEmailService();
            service.SendApprovedEmail(emailParam);

            //Assert
            MailProcessor.Received(3).BuildTemplateContent(Arg.Compat.Any<string>(), Arg.Compat.Any<string>(), Arg.Compat.Any<EmailBase>());
            MailProcessor.Received(3).Send(mainContent, Arg.Compat.Any<string>(), Arg.Compat.Any<string>(), Arg.Compat.Any<string>(), Arg.Compat.Any<string>());
        }

        [Fact]
        public void SendApprovedEmail_WhenCall_SendMail()
        {
            //Arrange
            var appSetting = new Mock<IAppSetting>();
            appSetting.Setup(a => a.Environment).Returns("UAT");
            appSetting.Setup(a => a.IsProduction).Returns(true);

            var builder = new ContainerBuilder();
            builder.RegisterInstance(appSetting.Object).As<IAppSetting>();
            var container = builder.Build();

            this.ScopeProvider = new SimpleLifetimeScopeProvider(container);
            var resolver = new AutofacDependencyResolver(container, ScopeProvider);
            DependencyResolver.SetResolver(resolver);

            var emailParam = new AffInformationEmail
            {
                Language = "en-US"
            };
            var mainContent = "mail content";
            MailProcessor.BuildTemplateContent(null, null, null).ReturnsForAnyArgs(mainContent);


            //Action
            var service = CreateEmailService();
            service.SendApprovedEmail(emailParam);

            //Assert
            MailProcessor.Received(3).BuildTemplateContent(Arg.Compat.Any<string>(), Arg.Compat.Any<string>(), Arg.Compat.Any<EmailBase>());
            MailProcessor.Received(3).Send(mainContent, Arg.Compat.Any<string>(), Arg.Compat.Any<string>(), Arg.Compat.Any<string>(), Arg.Compat.Any<string>());
        }

        [Fact]
        public void SendPaymentRequestUpdateEmail_WhenCallWithIsApproved_SendMail()
        {
            //Arrange
            var emailParam = new PaymentRequestUpdateEmail
            {
                Language = "en-US",
                TransId = 123,
                IsApproved = true,
                IsHtml = true
            };
            var mainContent = "mail content";
            MailProcessor.BuildTemplateContent(null, null, null).ReturnsForAnyArgs(mainContent);

            //Action
            var service = CreateEmailService();
            service.SendPaymentRequestUpdateEmail(emailParam);

            //Assert
            MailProcessor.Received().BuildTemplateContent(Arg.Compat.Any<string>(), Arg.Compat.Any<string>(), Arg.Compat.Any<PaymentRequestUpdateEmail>());
            MailProcessor.Received().Send(mainContent, emailParam.MailTo, "12Bet Affiliates - [123] - Your payment request has been processed and completed successfully", AppSetting.BccMail, AppSetting.CcMail);
        }

        [Fact]
        public void SendPaymentRequestUpdateEmail_WhenCallWithIsReject_SendMail()
        {
            //Arrange
            var emailParam = new PaymentRequestUpdateEmail
            {
                Language = "en-US",
                TransId = 123
            };
            var mainContent = "mail content";
            MailProcessor.BuildTemplateContent(null, null, null).ReturnsForAnyArgs(mainContent);

            //Action
            var service = CreateEmailService();
            service.SendPaymentRequestUpdateEmail(emailParam);

            //Assert
            MailProcessor.Received().BuildTemplateContent(Arg.Compat.Any<string>(), Arg.Compat.Any<string>(), Arg.Compat.Any<PaymentRequestUpdateEmail>());
            MailProcessor.Received().Send(mainContent, emailParam.MailTo, "12Bet Affiliates - [123] - Your payment request has been rejected", AppSetting.BccMail, AppSetting.CcMail);
        }

        [Fact]
        public void SendDisapprovedEmail_WhenCallWithIsHtml_SendMail()
        {
            //Arrange
            var emailParam = new AffInformationEmail
            {
                Language = "en-US",
                IsHtml = true
            };
            var mainContent = "mail content";
            MailProcessor.BuildTemplateContent(null, null, null).ReturnsForAnyArgs(mainContent);

            //Action
            var service = CreateEmailService();
            service.SendDisapprovedEmail(emailParam);

            //Assert
            MailProcessor.Received().BuildTemplateContent(Arg.Compat.Any<string>(), Arg.Compat.Any<string>(), Arg.Compat.Any<AffInformationEmail>());
            MailProcessor.Received().Send(mainContent, emailParam.MailTo, "12Bet Affiliates – Your Application", AppSetting.BccMail, AppSetting.CcMail);
        }

        [Fact]
        public void SendDisapprovedEmail_WhenCall_SendMail()
        {
            //Arrange
            var emailParam = new AffInformationEmail
            {
                Language = "en-US"
            };
            var mainContent = "mail content";
            MailProcessor.BuildTemplateContent(null, null, null).ReturnsForAnyArgs(mainContent);

            //Action
            var service = CreateEmailService();
            service.SendDisapprovedEmail(emailParam);

            //Assert
            MailProcessor.Received().BuildTemplateContent(Arg.Compat.Any<string>(), Arg.Compat.Any<string>(), Arg.Compat.Any<AffInformationEmail>());
            MailProcessor.Received().Send(mainContent, emailParam.MailTo, "12Bet Affiliates – Your Application", AppSetting.BccMail, AppSetting.CcMail);
        }

        private EmailService CreateEmailService()
        {
            var request = new HttpRequest("111", "http://127.0.0.1:8080/controller/action", null);
            var response = new HttpResponse(new StringWriter());
            HttpContext.Current = new HttpContext(request, response);

            return new EmailService(MailProcessor, AppSetting, commonSettingService);
        }
    }
}
