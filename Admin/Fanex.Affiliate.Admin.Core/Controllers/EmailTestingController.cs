using Fanex.Affiliate.Admin.Core.Attributes;
using Fanex.Affiliate.Admin.Core.Configurations;
using Fanex.Affiliate.Admin.Core.Constants;
using Fanex.Affiliate.Admin.Core.DTO.Email;
using Fanex.Affiliate.Admin.Core.Resources;
using Fanex.Affiliate.Admin.Core.Services;
using Fanex.Affiliate.Admin.Core.Services.Common;
using Fanex.Affiliate.Admin.Core.Utilities;
using Fanex.Affiliate.Admin.Core.ViewModels;
using System;
using System.Web.Mvc;

namespace Fanex.Affiliate.Admin.Core.Controllers
{
    [FunctionAuthorize(FunctionName = Function.AdminRoleMenu)]
    public class EmailTestingController : ReadOnlySessionBaseController
    {
        private const int _transId = 123;
        private const int _amount = 100;
        private const string _firstName = "First Name";

        private readonly IMailProcessor _mailProcessor;

        private readonly IAppSetting _appSetting;

        private readonly ICommonSettingService _commonSettingService;

        public EmailTestingController(
            ISessionService sessionService,
            IMailProcessor mailProcessor,
            IAppSetting appSetting,
            ICommonSettingService commonSettingService
            ) : base(sessionService)
        {
            _mailProcessor = mailProcessor;
            _appSetting = appSetting;
            _commonSettingService = commonSettingService;
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
                _commonSettingService.SetCulture(language);

                (var subject, var title) = GetSubjectAndTitle(function);
                result = GetEmailContent(language, function, format, title);

                _commonSettingService.SetCulture("en-US");
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
                _commonSettingService.SetCulture(language);
                (var subject, var title) = GetSubjectAndTitle(function);
                var content = GetEmailContent(language, function, format, title);

                _mailProcessor.Send(content, emailAddress, subject);
                _commonSettingService.SetCulture("en-US");
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
            var content = GetEmailTextContent(function, language);

            return format == "txt" ? content : GetEmailHtmlContent(content, language, title);
        }

        private string GetEmailTextContent(string function, string language)
        {
            var viewPath = string.Format(TemplateString.EmailTemplatePath, function, language);
            var physicalPath = Server.MapPath(viewPath);
            var model = GetModelByFunction(function);

            return model == null ? string.Empty : _mailProcessor.BuildTemplateContent(physicalPath, model);
        }

        private string GetEmailHtmlContent(string content, string language, string title)
        {
            var domain = SiteDomainHelper.GetRootUrl(this);

            var viewModel = new EmailTemplateViewModel
            {
                RootUrl = domain,
                UserSiteUrl = _appSetting.AsianAffSiteUrl,
                ImageRootUrl = string.Format(TemplateString.EmailImageRootPath, domain),
                Language = language,
                Content = content,
                Title = title
            };
            var viewPath = Server.MapPath("~/Areas/Portal/Views/Shared/Email/CommonLayout.cshtml");

            return _mailProcessor.BuildTemplateContent(viewPath, viewModel);
        }

        private static (string, string) GetSubjectAndTitle(string functionName)
        {
            switch (functionName)
            {
                case "AffiliateDisapproved":
                    return (Lang.EmailSubjectAffDisapproved, Lang.EmailTitleAffDisapproved);

                case "AffiliateApproved":
                    return (Lang.EmailSubjectAffApproved, Lang.EmailTitleAffApproved);

                case "WithdrawalGuidance":
                    return (Lang.EmailSubjectWithdrawalGuidance, Lang.EmailTitleWithdrawalGuidance);

                case "AffiliateInformation":
                    return (Lang.EmailSubjectAffInfor, Lang.EmailTitleAffInfor);

                case "PaymentRequest/Approved":
                    return (string.Format(Lang.EmailSubjectPaymentRequestApproved, _transId), Lang.EmailTitlePaymentRequestApproved);

                case "PaymentRequest/Rejected":
                    return (string.Format(Lang.EmailSubjectPaymentRequestRejected, _transId), Lang.EmailTitlePaymentRequestRejected);

                default:
                    return (Lang.Affiliate, Lang.Affiliate);
            }
        }

        private object GetModelByFunction(string functionName)
        {
            switch (functionName)
            {
                case "AffiliateApproved":
                    return new EmailBase
                    {
                        MailContact = _appSetting.MailContact
                    };
                case "AffiliateInformation":
                    return new AffInformationEmail
                    {
                        MailContact = _appSetting.MailContact,
                        UserSiteUrl = SiteDomainHelper.GetUserSiteUrl(AffType.Asian),
                        FirstName = _firstName,
                        UserName = "AFF12BET00000",
                        Password = "qwrfsdg"
                    };
                case "WithdrawalGuidance":
                    return new WithdrawalEmail
                    {
                        MailContact = _appSetting.MailContact,
                        FirstName = _firstName
                    };
                case "AffiliateDisapproved":
                    return new AffInformationEmail
                    {
                        MailContact = _appSetting.MailContact,
                        UserSiteUrl = _appSetting.AsianAffSiteUrl,
                        FirstName = _firstName
                    };
                case "PaymentRequest/Approved":
                    return new PaymentRequestUpdateEmail
                    {
                        MailContact = _appSetting.MailContact,
                        FirstName = _firstName,
                        TransId = _transId,
                        TransDate = DateTime.Now.ToString("MM/dd/yyyy hh:mm:ss"),
                        Amount = _amount
                    };
                case "PaymentRequest/Rejected":
                    return new PaymentRequestUpdateEmail
                    {
                        MailContact = _appSetting.MailContact,
                        FirstName = _firstName,
                        TransId = _transId,
                        TransDate = DateTime.Now.ToString("MM/dd/yyyy hh:mm:ss"),
                        Amount = _amount
                    };
                default:
                    return null;
            }
        }
    }
}
