using Fanex.Affiliate.Admin.Core.Criterias.UserPanel.TrackingStatistics;
using Fanex.Affiliate.Admin.Core.DTO.UserPanel;
using Fanex.Affiliate.Admin.Core.MetaModels.UserPanel;
using Fanex.Affiliate.Admin.Core.Services.UserPanel.Statistics;
using Fanex.Data.Repository;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Fanex.Affiliate.Admin.Core.UnitTests.Services.Customer
{
    public class TrackingStatisticsServiceTest
    {
        [Fact]
        public void GetTrackingCodeList_WhenCall_ReturnCorrectData()
        {
            // Arrange
            var dynamicRepository = new Mock<IDynamicRepository>();
            dynamicRepository.Setup(r => r.Fetch<TrackingInfo>(It.IsAny<TrackingCodeCriteria>(), It.IsAny<string>())).Returns(new List<TrackingInfo>());

            // Action
            var service = new TrackingStatisticsService(dynamicRepository.Object);
            var result = service.GetTrackingCodeList(1271808);

            // Assert
            dynamicRepository.Verify(obj => obj.Fetch<TrackingInfo>(It.IsAny<TrackingCodeCriteria>(), It.IsAny<string>()), Times.Once);
        }


        [Fact]
        public void GetTrackingStatisticsList_WhenCall_ReturnCorrectData()
        {
            // Arrange
            var dynamicRepository = new Mock<IDynamicRepository>();
            dynamicRepository.Setup(r => r.FetchMultiple<TrackingStatistics, TrackingStatistics>(It.IsAny<TrackingStatisticsCriteria>(), It.IsAny<string>()))
                .Returns((new List<TrackingStatistics> { new TrackingStatistics { Date = new DateTime(2019, 12, 27) } }, new List<TrackingStatistics>()));

            // Action
            var service = new TrackingStatisticsService(dynamicRepository.Object);
            var result = service.GetTrackingStatisticsList(new TrackingStatisticsParam());
            var item = result.FirstOrDefault();

            // Assert
            Assert.Single(result);
            Assert.Equal("December-2019", item.MonthText);
            Assert.Equal("12/27/2019", item.DateText);
        }

        [Fact]
        public void GetTrackingStatisticsDetailList_WhenCall_ReturnCorrectData()
        {
            // Arrange
            var dynamicRepository = new Mock<IDynamicRepository>();
            dynamicRepository.Setup(r => r.Fetch<TrackingStatistics>(It.IsAny<TrackingStatisticsDetailCriteria>(), It.IsAny<string>()))
                .Returns(new List<TrackingStatistics> { new TrackingStatistics() });

            // Action
            var service = new TrackingStatisticsService(dynamicRepository.Object);
            var result = service.GetTrackingStatisticsDetailList(new TrackingStatisticsDetailParam { SelectedMonth = new DateTime(2019, 12, 27) });
            var item = result.FirstOrDefault();

            // Assert
            Assert.Single(result);
        }
    }
}
