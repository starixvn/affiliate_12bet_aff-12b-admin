namespace Fanex.Affiliate.User.Core.UnitTests.BusinessService
{
    using Criterias;
    using Data.Repository;
    using DTO;
    using DTO.UserPanel;
    using Fanex.Affiliate.User.Core.BusinessService;
    using Fanex.Affiliate.User.Core.Constants;
    using Fanex.Affiliate.User.Core.CoreService;
    using MetalModels.UserPanel;
    using Moq;
    using System.Collections.Generic;
    using Xunit;

    public class NewsServiceTest
    {
        private readonly Mock<IDynamicRepository> NewsRepo;
        private readonly Mock<IMemoryCacheService> CacheService;

        public NewsServiceTest()
        {
            NewsRepo = new Mock<IDynamicRepository> { CallBase = true };
            CacheService = new Mock<IMemoryCacheService> { CallBase = true };
        }

        [Fact]
        public void GetLastestHotNews_WhenHaveInfoInCache_RepoNotCall()
        {
            var service = CreateService();
            CacheService.Setup(x => x.Get<LatestHotNews>(CacheKey.LatestHotNews)).Returns(CreateLastetHotNews);

            service.GetLatestHotNews(false);

            NewsRepo.Verify(ac => ac.Get<dynamic>(It.IsAny<GetLastestHotNewsCriteria>(), string.Empty), Times.Never);
        }

        [Fact]
        public void GetLastestHotNews_WhenCacheNotHaveInfo_RepoCall()
        {
            var service = CreateService();

            var fetchedData = GetData();
            CacheService.Setup(x => x.Get<LatestHotNews>(CacheKey.LatestHotNews)).Returns((LatestHotNews)null);
            NewsRepo.Setup(x => x.Get<dynamic>(It.IsAny<GetLastestHotNewsCriteria>(), string.Empty)).Returns(fetchedData);

            var result = service.GetLatestHotNews(false);

            NewsRepo.Verify(ac => ac.Get<dynamic>(It.IsAny<GetLastestHotNewsCriteria>(), string.Empty), Times.Once);
            CacheService.Verify(x => x.Set(CacheKey.LatestHotNews, result, 120), Times.Once);
        }

        [Fact]
        public void GetMessageList_WhenCall_CallRepo()
        {
            var param = new MessageHistoryParam();
            var service = CreateService();

            service.GetMessageList(param);

            NewsRepo.Verify(ac => ac.Fetch<MessageHistory>(It.IsAny<GetMessageListCriteria>(), string.Empty), Times.Once);
        }

        private static LatestHotNews CreateLastetHotNews()
            => new LatestHotNews
            {
                HotNewsList = new Dictionary<string, string> { { "aa", "bb" } },
                IsImportant = true
            };

        private static IDictionary<string, object> GetData()
        {
            return new Dictionary<string, object>
            {
                ["message_en"] = "A",
                ["Message_Bn"] = "B",
                ["message_th"] = "C",
                ["message_vi"] = "D",
                ["message_id"] = "E",
                ["message_kr"] = "F",
                ["message_cn"] = "G",
                ["message_hi"] = "H",
                ["important"] = "true"
            };
        }

        private NewsService CreateService()
            => new NewsService(NewsRepo.Object, CacheService.Object);
    }
}
