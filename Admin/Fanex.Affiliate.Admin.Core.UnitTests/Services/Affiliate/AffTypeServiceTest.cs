using Fanex.Affiliate.Admin.Core.Criterias;
using Fanex.Affiliate.Admin.Core.DTO;
using Fanex.Affiliate.Admin.Core.Services.Affiliate;
using Fanex.Caching;
using Fanex.Data.Repository;
using Moq;
using System.Collections.Generic;
using Xunit;

namespace Fanex.Affiliate.Admin.Core.UnitTests.Services.Affiliate
{
    public class AffTypeServiceTest
    {
        [Fact]
        public void GetAffTypeList_WhenCall_ReturnCorrectData()
        {
            // Arrange
            var cacheService = new Mock<ICacheService>();
            var dynamicRepository = new Mock<IDynamicRepository>();
            dynamicRepository.Setup(re => re.Fetch<AffTypeInfo>(It.IsAny<BaseCriteria>(), It.IsAny<string>())).Returns(new List<AffTypeInfo> { new AffTypeInfo() });

            // Action
            var service = new AffTypeService(dynamicRepository.Object, cacheService.Object);
            var result = service.GetAffTypeList();

            // Assert
            dynamicRepository.Verify(d => d.Fetch<AffTypeInfo>(It.Is<BaseCriteria>(c => !string.IsNullOrEmpty(c.GetSettingKey()) && c.IsValid()), It.IsAny<string>()));
            Assert.NotEmpty(result);
        }
    }
}
