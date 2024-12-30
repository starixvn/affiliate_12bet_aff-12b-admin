using Fanex.Affiliate.Admin.Core.Attributes;
using Fanex.Affiliate.Admin.Core.Configurations;
using Fanex.Affiliate.Admin.Core.Constants;
using Fanex.Affiliate.Admin.Core.DTO.Email;
using Fanex.Affiliate.Admin.Core.Resources;
using Fanex.Affiliate.Admin.Core.Services.Common;
using Fanex.Affiliate.Admin.Core.Utilities;
using Fanex.Affiliate.Admin.Core.ViewModels;
using System;
using System.Web.Mvc;

namespace Fanex.Affiliate.Admin.Core.Controllers
{
    [FunctionAuthorize(FunctionName = Function.UserControl)]
    public class EmailTestingController : ReadOnlySessionBaseController
    {
        private const int _transId = 123;
        private const int _amount = 100;
        private const string _firstName = "First Name";

        private readonly IMailProcessor _mailProcessor;

        private readonly IAppSetting _appSetting;

        private readonly ISystemService _languageService;

        public EmailTestingController(
            ISessionService sessionService,
            IMailProcessor mailProcessor,
            IAppSetting appSetting,
            ISystemService languageService
            ) : base(sessionService)
        {
            _mailProcessor = mailProcessor;
            _appSetting = appSetting;
            _languageService = languageService;
        }

        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Preview(string language, string function, string format)
        {
            string result;
            try
            {
                _languageService.SetCulture(language);

                (var _, var title) = GetSubjectAndTitle(function);
                result = GetEmailContent(language, function, format, title);

                _languageService.SetCulture(_appSetting.DefaultLanguageCode);
            }
            catch (Exception ex)
            {
                result = ex.Message + "\n" + ex.StackTrace;
            }

            return View(model: result);
        }

        [HttpPost]
        public ActionResult SendEmail(string language, string function, string format, string emailAddress)
        {
            var success = true;
            var message = string.Empty;

            try
            {
                _languageService.SetCulture(language);
                (var subject, var title) = GetSubjectAndTitle(function);
                var content = GetEmailContent(language, function, format, title);

                _mailProcessor.Send(content, emailAddress, subject);
                _languageService.SetCulture(_appSetting.DefaultLanguageCode);
            }
            catch (Exception ex)
            {
                success = false;
                message = ex.Message + "\n" + ex.StackTrace;
            }

            var result = new
            {
                Success = success,
                Message = message
            };

            return Json(result);
        }

        private string GetEmailContent(string language, string function, string format, string title)
        {
            var isHtml = format != "txt";
            var rootUrl = SiteDomainHelper.GetRootUrl(this);
            var model = GetModelByFunction(function, language, rootUrl, isHtml) as EmailBase;
            model.ContactInfo = TemplateString.GetContactInfoContent(model, GetEmailTextContent, _appSetting.EmailBackgroundUrl);

            var content = GetEmailTextContent(function, language, model);

            return isHtml ? GetEmailHtmlContent(content, language, title) : content;
        }

        private string GetEmailTextContent(string function, string language, object model)
        {
            var viewPath = string.Format(TemplateString.EmailTemplatePath, function, language);
            var viewPathBackup = string.Format(TemplateString.EmailTemplatePath, function, _appSetting.DefaultLanguageCode);
            var physicalPath = Server.MapPath(viewPath);
            var physicalPathBackup = Server.MapPath(viewPathBackup);

            return model == null ? string.Empty : _mailProcessor.BuildTemplateContent(physicalPath, physicalPathBackup, model);
        }

        private string GetEmailHtmlContent(string content, string language, string title)
        {
            var domain = SiteDomainHelper.GetRootUrl(this);

            var viewModel = new EmailTemplateViewModel
            {
                RootUrl = domain,
                ImageRootUrl = _appSetting.EmailBackgroundUrl,
                UserSiteUrl = _appSetting.AsianAffSiteUrl,
                Language = language,
                Content = content,
                Title = title
            };
            var viewPath = Server.MapPath(TemplateString.CommonLayoutEmailTemplatePath);
            var viewPathBackup = viewPath;

            return _mailProcessor.BuildTemplateContent(viewPath, viewPathBackup, viewModel);
        }

