using Fanex.Affiliate.Admin.Core.Configurations;
using Fanex.Affiliate.Admin.Core.Constants;
using Fanex.Affiliate.Admin.Core.DTO.Email;
using Fanex.Affiliate.Admin.Core.Resources;
using Fanex.Affiliate.Admin.Core.Services.Common;
using Fanex.Affiliate.Admin.Core.Utilities;
using Fanex.Affiliate.Admin.Core.ViewModels;
using System.Web;

namespace Fanex.Affiliate.Admin.Core.Services
{
    public class EmailService : IEmailService
    {
        private readonly IMailProcessor _mailProcessor;

        private readonly IAppSetting _appSetting;

        private readonly ICommonSettingService _commonSettingService;

        public EmailService(IMailProcessor mailProcessor, IAppSetting appSetting, ICommonSettingService commonSettingService)
        {
            _mailProcessor = mailProcessor;
            _appSetting = appSetting;
            _commonSettingService = commonSettingService;
        }

        public void SendApprovedEmail(AffInformationEmail emailParam)
        {
            emailParam.MailContact = _appSetting.MailContact;
            emailParam.ContactInfo = GetContactInfoContent(emailParam);

            _commonSettingService.SetCulture(emailParam.Language);
            SendApprovedAffEmail(emailParam);
            SendAffInforEmail(emailParam);
            SendWithdrawalGuidanceEmail(emailParam);
            _commonSettingService.SetCulture(LanguageSystem.DefaultLanguageCode);
        }

        private void SendApprovedAffEmail(AffInformationEmail emailParam)
        {
            var subject = Lang.EmailSubjectAffApproved;
            var model = new EmailBase
            {
                MailContact = emailParam.MailContact,
                ContactInfo = emailParam.ContactInfo
            };
            var content = GetEmailTextContent(TemplateString.AffiliateApproved, emailParam.Language, model);
            var contentToSend = emailParam.IsHtml ?
                GetEmailHtmlContent(content, Lang.EmailTitleAffApproved, emailParam.Language, emailParam.RootUrl) :
                content;

            _mailProcessor.Send(contentToSend, emailParam.MailTo, subject, _appSetting.BccMail, _appSetting.CcMail);
        }

        private void SendAffInforEmail(AffInformationEmail emailParam)
        {
            var subject = Lang.EmailSubjectAffInfor;

            emailParam.UserSiteUrl = SiteDomainHelper.GetUserSiteUrl(emailParam.AffType);
            var content = GetEmailTextContent(TemplateString.AffiliateInformation, emailParam.Language, emailParam);

            var contentToSend = emailParam.IsHtml ?
                GetEmailHtmlContent(content, Lang.EmailTitleAffInfor, emailParam.Language, emailParam.RootUrl) :
                content;

            _mailProcessor.Send(contentToSend, emailParam.MailTo, subject, _appSetting.BccMail, _appSetting.CcMail);
        }

        private void SendWithdrawalGuidanceEmail(AffInformationEmail emailParam)
        {
            var subject = Lang.EmailSubjectWithdrawalGuidance;
            var model = new WithdrawalEmail
            {
                MailContact = emailParam.MailContact,
                ContactInfo = emailParam.ContactInfo,
                FirstName = emailParam.FirstName
            };

            var content = GetEmailTextContent(TemplateString.WithdrawalGuidance, emailParam.Language, model);

            var contentToSend = emailParam.IsHtml ?
                GetEmailHtmlContent(content, Lang.EmailTitleWithdrawalGuidance, emailParam.Language, emailParam.RootUrl) :
                content;

            _mailProcessor.Send(contentToSend, emailParam.MailTo, subject, _appSetting.BccMail, _appSetting.CcMail);
        }

