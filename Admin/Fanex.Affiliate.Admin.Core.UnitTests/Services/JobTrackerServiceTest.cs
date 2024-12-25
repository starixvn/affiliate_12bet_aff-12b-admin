using Fanex.Affiliate.Admin.Core.Commands.Tracker;
using Fanex.Affiliate.Admin.Core.Services.Tracker;
using Fanex.Data.Repository;
using Moq;
using Xunit;

namespace Fanex.Affiliate.Admin.Core.UnitTests.Services.Tracker
{
    public class JobTrackerServiceTest
    {
        [Theory]
        [InlineData(false)]
        [InlineData(true)]
        public void InsertDataTracker_WhenCall_InsertSuccessfully(bool isDBNull)
        {
            // Arrange
            var dynamicRepository = new Mock<IDynamicRepository>();
            dynamicRepository.Setup(r => r.Execute(It.IsAny<InsertJobTrackerCommand>())).Callback<INonQueryCommand>(c => (c as InsertJobTrackerCommand).PushTrackerId = isDBNull ? 0 : 123);

            // Action
            var service = new JobTrackerService(dynamicRepository.Object);
            var (Id, Result) = service.InsertDataTracker(new InsertJobTrackerCommand());

            // Assert
            Assert.Equal(isDBNull ? 0 : 123, Id);
            Assert.True(Result.IsSuccessful);
        }

        [Fact]
        public void UpdateDataTracker_WhenCall_UpdateSuccessfully()
        {
            // Arrange
            var dynamicRepository = new Mock<IDynamicRepository>();
            dynamicRepository.Setup(r => r.Execute(It.IsAny<InsertJobTrackerCommand>()));

            // Action
            var service = new JobTrackerService(dynamicRepository.Object);
            var result = service.UpdateDataTracker(new UpdateJobTrackerCommand());

            // Assert
            Assert.True(result.IsSuccessful);
        }
    }
}
