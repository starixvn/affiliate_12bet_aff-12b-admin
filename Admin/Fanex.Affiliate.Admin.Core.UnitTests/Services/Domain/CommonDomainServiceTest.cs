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
            repository.Setup(r => r.Fetch<DomainInfo>(It.IsAny<BaseCriteria>(), It.IsAny<string>())).Returns(new List<DomainInfo> { new DomainInfo { CreateDate = new DateTime(2020, 09, 10) } });

            // Action
            var service = new CommonDomainService(repository.Object);
            var result = service.GetDomainList("Aff_Admin_SubAffiliateDomain_Sel");
            var item = result.FirstOrDefault();

            // Assert
            repository.Verify(d => d.Fetch<DomainInfo>(It.Is<BaseCriteria>(c => !string.IsNullOrEmpty(c.GetSettingKey()) && c.IsValid()), It.IsAny<string>()));
            Assert.Single(result);
            Assert.Equal("09/10/2020", item.CreatedDateText);
        }

        [Fact]
        public void GetDomainList_WhenCall_ReturnCorrectDataHasNull()
        {
            // Arrange
            var repository = new Mock<IDynamicRepository>();
            repository.Setup(r => r.Fetch<DomainInfo>(It.IsAny<BaseCriteria>(), It.IsAny<string>())).Returns(new List<DomainInfo> { new DomainInfo() });

            // Action
            var service = new CommonDomainService(repository.Object);
            var result = service.GetDomainList("Aff_Admin_SubAffiliateDomain_Sel");
            var item = result.FirstOrDefault();

            // Assert
            repository.Verify(d => d.Fetch<DomainInfo>(It.Is<BaseCriteria>(c => !string.IsNullOrEmpty(c.GetSettingKey()) && c.IsValid()), It.IsAny<string>()));
            Assert.Single(result);
            Assert.Null(item.CreatedDateText);
        }

        [Fact]
        public void InsertDomain_WhenCallWithValidParam_InsertSuccessfully()
        {
            // Arrange
            var repository = new Mock<IDynamicRepository>();
            repository.Setup(r => r.Execute(It.IsAny<InsertDomainCommand>(), It.IsAny<string>()));

            // Action
            var service = new CommonDomainService(repository.Object);
            var result = service.InsertDomain(new InsertUpdateDomainParam(), "Aff_Admin_SubAffiliateDomain_Ins");

            // Assert
            repository.Verify(d => d.Execute(It.Is<InsertDomainCommand>(c => !string.IsNullOrEmpty(c.GetSettingKey()) && c.IsValid()), It.IsAny<string>()));
            Assert.True(result.IsSuccessful);
        }

        [Fact]
        public void UpdateDomain_WhenCallWithValidParam_UpdateSuccessfully()
        {
            // Arrange
            var repository = new Mock<IDynamicRepository>();
            repository.Setup(r => r.Execute(It.IsAny<UpdateDomainCommand>(), It.IsAny<string>()));

            // Action
            var service = new CommonDomainService(repository.Object);
            var result = service.UpdateDomain(new InsertUpdateDomainParam(), "Aff_Admin_SubAffiliateDomain_Upd");

            // Assert
            repository.Verify(d => d.Execute(It.Is<UpdateDomainCommand>(c => !string.IsNullOrEmpty(c.GetSettingKey()) && c.IsValid()), It.IsAny<string>()));
            Assert.True(result.IsSuccessful);
        }

        [Fact]
        public void DeleteDomain_WhenCallWithValidParam_DeleteSuccessfully()
        {
            // Arrange
            var repository = new Mock<IDynamicRepository>();
            repository.Setup(r => r.Execute(It.IsAny<DeleteDomainCommand>(), It.IsAny<string>()));

            // Action
            var service = new CommonDomainService(repository.Object);
            var result = service.DeleteDomain(new DeleteDomainParam(), "Aff_Admin_SubAffiliateDomain_Del");

            // Assert
            repository.Verify(d => d.Execute(It.Is<DeleteDomainCommand>(c => !string.IsNullOrEmpty(c.GetSettingKey()) && c.IsValid()), It.IsAny<string>()));
            Assert.True(result.IsSuccessful);
        }
    }
}
