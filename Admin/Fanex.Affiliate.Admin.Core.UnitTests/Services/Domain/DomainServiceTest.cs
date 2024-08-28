using Fanex.Affiliate.Admin.Core.MetaModels.Domain;
using Fanex.Affiliate.Admin.Core.Repository.Domain;
using Fanex.Affiliate.Admin.Core.Services.Domain;
using NSubstitute;
using Xunit;

namespace Fanex.Affiliate.Admin.Core.UnitTests.Services.Domain
{
    public class DomainServiceTest
    {
        private readonly IDomainRepository domainRepository;

        public DomainServiceTest()
        {
            domainRepository = Substitute.For<IDomainRepository>();
        }

        [Fact]
        public void GetTargetCountryList_WhenCall_Excute()
        {
            // Arrange
            var param = new TargetCountryParam();
            var service = CreateDomainService;

            // Action
            service.GetTargetCountryList(param);

            // Assert
            domainRepository.Received().GetTargetCountryList(param);
        }

        [Fact]
        public void GetDomainList_WhenCall_Excute()
        {
            // Arrange
            var service = CreateDomainService;

            // Action
            service.GetDomainList(123);

            // Assert
            domainRepository.Received().GetDomainList(123, true);
        }

        [Fact]
        public void GetDomainById_WhenCall_Excute()
        {
            // Arrange
            var service = CreateDomainService;

            // Action
            service.GetDomainById(123, 234);

            // Assert
            domainRepository.Received().GetDomainById(123, 234, true);
        }

        [Fact]
        public void InsertDomain_WhenCall_Excute()
        {
            // Arrange
            var param = new InsertDomainParam();
            var service = CreateDomainService;

            // Action
            service.InsertDomain(param);

            // Assert
            domainRepository.Received().InsertDomain(param);
        }

        [Fact]
        public void UpdateDomain_WhenCall_Excute()
        {
            // Arrange
            var param = new UpdateDomainParam();
            var service = CreateDomainService;

            // Action
            service.UpdateDomain(param);

            // Assert
            domainRepository.Received().UpdateDomain(param);
        }

        [Fact]
        public void DeleteDomain_WhenCall_Excute()
        {
            // Arrange
            var param = new DeleteDomainParam();
            var service = CreateDomainService;

            // Action
            service.DeleteDomain(param);

            // Assert
            domainRepository.Received().DeleteDomain(param);
        }

        private DomainService CreateDomainService
            => new DomainService(domainRepository);
    }
}
