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
                    { "CrystalGpi", new Lazy<IJobScheduler>(() => new CrystalGpiScheduler()) },
                    { "CrystalGpiDaily", new Lazy<IJobScheduler>(() => new CrystalGpiDailyScheduler()) },
                    { "ISoftbet", new Lazy<IJobScheduler>(() => new ISoftbetScheduler()) },
                    { "ISoftbetDaily", new Lazy<IJobScheduler>(() => new ISoftbetDailyScheduler()) },
                    { "Opus", new Lazy<IJobScheduler>(() => new OpusScheduler()) },
                    { "Topaz", new Lazy<IJobScheduler>(() => new TopazScheduler()) },
                    { "VrLottery", new Lazy<IJobScheduler>(() => new ImOneVrLotteryScheduler()) },
                    { "GpiLottery", new Lazy<IJobScheduler>(() => new GpiLotteryScheduler()) },
                    { "LBKeno", new Lazy<IJobScheduler>(() => new LBKenoScheduler()) }
                };
            }
        }
    }
}