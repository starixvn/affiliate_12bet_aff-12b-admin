using Fanex.Affiliate.Admin.Core.Services.UnderMaintenanceService;
using Xunit;

namespace Fanex.Affiliate.Admin.Core.UnitTests.Services.UnderMaintenance
{
    public class UnderMaintenanceServiceTest
    {
        [Fact]
        public void GetUnderMaintenanceTime_WhenCall_ReturnCorrectData()
        {
            // Arrange
            var service = new UnderMaintenanceService();

            // Action
            var result = service.GetUnderMaintenanceTime();

            // Assert
            Assert.Equal("0:00", result.From);
            Assert.Equal("0:00", result.To);
        }

        [Fact]
        public void IsUnderMaintenance_WhenCall_ReturnCorrectData()
        {
            // Arrange
            var service = new UnderMaintenanceService();

            // Action
            var result = service.IsUnderMaintenance();

            // Assert
            Assert.False(result);
        }
    }
}
