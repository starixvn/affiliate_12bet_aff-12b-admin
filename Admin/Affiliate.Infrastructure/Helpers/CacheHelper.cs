namespace Affiliate.Infrastructure.Helpers
{
    using Fanex.Caching;
    using Newtonsoft.Json;

    public class CacheHelper
    {
        private const int TIMEOUT = 120; // 2 minutes.

        private static readonly ICache Cache = CacheService.CreateInstance(CacheType.MemoryCache);

        public static T Get<T>(string key)
        {
            return DeepClone(Cache.Get<T>(key));
        }

        public static void Set<T>(string key, T value, int timeout = TIMEOUT)
        {
            if (value != null)
            {
                Cache.Set(key, DeepClone(value), timeout);
            }
        }

        public static void Clear()
        {
            Cache.Clear();
        }

        public static void Remove(string key)
        {
            Cache.Remove(key);
        }

        private static T DeepClone<T>(T source)
        {
            if (source == null)
            {
                return source;
            }

            return JsonConvert.DeserializeObject<T>(JsonConvert.SerializeObject(source));
        }
    }
}