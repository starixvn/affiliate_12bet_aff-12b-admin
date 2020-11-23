using Fanex.Affiliate.User.Core.BusinessService;
using Fanex.Affiliate.User.Core.Controllers.UserPanel;
using Fanex.Affiliate.User.Core.CoreService;
using Fanex.Affiliate.User.Core.DTO.UserPanel;
using Fanex.Affiliate.User.Core.UnitTests.TestHelper;
using Fanex.Affiliate.User.Core.ViewModels.UserPanel;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Xunit;

namespace Fanex.Affiliate.User.Core.UnitTests.Controllers.UserPanel
{
    public class CommissionReportControllerTest
    {
        private readonly ISessionService SessionService;
        private readonly ILanguageService LanguageService;
        private readonly ICommissionReportService CommissionService;

        public CommissionReportControllerTest()
        {
            SessionService = NSubtituteMockingHelper.FakeSessionService();
            LanguageService = Substitute.For<ILanguageService>();
            CommissionService = Substitute.For<ICommissionReportService>();
        }


        [Fact]
        public void Index_WhenCall_ReturnCorrectView()
        {
            var controller = new CommissionReportController(SessionService, LanguageService, CommissionService);
            var result = controller.Index() as ViewResult;
            Assert.Contains("CommissionReport/Index.cshtml", result.ViewName);
        }

        [Fact]
        public void Index_WhenCallWithYearReportNotInRange_ReceiveNoItemList()
        {
            var date = DateTime.Now.AddYears(2);
            MockingHelper.SetClaimsPrincipal("[AffTurnover]", true);
            var controller = new CommissionReportController(SessionService, LanguageService, CommissionService);
            var result = controller.Index(date.Year) as JsonResult;
            var model = (CommissionReportDataViewModel)result.Data;
            Assert.Null(model.Data);
            Assert.Null(model.Total);
        }

        [Fact]
        public void Index_WhenCallWithYearReportInRange_ServiceRecieveCall()
        {
            var thisYear = DateTime.Now.Year;
            MockingHelper.SetClaimsPrincipal("[AffTurnover]", false);
            CommissionService.GenerateCommissionReportData(Arg.Compat.Any<int>(), Arg.Compat.Any<DateTime>())
                .Returns(new CommissionReportDataViewModel
                {
                    Data = new List<AffiliateCommisionByMonth> { new AffiliateCommisionByMonth {
                        ProductData = new List<AffiliateCommission> { new AffiliateCommission() },
                        Data = new AffiliateCommission()
                    } },
                    Total = new AffiliateCommission()
                });
            var controller = new CommissionReportController(SessionService, LanguageService, CommissionService);
            controller.Index(thisYear);
            CommissionService.Received().GenerateCommissionReportData(Arg.Compat.Any<int>(), new DateTime(thisYear, 1, 1));
        }
    }
}
