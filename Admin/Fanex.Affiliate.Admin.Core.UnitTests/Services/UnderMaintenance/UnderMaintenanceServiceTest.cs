using Fanex.Affiliate.Admin.Core.Services.UnderMaintenanceService;
using Moq;
using ONEbook.UM.Models;
using ONEbook.UM.Services;
using Xunit;

namespace Fanex.Affiliate.Admin.Core.UnitTests.Services.UnderMaintenance
{
    public class UnderMaintenanceServiceTest
    {
        [Fact]
        public void GetUnderMaintenanceTime_WhenCall_ReturnCorrectData()
        {
            // Arrange
            var umService = new Mock<IMaintenanceService>();
            umService.Setup(u => u.GetUnderMaintenanceTime()).Returns(new UnderMaintenanceTime { From = "13:00", To = "15:00" });
            var service = new UnderMaintenanceService(umService.Object);

            // Action
            var result = service.GetUnderMaintenanceTime();

            // Assert
            Assert.Equal("13:00", result.From);
            Assert.Equal("15:00", result.To);
        }

        [Fact]
        public void IsUnderMaintenance_WhenCall_ReturnCorrectData()
        {
            // Arrange
            var umService = new Mock<IMaintenanceService>();
            umService.Setup(u => u.IsUnderMaintenance()).Returns(true);
            var service = new UnderMaintenanceService(umService.Object);

            // Action
            var result = service.IsUnderMaintenance();

            // Assert
            Assert.True(result);
        }
    }
}
