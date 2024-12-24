using Fanex.Affiliate.Admin.Core.Commands.Domain;
using Fanex.Affiliate.Admin.Core.Criterias;
using Fanex.Affiliate.Admin.Core.DTO.Domain;
using Fanex.Affiliate.Admin.Core.MetaModels.Domain;
using Fanex.Affiliate.Admin.Core.Services.Domain;
using Fanex.Data.Repository;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Fanex.Affiliate.Admin.Core.UnitTests.Services.Domain
{
    public class CommonCommonDomainServiceTest
    {
        [Fact]
        public void GetDomainList_WhenCall_ReturnCorrectData()
        {
            // Arrange
            var repository = new Mock<IDynamicRepository>();
            repository.Setup(r => r.Fetch<DomainInfo>(It.IsAny<BaseCriteria>())).Returns(new List<DomainInfo> { new DomainInfo { CreateDate = new DateTime(2020, 09, 10) } });

            // Action
            var service = new CommonDomainService(repository.Object);
            var result = service.GetDomainList("Aff_Admin_SubAffiliateDomain_Sel");
            var item = result.FirstOrDefault();

            // Assert
            Assert.Single(result);
            Assert.Equal("09/10/2020", item.CreatedDateText);
        }

        [Fact]
        public void GetDomainList_WhenCall_ReturnCorrectDataHasNull()
        {
            // Arrange
            var repository = new Mock<IDynamicRepository>();
            repository.Setup(r => r.Fetch<DomainInfo>(It.IsAny<BaseCriteria>())).Returns(new List<DomainInfo> { new DomainInfo() });

            // Action
            var service = new CommonDomainService(repository.Object);
            var result = service.GetDomainList("Aff_Admin_SubAffiliateDomain_Sel");
            var item = result.FirstOrDefault();

            // Assert
            Assert.Single(result);
            Assert.Null(item.CreatedDateText);
        }

        [Fact]
        public void InsertDomain_WhenCallWithValidParam_InsertSuccessfully()
        {
            // Arrange
            var repository = new Mock<IDynamicRepository>();
            repository.Setup(r => r.Execute(It.IsAny<InsertDomainCommand>()));

            // Action
            var service = new CommonDomainService(repository.Object);
            var result = service.InsertDomain(new InsertUpdateDomainParam(), "Aff_Admin_SubAffiliateDomain_Ins");

            // Assert
            Assert.True(result.IsSuccessful);
        }

        [Fact]
        public void UpdateDomain_WhenCallWithValidParam_UpdateSuccessfully()
        {
            // Arrange
            var repository = new Mock<IDynamicRepository>();
            repository.Setup(r => r.Execute(It.IsAny<UpdateDomainCommand>()));

            // Action
            var service = new CommonDomainService(repository.Object);
            var result = service.UpdateDomain(new InsertUpdateDomainParam(), "Aff_Admin_SubAffiliateDomain_Upd");

            // Assert
            Assert.True(result.IsSuccessful);
        }

        [Fact]
        public void DeleteDomain_WhenCallWithValidParam_DeleteSuccessfully()
        {
            // Arrange
            var repository = new Mock<IDynamicRepository>();
            repository.Setup(r => r.Execute(It.IsAny<DeleteDomainCommand>()));

            // Action
            var service = new CommonDomainService(repository.Object);
            var result = service.DeleteDomain(new DeleteDomainParam(), "Aff_Admin_SubAffiliateDomain_Del");

            // Assert
            Assert.True(result.IsSuccessful);
        }
    }
}
