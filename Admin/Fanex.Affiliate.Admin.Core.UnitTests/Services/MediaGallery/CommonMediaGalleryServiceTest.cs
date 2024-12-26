using Fanex.Affiliate.Admin.Core.Criterias;
using Fanex.Affiliate.Admin.Core.Criterias.MediaGallery;
using Fanex.Affiliate.Admin.Core.DTO.MediaGallery;
using Fanex.Affiliate.Admin.Core.Services.MediaGallery;
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
            var repository = new Mock<IDynamicRepository>();
            repository.Setup(r => r.Fetch<Category>(It.IsAny<MediaCategoryCriteria>(), It.IsAny<string>())).Returns(new List<Category> { new Category() });

            // Action
            var service = new CommonMediaGalleryService(repository.Object);
            var result = service.GetCategoryList(1, 1);

            // Assert
            Assert.Single(result);
        }

        [Fact]
        public void GetCurrencyList_WhenCall_ReturnCorrectData()
        {
            // Arrange
            var repository = new Mock<IDynamicRepository>();
            repository.Setup(r => r.Fetch<MediaGalleryFilterItem>(It.IsAny<BaseCriteria>(), It.IsAny<string>())).Returns(new List<MediaGalleryFilterItem> { new MediaGalleryFilterItem() });

            // Action
            var service = new CommonMediaGalleryService(repository.Object);
            var result = service.GetCurrencyList();

            // Assert
            Assert.Single(result);
        }
    }
}
