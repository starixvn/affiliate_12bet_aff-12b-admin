using Fanex.Affiliate.Admin.Core.Criterias.UserPanel.StatementHistory;
using Fanex.Affiliate.Admin.Core.DTO.UserPanel;
using Fanex.Affiliate.Admin.Core.MetaModels;
using Fanex.Affiliate.Admin.Core.MetaModels.UserPanel;
using Fanex.Affiliate.Admin.Core.Services.UserPanel;
using Fanex.Data.Repository;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Fanex.Affiliate.Admin.Core.UnitTests.Services.Customer
{
    public class StatementHistoryServiceTest
    {
        [Fact]
        public void GetSubAffiliate_WhenCall_ReturnCorrectData()
        {
            // Arrange
            var repository = new Mock<IDynamicRepository>();
            repository.Setup(comm => comm.FetchMultiple<SubAffiliate, SubAffiliate>(It.IsAny<SubAffiliateCriteria>())).Returns((new List<SubAffiliate> { new SubAffiliate() }, new List<SubAffiliate> { new SubAffiliate() }));

            // Action
            var service = CreateStatementHistoryService(repository.Object);
            var result = service.GetSubAffiliate(new SubAffiliateParam());

            // Assert
            Assert.Single(result.MainData);
        }

        [Fact]
        public void GetDailyEarning_WhenCall_ReturnCorrectData()
        {
            // Arrange
            var repository = new Mock<IDynamicRepository>();
            repository.Setup(comm => comm.FetchMultiple<DailyEarning, DailyEarning>(It.IsAny<DailyEarningCriteria>())).Returns((new List<DailyEarning> { new DailyEarning() }, new List<DailyEarning> { new DailyEarning() }));

            // Action
            var service = CreateStatementHistoryService(repository.Object);

            // Action
            var result = service.GetDailyEarning(new DailyEarningParam());

            // Assert
            Assert.Single(result.MainData);
        }

        [Fact]
        public void GetDailyEarningDetail_WhenCall_ReturnCorrectData()
        {
            // Arrange
            var repository = new Mock<IDynamicRepository>();
            repository.Setup(comm => comm.Fetch<DailyEarningDetail>(It.IsAny<DailyEarningCriteria>())).Returns((new List<DailyEarningDetail> { new DailyEarningDetail() }));

            var service = CreateStatementHistoryService(repository.Object);

            // Action
            var result = service.GetDailyEarningDetail(new DailyEarningParam());

            // Assert
            Assert.Single(result.MainData);
        }

        [Fact]
        public void GetStatementHistoryList_WhenCall_ReturnCorrectData()
        {
            // Arrange
            var repository = new Mock<IDynamicRepository>();
            repository.Setup(comm => comm.Fetch<StatementHistory>(It.IsAny<StatementHistoryCriteria>())).Returns(new List<StatementHistory> {
                new StatementHistory { TransType = 201, MonthDate = new DateTime(2020, 06, 10), Status = "Current", CalculatedType = 1 },
                new StatementHistory { TransType = 204, MonthDate = new DateTime(2020, 06, 10), Status = "Current", CalculatedType = 1 },
                new StatementHistory { TransType = 201, MonthDate = new DateTime(2020, 05, 10), Status = "Current", CalculatedType = 2 },
                new StatementHistory { TransType = 204, MonthDate = new DateTime(2020, 05, 10), Status = "Current", CalculatedType = 2 },
                new StatementHistory { TransType = 201, MonthDate = new DateTime(2020, 04, 10), Status = "Current", CalculatedType = 0 },
                new StatementHistory { TransType = 204, MonthDate = new DateTime(2020, 04, 10), Status = "Current", CalculatedType = 0 },
                new StatementHistory { TransType = 204, MonthDate = new DateTime(2020, 03, 10), Status = "Current" },
                new StatementHistory { TransType = 200, MonthDate = new DateTime(2020, 06, 10), Status = "Cancelled", Description = "test" }
            });
            var service = CreateStatementHistoryService(repository.Object);

            // Action
            var result = service.GetStatementHistoryList(new AffiliatePagingByDateParam()).MainData.ToList();

            // Assert
            Assert.Equal("2020-06-10", result[0].NormalizeDateText);
            Assert.Equal("Monthly Total", result[1].DescriptionDisplay);
            Assert.Equal("Uncalculated Month", result[0].DescriptionDisplay);
            Assert.Equal("Current Month Statistics", result[2].DescriptionDisplay);
            Assert.Equal("Current Month Statistics", result[3].DescriptionDisplay);
            Assert.Equal("test", result[4].DescriptionDisplay);
        }

        private static StatementHistoryService CreateStatementHistoryService(IDynamicRepository dynamicRepository = null)
        {
            var fakeIDynamicRepository = dynamicRepository ?? Mock.Of<IDynamicRepository>();

            return new StatementHistoryService(fakeIDynamicRepository);
        }
    }
}
