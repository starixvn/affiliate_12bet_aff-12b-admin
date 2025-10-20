using Fanex.Affiliate.Admin.Core.Commands.Domain;
using Fanex.Affiliate.Admin.Core.Criterias.Domain;
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
    public class DomainControlServiceTest
    {
        [Fact]
        public void GetDomainControlList_WhenCall_ReturnCorrectData()
        {
            // Arrange
            var repository = new Mock<IDynamicRepository>();
            repository.Setup(r => r.Fetch<DomainControlInfo>(It.IsAny<DomainControlCriteria>(), It.IsAny<string>())).Returns(new List<DomainControlInfo> { new DomainControlInfo { CreatedDate = new DateTime(2019, 10, 29) } });

            var commonDomainService = new Mock<ICommonDomainService>();
            commonDomainService.Setup(c => c.GetTargetCountryListByDomain(It.IsAny<int>(), It.IsAny<int>()))
                .Returns(new List<TargetCountryByDomain>());

            // Action
            var service = new DomainControlService(repository.Object, commonDomainService.Object);
            var result = service.GetDomainControl("en-GB");
            var item = result.FirstOrDefault();

            // Assert
            repository.Verify(d => d.Fetch<DomainControlInfo>(It.Is<DomainControlCriteria>(c => !string.IsNullOrEmpty(c.GetSettingKey()) && c.IsValid()), It.IsAny<string>()));
            Assert.Single(result);
            Assert.Equal("10/29/2019", item.CreatedDateText);
        }

        [Fact]
        public void GetDomainControlList_WhenCall_ReturnCorrectDataHasNull()
        {
            // Arrange
            var repository = new Mock<IDynamicRepository>();
            repository.Setup(r => r.Fetch<DomainControlInfo>(It.IsAny<DomainControlCriteria>(), It.IsAny<string>())).Returns(new List<DomainControlInfo> { new DomainControlInfo() });

            var commonDomainService = new Mock<ICommonDomainService>();
            commonDomainService.Setup(c => c.GetTargetCountryListByDomain(It.IsAny<int>(), It.IsAny<int>()))
                .Returns(new List<TargetCountryByDomain>());

            // Action
            var service = new DomainControlService(repository.Object, commonDomainService.Object);
            var result = service.GetDomainControl("en-GB");
            var item = result.FirstOrDefault();

            // Assert
            repository.Verify(d => d.Fetch<DomainControlInfo>(It.Is<DomainControlCriteria>(c => !string.IsNullOrEmpty(c.GetSettingKey()) && c.IsValid()), It.IsAny<string>()));
            Assert.Single(result);
            Assert.Null(item.CreatedDateText);
        }

        [Fact]
        public void InsertDomainControl_WhenCallWithValidParam_InsertSuccessfully()
        {
            // Arrange
            var repository = new Mock<IDynamicRepository>();
            repository.Setup(r => r.Execute(It.IsAny<InsertDomainControlCommand>(), It.IsAny<string>()));
            var commonDomainService = new Mock<ICommonDomainService>();
            commonDomainService.Setup(c => c.GetTargetCountryListByDomain(It.IsAny<int>(), It.IsAny<int>()))
                .Returns(new List<TargetCountryByDomain>());

            // Action
            var service = new DomainControlService(repository.Object, commonDomainService.Object);
            var result = service.InsertDomainControl(new InsertDomainControlParam());

            // Assert
            repository.Verify(d => d.Execute(It.Is<InsertDomainControlCommand>(c => !string.IsNullOrEmpty(c.GetSettingKey()) && c.IsValid()), It.IsAny<string>()));
            Assert.True(result.IsSuccessful);
        }

        [Fact]
        public void UpdateDomainControl_WhenCallWithValidParam_UpdateSuccessfully()
        {
            // Arrange
            var repository = new Mock<IDynamicRepository>();
            repository.Setup(r => r.Execute(It.IsAny<UpdateDomainControlCommand>(), It.IsAny<string>()));
            var commonDomainService = new Mock<ICommonDomainService>();
            commonDomainService.Setup(c => c.GetTargetCountryListByDomain(It.IsAny<int>(), It.IsAny<int>()))
                .Returns(new List<TargetCountryByDomain>());

            // Action
            var service = new DomainControlService(repository.Object, commonDomainService.Object);
            var result = service.UpdateDomainControl(new UpdateDomainControlParam());

            // Assert
            repository.Verify(d => d.Execute(It.Is<UpdateDomainControlCommand>(c => !string.IsNullOrEmpty(c.GetSettingKey()) && !c.IsValid()), It.IsAny<string>()));
            Assert.True(result.IsSuccessful);
        }

        [Fact]
        public void DeleteDomainControl_WhenCallWithValidParam_DeleteSuccessfully()
        {
            // Arrange
            var repository = new Mock<IDynamicRepository>();
            repository.Setup(r => r.Execute(It.IsAny<DeleteDomainControlCommand>(), It.IsAny<string>()));
            var commonDomainService = new Mock<ICommonDomainService>();
            commonDomainService.Setup(c => c.GetTargetCountryListByDomain(It.IsAny<int>(), It.IsAny<int>()))
                .Returns(new List<TargetCountryByDomain>());

            // Action
            var service = new DomainControlService(repository.Object, commonDomainService.Object);
            var result = service.DeleteDomainControl(new DeleteDomainControlParam());

            // Assert
            repository.Verify(d => d.Execute(It.Is<DeleteDomainControlCommand>(c => !string.IsNullOrEmpty(c.GetSettingKey()) && c.IsValid()), It.IsAny<string>()));
            Assert.True(result.IsSuccessful);
        }

        [Fact]
        public void GetTargetCountryListByDomain_WhenCall_ReturnCorrectData()
        {
            // Arrange
            var affDomainId = 123;
            var redirectDomainId = 456;
            var expectedCountries = new List<TargetCountryByDomain> { new TargetCountryByDomain() };

            var repository = new Mock<IDynamicRepository>();
            var commonDomainService = new Mock<ICommonDomainService>();
            commonDomainService.Setup(c => c.GetTargetCountryListByDomain(affDomainId, redirectDomainId))
                .Returns(expectedCountries);

            // Action
            var service = new DomainControlService(repository.Object, commonDomainService.Object);
            var result = service.GetTargetCountryListByDomain(affDomainId, redirectDomainId);

            // Assert
            commonDomainService.Verify(c => c.GetTargetCountryListByDomain(affDomainId, redirectDomainId));
            Assert.Single(result);
            Assert.Equal(expectedCountries, result);
        }

        [Fact]
        public void GetDomainControlById_WhenCallWithValidId_ReturnCorrectData()
        {
            // Arrange
            var domainControlId = 123;
            var expectedControl = new DomainControlInfo { DomainControlId = domainControlId };
            var repository = new Mock<IDynamicRepository>();
            repository.Setup(r => r.Fetch<DomainControlInfo>(It.IsAny<DomainControlCriteria>(), It.IsAny<string>()))
                .Returns(new List<DomainControlInfo> { expectedControl });

            var commonDomainService = new Mock<ICommonDomainService>();
            commonDomainService.Setup(c => c.GetTargetCountryListByDomain(It.IsAny<int>(), It.IsAny<int>()))
                .Returns(new List<TargetCountryByDomain>());

            // Action
            var service = new DomainControlService(repository.Object, commonDomainService.Object);
            var result = service.GetDomainControlById(domainControlId);

            // Assert
            repository.Verify(d => d.Fetch<DomainControlInfo>(
                It.Is<DomainControlCriteria>(c =>
                    c.DomainControlId == domainControlId &&
                    c.TargetCountryCode == string.Empty &&
                    c.IsValid()),
                It.IsAny<string>()));
            Assert.Equal(domainControlId, result.DomainControlId);
        }

        [Fact]
        public void GetDomainControlById_WhenCallWithInvalidId_ReturnNull()
        {
            // Arrange
            var domainControlId = 999;
            var repository = new Mock<IDynamicRepository>();
            repository.Setup(r => r.Fetch<DomainControlInfo>(It.IsAny<DomainControlCriteria>(), It.IsAny<string>()))
                .Returns(new List<DomainControlInfo>());

            var commonDomainService = new Mock<ICommonDomainService>();
            commonDomainService.Setup(c => c.GetTargetCountryListByDomain(It.IsAny<int>(), It.IsAny<int>()))
                .Returns(new List<TargetCountryByDomain>());

            // Action
            var service = new DomainControlService(repository.Object, commonDomainService.Object);
            var result = service.GetDomainControlById(domainControlId);

            // Assert
            repository.Verify(d => d.Fetch<DomainControlInfo>(
                It.Is<DomainControlCriteria>(c =>
                    c.DomainControlId == domainControlId &&
                    c.TargetCountryCode == string.Empty &&
                    c.IsValid()),
                It.IsAny<string>()));
            Assert.Null(result);
        }

        [Fact]
        public void GetSubAffDomainControlList_WhenCall_ReturnCorrectData()
        {
            // Arrange
            var subDomains = new List<AffSubDomainInfo>
            {
                new AffSubDomainInfo { AffDomain = "domain1.com" },
                new AffSubDomainInfo { AffDomain = "domain2.com" },
                new AffSubDomainInfo { AffDomain = "domain1.com" }, // Duplicate
                new AffSubDomainInfo { AffDomain = null }, // Null domain
                new AffSubDomainInfo { AffDomain = string.Empty } // Empty domain
            };

            var repository = new Mock<IDynamicRepository>();
            repository.Setup(r => r.Fetch<AffSubDomainInfo>(It.IsAny<DomainControlCriteria>(), It.IsAny<string>()))
                .Returns(subDomains);

            var commonDomainService = new Mock<ICommonDomainService>();
            commonDomainService.Setup(c => c.GetTargetCountryListByDomain(It.IsAny<int>(), It.IsAny<int>()))
                .Returns(new List<TargetCountryByDomain>());

            // Action
            var service = new DomainControlService(repository.Object, commonDomainService.Object);
            var result = service.GetSubAffDomainControlList().ToList();

            // Assert
            repository.Verify(d => d.Fetch<AffSubDomainInfo>(
                It.Is<DomainControlCriteria>(c => c.IsValid()),
                It.IsAny<string>()));
            Assert.Equal(2, result.Count); // Only non-null, non-empty, distinct domains
            Assert.Contains("domain1", result);
            Assert.Contains("domain2", result);
        }

        [Fact]
        public void GetSubAffDomainControlList_WhenCallWithEmptyList_ReturnEmptyList()
        {
            // Arrange
            var repository = new Mock<IDynamicRepository>();
            repository.Setup(r => r.Fetch<AffSubDomainInfo>(It.IsAny<DomainControlCriteria>(), It.IsAny<string>()))
                .Returns(new List<AffSubDomainInfo>());

            var commonDomainService = new Mock<ICommonDomainService>();
            commonDomainService.Setup(c => c.GetTargetCountryListByDomain(It.IsAny<int>(), It.IsAny<int>()))
                .Returns(new List<TargetCountryByDomain>());

            // Action
            var service = new DomainControlService(repository.Object, commonDomainService.Object);
            var result = service.GetSubAffDomainControlList();

            // Assert
            repository.Verify(d => d.Fetch<AffSubDomainInfo>(
                It.Is<DomainControlCriteria>(c => c.IsValid()),
                It.IsAny<string>()));
            Assert.Empty(result);
        }
    }
}
