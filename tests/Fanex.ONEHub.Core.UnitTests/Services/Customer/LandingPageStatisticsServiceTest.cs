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
            dynamicRepository.Setup(re => re.Fetch<LandingPageStatistics>(It.IsAny<LandingPageStatisticsCriteria>(), It.IsAny<string>())).Returns(new List<LandingPageStatistics> { new LandingPageStatistics() });

            // Action
            var service = CreateService(dynamicRepository.Object);
            var result = service.GetLandingPageStatisticsList(new LandingPageStatisticsParam());

            // Assert
            dynamicRepository.Verify(d => d.Fetch<LandingPageStatistics>(It.Is<LandingPageStatisticsCriteria>(c => !string.IsNullOrEmpty(c.GetSettingKey()) && !c.IsValid()), It.IsAny<string>()));
            Assert.Single(result);
        }

        [Fact]
        public void GetLandingPageStatisticsDetailList_WhenCall_ReturnCorrectData()
        {
            // Arrange
            var dynamicRepository = new Mock<IDynamicRepository>();
            dynamicRepository.Setup(re => re.Fetch<LandingPageStatistics>(It.IsAny<LandingPageStatisticsDetailCriteria>(), It.IsAny<string>())).Returns(new List<LandingPageStatistics> { new LandingPageStatistics() });

            // Action
            var service = CreateService(dynamicRepository.Object);
            var result = service.GetLandingPageStatisticsDetailList(new LandingPageStatisticsDetailParam());

            // Assert
            dynamicRepository.Verify(d => d.Fetch<LandingPageStatistics>(It.Is<LandingPageStatisticsDetailCriteria>(c => !string.IsNullOrEmpty(c.GetSettingKey()) && !c.IsValid()), It.IsAny<string>()));
            Assert.Single(result.MainData);
        }

        public LandingPageStatisticsService CreateService(
            IDynamicRepository dynamicRepository = null)
        {
            var mockIDynamicRepository = dynamicRepository ?? Mock.Of<IDynamicRepository>();

            return new LandingPageStatisticsService(mockIDynamicRepository);
        }
    }
}
