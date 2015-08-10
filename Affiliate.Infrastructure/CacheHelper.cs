// -----------------------------------------------------------------------
// <copyright file="CacheService.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Affiliate.Infrastructure
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Fanex.Caching;
    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class CacheHelper
    {
        private const int TIMEOUT = 300; // 5 minutes.

        private static readonly ICache Cache = CacheService.CreateInstance(CacheType.EntlibCache);

        /// <summary>
        /// Getting cache value.
        /// </summary>
        /// <typeparam name="T">Parameter type.</typeparam>
        /// <param name="key">Cache key.</param>
        /// <returns>Return value.</returns>
        public static T Get<T>(string key)
        {
            return Cache.Get<T>(key);
        }

        /// <summary>
        /// Setting cache value.
        /// </summary>
        /// <typeparam name="T">Parameter type.</typeparam>
        /// <param name="key">Cache key.</param>
        /// <param name="value">Return value.</param>
        public static void Set<T>(string key, T value, int timeout = TIMEOUT)
        {
            Cache.Set(key, value, timeout);
        }

        /// <summary>
        /// Removing the cache.
        /// </summary>
        public static void Delete()
        {
            Cache.Clear();
        }

        /// <summary>
        /// Deletes the specified key.
        /// </summary>
        /// <param name="key">The key to remove.</param>
        public static void Remove(string key)
        {
            Cache.Remove(key);
        }
    }
}
