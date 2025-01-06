namespace Fanex.Affiliate.User.Core.UnitTests.Controllers.UserPanel
{
    using System;
    using System.Collections.Generic;
    using System.Web.Mvc;
    using Core.BusinessService;
    using Core.Controllers;
    using Fanex.Affiliate.User.Core.CoreService;
    using Fanex.Affiliate.User.Core.DTO.UserPanel;
    using Fanex.Affiliate.User.Core.MetalModels.UserPanel;
    using Fanex.Affiliate.User.Core.ViewModels.UserPanel;
    using NSubstitute;
    using TestHelper;
    using Xunit;

    public class SubAffEarningControllerTest
    {
        private readonly ISessionService SessionService = NSubstituteMockingHelper.FakeSessionService();
        private readonly ISystemService LanguageService = NSubstituteMockingHelper.FakeLanguageService();
        private readonly IStatementHistoryService StatementHistoryService = Substitute.For<IStatementHistoryService>();

        [Fact]
        public void Index_WhenCallWith_ReturnCorrectView()
        {
            // Arrange
            StatementHistoryService.GetSubAffCommission(Arg.Any<SubAffComParam>()).Returns(new List<SubAffComInfo> {
                new SubAffComInfo
                {
                    UserName = "ABC",
                    Commission = 1000m,
                    SubAffCommission = 500m
                }
            });

            var controller = new SubAffEarningController(SessionService, LanguageService, StatementHistoryService);

            // Action
            var result = controller.Index(new SubAffComParam { MonthDate = new DateTime(2020, 12, 08) }) as ViewResult;
            var model = result.Model as IEnumerable<SubAffComViewModel>;

            // Assert
            Assert.Single(model);
            Assert.Equal("~/Views/UserPanel/StatementHistory/SubAffEarning.cshtml", result.ViewName);
        }

        [Fact]
        public void Index_WhenCallWithIsOnlyContent_ReturnCorrectView()
        {
            // Arrange
            StatementHistoryService.GetSubAffCommission(Arg.Any<SubAffComParam>()).Returns(new List<SubAffComInfo> {
                new SubAffComInfo
                {
                    UserName = "ABC",
                    Commission = 1000m,
                    SubAffCommission = 500m
                }
            });
            var controller = new SubAffEarningController(SessionService, LanguageService, StatementHistoryService);

            // Action
            var result = controller.Index(new SubAffComParam { IsOnlyContent = true, RequestPage = "abc", MonthDate = null }) as ViewResult;
            var model = result.Model as IEnumerable<SubAffComViewModel>;

            // Assert
            Assert.Single(model);
            Assert.Equal("~/Views/UserPanel/StatementHistory/StatementHistoryPartial/_SubAffEarningContent.cshtml", result.ViewName);
        }
    }
}
