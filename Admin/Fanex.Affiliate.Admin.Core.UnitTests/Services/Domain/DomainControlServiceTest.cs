using Fanex.Affiliate.Admin.Core.MetaModels.Domain;
using Fanex.Affiliate.Admin.Core.Repository.Domain;
using Fanex.Affiliate.Admin.Core.Services.Domain;
using NSubstitute;
using Xunit;

namespace Fanex.Affiliate.Admin.Core.UnitTests.Services.Domain
{
    public class DomainControlServiceTest
    {
        private readonly IDomainService _domainService;
        private readonly IDomainControlRepository _domainControlRepository;

        public DomainControlServiceTest()
        {
            _domainService = Substitute.For<IDomainService>();
            _domainControlRepository = Substitute.For<IDomainControlRepository>();
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
            _domainControlRepository.Received().GetDomainControlList(param);
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
            _domainControlRepository.Received().GetDomainControlById(param);
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
            _domainControlRepository.Received().InsertDomainControl(param);
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
            _domainControlRepository.Received().UpdateDomainControl(param);
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
            _domainControlRepository.Received().DeleteDomainControl(param);
        }

        private DomainControlService CreateDomainControlService
            => new DomainControlService(_domainControlRepository, _domainService);
    }
}
