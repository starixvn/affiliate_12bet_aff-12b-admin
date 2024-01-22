using Fanex.Affiliate.User.Core.BusinessService;
using Fanex.Affiliate.User.Core.Constants;
using Fanex.Affiliate.User.Core.Controllers;
using Fanex.Affiliate.User.Core.CoreService;
using Fanex.Affiliate.User.Core.DTO.UserPanel;
using Fanex.Affiliate.User.Core.MetalModels.UserPanel;
using Fanex.Affiliate.User.Core.Repository.UserPanel;
using Fanex.Affiliate.User.Core.UnitTests.TestHelper;
using Fanex.Affiliate.User.Core.Utilities;
using Fanex.Affiliate.User.Core.ViewModels.UserPanel;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Xunit;

namespace Fanex.Affiliate.User.Core.UnitTests.Controllers.UserPanel
{
    public class StatementHistoryControllerTest
    {
        private readonly ISessionService SessionService;
        private readonly ISystemService LanguageService;
        private readonly IAppSetting AppSetting;
        private readonly IStatementHistoryRepository Repo;

        public StatementHistoryControllerTest()
        {
            SessionService = NSubtituteMockingHelper.FakeSessionService();
            LanguageService = NSubtituteMockingHelper.FakeLanguageService();
            AppSetting = Substitute.For<IAppSetting>();
            Repo = Substitute.For<IStatementHistoryRepository>();
        }

        [Fact]
        public void Index_WhenCallWithNoParm_ReturnCorrectViewWithDefaultValue()
        {
            var controller = CreateController();
            var defaultParam = new StatementHistoryParam
            {
                AffId = SessionService.UserSession.AffId,
                IsInternal = SessionService.IsInternal,
                PageSize = 20
            };
            Repo.GetStatementHistory(Arg.Compat.Any<StatementHistoryParam>()).Returns(new List<StatementHistory>());
            var result = controller.Index() as ViewResult;

            Repo.Received().GetStatementHistory(Arg.Compat.Is<StatementHistoryParam>(param =>
                param.AffId == defaultParam.AffId &&
                param.IsInternal == defaultParam.IsInternal &&
                param.PageSize == defaultParam.PageSize));
            Assert.Equal("~/Views/UserPanel/StatementHistory/Index.cshtml", result.ViewName);
        }

        [Theory]
        [InlineData(null)]
        [InlineData(20)]
        public void Index_WhenCallWithParam_ReturnPartialView(int? pageSize)
        {
            var controller = CreateController();
            var param = new StatementHistoryParam
            {
                PageIndex = 1,
                PageSize = pageSize
            };
            Repo.When(a => a.GetStatementHistory(Arg.Compat.Any<StatementHistoryParam>()))
                .Do(a =>
               {
                   param.NRows = 10;
                   param.PageSize = 20;
               });
            var result = controller.Index(param) as PartialViewResult;

            var startRowNumber = (param.PageIndex - 1) * param.PageSize + 1;
            Assert.Equal("~/Views/UserPanel/StatementHistory/StatementHistoryPartial/_StatementHistoryContent.cshtml", result.ViewName);
            Assert.Equal(param.NRows, result.ViewBag.TotalItem);
            Assert.Equal(startRowNumber, result.ViewBag.StartRowNumber);
        }

