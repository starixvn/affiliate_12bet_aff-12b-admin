using Fanex.Affiliate.Admin.Core.Controllers.UserPanel;
using Fanex.Affiliate.Admin.Core.DTO;
using Fanex.Affiliate.Admin.Core.DTO.UserPanel;
using Fanex.Affiliate.Admin.Core.Extensions;
using Fanex.Affiliate.Admin.Core.MetaModels.UserPanel;
using Fanex.Affiliate.Admin.Core.Services.Common;
using Fanex.Affiliate.Admin.Core.Services.UserPanel.Statistics;
using Fanex.Affiliate.Admin.Core.UnitTests.TestHelper;
using Moq;
using System.Collections.Generic;
using Xunit;

namespace Fanex.Affiliate.Admin.Core.UnitTests.Controllers.Customer
{
    public class ReferrersControllerTest
    {
        [Fact]
        public void GetReferrers_WhenCallWithValidParam_ExcuteWithCorrectData()
        {
            // Arrange
            var param = new ReferrersParam();
            ReferrersParam passedParam = null;
            var ReferrersStatisticsService = new Mock<IReferrersStatisticsService>();
            ReferrersStatisticsService.Setup(t => t.GetReferrersList(param)).Returns(new List<ReferrersStatistics> { new ReferrersStatistics() }).Callback<ReferrersParam>(p => passedParam = p);
            var controller = CreateReferrersController(ReferrersStatisticsService: ReferrersStatisticsService.Object);

            // Action
            var result = controller.GetReferrersList(param) as JsonNetResult;
            var entityResult = result.Data as IEnumerable<ReferrersStatistics>;

            // Assert
            Assert.Single(entityResult);
            Assert.Equal(param, passedParam);
        }

        [Fact]
        public void GetReferrers_WhenCallWithInvalidParam_ReturnEmptyData()
        {
            // Arrange
            var controller = CreateReferrersController();
            controller.ModelState.AddModelError("Error01", string.Empty);

            // Action
            var result = controller.GetReferrersList(new ReferrersParam()) as JsonNetResult;

            // Assert
            Assert.Null(result.Data);
        }

        [Fact]
        public void GetReferrersDetailList_WhenCallWithValidParam_ExcuteWithCorrectData()
        {
            // Arrange
            var param = new ReferrersDetailParam();
            ReferrersDetailParam passedParam = null;
            var ReferrersStatisticsService = new Mock<IReferrersStatisticsService>();
            ReferrersStatisticsService.Setup(t => t.GetReferrersDetailList(param)).Returns(new EntityResult<ReferrersStatistics> { MainData = new List<ReferrersStatistics> { new ReferrersStatistics() }, TotalItem = 1 }).Callback<ReferrersDetailParam>(p => passedParam = p);
            var controller = CreateReferrersController(ReferrersStatisticsService: ReferrersStatisticsService.Object);

            // Action
            var result = controller.GetReferrersDetailList(param) as JsonNetResult;
            var entityResult = result.Data as EntityResult<ReferrersStatistics>;

            // Assert
            Assert.Single(entityResult.MainData);
            Assert.Equal(1, entityResult.TotalItem);
            Assert.Equal(param, passedParam);
        }

        [Fact]
        public void GetReferrersDetailList_WhenCallWithInvalidParam_ReturnEmptyData()
        {
            // Arrange
            var controller = CreateReferrersController();
            controller.ModelState.AddModelError("Error01", string.Empty);

            // Action
            var result = controller.GetReferrersDetailList(new ReferrersDetailParam()) as JsonNetResult;

            // Assert
            Assert.Null(result.Data);
        }

        private ReferrersController CreateReferrersController(
            ISessionService sessionService = null,
            IReferrersStatisticsService ReferrersStatisticsService = null
            )
        {
            var fakeSessionService = sessionService ?? MockingHelper.GetSessionService();
            var fakeReferrersStatisticsService = ReferrersStatisticsService ?? Mock.Of<IReferrersStatisticsService>();

            return new ReferrersController(fakeSessionService, fakeReferrersStatisticsService);
        }
    }
}