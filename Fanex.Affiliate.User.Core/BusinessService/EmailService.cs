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
            var emailContent = GetEmailTextContent(emailParam);
            if (!emailParam.User.MailOption)
            {
                emailContent = GetEmailHtmlContent(emailContent, emailParam.User.PreferredLanguage, emailParam.RootDomain);
            }

            _mailProcessor.Send(emailContent, emailParam.MailTo, Lang.SubjectForgetPass);
        }

        private string GetEmailTextContent(RecoverPasswordEmailParam emailParam)
        {
            const string FunctionName = "ForgetPassword";

            var viewPath = string.Format(TemplateString.EmailTemplatePath, FunctionName, emailParam.User.PreferredLanguage);
            var physicalPath = HttpContext.Current.Server.MapPath(viewPath);

            return _mailProcessor.BuildTemplateContent(physicalPath, GetForgetPasswordModel(emailParam));
        }

        private string GetEmailHtmlContent(string content, string language, string domain)
        {
            var viewModel = new EmailTemplateViewModel
            {
                RootUrl = domain,
                ImageRootUrl = string.Format(TemplateString.EmailImageRootPath, domain),
                Language = language,
                Content = content,
                Title = string.Empty
            };
            var viewPath = HttpContext.Current.Server.MapPath("~/Views/Shared/Email/CommonLayout.cshtml");

            return _mailProcessor.BuildTemplateContent(viewPath, viewModel);
        }

        private object GetForgetPasswordModel(RecoverPasswordEmailParam emailParam)
        {
            return new
            {
                RecoverPwdURL = emailParam.RootDomain + "Password/Recovery",
                CustId = emailParam.User.AffId,
                ValidationCode = emailParam.Code,
                FirstName = emailParam.User.FirstName,
                UserName = emailParam.User.UserName,
                MailContact = emailParam.MailContact
            };
        }
    }
}
