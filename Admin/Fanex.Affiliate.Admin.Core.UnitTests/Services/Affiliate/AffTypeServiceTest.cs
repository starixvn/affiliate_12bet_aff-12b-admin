using Fanex.Affiliate.Admin.Core.Criterias;
using Fanex.Affiliate.Admin.Core.DTO;
using Fanex.Affiliate.Admin.Core.Services.Affiliate;
using Fanex.Data.Repository;
using Moq;
using Xunit;

namespace Fanex.Affiliate.Admin.Core.UnitTests.Services.Affiliate
{
    public class AffTypeServiceTest
    {
        [Fact]
        public void GetAffTypeList_WhenCall_ReturnCorrectData()
        {
            // Arrange
            var dynamicRepository = new Mock<IDynamicRepository>();
            dynamicRepository.Setup(re => re.Get<AffTypeInfo>(It.IsAny<BaseCriteria>())).Returns(new AffTypeInfo());

            // Action
            var service = new AffTypeService(dynamicRepository.Object);
            var result = service.GetAffTypeList();

            // Assert
            Assert.NotNull(result);
        }
    }
}
