using Fanex.Affiliate.User.Core.BusinessService;
using Fanex.Affiliate.User.Core.Enum;
using Fanex.Affiliate.User.Core.MetalModels;
using Fanex.Affiliate.User.Core.MetalModels.Email;
using Fanex.Affiliate.User.Core.Resources;
using Fanex.Affiliate.User.Core.Utilities;
using Fanex.Affiliate.User.Core.ViewModels;
using Newtonsoft.Json;
using NSubstitute;
using System;
using System.IO;
using System.Web;
using Xunit;

namespace Fanex.Affiliate.User.Core.UnitTests.BusinessService
{
    public class EmailServiceTest
    {
        private readonly IMailProcessor MailProcessor;

        public EmailServiceTest()
        {
            MailProcessor = Substitute.For<IMailProcessor>();
        }

        [Fact]
        public void SendCodeReoverPasswordEmail_WhenCall_SendMail()
        {
            var service = CreateService();
            var param = CreateRecoverEmailParam();
            var mainContent = "mail content";
            SetUpEnviroment();
            MailProcessor.BuildTemplateContent(null, null).ReturnsForAnyArgs(mainContent);

            service.SendRecoverPasswordEmail(param);

            MailProcessor.Received().BuildTemplateContent(Arg.Compat.Any<string>(), Arg.Compat.Any<RecoverPasswordEmailParam>());
            MailProcessor.Received().BuildTemplateContent(Arg.Compat.Any<string>(), Arg.Compat.Any<EmailTemplateViewModel>());
            MailProcessor.Received().Send(mainContent, param.MailTo, Lang.SubjectForgetPass, param.MailBcc, param.MailCc);
        }

        [Fact]
        public void SendNewPasswordEmail_WhenCall_SendMail()
        {
            var service = CreateService();
            var param = CreateNewPasswordEmailParam();
            var mainContent = "mail content";
            SetUpEnviroment();
            MailProcessor.BuildTemplateContent(null, null).ReturnsForAnyArgs(mainContent);

            service.SendNewPasswordEmail(param);

            MailProcessor.Received().BuildTemplateContent(Arg.Compat.Any<string>(), Arg.Compat.Any<NewPasswordEmailParam>());
            MailProcessor.Received().BuildTemplateContent(Arg.Compat.Any<string>(), Arg.Compat.Any<EmailTemplateViewModel>());
            MailProcessor.Received().Send(mainContent, param.MailTo, Lang.SubjectNewPass, param.MailBcc, param.MailCc);
        }

        [Theory]
        [InlineData(PaymentRequestType.Cancel)]
        [InlineData(PaymentRequestType.Create)]
        public void SendPaymentEmail_WhenCall_SendMail(PaymentRequestType type)
        {
            var service = CreateService();
            var param = CreatePaymentEmailParam(type);
            var mainContent = "mail content";
            SetUpEnviroment();
            MailProcessor.BuildTemplateContent(null, null).ReturnsForAnyArgs(mainContent);

            service.SendPaymentEmail(param);

            MailProcessor.Received().BuildTemplateContent(Arg.Compat.Any<string>(), Arg.Compat.Any<PaymentEmailParam>());
            MailProcessor.Received().BuildTemplateContent(Arg.Compat.Any<string>(), Arg.Compat.Any<EmailTemplateViewModel>());
            MailProcessor.Received().Send(mainContent, param.MailTo, Arg.Compat.Is<string>(c => c.Contains(param.TransId.ToString())), param.MailBcc, param.MailCc);
        }

        [Fact]
        public void SendSignUpEmail_WhenCall_SendMail()
        {
            var service = CreateService();
            var param = CreateEmailBaseParam();
            var mainContent = "mail content";
            SetUpEnviroment();
            MailProcessor.BuildTemplateContent(null, null).ReturnsForAnyArgs(mainContent);

            service.SendSignUpEmail(param);

            MailProcessor.Received().BuildTemplateContent(Arg.Compat.Any<string>(), Arg.Compat.Any<EmailBaseParam>());
            MailProcessor.Received().BuildTemplateContent(Arg.Compat.Any<string>(), Arg.Compat.Any<EmailTemplateViewModel>());
            MailProcessor.Received().Send(mainContent, param.MailTo, Lang.RegisSubject, param.MailBcc, param.MailCc);
        }

        [Fact]
        public void SendNoticeEmailToCsd_WhenCall_SendMail()
        {
            var service = CreateService();
            var param = CreateNoticeEmailParam();
            var mainContent = "mail content";
            SetUpEnviroment();
            MailProcessor.BuildTemplateContent(null, null).ReturnsForAnyArgs(mainContent);

            service.SendNoticeEmailToCsd(param);

            MailProcessor.Received().BuildTemplateContent(Arg.Compat.Any<string>(), Arg.Compat.Any<NoticeEmailParam>());
            MailProcessor.Received().Send(mainContent, param.MailTo, Arg.Compat.Is<string>(c => c.Contains(param.UserName)), param.MailBcc, param.MailCc);
        }

