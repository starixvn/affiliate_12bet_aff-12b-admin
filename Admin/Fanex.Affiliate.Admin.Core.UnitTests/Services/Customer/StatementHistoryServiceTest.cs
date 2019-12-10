using Fanex.Affiliate.Admin.Core.DTO;
using Fanex.Affiliate.Admin.Core.DTO.UserPanel;
using Fanex.Affiliate.Admin.Core.MetaModels;
using Fanex.Affiliate.Admin.Core.MetaModels.UserPanel;
using Fanex.Affiliate.Admin.Core.Repository.UserPanel;
using Fanex.Affiliate.Admin.Core.Services.UserPanel;
using Moq;
using System.Collections.Generic;
using Xunit;

namespace Fanex.Affiliate.Admin.Core.UnitTests.Services.Customer
{
    public class StatementHistoryServiceTest
    {
        [Fact]
        public void GetDailyEarning_WhenCall_ReturnCorrectData()
        {
            // Arrange
            var param = new DailyEarningParam();
            var repository = new Mock<IStatementHistoryRepository>();
            repository.Setup(comm => comm.GetDailyEarning(param)).Returns(new DailyEarningResult{ MainData = new List<DailyEarning> { new DailyEarning() }, Total = new DailyEarning() });
            var service = CreateStatementHistoryService(repository.Object);

            // Action
            var result = service.GetDailyEarning(param);

            // Assert
            Assert.Single(result.MainData);
        }

        [Fact]
        public void GetDailyEarningDetail_WhenCall_ReturnCorrectData()
        {
            // Arrange
            var param = new DailyEarningParam();
            var repository = new Mock<IStatementHistoryRepository>();
            repository.Setup(comm => comm.GetDailyEarningDetail(param)).Returns(new List<DailyEarningDetail> { new DailyEarningDetail() });
            var service = CreateStatementHistoryService(repository.Object);

            // Action
            var result = service.GetDailyEarningDetail(param);

            // Assert
            Assert.Single(result.MainData);
        }

        [Fact]
        public void GetStatementHistoryList_WhenCall_ReturnCorrectData()
        {
            // Arrange
            var param = new AffiliatePagingByDateParam();
            var repository = new Mock<IStatementHistoryRepository>();
            repository.Setup(comm => comm.GetStatementHistoryList(param)).Returns(new EntityResult<StatementHistory> { MainData = new List<StatementHistory> { new StatementHistory() }, TotalItem = 1 });
            var service = CreateStatementHistoryService(repository.Object);

            // Action
            var result = service.GetStatementHistoryList(param);

            // Assert
            Assert.Single(result.MainData);
        }

        private static StatementHistoryService CreateStatementHistoryService(IStatementHistoryRepository statementHistoryRepository = null)
        {
            var fakeStatementHistoryRepository = statementHistoryRepository ?? Mock.Of<IStatementHistoryRepository>();

            return new StatementHistoryService(fakeStatementHistoryRepository);
        }
    }
}
