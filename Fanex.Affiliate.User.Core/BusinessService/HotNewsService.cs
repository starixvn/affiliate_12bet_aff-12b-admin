using Fanex.Affiliate.User.Core.Constants;
using Fanex.Affiliate.User.Core.DTO;
using Fanex.Affiliate.User.Core.Repository;
using Fanex.Affiliate.User.Core.Utilities;

namespace Fanex.Affiliate.User.Core.BusinessService
{
    public class HotNewsService : IHotNewsService
    {
        private readonly IHotNewsRepository _hotNewsRepository;
        private readonly IMemoryCacheService _cacheService;

        public HotNewsService(IHotNewsRepository hotNewsRepository, IMemoryCacheService cacheService)
        {
            _hotNewsRepository = hotNewsRepository;
            _cacheService = cacheService;
        }

        public LatestHotNews GetLatestHotNews(bool isInternal)
        {
            var hotNews = _cacheService.Get<LatestHotNews>(CacheKey.LatestHotNews);
            if (hotNews == null)
            {
                hotNews = _hotNewsRepository.GetLastestHotNews(isInternal);
            }

            _cacheService.Set(CacheKey.LatestHotNews, hotNews);

            return hotNews;
        }
    }
}
