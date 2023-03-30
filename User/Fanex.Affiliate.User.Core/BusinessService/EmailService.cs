using Fanex.Affiliate.User.Core.Constants;
using Fanex.Affiliate.User.Core.Enum;
using Fanex.Affiliate.User.Core.MetalModels;
using Fanex.Affiliate.User.Core.MetalModels.Email;
using Fanex.Affiliate.User.Core.Resources;
using Fanex.Affiliate.User.Core.Utilities;
using Fanex.Affiliate.User.Core.ViewModels;
using System.Web;

namespace Fanex.Affiliate.User.Core.BusinessService
{
    public class EmailService : IEmailService
    {
        private readonly IMailProcessor _mailProcessor;
        private readonly ILanguageService _languageService;

        public EmailService(
            IMailProcessor mailProcessor,
            ILanguageService languageService)
        {
            _mailProcessor = mailProcessor;
            _languageService = languageService;
        }

        public void SendRecoverPasswordEmail(RecoverPasswordEmailParam emailParam)
        {
            const string TemplateName = "RecoverPassword";
            var preferredLanguage = emailParam.User.PreferredLanguage;
            var emailContent = GetEmailTextContent(TemplateName, preferredLanguage, emailParam);

            HandleSetCulture(preferredLanguage);
            if (!emailParam.User.MailOption)
            {
                emailContent = GetEmailHtmlContent(emailContent, Lang.EmailTitlePasswordRecovery, preferredLanguage, emailParam.RootDomain);
            }

            _mailProcessor.Send(emailContent, emailParam.MailTo, Lang.SubjectForgetPass, emailParam.MailBcc, emailParam.MailCc);
            _languageService.SetCulture(emailParam.CurrentLanguage);
        }

        public void SendNewPasswordEmail(NewPasswordEmailParam emailParam)
        {
            const string TemplateName = "NewPassword";
            var preferredLanguage = emailParam.User.PreferredLanguage;
            var emailContent = GetEmailTextContent(TemplateName, preferredLanguage, emailParam);

            HandleSetCulture(preferredLanguage);
            if (!emailParam.User.MailOption)
            {
                emailContent = GetEmailHtmlContent(emailContent, Lang.EmailTitleNewPassword, preferredLanguage, emailParam.RootDomain);
            }

            _mailProcessor.Send(emailContent, emailParam.MailTo, Lang.SubjectNewPass, emailParam.MailBcc, emailParam.MailCc);
            _languageService.SetCulture(emailParam.CurrentLanguage);
        }

        public void SendPaymentEmail(PaymentEmailParam emailParam)
        {
            var templateName = emailParam.PaymentType == PaymentRequestType.Cancel ? "PaymentCancel" : "PaymentRequest";
            var preferredLanguage = emailParam.User.PreferredLanguage;
            var emailContent = GetEmailTextContent(templateName, preferredLanguage, emailParam);

            HandleSetCulture(preferredLanguage);
            if (!emailParam.User.MailOption)
            {
                emailContent = GetEmailHtmlContent(emailContent, Lang.EmailTitlePayment, preferredLanguage, emailParam.RootDomain);
            }

            var subject = Lang.CancelSubject.Replace("{transid}", emailParam.TransId.ToString());
            _mailProcessor.Send(emailContent, emailParam.MailTo, subject, emailParam.MailBcc, emailParam.MailCc);
            _languageService.SetCulture(emailParam.CurrentLanguage);
        }

        public void SendSignUpEmail(EmailBaseParam emailParam)
        {
            const string TemplateName = "Signup";
            var preferredLanguage = emailParam.User.PreferredLanguage;
            var emailContent = GetEmailTextContent(TemplateName, preferredLanguage, emailParam);

            HandleSetCulture(preferredLanguage);
            if (!emailParam.User.MailOption)
            {
                emailContent = GetEmailHtmlContent(emailContent, Lang.EmailTitleSignUp, preferredLanguage, emailParam.RootDomain);
            }

            _mailProcessor.Send(emailContent, emailParam.MailTo, Lang.RegisSubject, emailParam.MailBcc, emailParam.MailCc);
            _languageService.SetCulture(emailParam.CurrentLanguage);
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
            var preferredLanguage = emailParam.User.PreferredLanguage;
            var emailContent = GetEmailTextContent(TemplateName, preferredLanguage, emailParam);

            HandleSetCulture(preferredLanguage);
            if (!emailParam.User.MailOption)
            {
                emailContent = GetEmailHtmlContent(emailContent, Lang.EmailTittleVerificationAccount, preferredLanguage, emailParam.RootDomain);
            }

            _mailProcessor.Send(emailContent, emailParam.MailTo, Lang.SubjectVerificationAccount, emailParam.MailBcc, emailParam.MailCc);
            _languageService.SetCulture(emailParam.CurrentLanguage);
        }

        private string GetEmailTextContent(string templateName, string preferredLanguage, object model)
        {
            var viewPath = string.Format(TemplateString.EmailTemplatePath, templateName, preferredLanguage);
            var viewPathBackup = string.Format(TemplateString.EmailTemplatePath, templateName, SystemLanguage.DefaultLanguageCode);
            var physicalPath = HttpContext.Current.Server.MapPath(viewPath);
            var physicalPathBackup = HttpContext.Current.Server.MapPath(viewPathBackup);

            return _mailProcessor.BuildTemplateContent(physicalPath, physicalPathBackup, model);
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
            var viewPathBackup = viewPath;

            return _mailProcessor.BuildTemplateContent(viewPath, viewPathBackup, viewModel);
        }

        private void HandleSetCulture(string preferredLanguage)
        {
            var userLang = _languageService.IsValid(preferredLanguage) ? preferredLanguage : SystemLanguage.DefaultLanguageCode;
            _languageService.SetCulture(userLang);
        }
    }
}
