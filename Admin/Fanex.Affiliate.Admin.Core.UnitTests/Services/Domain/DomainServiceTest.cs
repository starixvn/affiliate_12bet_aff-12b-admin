using Fanex.Affiliate.Admin.Core.DTO;
using Fanex.Affiliate.Admin.Core.DTO.Domain;
using Fanex.Affiliate.Admin.Core.MetaModels.Domain;
using Fanex.Affiliate.Admin.Core.Services.Domain;
using Moq;
using System.Collections.Generic;
using Xunit;

namespace Fanex.Affiliate.Admin.Core.UnitTests.Services.Domain
{
    public class DomainServiceTest
    {
        [Fact]
        public void GetDomainList_WhenCall_CallCorrect()
        {
            // Arrange
            var spKey = string.Empty;
            var commonService = new Mock<ICommonDomainService> { CallBase = true };
            commonService.Setup(r => r.GetDomainList(It.IsAny<string>())).Returns(new List<DomainInfo> { new DomainInfo() }).Callback<string>(p => spKey = p);

            var service = CreateDomainService(commonService.Object);

            // Action
            var result = service.GetDomainList(true);

            // Assert
            Assert.Single(result);
            Assert.Equal("Aff_Admin_AffiliateDomain_Sel", spKey);
        }

        [Fact]
        public void GetDomainList_WhenCallWithFalse_CallCorrect()
        {
            // Arrange
            var spKey = string.Empty;
            var commonService = new Mock<ICommonDomainService> { CallBase = true };
            commonService.Setup(r => r.GetDomainList(It.IsAny<string>())).Returns(new List<DomainInfo> { new DomainInfo() }).Callback<string>(p => spKey = p);

            var service = CreateDomainService(commonService.Object);

            // Action
            var result = service.GetDomainList(false);

            // Assert
            Assert.Single(result);
            Assert.Equal("Aff_Admin_LicenseeDomain_Sel", spKey);
        }

        [Fact]
        public void InsertDomain_WhenCall_CallCorrect()
        {
            // Arrange
            var spKey = string.Empty;
            var commonService = new Mock<ICommonDomainService> { CallBase = true };
            commonService.Setup(r => r.InsertDomain(It.IsAny<InsertDomainParam>(), It.IsAny<string>())).Returns(new ResultInfo()).Callback<InsertDomainParam, string>((p, s) => spKey = s);

            var service = CreateDomainService(commonService.Object);

            // Action
            var result = service.InsertDomain(new InsertDomainParam { IsAffDomain = true });

            // Assert
            Assert.True(result.IsSuccessful);
            Assert.Equal("Aff_Admin_AffiliateDomain_Ins", spKey);
        }

        [Fact]
        public void InsertDomain_WhenCallWithFalse_CallCorrect()
        {
            // Arrange
            var spKey = string.Empty;
            var commonService = new Mock<ICommonDomainService> { CallBase = true };
            commonService.Setup(r => r.InsertDomain(It.IsAny<InsertDomainParam>(), It.IsAny<string>())).Returns(new ResultInfo()).Callback<InsertDomainParam, string>((p, s) => spKey = s);

            var service = CreateDomainService(commonService.Object);

            // Action
            var result = service.InsertDomain(new InsertDomainParam());

            // Assert
            Assert.True(result.IsSuccessful);
            Assert.Equal("Aff_Admin_LicenseeDomain_Ins", spKey);
        }

        [Fact]
        public void UpdateDomain_WhenCall_CallCorrect()
        {
            // Arrange
            var spKey = string.Empty;
            var commonService = new Mock<ICommonDomainService> { CallBase = true };
            commonService.Setup(r => r.UpdateDomain(It.IsAny<UpdateDomainParam>(), It.IsAny<string>())).Returns(new ResultInfo()).Callback<UpdateDomainParam, string>((p, s) => spKey = s);

            var service = CreateDomainService(commonService.Object);

            // Action
            var result = service.UpdateDomain(new UpdateDomainParam { IsAffDomain = true });

            // Assert
            Assert.True(result.IsSuccessful);
            Assert.Equal("Aff_Admin_AffiliateDomain_Upd", spKey);
        }

        [Fact]
        public void UpdateDomain_WhenCallWithFalse_CallCorrect()
        {
            // Arrange
            var spKey = string.Empty;
            var commonService = new Mock<ICommonDomainService> { CallBase = true };
            commonService.Setup(r => r.UpdateDomain(It.IsAny<UpdateDomainParam>(), It.IsAny<string>())).Returns(new ResultInfo()).Callback<UpdateDomainParam, string>((p, s) => spKey = s);

            var service = CreateDomainService(commonService.Object);

            // Action
            var result = service.UpdateDomain(new UpdateDomainParam());

            // Assert
            Assert.True(result.IsSuccessful);
            Assert.Equal("Aff_Admin_LicenseeDomain_Upd", spKey);
        }

