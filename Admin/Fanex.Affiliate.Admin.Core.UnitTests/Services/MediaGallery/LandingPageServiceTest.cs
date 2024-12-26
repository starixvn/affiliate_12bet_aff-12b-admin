using Fanex.Affiliate.Admin.Core.Commands.MediaGallery;
using Fanex.Affiliate.Admin.Core.Criterias.MediaGallery;
using Fanex.Affiliate.Admin.Core.DTO.MediaGallery;
using Fanex.Affiliate.Admin.Core.MetaModels.MediaGallery;
using Fanex.Affiliate.Admin.Core.Services.MediaGallery;
using Fanex.Data.Repository;
using Moq;
using System.Collections.Generic;
using Xunit;

namespace Fanex.Affiliate.Admin.Core.UnitTests.Services.MediaGallery
{
    public class LandingPageServiceTest
    {
        [Fact]
        public void GetLandingPageList_WhenCall_ReturnCorrectData()
        {
            // Arrange
            var dynamicRepository = new Mock<IDynamicRepository>();
            dynamicRepository.Setup(l => l.Fetch<LandingPage>(It.IsAny<LandingPageCriteria>(), It.IsAny<string>())).Returns(new List<LandingPage> { new LandingPage() });
            var service = CreateLandingPageService(dynamicRepository.Object);

            // Action
            var result = service.GetLandingPageList();

            // Assert
            Assert.Single(result);
        }

        [Fact]
        public void GetLandingPageById_WhenCall_ReturnCorrectData()
        {
            // Arrange
            var pageId = 123;
            var dynamicRepository = new Mock<IDynamicRepository>();
            dynamicRepository.Setup(l => l.Get<LandingPage>(It.IsAny<LandingPageCriteria>(), It.IsAny<string>())).Returns(new LandingPage { PageId = 123 });
            var service = CreateLandingPageService(dynamicRepository.Object);

            // Action
            var result = service.GetLandingPageById(pageId);

            // Assert
            Assert.Equal(pageId, result.PageId);
        }

        [Fact]
        public void InsertLandingPage_WhenCall_ExcuteCorrect()
        {
            // Arrange
            var param = new InsertUpdateLandingPageParam { PageCode = "123" };
            var dynamicRepository = new Mock<IDynamicRepository>();
            dynamicRepository.Setup(l => l.Execute(It.IsAny<InsertLandingPageCommand>(), It.IsAny<string>()));
            var service = CreateLandingPageService(dynamicRepository.Object);

            // Action
            var result = service.InsertLandingPage(param);

            // Assert
            Assert.True(result.IsSuccessful);
        }

        [Fact]
        public void InsertLandingPage_WhenCall_ExcuteIncorrect()
        {
            // Arrange
            var param = new InsertUpdateLandingPageParam { PageCode = "123@ abc" };
            var service = CreateLandingPageService();

            // Action
            var result = service.InsertLandingPage(param);

            // Assert
            Assert.False(result.IsSuccessful);
            Assert.Equal("Special characters are not allowed:\n the blank/empty space and ; / ? : @ = & \" < > # % { } | \\ ^ ~ [ ] `", result.Message);
        }

        [Fact]
        public void UpdateLandingPage_WhenCall_ExcuteCorrect()
        {
            // Arrange
            var param = new InsertUpdateLandingPageParam { PageCode = "abc" };
            var dynamicRepository = new Mock<IDynamicRepository>();
            dynamicRepository.Setup(l => l.Execute(It.IsAny<UpdateLandingPageCommand>(), It.IsAny<string>()));
            var service = CreateLandingPageService(dynamicRepository.Object);

            // Action
            var result = service.UpdateLandingPage(param);

            // Assert
            Assert.True(result.IsSuccessful);
        }

        [Fact]
        public void UpdateLandingPage_WhenCall_ExcuteIncorrect()
        {
            // Arrange
            var param = new InsertUpdateLandingPageParam { PageCode = "abc@ 123" };
            var service = CreateLandingPageService();

            // Action
            var result = service.UpdateLandingPage(param);

            // Assert
            Assert.False(result.IsSuccessful);
            Assert.Equal("Special characters are not allowed:\n the blank/empty space and ; / ? : @ = & \" < > # % { } | \\ ^ ~ [ ] `", result.Message);
        }

        [Fact]
        public void DeleteLandingPage_WhenCall_ExcuteCorrect()
        {
            // Arrange
            var param = new DeleteLandingPageParam();
            var dynamicRepository = new Mock<IDynamicRepository>();
            dynamicRepository.Setup(l => l.Execute(It.IsAny<DeleteLandingPageCommand>(), It.IsAny<string>()));
            var service = CreateLandingPageService(dynamicRepository.Object);

            // Action
            var result = service.DeleteLandingPage(param);

            // Assert
            Assert.True(result.IsSuccessful);
        }

        private static LandingPageService CreateLandingPageService(IDynamicRepository dynamicRepository = null)
        {
            var fakedynamicRepository = dynamicRepository ?? Mock.Of<IDynamicRepository>();

            return new LandingPageService(fakedynamicRepository);
        }
    }
}
