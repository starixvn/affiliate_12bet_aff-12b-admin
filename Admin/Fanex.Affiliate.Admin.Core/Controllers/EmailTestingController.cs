using System;
using System.Web.Mvc;
using Fanex.Affiliate.Admin.Core.Attributes;
using Fanex.Affiliate.Admin.Core.Configurations;
using Fanex.Affiliate.Admin.Core.Constants;
using Fanex.Affiliate.Admin.Core.DTO.Email;
using Fanex.Affiliate.Admin.Core.Services;
using Fanex.Affiliate.Admin.Core.Services.Common;
using Fanex.Affiliate.Admin.Core.Utilities;
using Fanex.Affiliate.Admin.Core.ViewModels;

namespace Fanex.Affiliate.Admin.Core.Controllers
{
    [FunctionAuthorize(FunctionName = Function.UserControl)]
    public class EmailTestingController : BaseController
    {
        private readonly IMailProcessor _mailProcessor;

        private readonly IAppSetting _appSetting;

        private readonly ILanguageService _languageService;

        public EmailTestingController(
            ISessionService sessionService,
            IMailProcessor mailProcessor,
            IAppSetting appSetting,
            ILanguageService languageService
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
            var result = string.Empty;

            try
            {
                _languageService.SetCulture(language);
                result = GetEmailContent(language, function, format);
                _languageService.SetCulture("en-US");
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
                var content = GetEmailContent(language, function, format);

                _mailProcessor.Send(content, emailAddress, "Affilite 12Bet testing");
                _languageService.SetCulture("en-US");
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

        private string GetEmailContent(string language, string function, string format)
        {
            var content = GetEmailTextContent(function, language);

            return format == "txt" ? content : GetEmailHtmlContent(content, language);
        }

        private string GetEmailTextContent(string function, string language)
        {
            var viewPath = string.Format(TemplateString.EmailTemplatePath, function, language);
            var physicalPath = Server.MapPath(viewPath);
            var model = GetModelByFunction(function);

            return model == null ? string.Empty : _mailProcessor.BuildTemplateContent(physicalPath, model);
        }

        private string GetEmailHtmlContent(string content, string language)
        {
            var domain = SiteDomainHelper.GetRootUrl(this);

            var viewModel = new EmailTemplateViewModel
            {
                RootUrl = domain,
                UserSiteUrl = "https://ha.nexdev.net:7613/12BetUserAsianNew",
                ImageRootUrl = string.Format(TemplateString.EmailImageRootPath, domain),
                Language = language,
                Content = content,
                Title = "This is title"
            };
            var viewPath = Server.MapPath("~/Areas/Portal/Views/Shared/Email/CommonLayout.cshtml");

            return _mailProcessor.BuildTemplateContent(viewPath, viewModel);
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
                        FirstName = "First Name",
                        UserName = "AFF12BET00000",
                        Password = "qwrfsdg"
                    };
                case "WithdrawalGuidance":
                    return new WithdrawalEmail
                    {
                        MailContact = _appSetting.MailContact,
                        FirstName = "First Name"
                    };
                default:
                    return null;
            }
        }
    }
}
