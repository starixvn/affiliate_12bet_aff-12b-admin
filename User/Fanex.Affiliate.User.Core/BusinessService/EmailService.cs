using System.Web;
using Fanex.Affiliate.User.Core.Constants;
using Fanex.Affiliate.User.Core.Enum;
using Fanex.Affiliate.User.Core.MetalModels;
using Fanex.Affiliate.User.Core.MetalModels.Email;
using Fanex.Affiliate.User.Core.Resources;
using Fanex.Affiliate.User.Core.Utilities;
using Fanex.Affiliate.User.Core.ViewModels;

namespace Fanex.Affiliate.User.Core.BusinessService
{
    public class EmailService : IEmailService
    {
        private readonly IMailProcessor _mailProcessor;

        public EmailService(IMailProcessor mailProcessor)
        {
            _mailProcessor = mailProcessor;
        }

        public void SendRecoverPasswordEmail(RecoverPasswordEmailParam emailParam)
        {
            const string TemplateName = "RecoverPassword";
            var emailContent = GetEmailTextContent(TemplateName, emailParam.User.PreferredLanguage, emailParam);
            if (!emailParam.User.MailOption)
            {
                emailContent = GetEmailHtmlContent(emailContent, Lang.EmailTitlePasswordRecovery, emailParam.User.PreferredLanguage, emailParam.RootDomain);
            }

            _mailProcessor.Send(emailContent, emailParam.MailTo, Lang.SubjectForgetPass, emailParam.MailBcc, emailParam.MailCc);
        }

        public void SendNewPasswordEmail(NewPasswordEmailParam emailParam)
        {
            const string TemplateName = "NewPassword";
            var emailContent = GetEmailTextContent(TemplateName, emailParam.User.PreferredLanguage, emailParam);
            if (!emailParam.User.MailOption)
            {
                emailContent = GetEmailHtmlContent(emailContent, Lang.EmailTitleNewPassword, emailParam.User.PreferredLanguage, emailParam.RootDomain);
            }

            _mailProcessor.Send(emailContent, emailParam.MailTo, Lang.SubjectNewPass, emailParam.MailBcc, emailParam.MailCc);
        }

        public void SendPaymentEmail(PaymentEmailParam emailParam)
        {
            var templateName = emailParam.PaymentType == PaymentRequestType.Cancel ? "PaymentCancel" : "PaymentRequest";
            var subject = Lang.CancelSubject.Replace("{transid}", emailParam.TransId.ToString());

            var emailContent = GetEmailTextContent(templateName, emailParam.User.PreferredLanguage, emailParam);

            if (!emailParam.User.MailOption)
            {
                emailContent = GetEmailHtmlContent(emailContent, Lang.EmailTitlePayment, emailParam.User.PreferredLanguage, emailParam.RootDomain);
            }

            _mailProcessor.Send(emailContent, emailParam.MailTo, subject, emailParam.MailBcc, emailParam.MailCc);
        }

        public void SendSignUpEmail(EmailBaseParam emailParam)
        {
            const string TemplateName = "Signup";
            var emailContent = GetEmailTextContent(TemplateName, emailParam.User.PreferredLanguage, emailParam);
            if (!emailParam.User.MailOption)
            {
                emailContent = GetEmailHtmlContent(emailContent, Lang.EmailTitleSignUp, emailParam.User.PreferredLanguage, emailParam.RootDomain);
            }

            _mailProcessor.Send(emailContent, emailParam.MailTo, Lang.RegisSubject, emailParam.MailBcc, emailParam.MailCc);
        }

        public void SendNoticeEmailToCsd(NoticeEmailParam emailParam)
        {
            const string TemplateName = "Csd";
            var emailContent = GetEmailTextContent(TemplateName, SystemLanguage.DefaultLanguageCode, emailParam);

            var subject = string.Format(Lang.CsdSubject, emailParam.UserName);
            _mailProcessor.Send(emailContent, emailParam.MailTo, subject, emailParam.MailBcc, emailParam.MailCc);
        }

        public void SendContactUsEmail(EmailBaseParam emailParam, ContactUsParam model)
        {
            const string TemplateName = "ContactUs";
            var emailContent = GetEmailTextContent(TemplateName, SystemLanguage.DefaultLanguageCode, model);

            var subject = $"12Bet Affiliates - Contact Us - {model.Subject}";
            _mailProcessor.Send(emailContent, emailParam.MailTo, subject, emailParam.MailBcc, emailParam.MailCc);
        }

        public void SendVerificationEmail(VerificationAccountEmailParam emailParam)
        {
            const string TemplateName = "VerificationAccount";
            var emailContent = GetEmailTextContent(TemplateName, emailParam.User.PreferredLanguage, emailParam);
            if (!emailParam.User.MailOption)
            {
                emailContent = GetEmailHtmlContent(emailContent, Lang.EmailTittleVerificationAccount, emailParam.User.PreferredLanguage, emailParam.RootDomain);
            }

            _mailProcessor.Send(emailContent, emailParam.MailTo, Lang.SubjectVerificationAccount, emailParam.MailBcc, emailParam.MailCc);
        }

        private string GetEmailTextContent(string templateName, string prefrerredLanguage, object model)
        {
            var viewPath = string.Format(TemplateString.EmailTemplatePath, templateName, prefrerredLanguage);
            var physicalPath = HttpContext.Current.Server.MapPath(viewPath);

            return _mailProcessor.BuildTemplateContent(physicalPath, model);
        }

        private string GetEmailHtmlContent(string content, string title, string language, string domain)
        {
            var viewModel = new EmailTemplateViewModel
            {
                RootUrl = domain,
                ImageRootUrl = string.Format(TemplateString.EmailImageRootPath, domain),
                Language = language,
                Content = content,
                Title = title
            };
            var viewPath = HttpContext.Current.Server.MapPath("~/Views/Shared/Email/CommonLayout.cshtml");

            return _mailProcessor.BuildTemplateContent(viewPath, viewModel);
        }
    }
}
