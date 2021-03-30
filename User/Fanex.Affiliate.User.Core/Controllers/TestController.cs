namespace Fanex.Affiliate.User.Core.Controllers
{
    using BusinessService;
    using Constants;
    using CoreService;
    using DTO;
    using Enum;
    using Fanex.Affiliate.User.Core.MetalModels;
    using Fanex.Affiliate.User.Core.Resources;
    using Filters;
    using MetalModels.Email;
    using ONEbook.UM.Filters;
    using System;
    using System.Collections.Generic;
    using System.Web.Mvc;
    using Utilities;
    using ViewModels;

    [CheckUMFilter]
    [CustomAuthorize]
    public class TestController : BaseController
    {
        private readonly IMailProcessor _mailProcessor;
        private readonly IAppSetting _appSetting;

        private readonly string _mailContact;
        private readonly string _userName;

        public TestController(ISessionService sessionService,
            ILanguageService languageService, IAppSetting appSetting, IMailProcessor mailProcessor) : base(sessionService, languageService)
        {
            _mailProcessor = mailProcessor;
            _appSetting = appSetting;

            _mailContact = appSetting.MailContact;
            _userName = sessionService.UserSession?.UserName ?? "12B0000";
        }

        [HttpGet]
        public ActionResult Index()
        {
            if (!IsValidUser())
            {
                return HttpNotFound();
            }

            return View();
        }

        [HttpGet]
        public ActionResult Preview(TestCriteria criteria)
        {
            if (!IsValidUser())
            {
                return HttpNotFound();
            }

            try
            {
                LanguageService.SetCulture(criteria.Language);
                ViewBag.Data = GetEmailContent(criteria);
                LanguageService.SetCulture("en-US");
            }
            catch (Exception ex)
            {
                ViewBag.Data = ex.Message + "\n" + ex.StackTrace;
            }

            return View();
        }

        [HttpPost]
        public ActionResult SendEmail(TestCriteria criteria)
        {
            if (!IsValidUser())
            {
                return HttpNotFound();
            }

            var success = true;
            var message = string.Empty;

            try
            {
                var funtion = criteria.Function.ToLowerInvariant();
                var content = GetEmailContent(criteria);
                LanguageService.SetCulture(criteria.Language);
                string Subject = SubjectList.ContainsKey(funtion) ? SubjectList[funtion] : Lang.AffiliateProgram;
                LanguageService.SetCulture("en-US");

                _mailProcessor.Send(content, criteria.EmailAddress, Subject);
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

        private static readonly IDictionary<string, string> SubjectList = new Dictionary<string, string>
        {
            ["contactus"] = Lang.ContactUs,
            ["csd"] = Lang.CsdSubject,
            ["recoverpassword"] = Lang.SubjectForgetPass,
            ["paymentcancel"] = Lang.CancelSubject.Replace("{transid}", "12345"),
            ["paymentrequest"] = Lang.CancelSubject.Replace("{transid}", "12345"),
            ["newpassword"] = Lang.SubjectNewPass,
            ["signup"] = Lang.RegisSubject,
            ["verificationaccount"] = Lang.SubjectVerificationAccount
        };


        private string GetEmailContent(TestCriteria criteria)
        {
            var content = GetEmailTextContent(criteria);

            return criteria.Format == "txt" ? content : GetEmailHtmlContent(content, criteria.Language);
        }

        private string GetEmailTextContent(TestCriteria criteria)
        {
            var language = criteria.Language;
            var function = criteria.Function;

            var viewPath = string.Format(TemplateString.EmailTemplatePath, function, language);
            var physicalPath = Server.MapPath(viewPath);
            var model = GetModelByFunction(function.ToLowerInvariant());

            return model == null ? string.Empty : _mailProcessor.BuildTemplateContent(physicalPath, model);
        }

        private string GetEmailHtmlContent(string content, string language)
        {
            var domain = SiteDomainHelper.GetRootUrl(this, _appSetting);

            var viewModel = new EmailTemplateViewModel
            {
                RootUrl = domain,
                ImageRootUrl = string.Format(TemplateString.EmailImageRootPath, domain),
                Language = language,
                Content = content,
                Title = "This is title"
            };
            var viewPath = Server.MapPath("~/Views/Shared/Email/CommonLayout.cshtml");

            return _mailProcessor.BuildTemplateContent(viewPath, viewModel);
        }

        private object GetModelByFunction(string functionName)
        {
            switch (functionName)
            {
                case "contactus":
                    return GetContactUsModel();

                case "csd":
                    return GetCSDModel();

                case "recoverpassword":
                    return GetForgetPasswordModel();

                case "paymentcancel":
                    return GetPaymentModel(PaymentRequestType.Cancel);

                case "paymentrequest":
                    return GetPaymentModel(PaymentRequestType.Create);

                case "newpassword":
                    return GetRecoverPasswordModel();

                case "signup":
                    return GetSignupModel();

                case "verificationaccount":
                    return GetVerificationAccountModel();

                default:
                    return null;
            }
        }

        private object GetContactUsModel()
        {
            return new ContactUsParam
            {
                Sender = "Sender",
                Affiliate = _userName,
                Email = "abc@12b.com",
                Content = SiteDomainHelper.GetRootUrl(this, _appSetting),
            };
        }

        private object GetCSDModel()
        {
            return new NoticeEmailParam
            {
                UserName = _userName,
                FirstName = "Firs Name",
                LastName = "Last Name",
                SiteUrl = "abc.com.vn",
                MoreSiteUrls = "xyz.com.vn",
                MailContact = _mailContact
            };
        }

        private object GetForgetPasswordModel()
        {
            return new RecoverPasswordEmailParam
            {
                User = GetUserInfo(),
                MailContact = _mailContact,
                Code = "12345",
                RootDomain = SiteDomainHelper.GetRootUrl(this, _appSetting),
                MailTo = _mailContact
            };
        }

        private object GetPaymentModel(PaymentRequestType type)
        {
            const long TranId = 1L;
            const decimal Amount = 989;

            return new PaymentEmailParam
            {
                User = GetUserInfo(),
                MailContact = _mailContact,
                TransId = TranId,
                RequestDate = DateTime.Now.Date,
                Amount = Amount,
                RootDomain = SiteDomainHelper.GetRootUrl(this, _appSetting),
                MailTo = _mailContact,
                PaymentType = type
            };
        }

        private object GetRecoverPasswordModel()
        {
            return new NewPasswordEmailParam
            {
                User = GetUserInfo(),
                MailContact = _mailContact,
                NewPassword = "1234aa",
                RootDomain = "http://affiliate.12ying.com/",
                MailTo = _mailContact,
            };
        }

        private object GetSignupModel()
        {
            return new EmailBaseParam
            {
                User = GetUserInfo(),
                MailContact = _mailContact
            };
        }

        private object GetVerificationAccountModel()
        {
            return new VerificationAccountEmailParam
            {
                User = GetUserInfo(),
                MailContact = _mailContact,
                RootDomain = SiteDomainHelper.GetRootUrl(this, _appSetting),
                Code = "123456",
                Ip = "127.0.0.1"
            };
        }

        private UserInfo GetUserInfo()
        {
            return new UserInfo
            {
                AffId = 1,
                FirstName = "Fisrt name",
                UserName = _userName,
                Email = "abc@nova.com",
                LastName = "Last name",
                MailOption = false,
                PreferredLanguage = "en-US"
            };
        }

        private bool IsValidUser()
        {
            return _appSetting.IsLocal || SessionService.IsInternal;
        }
    }

    public class TestCriteria
    {
        public string Language { get; set; }

        public string Function { get; set; }

        public string Format { get; set; }

        public string EmailAddress { get; set; }
    }
}