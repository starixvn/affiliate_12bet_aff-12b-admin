namespace Fanex.Affiliate.Admin.Core.Configurations
{
    using System.Web.Mvc;
    using Fanex.Caching;
    using Fanex.Caching.Redis;

    public static class RedisConfig
    {
        public static void RegisterRedis()
        {
            var appSetting = DependencyResolver.Current.GetService<IAppSetting>();
            var applicationName = appSetting.GetValue("Redis.ApplicationName");
            var connectionString = appSetting.GetValue("Redis.ConnectionString");

            if (!string.IsNullOrEmpty(connectionString))
            {
                CacheServiceManager.Use(() => new RedisCache(new RedisCacheOptions
                {
                    ConnectionString = connectionString,

                    ApplicationName = applicationName
                }));
            }
        }
    }
}