        private static (string, string) GetSubjectAndTitle(string functionName)
        {
            switch (functionName)
            {
                case TemplateString.AffiliateDisapproved:
                    return (Lang.EmailSubjectAffDisapproved, Lang.EmailTitleAffDisapproved);

                case TemplateString.AffiliateApproved:
                    return (Lang.EmailSubjectAffApproved, Lang.EmailTitleAffApproved);

                case TemplateString.WithdrawalGuidance:
                    return (Lang.EmailSubjectWithdrawalGuidance, Lang.EmailTitleWithdrawalGuidance);

                case TemplateString.AffiliateInformation:
                    return (Lang.EmailSubjectAffInfor, Lang.EmailTitleAffInfor);

                case TemplateString.PaymentRequestApproved:
                    return (string.Format(Lang.EmailSubjectPaymentRequestApproved, _transId), Lang.EmailTitlePaymentRequestApproved);

                case TemplateString.PaymentRequestRejected:
                    return (string.Format(Lang.EmailSubjectPaymentRequestRejected, _transId), Lang.EmailTitlePaymentRequestRejected);

                default:
                    return (Lang.Affiliate, Lang.Affiliate);
            }
        }

        private object GetModelByFunction(string functionName, string language, string rootUrl, bool isHtml)
        {
            switch (functionName)
            {
                case TemplateString.AffiliateApproved:
                    return new EmailBase
                    {
                        RootUrl = rootUrl,
                        Language = language,
                        IsHtml = isHtml,
                        MailContact = _appSetting.MailContact
                    };
                case TemplateString.AffiliateInformation:
                    return new AffInformationEmail
                    {
                        RootUrl = rootUrl,
                        Language = language,
                        IsHtml = isHtml,
                        MailContact = _appSetting.MailContact,
                        UserSiteUrl = SiteDomainHelper.GetUserSiteUrl(AffType.Asian),
                        FirstName = _firstName,
                        UserName = "AFF12BET00000",
                        Password = "qwrfsdg"
                    };
                case TemplateString.WithdrawalGuidance:
                    return new WithdrawalEmail
                    {
                        RootUrl = rootUrl,
                        Language = language,
                        IsHtml = isHtml,
                        MailContact = _appSetting.MailContact,
                        FirstName = _firstName
                    };
                case TemplateString.AffiliateDisapproved:
                    return new AffInformationEmail
                    {
                        RootUrl = rootUrl,
                        Language = language,
                        IsHtml = isHtml,
                        MailContact = _appSetting.MailContact,
                        UserSiteUrl = _appSetting.AsianAffSiteUrl,
                        FirstName = _firstName
                    };
                case TemplateString.PaymentRequestApproved:
                    return new PaymentRequestUpdateEmail
                    {
                        RootUrl = rootUrl,
                        Language = language,
                        IsHtml = isHtml,
                        MailContact = _appSetting.MailContact,
                        FirstName = _firstName,
                        TransId = _transId,
                        TransDate = DateTimeHelper.NowDateTimeOffset.ToString("MM/dd/yyyy hh:mm:ss"),
                        Amount = _amount
                    };
                case TemplateString.PaymentRequestRejected:
                    return new PaymentRequestUpdateEmail
                    {
                        RootUrl = rootUrl,
                        Language = language,
                        IsHtml = isHtml,
                        MailContact = _appSetting.MailContact,
                        FirstName = _firstName,
                        TransId = _transId,
                        TransDate = DateTimeHelper.NowDateTimeOffset.ToString("MM/dd/yyyy hh:mm:ss"),
                        Amount = _amount
                    };
                default:
                    return null;
            }
        }
    }
}