        [Theory]
        [InlineData(TransType.MonthlyEarning, "running", "December-2020", "Processing", "Transaction", "Current Month Statistics", "Current")]
        [InlineData(TransType.MonthlyEarning, "pending", "December-2020", "Processing", "Transaction", "Uncalculated Month", "Uncalculated")]
        [InlineData(TransType.MonthlyEarning, "completed", "December-2020", "Approved", "Transaction", "Monthly Total", "test")]
        [InlineData(TransType.NewFDM, "cancelled", "December-2020", "Cancelled", "Transaction", "Current Month Statistics", "Current")]
        [InlineData(TransType.NewFDM, "approved", "December-2020", "Approved", "Transaction", "Uncalculated Month", "Uncalculated")]
        [InlineData(TransType.NewFDM, "rejected", "December-2020", "Rejected", "Transaction", "Monthly Total", "test")]
        [InlineData(TransType.MonthlyTotal, "current", "December-2020", "Current", "Transaction", "Current Month Statistics", "Current")]
        [InlineData(TransType.MonthlyTotal, "test", "December-2020", "test", "Transaction", "Uncalculated Month", "Uncalculated")]
        [InlineData(TransType.MonthlyTotal, "current", "December-2020", "Current", "Transaction", "Monthly Total", "test")]
        [InlineData(TransType.Payment, "current", "12-07-2020", "Current", "Payment Request", "test", "test")]
        [InlineData(TransType.Adjustment, "current", "12-07-2020", "Current", "Adjustment", "test", "test")]
        [InlineData(TransType.Adjustment, "CURRENT", "12-07-2020", "Current", "Adjustment", "test", "test")]
        [InlineData(123, "abc", "December-2020", "abc", "Transaction", "test", "test")]
        [InlineData(123, "", "December-2020", "", "Transaction", "test", "test")]
        public void Index_WhenCall_ReturnCorrectDescriptionDisplay(int transType, string status, string dateString, string displayStatus,
            string transTypeString, string descriptionDisplay, string description)
        {
            var controller = CreateController();

            var param = new StatementHistoryParam
            {
                PageIndex = 1,
                PageSize = 20
            };
            Repo.When(a => a.GetStatementHistory(Arg.Compat.Any<StatementHistoryParam>()))
                .Do(a =>
                {
                    param.NRows = 1;
                });
            Repo.GetStatementHistory(Arg.Any<StatementHistoryParam>()).Returns(new List<StatementHistory> {
                new StatementHistory { TransType = transType, MonthDate = new DateTime(2020, 12, 07), Status = status, Description = description }
            });

            var result = controller.Index(param) as PartialViewResult;
            var model = result.Model as List<StatementHistoryViewModel>;
            var item = model.FirstOrDefault();

            var startRowNumber = (param.PageIndex - 1) * param.PageSize + 1;
            Assert.Equal("~/Views/UserPanel/StatementHistory/StatementHistoryPartial/_StatementHistoryContent.cshtml", result.ViewName);
            Assert.Equal(param.NRows, result.ViewBag.TotalItem);
            Assert.Equal(startRowNumber, result.ViewBag.StartRowNumber);
            Assert.Equal(dateString, item.DateString);
            Assert.Equal(displayStatus, item.DisplayStatus);
            Assert.Equal(transTypeString, item.TransTypeString);
            Assert.Equal(descriptionDisplay, item.DescriptionDisplay);
        }

        [Fact]
        public void Index_WhenCall_ShouldHasSumRow_ReturnPartialView()
        {
            var controller = CreateController();

            var param = new StatementHistoryParam
            {
                PageIndex = 1,
                PageSize = 20
            };
            Repo.When(a => a.GetStatementHistory(Arg.Compat.Any<StatementHistoryParam>()))
                .Do(a =>
                {
                    param.NRows = 3;
                });
            Repo.GetStatementHistory(Arg.Any<StatementHistoryParam>()).Returns(new List<StatementHistory> {
                new StatementHistory { TransType = TransType.MonthlyEarning, MonthDate = new DateTime(2020, 12, 07), Status = "Current", Description = "Current Monthly Earning" },
                new StatementHistory { TransType = TransType.NewFDM, MonthDate = new DateTime(2020, 12, 07), Status = "Current", Description = "Current New FDM Deposit" }
            });

            var result = controller.Index(param) as PartialViewResult;
            var model = result.Model as List<StatementHistoryViewModel>;
            var item = model.FirstOrDefault();

            var startRowNumber = (param.PageIndex - 1) * param.PageSize + 1;
            Assert.Equal("~/Views/UserPanel/StatementHistory/StatementHistoryPartial/_StatementHistoryContent.cshtml", result.ViewName);
            Assert.Equal(param.NRows, result.ViewBag.TotalItem);
            Assert.Equal(startRowNumber, result.ViewBag.StartRowNumber);
            Assert.Equal("December-2020", item.DateString);
            Assert.Equal("Current", item.DisplayStatus);
            Assert.Equal("Transaction", item.TransTypeString);
            Assert.Equal("Current Month Statistics", item.DescriptionDisplay);
        }

        private StatementHistoryController CreateController()
            => new StatementHistoryController(SessionService, LanguageService, AppSetting, Repo);
    }
}
