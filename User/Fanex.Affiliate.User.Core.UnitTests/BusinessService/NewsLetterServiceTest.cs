namespace Fanex.Affiliate.User.Core.UnitTests.BusinessService
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Criterias;
    using Data.Repository;
    using DTO.UserPanel;
    using Fanex.Affiliate.User.Core.BusinessService;
    using Fanex.Affiliate.User.Core.CoreService;
    using Fanex.Affiliate.User.Core.Utilities;
    using Moq;
    using TestHelper;
    using Xunit;

    public class NewsLetterServiceTest
    {
        private readonly Mock<IDynamicRepository> _repository;

        public NewsLetterServiceTest()
        {
            _repository = new Mock<IDynamicRepository> { CallBase = true };
        }

        [Fact]
        public async Task GetNewsLetterById_WhenCall_ReturnEmptyString()
        {
            // Arrange
            var newsLetterId = 123;
            NewsLetter newsLetter = null;
            _repository.Setup(p => p.Get<NewsLetter>(It.IsAny<GetNewsLetterByIdCriteria>(), string.Empty)).Returns(newsLetter);
            var service = CreateNewsLetterService(_repository.Object);

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
            NewsLetter newsLetter = new NewsLetter
            {
                FileName = "abc.html",
                EffectiveFrom = DateTimeHelper.Now,
                EffectiveTo = DateTimeHelper.Now,
                Language = Constant.DefaultLanguage,
                NewsLetterId = 1,
                UploadDate = DateTimeHelper.Now
            };

            _repository.Setup(p => p.Get<NewsLetter>(It.IsAny<GetNewsLetterByIdCriteria>(), string.Empty)).Returns(newsLetter);
            var httpClientUtility = new Mock<IHttpClientUtility>();
            httpClientUtility.Setup(h => h.GetContent(It.IsAny<string>(), It.IsAny<string>())).Returns(Task.FromResult(content));
            var service = CreateNewsLetterService(_repository.Object, null, httpClientUtility.Object);

            // Action
            var result = await service.GetContentById(newsLetterId);

            // Assert
            Assert.Equal(content, result);
        }

        [Fact]
        public async Task GetCurrentContent_WhenCall_ReturnEmptyString()
        {
            // Arrange
            _repository.Setup(p => p.Fetch<NewsLetter>(It.IsAny<GetNewsLettersByDateCriteria>(), string.Empty)).Returns(new List<NewsLetter>());

            var service = CreateNewsLetterService(newsLetterRepository: _repository.Object);

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
            _repository.Setup(p => p.Fetch<NewsLetter>(It.IsAny<GetNewsLettersByDateCriteria>(), string.Empty))
                .Returns(new List<NewsLetter> { new NewsLetter { Language = "en-US", FileName = "abc.html" } });
            var httpClientUtility = new Mock<IHttpClientUtility>();
            httpClientUtility.Setup(h => h.GetContent(It.IsAny<string>(), It.IsAny<string>())).Returns(Task.FromResult(content));

            var service = CreateNewsLetterService(_repository.Object, sessionService.Object, httpClientUtility.Object);

            // Action
            var result = await service.GetCurrentContent();

            // Assert
            Assert.Equal(content, result);
        }

        private static NewsLetterService CreateNewsLetterService(
            IDynamicRepository newsLetterRepository = null,
            ISessionService sessionService = null,
            IHttpClientUtility httpClientUtility = null)
        {
            var fakeNewsLetterRepository = newsLetterRepository ?? Mock.Of<IDynamicRepository>();
            var fakeSessionService = sessionService ?? MockingHelper.GetSessionService();
            var fakeHttpClientUtility = httpClientUtility ?? Mock.Of<IHttpClientUtility>();

            return new NewsLetterService(fakeNewsLetterRepository, fakeSessionService, fakeHttpClientUtility);
        }
    }
}
