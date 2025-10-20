namespace Fanex.Affiliate.Admin.Core.UnitTests.Controllers.Customer
{
    using Core.Extensions;
    using DTO.UserPanel.Statistics;
    using Fanex.Affiliate.Admin.Core.Controllers.UserPanel;
    using Fanex.Affiliate.Admin.Core.Services.Common;
    using Fanex.Affiliate.Admin.Core.Services.UserPanel.Statistics;
    using MetaModels.UserPanel;
    using Moq;
    using System.Collections.Generic;
    using System.Linq;
    using Xunit;

    public class MarketingStatisticsControllerTest
    {
        private readonly Mock<ISessionService> _sessionService;
        private readonly Mock<IMarketingStatisticsService> _marketingStatisticsService;
        private readonly MarketingStatisticsController _controller;

        public MarketingStatisticsControllerTest()
        {
            _sessionService = new Mock<ISessionService>();
            _marketingStatisticsService = new Mock<IMarketingStatisticsService>();
            _controller = new MarketingStatisticsController(_sessionService.Object, _marketingStatisticsService.Object);
        }

        [Fact]
        public void GetMarketingStatisticsList_WhenModelStateIsInvalid_ReturnsEmptyJsonResult()
        {
            // Arrange
            var param = new MarketingStatisticsParam();
            _controller.ModelState.AddModelError("Error", "Invalid model state");

            // Act
            var result = _controller.GetMarketingStatisticsList(param);

            // Assert
            Assert.IsType<JsonNetResult>(result);
        }

        [Fact]
        public void GetMarketingStatisticsList_WhenModelStateIsValid_ReturnsGroupedStatistics()
        {
            // Arrange
            var param = new MarketingStatisticsParam();
            var statistics = new List<MarketingStatistics>
            {
                new MarketingStatistics { ChannelName = "Channel1" },
                new MarketingStatistics { ChannelName = "Channel1" },
                new MarketingStatistics { ChannelName = "Channel2" }
            };

            _marketingStatisticsService
                .Setup(x => x.GetMarketingStatistics(param))
                .Returns(statistics);

            // Act
            var result = _controller.GetMarketingStatisticsList(param) as JsonNetResult;

            // Assert
            Assert.NotNull(result);
            var data = result.Data as IEnumerable<IGrouping<string, MarketingStatistics>>;
            Assert.NotNull(data);
            Assert.Equal(2, data.Count());
            Assert.Equal("Channel1", data.First().Key);
            Assert.Equal(2, data.First().Count());
        }
    }
}
