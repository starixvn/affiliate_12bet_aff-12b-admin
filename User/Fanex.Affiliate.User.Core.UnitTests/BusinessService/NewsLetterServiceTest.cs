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
            var service = CreateNewsLetterService(newsLetterRepository: newsLetterRepository.Object);

            // Action
            var result = await service.GetContentById(newsLetterId);

            // Assert
            Assert.Empty(result);
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
            IAppSetting appSetting = null)
        {
            var fakeNewsLetterRepository = newsLetterRepository ?? Mock.Of<INewsLetterRepository>();
            var fakeSessionService = sessionService ?? MockingHelper.GetSessionService();
            var fakeAppSetting = appSetting ?? Mock.Of<IAppSetting>();

            return new NewsLetterService(fakeNewsLetterRepository, fakeSessionService, fakeAppSetting);
        }
    }
}
