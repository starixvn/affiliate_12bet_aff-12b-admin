using Fanex.Affiliate.Admin.Core.Commands.Affiliate;
using Fanex.Affiliate.Admin.Core.Criterias.Affiliate;
using Fanex.Affiliate.Admin.Core.DTO.Affiliate;
using Fanex.Affiliate.Admin.Core.MetaModels.Affiliate;
using Fanex.Affiliate.Admin.Core.Services.Affiliate;
using Fanex.Data.Repository;
using Moq;
using Xunit;

namespace Fanex.Affiliate.Admin.Core.UnitTests.Services.Affiliate
{
    public class AffiliateServiceTest
    {
        [Fact]
        public void GetAffiliateBasicInfo_WhenCall_ReturnCorrectData()
        {
            // Arrange
            var dynamicRepository = new Mock<IDynamicRepository>();
            dynamicRepository.Setup(re => re.Get<AffiliateBasicInfo>(It.IsAny<AffBasicInfoCriteria>())).Returns(new AffiliateBasicInfo());

            // Action
            var service = new AffiliateService(dynamicRepository.Object);
            var result = service.GetAffiliateBasicInfo(123, "aff");

            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public void ApproveAffiliate_WhenCall_ReturnCorrectData()
        {
            // Arrange
            var dynamicRepository = new Mock<IDynamicRepository>();
            dynamicRepository.Setup(re => re.Get<AffiliateBasicInfo>(It.IsAny<AffBasicInfoCriteria>()))
                .Returns(new AffiliateBasicInfo()).Callback<ApproveAffCommand>(a => a.AffCode = "123456");

            // Action
            var service = new AffiliateService(dynamicRepository.Object);
            var (result, affCode) = service.ApproveAffiliate(new ApproveAffiliateParam());

            // Assert
            Assert.True(result.IsSuccessful);
            Assert.NotNull(affCode);
        }
    }
}
