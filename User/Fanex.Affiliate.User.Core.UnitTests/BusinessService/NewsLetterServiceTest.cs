using Fanex.Affiliate.User.Core.BusinessService;
using Fanex.Affiliate.User.Core.CoreService;
using Fanex.Affiliate.User.Core.DTO.UserPanel;
using Fanex.Affiliate.User.Core.Repository.UserPanel;
using Fanex.Affiliate.User.Core.UnitTests.TestHelper;
using Fanex.Affiliate.User.Core.Utilities;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace Fanex.Affiliate.User.Core.UnitTests.BusinessService
{
    public class NewsLetterServiceTest
    {
        [Fact]
        public async Task GetNewsLetterById_WhenCall_ReturnEmptyString()
        {
            // Arrange
            var newsLetterId = 123;
            NewsLetter newsLetter = null;
            var newsLetterRepository = new Mock<INewsLetterRepository>();
            newsLetterRepository.Setup(p => p.GetNewsLetterById(newsLetterId)).Returns(newsLetter);
            var service = CreateNewsLetterService(newsLetterRepository.Object);

            // Action
            var result = await service.GetContentById(newsLetterId);

            // Assert
            Assert.Empty(result);
        }

        [Fact]
        public async Task GetNewsLetterById_WhenCall_ReturnContent()
        {
            // Arrange
            var newsLetterId = 123;
            var content = "test";
            NewsLetter newsLetter = new NewsLetter { FileName = "abc.html" };
            var newsLetterRepository = new Mock<INewsLetterRepository>();
            newsLetterRepository.Setup(p => p.GetNewsLetterById(newsLetterId)).Returns(newsLetter);
            var httpClientUtility = new Mock<IHttpClientUtility>();
            httpClientUtility.Setup(h => h.GetContent(It.IsAny<string>(), It.IsAny<string>())).Returns(Task.FromResult(content));
            var service = CreateNewsLetterService(newsLetterRepository.Object, null, httpClientUtility.Object);

            // Action
            var result = await service.GetContentById(newsLetterId);

            // Assert
            Assert.Equal(content, result);
        }

        [Fact]
        public async Task GetCurrentContent_WhenCall_ReturnEmptyString()
        {
            // Arrange
            var newsLetterRepository = new Mock<INewsLetterRepository>();
            newsLetterRepository.Setup(p => p.GetNewsLettersByDate(It.IsAny<DateTime>())).Returns(new List<NewsLetter>());

            var service = CreateNewsLetterService(newsLetterRepository: newsLetterRepository.Object);

            // Action
            var result = await service.GetCurrentContent();

            // Assert
            Assert.Empty(result);
        }

        [Fact]
        public async Task GetCurrentContent_WhenCall_ReturnContent()
        {
            // Arrange
            var content = "test";
            var sessionService = new Mock<ISessionService>();
            sessionService.Setup(s => s.CurrentLanguage).Returns("en-US");
            var newsLetterRepository = new Mock<INewsLetterRepository>();
            newsLetterRepository.Setup(p => p.GetNewsLettersByDate(It.IsAny<DateTime>())).Returns(new List<NewsLetter> { new NewsLetter { Language = "en-US", FileName = "abc.html" } });
            var httpClientUtility = new Mock<IHttpClientUtility>();
            httpClientUtility.Setup(h => h.GetContent(It.IsAny<string>(), It.IsAny<string>())).Returns(Task.FromResult(content));

            var service = CreateNewsLetterService(newsLetterRepository.Object, sessionService.Object, httpClientUtility.Object);

            // Action
            var result = await service.GetCurrentContent();

            // Assert
            Assert.Equal(content, result);
        }

        [Fact]
        public void HasNewCurrentTime_WhenCall_ReturnCorrectData()
        {
            // Arrange
            var newsLetterRepository = new Mock<INewsLetterRepository>();
            newsLetterRepository.Setup(p => p.GetNewsLettersByDate(It.IsAny<DateTime>())).Returns(new List<NewsLetter> { new NewsLetter() });
            var service = CreateNewsLetterService(newsLetterRepository: newsLetterRepository.Object);

            // Action
            var result = service.HasNewCurrentTime();

            // Assert
            Assert.True(result);
        }

        private static NewsLetterService CreateNewsLetterService(
            INewsLetterRepository newsLetterRepository = null,
            ISessionService sessionService = null,
            IHttpClientUtility httpClientUtility = null)
        {
            var fakeNewsLetterRepository = newsLetterRepository ?? Mock.Of<INewsLetterRepository>();
            var fakeSessionService = sessionService ?? MockingHelper.GetSessionService();
            var fakeHttpClientUtility = httpClientUtility ?? Mock.Of<IHttpClientUtility>();

            return new NewsLetterService(fakeNewsLetterRepository, fakeSessionService, fakeHttpClientUtility);
        }
    }
}
