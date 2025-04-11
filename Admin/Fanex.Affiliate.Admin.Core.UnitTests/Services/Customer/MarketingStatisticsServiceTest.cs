namespace Fanex.Affiliate.Admin.Core.UnitTests.Services.Customer
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Criterias.UserPanel.MarketingStatistics;
    using Data.Repository;
    using DTO.UserPanel.Statistics;
    using Fanex.Affiliate.Admin.Core.Services.UserPanel.Statistics;
    using MetaModels.UserPanel;
    using Moq;
    using Xunit;

    public class MarketingStatisticsServiceTest
    {
        private readonly Mock<IDynamicRepository> _dynamicRepository;
        private readonly MarketingStatisticsService _service;

        public MarketingStatisticsServiceTest()
        {
            _dynamicRepository = new Mock<IDynamicRepository>();
            _service = new MarketingStatisticsService(_dynamicRepository.Object);
        }

        [Fact]
        public void Constructor_InitializesCorrectly()
        {
            // Arrange & Act
            var service = new MarketingStatisticsService(_dynamicRepository.Object);

            // Assert
            Assert.NotNull(service);
        }

        [Fact]
        public void GetMarketingStatistics_WithValidParam_ReturnsCorrectData()
        {
            // Arrange
            var param = new MarketingStatisticsParam
            {
                AffId = 123,
                FromDate = new DateTime(2024, 1, 1),
                ToDate = new DateTime(2024, 1, 31),
                LanguageCodes = "en,vi",
                IncludedInActive = true
            };

            var expectedStatistics = new List<MarketingStatistics>
            {
                new MarketingStatistics { ChannelName = "Channel1" },
                new MarketingStatistics { ChannelName = "Channel2" }
            };

            _dynamicRepository
                .Setup(x => x.Fetch<MarketingStatistics>(It.IsAny<MarketingStatisticsCriteria>(), string.Empty))
                .Returns(expectedStatistics);

            // Act
            var result = _service.GetMarketingStatistics(param);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(expectedStatistics.Count, result.Count());
            _dynamicRepository.Verify(x => x.Fetch<MarketingStatistics>(
                It.Is<MarketingStatisticsCriteria>(c =>
                    c.AffId == param.AffId &&
                    c.FromDate == param.FromDate &&
                    c.ToDate == param.ToDate &&
                    c.LanguageCodes.SequenceEqual(param.LanguageCodes) &&
                    c.IncludedInActive == param.IncludedInActive), string.Empty), Times.Once);
        }
    }
}
