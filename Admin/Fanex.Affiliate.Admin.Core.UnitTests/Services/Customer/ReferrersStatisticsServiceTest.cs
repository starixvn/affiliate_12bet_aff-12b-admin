using Fanex.Affiliate.Admin.Core.Criterias.UserPanel.ReferrersStatistics;
using Fanex.Affiliate.Admin.Core.DTO.UserPanel;
using Fanex.Affiliate.Admin.Core.MetaModels.UserPanel;
using Fanex.Affiliate.Admin.Core.Services.UserPanel.Statistics;
using Fanex.Data.Repository;
using Moq;
using System.Collections.Generic;
using Xunit;

namespace Fanex.Affiliate.Admin.Core.UnitTests.Services.Customer
{
    public class ReferrersStatisticServiceTest
    {
        [Fact]
        public void GetReferrersList_WhenCall_ReturnCorrectData()
        {
            // Arrange
            var dynamicRepository = new Mock<IDynamicRepository>();
            dynamicRepository.Setup(re => re.Fetch<ReferrersStatistics>(It.IsAny<ReferrersStatisticsCriteria>(), It.IsAny<string>())).Returns(new List<ReferrersStatistics> { new ReferrersStatistics() });

            // Action
            var service = new ReferrersStatisticsService(dynamicRepository.Object);
            var result = service.GetReferrersList(new ReferrersParam());

            // Assert
            Assert.Single(result);
        }

        [Fact]
        public void GetReferrersDetailList_WhenCall_ReturnCorrectData()
        {
            // Arrange
            var dynamicRepository = new Mock<IDynamicRepository>();
            dynamicRepository.Setup(re => re.Fetch<ReferrersStatistics>(It.IsAny<ReferrersStatisticsDetailCriteria>(), It.IsAny<string>())).Returns(new List<ReferrersStatistics> { new ReferrersStatistics() });

            // Action
            var service = new ReferrersStatisticsService(dynamicRepository.Object);
            var result = service.GetReferrersDetailList(new ReferrersDetailParam());

            // Assert
            Assert.Single(result.MainData);
        }
    }
}
