using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Fanex.Affiliate.ThirdPartyProduct.Utilities
{
    public static class ParallelForeachAsync
    {
        public static async Task<IEnumerable<M>> ForEachSemaphoreAsync<T, M>(this IEnumerable<T> source, int degreeOfParallelism, Func<T, Task<M>> func)
        {
            var tasks = new List<Task<M>>();
            using (var throttler = new SemaphoreSlim(degreeOfParallelism))
            {
                foreach (var item in source)
                {
                    await throttler.WaitAsync();
                    tasks.Add(Task.Run<M>(async () =>
                    {
                        try
                        {
                            return await func(item);
                        }
                        finally
                        {
                            throttler.Release();
                        }
                    }));
                }
                var result = await Task.WhenAll(tasks);
                return result.AsEnumerable();
            }
        }
    }
}