        public void SendApprovedPaymentRequestEmail(PaymentRequestUpdateEmail emailParam)
        {
            _commonSettingService.SetCulture(emailParam.Language);
            AssignEmailBaseInfo(emailParam);

            var content = GetEmailTextContent(TemplateString.PaymentRequestApproved, emailParam.Language, emailParam);
            var contentToSend = emailParam.IsHtml ?
                GetEmailHtmlContent(content, Lang.EmailTitlePaymentRequestApproved, emailParam.Language, emailParam.RootUrl) :
                content;
            var subject = string.Format(Lang.EmailSubjectPaymentRequestApproved, emailParam.TransId);

            _mailProcessor.Send(contentToSend, emailParam.MailTo, subject, _appSetting.BccMail, _appSetting.CcMail);
            _commonSettingService.SetCulture(LanguageSystem.DefaultLanguageCode);
        }

        public void SendRejectedPaymentRequestEmail(PaymentRequestUpdateEmail emailParam)
        {
            _commonSettingService.SetCulture(emailParam.Language);
            AssignEmailBaseInfo(emailParam);

            var content = GetEmailTextContent(TemplateString.PaymentRequestRejected, emailParam.Language, emailParam);
            var contentToSend = emailParam.IsHtml ?
                GetEmailHtmlContent(content, Lang.EmailTitlePaymentRequestRejected, emailParam.Language, emailParam.RootUrl) :
                content;
            var subject = string.Format(Lang.EmailSubjectPaymentRequestRejected, emailParam.TransId);

            _mailProcessor.Send(contentToSend, emailParam.MailTo, subject, _appSetting.BccMail, _appSetting.CcMail);
            _commonSettingService.SetCulture(LanguageSystem.DefaultLanguageCode);
        }

        public void SendDisapprovedEmail(AffInformationEmail emailParam)
        {
            _commonSettingService.SetCulture(emailParam.Language);
            SendDisapprovedAffiliateEmail(emailParam);
            _commonSettingService.SetCulture(LanguageSystem.DefaultLanguageCode);
        }

        private void SendDisapprovedAffiliateEmail(AffInformationEmail emailParam)
        {
            var subject = Lang.EmailSubjectAffDisapproved;
            AssignEmailBaseInfo(emailParam);

            var content = GetEmailTextContent(TemplateString.AffiliateDisapproved, emailParam.Language, emailParam);

            var contentToSend = emailParam.IsHtml ?
                GetEmailHtmlContent(content, Lang.EmailTitleAffDisapproved, emailParam.Language, emailParam.RootUrl) :
                content;

            _mailProcessor.Send(contentToSend, emailParam.MailTo, subject, _appSetting.BccMail, _appSetting.CcMail);
        }

        private string GetEmailTextContent(string templateName, string prefrerredLanguage, object model)
        {
            var viewPath = string.Format(TemplateString.EmailTemplatePath, templateName, prefrerredLanguage);
            var viewPathBackup = string.Format(TemplateString.EmailTemplatePath, templateName, LanguageSystem.DefaultLanguageCode);
            var physicalPath = HttpContext.Current.Server.MapPath(viewPath);
            var physicalPathBackup = HttpContext.Current.Server.MapPath(viewPathBackup);

            return _mailProcessor.BuildTemplateContent(physicalPath, physicalPathBackup, model);
        }

        private string GetEmailHtmlContent(string content, string title, string language, string domain)
        {
            var viewModel = new EmailTemplateViewModel
            {
                RootUrl = domain,
                ImageRootUrl = _appSetting.EmailBackgroundUrl,
                Language = language,
                Content = content,
                Title = title
            };
            var viewPath = HttpContext.Current.Server.MapPath(TemplateString.CommonLayoutEmailTemplatePath);
            var viewPathBackup = viewPath;

            return _mailProcessor.BuildTemplateContent(viewPath, viewPathBackup, viewModel);
        }

        public string GetContactInfoContent(EmailBase param)
        {
            return TemplateString.GetContactInfoContent(param, GetEmailTextContent, _appSetting.EmailBackgroundUrl);
        }

        private void AssignEmailBaseInfo(EmailBase param)
        {
            param.MailContact = _appSetting.MailContact;
            param.ContactInfo = GetContactInfoContent(param);
        }
    }
}