        [Fact]
        public void DeleteDomain_WhenCall_CallCorrect()
        {
            // Arrange
            var spKey = string.Empty;
            var commonService = new Mock<ICommonDomainService> { CallBase = true };
            commonService.Setup(r => r.DeleteDomain(It.IsAny<DeleteDomainParam>(), It.IsAny<string>())).Returns(new ResultInfo()).Callback<DeleteDomainParam, string>((p, s) => spKey = s);

            var service = CreateDomainService(commonService.Object);

            // Action
            var result = service.DeleteDomain(new DeleteDomainParam { IsAffDomain = true });

            // Assert
            Assert.True(result.IsSuccessful);
            Assert.Equal("Aff_Admin_AffiliateDomain_Del", spKey);
        }

        [Fact]
        public void DeleteDomain_WhenCallWithFalse_CallCorrect()
        {
            // Arrange
            var spKey = string.Empty;
            var commonService = new Mock<ICommonDomainService> { CallBase = true };
            commonService.Setup(r => r.DeleteDomain(It.IsAny<DeleteDomainParam>(), It.IsAny<string>())).Returns(new ResultInfo()).Callback<DeleteDomainParam, string>((p, s) => spKey = s);

            var service = CreateDomainService(commonService.Object);

            // Action
            var result = service.DeleteDomain(new DeleteDomainParam());

            // Assert
            Assert.True(result.IsSuccessful);
            Assert.Equal("Aff_Admin_LicenseeDomain_Del", spKey);
        }

        [Fact]
        public void GetTargetCountryListByDomain_WhenCall_ReturnCorrectData()
        {
            // Arrange
            var affDomainId = 123;
            var redirectDomainId = 456;
            var expectedCountries = new List<TargetCountryByDomain> { new TargetCountryByDomain() };

            var commonService = new Mock<ICommonDomainService> { CallBase = true };
            commonService.Setup(r => r.GetTargetCountryListByDomain(affDomainId, redirectDomainId))
                .Returns(expectedCountries);

            var service = CreateDomainService(commonService.Object);

            // Action
            var result = service.GetTargetCountryListByDomain(affDomainId, redirectDomainId);

            // Assert
            commonService.Verify(c => c.GetTargetCountryListByDomain(affDomainId, redirectDomainId));
            Assert.Single(result);
            Assert.Equal(expectedCountries, result);
        }

        [Fact]
        public void GetTargetCountryListByDomain_WhenCallWithDefaultParams_ReturnCorrectData()
        {
            // Arrange
            var expectedCountries = new List<TargetCountryByDomain> { new TargetCountryByDomain() };

            var commonService = new Mock<ICommonDomainService> { CallBase = true };
            commonService.Setup(r => r.GetTargetCountryListByDomain(0, 0))
                .Returns(expectedCountries);

            var service = CreateDomainService(commonService.Object);

            // Action
            var result = service.GetTargetCountryListByDomain();

            // Assert
            commonService.Verify(c => c.GetTargetCountryListByDomain(0, 0));
            Assert.Single(result);
            Assert.Equal(expectedCountries, result);
        }

        [Fact]
        public void GetDomainById_WhenCallWithValidId_ReturnCorrectData()
        {
            // Arrange
            var domainId = 123;
            var expectedDomain = new DomainInfo { DomainId = domainId };
            var commonService = new Mock<ICommonDomainService> { CallBase = true };
            commonService.Setup(r => r.GetDomainList(It.IsAny<string>()))
                .Returns(new List<DomainInfo> { expectedDomain });

            var service = CreateDomainService(commonService.Object);

            // Action
            var result = service.GetDomainById(domainId);

            // Assert
            commonService.Verify(c => c.GetDomainList("Aff_Admin_AffiliateDomain_Sel"));
            Assert.NotNull(result);
            Assert.Equal(domainId, result.DomainId);
        }

        [Fact]
        public void GetDomainById_WhenCallWithInvalidId_ReturnNull()
        {
            // Arrange
            var domainId = 999;
            var commonService = new Mock<ICommonDomainService> { CallBase = true };
            commonService.Setup(r => r.GetDomainList(It.IsAny<string>()))
                .Returns(new List<DomainInfo>());

            var service = CreateDomainService(commonService.Object);

            // Action
            var result = service.GetDomainById(domainId);

            // Assert
            commonService.Verify(c => c.GetDomainList("Aff_Admin_AffiliateDomain_Sel"));
            Assert.Null(result);
        }

        [Fact]
        public void GetDomainById_WhenCallWithLicenseeDomain_ReturnCorrectData()
        {
            // Arrange
            var domainId = 123;
            var expectedDomain = new DomainInfo { DomainId = domainId };
            var commonService = new Mock<ICommonDomainService> { CallBase = true };
            commonService.Setup(r => r.GetDomainList(It.IsAny<string>()))
                .Returns(new List<DomainInfo> { expectedDomain });

            var service = CreateDomainService(commonService.Object);

            // Action
            var result = service.GetDomainById(domainId, false);

            // Assert
            commonService.Verify(c => c.GetDomainList("Aff_Admin_LicenseeDomain_Sel"));
            Assert.NotNull(result);
            Assert.Equal(domainId, result.DomainId);
        }

        private static DomainService CreateDomainService(ICommonDomainService domaincommonService = null)
        {
            var fakeDomaincommonService = domaincommonService ?? Mock.Of<ICommonDomainService>();

            return new DomainService(fakeDomaincommonService);
        }
    }
}
