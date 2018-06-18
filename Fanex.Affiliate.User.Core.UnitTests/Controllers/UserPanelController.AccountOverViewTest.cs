using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Fanex.Affiliate.User.Core.DTO.UserPanel;
using Fanex.Affiliate.User.Core.MetalModels.UserPanel;
using Fanex.Affiliate.User.Core.Repository.UserPanel;
using Fanex.Affiliate.User.Core.UnitTests.TestHelper;
using Fanex.Affiliate.User.Core.ViewModels.UserPanel;
using Moq;
using Newtonsoft.Json;
using Xunit;

namespace Fanex.Affiliate.User.Core.UnitTests.Controllers
{
    public class AccountOverviewTest
    {
        [Theory]
        [InlineData(null)]
        [InlineData("04/01/2018")]
        public void AccountOverView_GetData_DataReturnCorrectly(string monthDateString)
        {
            // Arrange
            var monthDate = string.IsNullOrEmpty(monthDateString) ? DateTime.Now.Date : (DateTime?)DateTime.Parse(monthDateString);
            var viewModel = new AccountOverviewViewModel
            {
                Statistics = new AccountOverviewStatistics(),
                CommissionSettingList = new List<CommissionSetting>(),
                SelectedMonth = monthDate.Value
            };

            var statisticsRepository = new Mock<IStatisticsRepository>();
            statisticsRepository.Setup(s => s.GetStatisticsOverview(It.IsAny<StatisticsOverviewParam>()))
                                .Returns(new AccountOverviewStatistics());

            var commissionRepository = new Mock<ICommissionRepository>();
            commissionRepository.Setup(c => c.GetCommissionSettingList(Constant.DefaultAffId, false))
                                .Returns(new List<CommissionSetting>());
            var controller = MockingHelper.GetUserPanelController(
                statisticsRepository: statisticsRepository.Object,
                commissionRepository: commissionRepository.Object);

            var expectedResult = JsonConvert.SerializeObject(viewModel);

            // Action
            var viewResult = (ViewResult)controller.AccountOverview(monthDate);
            var result = JsonConvert.SerializeObject(viewResult.Model);

            // Assert
            Assert.Equal(expectedResult, result);
        }

        [Theory]
        [InlineData(true, null)]
        [InlineData(false, "04/01/2018")]
        public void AccountOverView_GetData_PassParamCorrectly(bool isInternal, string monthDateString)
        {
            // Arrange
            var monthDate = string.IsNullOrEmpty(monthDateString) ? DateTime.Now.Date : (DateTime?)DateTime.Parse(monthDateString);
            StatisticsOverviewParam passedParam = null;
            var sessionService = MockingHelper.GetSessionService();
            sessionService.IsInternal = isInternal;

            var statisticsRepository = new Mock<IStatisticsRepository>();
            statisticsRepository.Setup(s => s.GetStatisticsOverview(It.IsAny<StatisticsOverviewParam>()))
                                .Callback<StatisticsOverviewParam>(param => passedParam = param);

            var controller = MockingHelper.GetUserPanelController(
                sessionService: sessionService,
                statisticsRepository: statisticsRepository.Object);

            var expectedParam = new StatisticsOverviewParam
            {
                AffId = Constant.DefaultAffId,
                MonthDate = monthDate.Value,
                IsInternal = isInternal
            };

            var expectedParamJson = JsonConvert.SerializeObject(expectedParam);

            // Action
            controller.AccountOverview(monthDate);
            var result = JsonConvert.SerializeObject(passedParam);

            // Assert
            Assert.Equal(expectedParamJson, result);
        }
    }
}