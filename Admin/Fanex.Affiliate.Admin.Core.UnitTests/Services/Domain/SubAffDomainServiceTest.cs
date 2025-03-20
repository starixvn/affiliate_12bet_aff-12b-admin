using Fanex.Affiliate.Admin.Core.DTO;
using Fanex.Affiliate.Admin.Core.DTO.Domain;
using Fanex.Affiliate.Admin.Core.MetaModels.Domain;
using Fanex.Affiliate.Admin.Core.Services.Domain;
using Moq;
using System.Collections.Generic;
using Xunit;

namespace Fanex.Affiliate.Admin.Core.UnitTests.Services.Domain
{
    public class SubAffDomainServiceTest
    {
        [Fact]
        public void GetSubAffDomainList_WhenCall_CallCorrect()
        {
            // Arrange
            var spKey = string.Empty;
            var commonService = new Mock<ICommonDomainService> { CallBase = true };
            commonService.Setup(r => r.GetDomainList(It.IsAny<string>())).Returns(new List<DomainInfo> { new DomainInfo() }).Callback<string>(p => spKey = p);

            var service = CreateSubAffDomainService(commonService.Object);

            // Action
            var result = service.GetSubAffDomainList();

            // Assert
            Assert.Single(result);
            Assert.Equal("Aff_Admin_SubAffiliateDomain_Sel", spKey);
        }

        [Fact]
        public void InsertSubAffDomain_WhenCall_CallCorrect()
        {
            // Arrange
            var spKey = string.Empty;
            var commonService = new Mock<ICommonDomainService> { CallBase = true };
            commonService.Setup(r => r.InsertDomain(It.IsAny<InsertDomainParam>(), It.IsAny<string>())).Returns(new ResultInfo()).Callback<InsertDomainParam, string>((p, s) => spKey = s);

            var service = CreateSubAffDomainService(commonService.Object);

            // Action
            var result = service.InsertSubAffDomain(new InsertDomainParam());

            // Assert
            Assert.True(result.IsSuccessful);
            Assert.Equal("Aff_Admin_SubAffiliateDomain_Ins", spKey);
        }

        [Fact]
        public void UpdateSubAffDomain_WhenCall_CallCorrect()
        {
            // Arrange
            var spKey = string.Empty;
            var commonService = new Mock<ICommonDomainService> { CallBase = true };
            commonService.Setup(r => r.UpdateDomain(It.IsAny<UpdateDomainParam>(), It.IsAny<string>())).Returns(new ResultInfo()).Callback<UpdateDomainParam, string>((p, s) => spKey = s);

            var service = CreateSubAffDomainService(commonService.Object);

            // Action
            var result = service.UpdateSubAffDomain(new UpdateDomainParam());

            // Assert
            Assert.True(result.IsSuccessful);
            Assert.Equal("Aff_Admin_SubAffiliateDomain_Upd", spKey);
        }

        [Fact]
        public void DeleteSubAffDomain_WhenCall_CallCorrect()
        {
            // Arrange
            var spKey = string.Empty;
            var commonService = new Mock<ICommonDomainService> { CallBase = true };
            commonService.Setup(r => r.DeleteDomain(It.IsAny<DeleteDomainParam>(), It.IsAny<string>())).Returns(new ResultInfo()).Callback<DeleteDomainParam, string>((p, s) => spKey = s);

            var service = CreateSubAffDomainService(commonService.Object);

            // Action
            var result = service.DeleteSubAffDomain(new DeleteDomainParam());

            // Assert
            Assert.True(result.IsSuccessful);
            Assert.Equal("Aff_Admin_SubAffiliateDomain_Del", spKey);
        }

        private static SubAffDomainService CreateSubAffDomainService(ICommonDomainService subAffDomaincommonService = null)
        {
            var fakeSubAffDomaincommonService = subAffDomaincommonService ?? Mock.Of<ICommonDomainService>();

            return new SubAffDomainService(fakeSubAffDomaincommonService);
        }
    }
}
