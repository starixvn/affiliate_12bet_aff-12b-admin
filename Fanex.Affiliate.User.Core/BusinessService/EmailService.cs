using System.Web;
using Fanex.Affiliate.User.Core.Constants;
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
            var model = GetForgetPasswordModel(emailParam);
            const string TemplateName = "RecoverPassword";
            var emailContent = GetEmailTextContent(TemplateName, emailParam.User.PreferredLanguage, model);
            if (!emailParam.User.MailOption)
            {
                emailContent = GetEmailHtmlContent(emailContent, Lang.EmailTitlePasswordRecovery, emailParam.User.PreferredLanguage, emailParam.RootDomain);
            }

            _mailProcessor.Send(emailContent, emailParam.MailTo, Lang.SubjectForgetPass);
        }

        public void SendNewPasswordEmail(NewPasswordEmailParam emailParam)
        {
            var model = GetPasswordRecoveryModel(emailParam);
            const string TemplateName = "NewPassword";
            var emailContent = GetEmailTextContent(TemplateName, emailParam.User.PreferredLanguage, model);
            if (!emailParam.User.MailOption)
            {
                emailContent = GetEmailHtmlContent(emailContent, Lang.EmailTitleNewPassword, emailParam.User.PreferredLanguage, emailParam.RootDomain);
            }

            _mailProcessor.Send(emailContent, emailParam.MailTo, Lang.SubjectNewPass);
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

        private object GetForgetPasswordModel(RecoverPasswordEmailParam emailParam)
        {
            return new
            {
                RecoverPwdURL = emailParam.RootDomain + "Password/RecoverPassword",
                CustId = emailParam.User.AffId,
                ValidationCode = emailParam.Code,
                FirstName = emailParam.User.FirstName,
                UserName = emailParam.User.UserName,
                MailContact = emailParam.MailContact
            };
        }

        private object GetPasswordRecoveryModel(NewPasswordEmailParam emailParam)
        {
            return new
            {
                FirstName = emailParam.User.FirstName,
                MailContact = emailParam.MailContact,
                UserName = emailParam.User.UserName,
                Password = emailParam.NewPassword,
                Url = emailParam.RootDomain
            };
        }
    }
}
