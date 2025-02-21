using Fanex.Affiliate.Link.Services.Interface;
using Fanex.Caching;
using Newtonsoft.Json;
using System;

namespace Fanex.Affiliate.Link.Services.Impl
{
    public class MemoryCacheService : IMemoryCacheService
    {
        private const int TIMEOUT = 120; // 2 minutes.

        private readonly ICache _cache;

        public MemoryCacheService(ICache cache)
        {
            _cache = cache;
        }

        public T Get<T>(string key)
        {
            _cache.TryGet<T>(key, out T value);

            return DeepClone(value);
        }

        public void Set<T>(string key, T value, int seconds = TIMEOUT)
        {
            var cachePolicy = new CacheItemOptions().SetAbsoluteExpiration(TimeSpan.FromSeconds(seconds));
            _cache.Set(key, DeepClone(value), cachePolicy);
        }

        private static T DeepClone<T>(T source)
        {
            return JsonConvert.DeserializeObject<T>(JsonConvert.SerializeObject(source));
        }
    }
}