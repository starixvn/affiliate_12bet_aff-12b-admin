namespace Affiliate.Infrastructure.Helpers
{
    using Fanex.Caching;
    using Fanex.Caching.Memory;
    using Newtonsoft.Json;
    using System;

    public static class CacheHelper
    {
        private const int TIMEOUT = 120; // 2 minutes.

        private static readonly ICache Cache = new MemoryCache(new MemoryCacheOptions());

        public static T Get<T>(string key)
        {
            Cache.TryGet<T>(key, out T value);

            return DeepClone(value);
        }

        public static void Set<T>(string key, T value, int timeout = TIMEOUT)
        {
            if (!Equals(value, default(T)))
            {
                var cachePolicy = new CacheItemOptions().SetAbsoluteExpiration(TimeSpan.FromSeconds(timeout));

                Cache.Set(key, DeepClone(value), cachePolicy);
            }
        }

        public static void Clear()
        {
            Cache.Dispose();
        }

        public static void Remove(string key)
        {
            Cache.Remove(key);
        }

        private static T DeepClone<T>(T source)
        {
            if (Equals(source, default(T)))
            {
                return source;
            }

            return JsonConvert.DeserializeObject<T>(JsonConvert.SerializeObject(source));
        }
    }
}