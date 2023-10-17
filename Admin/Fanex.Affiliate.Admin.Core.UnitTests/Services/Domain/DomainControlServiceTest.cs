using Fanex.Affiliate.Admin.Core.MetaModels.Domain;
using Fanex.Affiliate.Admin.Core.Repository.Domain;
using Fanex.Affiliate.Admin.Core.Services.Domain;
using NSubstitute;
using Xunit;

namespace Fanex.Affiliate.Admin.Core.UnitTests.Services.Domain
{
    public class DomainControlServiceTest
    {
        private readonly IDomainControlRepository domainControlRepository;

        public DomainControlServiceTest()
        {
            domainControlRepository = Substitute.For<IDomainControlRepository>();
        }

        [Fact]
        public void GetDomainControlList_WhenCall_Excute()
        {
            // Arrange
            var param = "123";
            var service = CreateDomainControlService;

            // Action
            service.GetDomainControlList(param);

            // Assert
            domainControlRepository.Received().GetDomainControlList(param);
        }

        [Fact]
        public void GetDomainControlById_WhenCall_Excute()
        {
            // Arrange
            var param = 123;
            var service = CreateDomainControlService;

            // Action
            service.GetDomainControlById(param);

            // Assert
            domainControlRepository.Received().GetDomainControlById(param);
        }

        [Fact]
        public void InsertDomainControl_WhenCall_Excute()
        {
            // Arrange
            var param = new InsertDomainControlParam();
            var service = CreateDomainControlService;

            // Action
            service.InsertDomainControl(param);

            // Assert
            domainControlRepository.Received().InsertDomainControl(param);
        }

        [Fact]
        public void UpdateDomainControl_WhenCall_Excute()
        {
            // Arrange
            var param = new UpdateDomainControlParam();
            var service = CreateDomainControlService;

            // Action
            service.UpdateDomainControl(param);

            // Assert
            domainControlRepository.Received().UpdateDomainControl(param);
        }

        [Fact]
        public void DeleteDomainControl_WhenCall_Excute()
        {
            // Arrange
            var param = new DeleteDomainControlParam();
            var service = CreateDomainControlService;

            // Action
            service.DeleteDomainControl(param);

            // Assert
            domainControlRepository.Received().DeleteDomainControl(param);
        }

        private DomainControlService CreateDomainControlService
            => new DomainControlService(domainControlRepository);
    }
}
