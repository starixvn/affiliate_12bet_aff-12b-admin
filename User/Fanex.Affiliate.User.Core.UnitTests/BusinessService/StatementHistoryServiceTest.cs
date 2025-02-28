namespace Fanex.Affiliate.User.Core.UnitTests.BusinessService
{
    using Core.BusinessService;
    using Criterias;
    using Data.Repository;
    using DTO;
    using DTO.UserPanel;
    using MetalModels.UserPanel;
    using Newtonsoft.Json;
    using NSubstitute;
    using System.Collections.Generic;
    using System.Linq;
    using Xunit;

    public class StatementHistoryServiceTest
    {
        private readonly IDynamicRepository _repository = Substitute.For<IDynamicRepository>();

        [Fact]
        public void GetStatementHistory_WhenCalled_ReturnCorrectData()
        {
            // Arrange
            var param = new StatementHistoryParam { AffId = 1 };
            var expected = new List<StatementHistory>
            {
                new StatementHistory
                {
                    Amount = 10
                }
            };

            _repository.Fetch<StatementHistory>(Arg.Any<GetStatementHistoryCriteria>())
                .Returns(expected);

            var service = new StatementHistoryService(_repository);

            // Action
            var result = service.GetStatementHistory(param);

            // Assert
            Assert.Equal(JsonConvert.SerializeObject(expected), JsonConvert.SerializeObject(result));
        }

        [Theory]
        [InlineData(10)]
        [InlineData(20)]
        public void GetStatementHistory_WhenCalled_ReturnNRows(int nRows)
        {
            // Arrange
            var param = new StatementHistoryParam { AffId = 1, NRows = 0 };
            var expected = new List<StatementHistory>
            {
                new StatementHistory
                {
                    Amount = 10
                }
            };

            _repository.Fetch<StatementHistory>(Arg.Any<GetStatementHistoryCriteria>())
                .Returns(expected)
                .AndDoes(c =>
                {
                    var criteria = c.Arg<GetStatementHistoryCriteria>();
                    criteria.NRows = nRows;
                });

            var service = new StatementHistoryService(_repository);

            // Action
            _ = service.GetStatementHistory(param);

            // Assert
            Assert.Equal(nRows, param.NRows);
        }

        [Fact]
        public void GetProductList_WhenCalled_ReturnCorrectData()
        {
            // Arrange
            var expected = new List<ProductItem>
            {
                new ProductItem
                {
                    ProductId = 1,
                    ProductName = "test"
                }
            };

            _repository.Fetch<ProductItem>(Arg.Any<GetProductListCriteria>())
                .Returns(expected);

            var service = new StatementHistoryService(_repository);

            // Action
            var result = service.GetProductList(true);

            // Assert
            Assert.Equal(JsonConvert.SerializeObject(expected), JsonConvert.SerializeObject(result));
        }

        [Fact]
        public void GetSubAffCommission_WhenCalled_ReturnCorrectData()
        {
            // Arrange
            var param = new SubAffComParam { AffId = 1 };
            var expected = new List<SubAffComInfo>
            {
                new SubAffComInfo
                {
                    AffId = 1
                }
            };

            _repository.Fetch<SubAffComInfo>(Arg.Any<GetSubAffCommissionCriteria>())
                .Returns(expected);

            var service = new StatementHistoryService(_repository);

            // Action
            var result = service.GetSubAffCommission(param);

            // Assert
            Assert.Equal(JsonConvert.SerializeObject(expected), JsonConvert.SerializeObject(result));
        }

        [Fact]
        public void GetDailyEarning_WhenCalled_ReturnCorrectData()
        {
            // Arrange
            var param = new DailyEarningParam { AffId = 1 };
            var data = new List<DailyEarning>();

            var expected = new EntityResult<DailyEarning>
            {
                MainDataList = data,
                Total = data.FirstOrDefault()
            };

            _repository.FetchMultiple<DailyEarning, DailyEarning>(Arg.Any<GetDailyEarningCriteria>())
                .Returns((data, data));

            var service = new StatementHistoryService(_repository);

            // Action
            var result = service.GetDailyEarning(param);

            // Assert
            Assert.Equal(JsonConvert.SerializeObject(expected), JsonConvert.SerializeObject(result));
        }
    }
}
