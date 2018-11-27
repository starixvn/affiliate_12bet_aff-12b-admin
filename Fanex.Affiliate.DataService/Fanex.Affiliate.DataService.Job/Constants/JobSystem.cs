using System;
using System.Collections.Generic;
using Fanex.Affiliate.DataService.Job.Modules;

namespace Fanex.Affiliate.DataService.Job.Constants
{
    public static class JobSystem
    {
        public static IDictionary<string, Lazy<IJobScheduler>> JobHandlerList
        {
            get
            {
                return new Dictionary<string, Lazy<IJobScheduler>>(StringComparer.InvariantCultureIgnoreCase)
                {
                    { "Playtech", new Lazy<IJobScheduler>(() => new PlaytechCasinoScheduler()) },
                    { "Betsoft", new Lazy<IJobScheduler>(() => new BetsoftScheduler()) },
                    { "Ebet", new Lazy<IJobScheduler>(() => new EbetScheduler()) },
                    { "Crystal", new Lazy<IJobScheduler>(() => new CrystalGpiScheduler()) },
                    { "ISoftbet", new Lazy<IJobScheduler>(() => new ISoftbetScheduler()) }
                };
            }
        }
    }
}