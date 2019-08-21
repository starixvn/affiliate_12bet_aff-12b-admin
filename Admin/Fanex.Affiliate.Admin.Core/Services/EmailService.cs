using System;
using System.Web;
using Fanex.Affiliate.Admin.Core.Configurations;
using Fanex.Affiliate.Admin.Core.Constants;
using Fanex.Affiliate.Admin.Core.DTO.Email;
using Fanex.Affiliate.Admin.Core.Resources;
using Fanex.Affiliate.Admin.Core.Utilities;
using Fanex.Affiliate.Admin.Core.ViewModels;

namespace Fanex.Affiliate.Admin.Core.Services
{
    public class EmailService : IEmailService
    {
        private readonly IMailProcessor _mailProcessor;

        private readonly IAppSetting _appSetting;

        private readonly ILanguageService _languageService;

        public EmailService(IMailProcessor mailProcessor, IAppSetting appSetting, ILanguageService languageService)
        {
            _mailProcessor = mailProcessor;
            _appSetting = appSetting;
            _languageService = languageService;
        }

        public void SendApprovedEmail(AffInformationEmail emailParam)
        {
            _languageService.SetCulture(emailParam.Language);
            SendApprovedAffEmail(emailParam);
            SendAffInforEmail(emailParam);
            SendWithdrawalGuidanceEmail(emailParam);
            _languageService.SetCulture(LanguageSystem.DefaultLanguage);
        }

        private void SendApprovedAffEmail(AffInformationEmail emailParam)
        {
            const string TemplateName = "AffiliateApproved";
            var model = new EmailBase
            {
                MailContact = _appSetting.MailContact
            };

            var content = GetEmailTextContent(TemplateName, emailParam.Language, model);
            var subject = Lang.EmailSubjectAffApproved;

            var contentToSend = emailParam.IsHtml ?
                GetEmailHtmlContent(content, Lang.EmailTitleAffApproved, emailParam.Language, emailParam.RootUrl) :
                content;

            _mailProcessor.Send(contentToSend, emailParam.MailTo, subject, _appSetting.BccMail, _appSetting.CcMail);
        }

        private void SendAffInforEmail(AffInformationEmail emailParam)
        {
            const string TemplateName = "AffiliateInformation";
            var model = new AffInformationEmail
            {
                MailContact = _appSetting.MailContact,
                UserSiteUrl = SiteDomainHelper.GetUserSiteUrl(2),
                FirstName = emailParam.FirstName,
                UserName = emailParam.UserName,
                Password = emailParam.Password
            };

            var content = GetEmailTextContent(TemplateName, emailParam.Language, model);
            var subject = Lang.EmailSubjectAffInfor;

            var contentToSend = emailParam.IsHtml ?
                GetEmailHtmlContent(content, Lang.EmailTitleAffInfor, emailParam.Language, emailParam.RootUrl) :
                content;

            _mailProcessor.Send(contentToSend, emailParam.MailTo, subject, _appSetting.BccMail, _appSetting.CcMail);
        }

        private void SendWithdrawalGuidanceEmail(AffInformationEmail emailParam)
        {
            const string TemplateName = "WithdrawalGuidance";
            var model = new WithdrawalEmail
            {
                MailContact = _appSetting.MailContact,
                FirstName = emailParam.FirstName
            };

            var content = GetEmailTextContent(TemplateName, emailParam.Language, model);
            var subject = Lang.EmailSubjectAffInfor;

            var contentToSend = emailParam.IsHtml ?
                GetEmailHtmlContent(content, Lang.EmailTitleWithdrawalGuidance, emailParam.Language, emailParam.RootUrl) :
                content;

            _mailProcessor.Send(contentToSend, emailParam.MailTo, subject, _appSetting.BccMail, _appSetting.CcMail);
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
            var viewPath = HttpContext.Current.Server.MapPath("~/Areas/Portal/Views/Shared/Email/CommonLayout.cshtml");

            return _mailProcessor.BuildTemplateContent(viewPath, viewModel);
        }

        public void SendPaymentRequestUpdateEmail(PaymentRequestUpdateEmail emailParam)
        {
            _languageService.SetCulture(emailParam.Language);
            emailParam.MailContact = _appSetting.MailContact;

            var TemplateName = emailParam.IsApproved ? "PaymentRequest/Approved" : "PaymentRequest/Rejected";

            var content = GetEmailTextContent(TemplateName, emailParam.Language, emailParam);
            var subject = emailParam.IsApproved ?
                string.Format(Lang.EmailSubjectPaymentRequestApproved, emailParam.TransId) :
                string.Format(Lang.EmailSubjectPaymentRequestRejected, emailParam.TransId);
            var title = emailParam.IsApproved ? Lang.EmailTitlePaymentRequestApproved : Lang.EmailTitlePaymentRequestRejected;

            var contentToSend = emailParam.IsHtml ?
                GetEmailHtmlContent(content, title, emailParam.Language, emailParam.RootUrl) :
                content;

            _mailProcessor.Send(contentToSend, emailParam.MailTo, subject, _appSetting.BccMail, _appSetting.CcMail);
            _languageService.SetCulture(LanguageSystem.DefaultLanguage);
        }

        public void SendDisapprovedEmail(AffInformationEmail emailParam)
        {
            _languageService.SetCulture(emailParam.Language);
            SendDisapprovedAffiliateEmail(emailParam);
            _languageService.SetCulture(LanguageSystem.DefaultLanguage);
        }

        private void SendDisapprovedAffiliateEmail(AffInformationEmail emailParam)
        {
            const string TemplateName = "AffiliateDisapproved";
            var model = new AffInformationEmail
            {
                MailContact = _appSetting.MailContact,
                FirstName = emailParam.FirstName
            };

            var content = GetEmailTextContent(TemplateName, emailParam.Language, model);
            var subject = Lang.EmailSubjectAffDisapproved;

            var contentToSend = emailParam.IsHtml ?
                GetEmailHtmlContent(content, Lang.EmailTitleAffDisapproved, emailParam.Language, emailParam.RootUrl) :
                content;

            _mailProcessor.Send(contentToSend, emailParam.MailTo, subject, _appSetting.BccMail, _appSetting.CcMail);
        }
    }
}
