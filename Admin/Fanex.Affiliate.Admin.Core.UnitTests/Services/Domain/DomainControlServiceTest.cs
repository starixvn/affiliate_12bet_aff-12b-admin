using Fanex.Affiliate.Admin.Core.DTO.Domain;
using Fanex.Affiliate.Admin.Core.MetaModels.Domain;
using Fanex.Affiliate.Admin.Core.Repository.Domain;
using Fanex.Affiliate.Admin.Core.Services.Domain;
using Fanex.Data;
using Moq;
using NSubstitute;
using System;
using Xunit;

namespace Fanex.Affiliate.Admin.Core.UnitTests.Services.Domain
{
    public class DomainControlServiceTest
    {
        private readonly IDomainService domainService;
        private readonly IDomainControlRepository domainControlRepository;

        public DomainControlServiceTest()
        {
            domainService = Substitute.For<IDomainService>();
            domainControlRepository = Substitute.For<IDomainControlRepository>();
        }

        [Fact]
        public void GetVisibleTargetCountriesByUser_WhenCall_Excute()
        {
            // Arrange
            var param = 123;
            var service = CreateDomainControlService;

            // Action
            service.GetVisibleTargetCountriesByUser(param);

            // Assert
            domainService.Received().GetVisibleTargetCountriesByUser(param);
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
            => new DomainControlService(domainService, domainControlRepository);
    }
}
