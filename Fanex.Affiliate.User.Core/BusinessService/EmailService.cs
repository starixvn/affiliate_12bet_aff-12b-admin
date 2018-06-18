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
            var model = GetForgetPasswordModel(emailParam);
            const string TemplateName = "RecoverPassword";
            var emailContent = GetEmailTextContent(TemplateName, emailParam.User.PreferredLanguage, model);
            if (!emailParam.User.MailOption)
            {
                emailContent = GetEmailHtmlContent(emailContent, Lang.EmailTitlePasswordRecovery, emailParam.User.PreferredLanguage, emailParam.RootDomain);
            }

            _mailProcessor.Send(emailContent, emailParam.MailTo, Lang.SubjectForgetPass, emailParam.MailBcc, emailParam.MailCc);
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

            _mailProcessor.Send(emailContent, emailParam.MailTo, Lang.SubjectNewPass, emailParam.MailBcc, emailParam.MailCc);
        }

        public void SendPaymentEmail(PaymentEmailParam emailParam)
        {
            var model = GetPaymentModel(emailParam);
            var templateName = emailParam.PaymentType == PaymentRequestType.Cancel ? "PaymentCancel" : "PaymentRequest";
            var subject = Lang.CancelSubject.Replace("{transid}", emailParam.TransId.ToString());

            var emailContent = GetEmailTextContent(templateName, emailParam.User.PreferredLanguage, model);

            if (!emailParam.User.MailOption)
            {
                emailContent = GetEmailHtmlContent(emailContent, Lang.EmailTitlePayment, emailParam.User.PreferredLanguage, emailParam.RootDomain);
            }

            _mailProcessor.Send(emailContent, emailParam.MailTo, subject, emailParam.MailBcc, emailParam.MailCc);
        }

        public void SendSignUpEmail(EmailBaseParam emailParam)
        {
            var model = GetSignUpModel(emailParam);
            const string TemplateName = "Signup";
            var emailContent = GetEmailTextContent(TemplateName, emailParam.User.PreferredLanguage, model);
            if (!emailParam.User.MailOption)
            {
                emailContent = GetEmailHtmlContent(emailContent, Lang.EmailTitleSignUp, emailParam.User.PreferredLanguage, emailParam.RootDomain);
            }

            _mailProcessor.Send(emailContent, emailParam.MailTo, Lang.RegisSubject, emailParam.MailBcc, emailParam.MailCc);
        }

        public void SendNoticeEmailToCsd(EmailBaseParam emailParam, UserSignUpParam model)
        {
            const string TemplateName = "Csd";
            var emailContent = GetEmailTextContent(TemplateName, SystemLanguage.DefaultLanguageCode, model);

            var subject = string.Format(Lang.CsdSubject, model.UserName);
            _mailProcessor.Send(emailContent, emailParam.MailTo, subject, emailParam.MailBcc, emailParam.MailCc);
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

        private object GetPaymentModel(PaymentEmailParam emailParam)
        {
            return new
            {
                FirstName = emailParam.User.FirstName,
                MailContact = emailParam.MailContact,
                TransId = emailParam.TransId,
                Date = emailParam.RequestDate.ToString("MM/dd/yyyy"),
                Amount = emailParam.Amount
            };
        }

        private object GetSignUpModel(EmailBaseParam emailParam)
        {
            return new
            {
                FirstName = emailParam.User.FirstName,
                MailContact = emailParam.MailContact
            };
        }
    }
}