        [Fact]
        public void SendContactUsEmail_WhenCall_SendMail()
        {
            var service = CreateService();
            var param = CreateEmailBaseParam();
            var contactUsParam = CreateContactUsParam();
            var mainContent = "mail content";
            SetUpEnviroment();
            MailProcessor.BuildTemplateContent(null, null).ReturnsForAnyArgs(mainContent);

            service.SendContactUsEmail(param, contactUsParam);

            MailProcessor.Received().BuildTemplateContent(Arg.Compat.Any<string>(), Arg.Compat.Any<ContactUsParam>());
            MailProcessor.Received().Send(mainContent, param.MailTo, Arg.Compat.Is<string>(c => c.Contains(contactUsParam.Subject)), param.MailBcc, param.MailCc);
        }

        [Fact]
        public void SendVerificationEmail_WhenCall_SendMail()
        {
            var service = CreateService();
            var param = CreateVerificationAccountEmailParam();
            var mainContent = "mail content";
            SetUpEnviroment();
            MailProcessor.BuildTemplateContent(null, null).ReturnsForAnyArgs(mainContent);

            service.SendVerificationEmail(param);

            MailProcessor.Received().BuildTemplateContent(Arg.Compat.Any<string>(), Arg.Compat.Any<VerificationAccountEmailParam>());
            MailProcessor.Received().BuildTemplateContent(Arg.Compat.Any<string>(), Arg.Compat.Any<EmailTemplateViewModel>());
            MailProcessor.Received().Send(mainContent, param.MailTo, Lang.SubjectVerificationAccount, param.MailBcc, param.MailCc);
        }

        private static void SetUpEnviroment()
        {
            var request = new HttpRequest("111", "http://127.0.0.1:8080/controller/action", null);
            var response = new HttpResponse(new StringWriter());
            HttpContext.Current = new HttpContext(request, response);
        }

        private static RecoverPasswordEmailParam CreateRecoverEmailParam()
        {
            var serialBaseClass = JsonConvert.SerializeObject(CreateEmailBaseParam());
            var param = JsonConvert.DeserializeObject<RecoverPasswordEmailParam>(serialBaseClass);
            param.Code = "123";
            return param;
        }

        private static NewPasswordEmailParam CreateNewPasswordEmailParam()
        {
            var serialBaseClass = JsonConvert.SerializeObject(CreateEmailBaseParam());
            var param = JsonConvert.DeserializeObject<NewPasswordEmailParam>(serialBaseClass);
            param.NewPassword = "123";
            return param;
        }

        private static PaymentEmailParam CreatePaymentEmailParam(PaymentRequestType type)
        {
            var serialBaseClass = JsonConvert.SerializeObject(CreateEmailBaseParam());
            var param = JsonConvert.DeserializeObject<PaymentEmailParam>(serialBaseClass);
            param.PaymentType = type;
            param.RequestDate = DateTime.Now;
            param.TransId = 1123;
            param.Amount = 20000;
            return param;
        }

        private static NoticeEmailParam CreateNoticeEmailParam()
        {
            var serialBaseClass = JsonConvert.SerializeObject(CreateEmailBaseParam());
            var param = JsonConvert.DeserializeObject<NoticeEmailParam>(serialBaseClass);
            param.FirstName = "first";
            param.LastName = "last";
            param.UserName = "UserName";
            param.SiteUrl = "url";
            param.MoreSiteUrls = "moreurl";
            param.NoSiteDesc = "1";
            return param;
        }

        private static VerificationAccountEmailParam CreateVerificationAccountEmailParam()
        {
            var serialBaseClass = JsonConvert.SerializeObject(CreateEmailBaseParam());
            var param = JsonConvert.DeserializeObject<VerificationAccountEmailParam>(serialBaseClass);
            param.Code = "123";
            param.Ip = "127.0.0.1";
            return param;
        }

        private static EmailBaseParam CreateEmailBaseParam()
            => new EmailBaseParam
            {
                MailBcc = "abc@gmail.com",
                MailCc = "12b@gmail.com",
                MailContact = "ab",
                MailTo = "12@gmail.com",
                RootDomain = "abc.com",
                User = new DTO.UserInfo
                {
                    AffId = 1,
                    Email = "abb@gmail.com",
                    FirstName = "User",
                    LastName = "Name",
                    MailOption = false,
                    PreferredLanguage = "vn-vi",
                    SecretKey = "124m",
                    UserName = "test"
                }
            };

        private static ContactUsParam CreateContactUsParam()
            => new ContactUsParam
            {
                Affiliate = " 12",
                Browser = "Chrome",
                Captcha = "1234",
                Content = "abc",
                Creator = 1,
                Email = "abc@email.com",
                ErrCode = 0,
                ErrMsg = "",
                Ip = "127.0.0.1",
                Mid = 2,
                Sender = "sencder",
                SessionId = "3",
                Subject = "test subject"
            };

        private EmailService CreateService()
            => new EmailService(MailProcessor);
    }
}
