using Fanex.Affiliate.Admin.Core.Criterias;
using Fanex.Affiliate.Admin.Core.Criterias.MediaGallery;
using Fanex.Affiliate.Admin.Core.DTO.MediaGallery;
using Fanex.Affiliate.Admin.Core.Services.MediaGallery;
using Fanex.Caching;
using Fanex.Data.Repository;
using Moq;
using System.Collections.Generic;
using Xunit;

namespace Fanex.Affiliate.Admin.Core.UnitTests.Services.MediaGallery
{
    public class CommonMediaGalleryServiceTest
    {
        [Fact]
        public void GetCategoryList_WhenCall_ReturnCorrectData()
        {
            // Arrange
            var dynamicRepository = new Mock<IDynamicRepository>();
            dynamicRepository.Setup(r => r.Fetch<Category>(It.IsAny<MediaCategoryCriteria>(), It.IsAny<string>())).Returns(new List<Category> { new Category() });

            // Action
            var service = new CommonMediaGalleryService(dynamicRepository.Object, null);
            var result = service.GetCategoryList(1, 1);

            // Assert
            dynamicRepository.Verify(d => d.Fetch<Category>(It.Is<MediaCategoryCriteria>(c => !string.IsNullOrEmpty(c.GetSettingKey()) && c.IsValid()), It.IsAny<string>()));
            Assert.Single(result);
        }

        [Fact]
        public void GetCurrencyList_WhenCall_ReturnCorrectData()
        {
            // Arrange
            var dynamicRepository = new Mock<IDynamicRepository>();
            dynamicRepository.Setup(r => r.Fetch<MediaGalleryFilterItem>(It.IsAny<BaseCriteria>(), It.IsAny<string>())).Returns(new List<MediaGalleryFilterItem> { new MediaGalleryFilterItem() });
            var cacheService = new Mock<ICacheService>();

            // Action
            var service = new CommonMediaGalleryService(dynamicRepository.Object, cacheService.Object);
            var result = service.GetCurrencyList();

            // Assert
            dynamicRepository.Verify(d => d.Fetch<MediaGalleryFilterItem>(It.Is<BaseCriteria>(c => !string.IsNullOrEmpty(c.GetSettingKey()) && c.IsValid()), It.IsAny<string>()));
            Assert.Single(result);
        }
    }
}
