using AutoFixture;
using Fanex.Affiliate.Admin.Core.DTO;
using Fanex.Affiliate.Admin.Core.DTO.ProcessingRequest;
using Fanex.Affiliate.Admin.Core.MetaModels.ProcessingRequest;
using Fanex.Affiliate.Admin.Core.Repository.ProcessingRequest;
using Fanex.Affiliate.Admin.Core.Services.Common;
using Fanex.Affiliate.Admin.Core.Services.ProcessingRequest;
using Fanex.Affiliate.Admin.Core.UnitTests.TestHelper;
using Moq;
using System.Collections.Generic;
using Xunit;

namespace Fanex.Affiliate.Admin.Core.UnitTests.Services.Affiliate
{
    public class ProcessingRequestServiceTest
    {

        private readonly Fixture fixture = new Fixture();
        [Fact]
        public void AddProcessingRequest_WhenCall_ReturnCorrectData()
        {
            //Arrange
            var repository = new Mock<IProcessingRequestRepository>();
            repository.Setup(re => re.AddProcessingRequest(It.IsAny<ProcessingRequestInsParam>())).Returns(new ResultInfo());

            var service = CreateProcessingRequestService(repository.Object);

            //Action
            var result = service.AddProcessingRequest(fixture.Create<ProcessingRequestInsParam>());

            //Assert
            Assert.True(result.IsSuccessful);
        }

        [Fact]
        public void DeleteProcessingRequest_WhenCall_ReturnCorrectData()
        {
            //Arrange
            var repository = new Mock<IProcessingRequestRepository>();
            repository.Setup(re => re.DeleteProcessingRequest(It.IsAny<ProcessingRequestDelParam>())).Returns(new ResultInfo());

            var service = CreateProcessingRequestService(repository.Object);

            //Action
            var result = service.DeleteProcessingRequest(fixture.Create<ProcessingRequestDelParam>());

            //Assert
            Assert.True(result.IsSuccessful);
        }

        [Fact]
        public void GetProcessingRequestDetail_WhenCall_ReturnCorrectData()
        {
            //Arrange
            var repository = new Mock<IProcessingRequestRepository>();
            repository.Setup(re => re.GetProcessingRequestDetail(It.IsAny<int>())).Returns(fixture.Create<ProcessingRequestDetailInfo>());

            var service = CreateProcessingRequestService(repository.Object);

            //Action
            var result = service.GetProcessingRequestDetail(123);

            //Assert
            Assert.NotNull(result);
        }

        [Fact]
        public void GetProcessingRequestList_WhenCall_ReturnCorrectData()
        {
            //Arrange
            var repository = new Mock<IProcessingRequestRepository>();
            repository.Setup(re => re.GetProcessingRequestList(It.IsAny<ProcessingRequestSelParam>())).Returns(new ProcessingRequestEntity {
                MainData = new List<ProcessingRequestBasicInfo> { fixture.Create<ProcessingRequestBasicInfo>() },
                Summary = fixture.Create<ProcessingRequestSummaryInfo>(),
                TotalItem = 1
            });

            var service = CreateProcessingRequestService(repository.Object);

            //Action
            var result = service.GetProcessingRequestList(fixture.Create<ProcessingRequestSelParam>());

            //Assert
            Assert.Single(result.MainData);
            Assert.NotNull(result.Summary);
            Assert.Equal(1, result.TotalItem);
        }

        [Fact]
        public void UpdateProcessingRequest_WhenCall_ReturnCorrectData()
        {
            //Arrange
            var repository = new Mock<IProcessingRequestRepository>();
            repository.Setup(re => re.UpdateProcessingRequest(It.IsAny<ProcessingRequestUpdParam>())).Returns(new ResultInfo());

            var service = CreateProcessingRequestService(repository.Object);

            //Action
            var result = service.UpdateProcessingRequest(fixture.Create<ProcessingRequestUpdParam>());

            //Assert
            Assert.True(result.IsSuccessful);
        }

        private ProcessingRequestService CreateProcessingRequestService(
            IProcessingRequestRepository repository,
            ISessionService sessionService = null
            )
        {
            var fakeProcessingRequestRepository = repository ?? Mock.Of<IProcessingRequestRepository>();
            var fakeSessionService = sessionService ?? MockingHelper.GetSessionService();

            return new ProcessingRequestService(fakeSessionService, fakeProcessingRequestRepository);
        }
    }
}
