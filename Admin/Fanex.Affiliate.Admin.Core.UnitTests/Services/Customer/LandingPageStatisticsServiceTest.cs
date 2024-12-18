using Fanex.Affiliate.Admin.Core.Criterias.UserPanel.LandingPageStatistics;
using Fanex.Affiliate.Admin.Core.DTO.UserPanel;
using Fanex.Affiliate.Admin.Core.MetaModels.UserPanel;
using Fanex.Affiliate.Admin.Core.Services.UserPanel.Statistics;
using Fanex.Data.Repository;
using Moq;
using System.Collections.Generic;
using Xunit;

namespace Fanex.Affiliate.Admin.Core.UnitTests.Services.Customer
{
    public class LandingPageStatisticsServiceTest
    {
        [Fact]
        public void GetLandingPageStatisticsList_WhenCall_ReturnCorrectData()
        {
            // Arrange
            var dynamicRepository = new Mock<IDynamicRepository>();
            dynamicRepository.Setup(re => re.Fetch<LandingPageStatistics>(It.IsAny<LandingPageStatisticsCriteria>())).Returns(new List<LandingPageStatistics> { new LandingPageStatistics() });

            // Action
            var service = new LandingPageStatisticsService(dynamicRepository.Object);
            var result = service.GetLandingPageStatisticsList(new LandingPageStatisticsParam());

            // Assert
            Assert.Single(result);
        }

        [Fact]
        public void GetLandingPageStatisticsDetailList_WhenCall_ReturnCorrectData()
        {
            // Arrange
            var dynamicRepository = new Mock<IDynamicRepository>();
            dynamicRepository.Setup(re => re.Fetch<LandingPageStatistics>(It.IsAny<LandingPageStatisticsDetailCriteria>())).Returns(new List<LandingPageStatistics> { new LandingPageStatistics() });

            // Action
            var service = new LandingPageStatisticsService(dynamicRepository.Object);
            var result = service.GetLandingPageStatisticsDetailList(new LandingPageStatisticsDetailParam());

            // Assert
            Assert.Single(result.MainData);
        }
    }
}
