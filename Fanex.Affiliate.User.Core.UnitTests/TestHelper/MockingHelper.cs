using System;
using System.Collections.Specialized;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Fanex.Affiliate.User.Core.BusinessService;
using Fanex.Affiliate.User.Core.Controllers;
using Fanex.Affiliate.User.Core.CoreService;
using Fanex.Affiliate.User.Core.DTO.Auth;
using Fanex.Affiliate.User.Core.Repository.UserPanel;
using Fanex.Affiliate.User.Core.Utilities;
using Moq;

namespace Fanex.Affiliate.User.Core.UnitTests.TestHelper
{
    public static class MockingHelper
    {
        public static ISessionService GetSessionService()
        {
            return Mock.Of<ISessionService>(ss =>
                ss.CaptchaCode == Constant.Captcha &&
                ss.CurrentLanguage == Constant.DefaultLanguage &&
                !ss.IsInternal &&
                ss.UserSession == new UserSession
                {
                    AffId = Constant.DefaultAffId,
                    AffType = Constant.AffTypeAsian,
                    OnUserId = 1,
                    PreferredLanguage = Constant.DefaultLanguage,
                    UserName = Constant.DefaultUserName
                }
            );
        }

        public static UserPanelController GetUserPanelController(
            ISessionService sessionService = null,
            IAppSetting appSetting = null,
            IAccountRepository accountRepository = null,
            IMarketingService marketingService = null,
            IStatisticsRepository statisticsRepository = null,
            IStatementHistoryRepository statementHistoryRepository = null,
            ICommissionRepository commissionRepository = null,
            IPaymentRepository paymentRepository = null,
            IEmailService emailService = null,
            IAccountService accountService = null,
            IPaymentService paymentService = null)
        {
            accountRepository = accountRepository ?? Mock.Of<IAccountRepository>();
            accountService = accountService ?? Mock.Of<IAccountService>();
            paymentRepository = paymentRepository ?? Mock.Of<IPaymentRepository>();
            emailService = emailService ?? Mock.Of<IEmailService>();
            paymentService = paymentService ?? new PaymentService(
                accountRepository,
                accountService,
                paymentRepository,
                emailService);

            var controller = new UserPanelController(
                sessionService ?? GetSessionService(),
                appSetting ?? Mock.Of<IAppSetting>(),
                accountRepository,
                marketingService ?? Mock.Of<IMarketingService>(),
                statisticsRepository ?? Mock.Of<IStatisticsRepository>(),
                statementHistoryRepository ?? Mock.Of<IStatementHistoryRepository>(),
                commissionRepository ?? Mock.Of<ICommissionRepository>(),
                paymentService);

            var request = GetHttpRequest();
            var server = new Mock<HttpServerUtilityBase>();
            server.Setup(s => s.HtmlEncode(It.IsAny<string>())).Returns<string>(s => $"Encoded{s}");

            controller.ControllerContext = Mock.Of<ControllerContext>(ctx =>
                ctx.HttpContext.Request == request &&
                ctx.HttpContext.Server == server.Object
            );

            var context = Mock.Of<HttpContextBase>(ctx => ctx.Request == request);
            var requestContext = new RequestContext(context, new RouteData());
            controller.Url = new UrlHelper(requestContext);

            return controller;
        }

        public static HttpRequestBase GetHttpRequest()
        {
            return Mock.Of<HttpRequestBase>(req =>
                req.ApplicationPath == "/" &&
                req.UserHostAddress == "::1" &&
                req.Url == new Uri(Constant.DefaultRootDomain) &&
                req.ServerVariables == new NameValueCollection() &&
                req.Browser.Browser == Constant.Browser
            );
        }
    }
}