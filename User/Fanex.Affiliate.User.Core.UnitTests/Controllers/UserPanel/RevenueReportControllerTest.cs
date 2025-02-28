using Fanex.Affiliate.User.Core.BusinessService;
using Fanex.Affiliate.User.Core.Controllers.UserPanel;
using Fanex.Affiliate.User.Core.CoreService;
using Fanex.Affiliate.User.Core.DTO.UserPanel;
using Fanex.Affiliate.User.Core.MetalModels.UserPanel;
using Fanex.Affiliate.User.Core.UnitTests.TestHelper;
using Fanex.Affiliate.User.Core.Utilities;
using Fanex.Affiliate.User.Core.ViewModels.UserPanel;
using NSubstitute;
using System.Collections.Generic;
using System.Web.Mvc;
using Xunit;

namespace Fanex.Affiliate.User.Core.UnitTests.Controllers.UserPanel
{

    public class RevenueReportControllerTest
    {
        private readonly ISessionService SessionService;
        private readonly ISystemService SystemService;
        private readonly ICommissionReportService CommissionService;

        public RevenueReportControllerTest()
        {
            SessionService = NSubstituteMockingHelper.FakeSessionService();
            SystemService = Substitute.For<ISystemService>();
            CommissionService = Substitute.For<ICommissionReportService>();
        }


        [Fact]
        public void Index_WhenCall_ReturnCorrectView()
        {
            RevenueReportController controller = CreateController();
            SessionService.UserSession.Returns(new DTO.Auth.UserSession());
            SystemService.GetBaseCurrencyList().Returns(new List<Currency> { new Currency { Code = "USD", ExchangeId = 123 } });

            var result = controller.Index(null) as ViewResult;

            Assert.Contains("CommissionReport/Index.cshtml", result.ViewName);
        }


        [Fact]
        public void Index_WhenCallWithYearReportNotInRange_ReceiveNoItemList()
        {
            var date = DateTimeHelper.Now.AddYears(2);
            var controller = CreateController();

            var result = controller.Index(date.Year) as JsonResult;
            var model = (CommissionReportDataViewModel)result.Data;

            Assert.Null(model.Data);
            Assert.Null(model.Total);
        }

        [Fact]
        public void Index_WhenCallWithYearReportInRange_ServiceRecieveCall()
        {
            var thisYear = DateTimeHelper.Now.Year;
            var controller = CreateController();

            controller.Index(thisYear, 1);

            CommissionService.Received().GenerateCommissionReportData(Arg.Any<CommissionReportParam>());
        }

        private RevenueReportController CreateController()
            => new RevenueReportController(SessionService, SystemService, CommissionService);

    }
}
