using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Fanex.Affiliate.User.Core.BusinessService;
using Fanex.Affiliate.User.Core.Controllers.UserPanel;
using Fanex.Affiliate.User.Core.CoreService;
using Fanex.Affiliate.User.Core.DTO.UserPanel;
using Fanex.Affiliate.User.Core.MetalModels.UserPanel;
using Fanex.Affiliate.User.Core.Repository.UserPanel;
using Fanex.Affiliate.User.Core.UnitTests.TestHelper;
using Fanex.Affiliate.User.Core.ViewModels.UserPanel;
using Moq;
using Newtonsoft.Json;
using Xunit;

namespace Fanex.Affiliate.User.Core.UnitTests.Controllers.UserPanel
{
    public class AccountOverViewControllerTest
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

            var sessionService = MockingHelper.GetSessionService();

            var statisticsRepository = new Mock<IStatisticsRepository>();
            statisticsRepository.Setup(s => s.GetStatisticsOverview(It.IsAny<StatisticsOverviewParam>()))
                                .Returns(new AccountOverviewStatistics());

            var accountOverViewRepository = new Mock<IAccountRepository>();
            accountOverViewRepository.Setup(c => c.GetCommissionSettingList(Constant.DefaultAffId, false))
                                .Returns(new List<CommissionSetting>());


            var controller = MockingHelper.FakeController(
                CreateController(
                    sessionService: sessionService,
                    statisticsRepository: statisticsRepository.Object,
                    accountRepository: accountOverViewRepository.Object)
                ) as AccountOverviewController;


            var expectedResult = JsonConvert.SerializeObject(viewModel);

            // Action
            var viewResult = (ViewResult)controller.Index(monthDate);
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

            var accountOverViewRepository = new Mock<IAccountRepository>();
            accountOverViewRepository.Setup(c => c.GetCommissionSettingList(Constant.DefaultAffId, false))
                                .Returns(new List<CommissionSetting>());

            var controller = MockingHelper.FakeController(
                CreateController(sessionService: sessionService, statisticsRepository:
                statisticsRepository.Object, accountRepository: accountOverViewRepository.Object)
                ) as AccountOverviewController;

            var expectedParam = new StatisticsOverviewParam
            {
                AffId = Constant.DefaultAffId,
                MonthDate = monthDate.Value,
                IsInternal = isInternal
            };

            var expectedParamJson = JsonConvert.SerializeObject(expectedParam);

            // Action
            controller.Index(monthDate);
            var result = JsonConvert.SerializeObject(passedParam);

            // Assert
            Assert.Equal(expectedParamJson, result);
        }

        private static AccountOverviewController CreateController(
            ISessionService sessionService = null,
            ILanguageService languageService = null,
            IStatisticsRepository statisticsRepository = null,
            IAccountRepository accountRepository = null)
        {
            sessionService = sessionService ?? MockingHelper.GetSessionService();
            languageService = languageService ?? Mock.Of<ILanguageService>();
            statisticsRepository = statisticsRepository ?? Mock.Of<IStatisticsRepository>();
            accountRepository = accountRepository ?? Mock.Of<IAccountRepository>();

            return new AccountOverviewController(sessionService, languageService, statisticsRepository, accountRepository);
        }
    }
}