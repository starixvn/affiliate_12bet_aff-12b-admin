using Fanex.Affiliate.Admin.Core.Criterias.Customer;
using Fanex.Affiliate.Admin.Core.DTO.Customer;
using Fanex.Affiliate.Admin.Core.MetaModels.Customer;
using Fanex.Affiliate.Admin.Core.Services.Customer;
using Fanex.Data.Repository;
using Moq;
using System;
using Xunit;

namespace Fanex.Affiliate.Admin.Core.UnitTests.Services.Customer
{
    public class CustomerStatisticsServiceTest
    {
        [Fact]
        public void GetCustomerStatistics_WhenCall_ReturnCorrectData()
        {
            // Arrange
            var dynamicRepository = new Mock<IDynamicRepository>();
            dynamicRepository.Setup(r => r.Get<CustomerStatistics>(It.IsAny<CustomerStatisticsCriteria>())).Returns(new CustomerStatistics());

            // Action
            var service = new CustomerStatisticsService(dynamicRepository.Object);
            var result = service.GetCustomerStatistics(new CustomerStatisticsParam { MonthDate = new DateTime() });

            // Assert
            Assert.NotNull(result);
        }
    }
}
