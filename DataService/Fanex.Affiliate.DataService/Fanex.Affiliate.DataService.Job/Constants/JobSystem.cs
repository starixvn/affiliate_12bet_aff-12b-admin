using System;
using System.Collections.Generic;
using Fanex.Affiliate.DataService.Job.Modules;
using Fanex.Affiliate.DataService.Job.Modules.BigGaming;

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
                    { "Betsoft", new Lazy<IJobScheduler>(() => new BetsoftScheduler()) },
                    { "Topaz", new Lazy<IJobScheduler>(() => new TopazScheduler()) },
                    { "GpiLottery", new Lazy<IJobScheduler>(() => new GpiLotteryScheduler()) },
                    //{ "Playtech", new Lazy<IJobScheduler>(() => new PlaytechCasinoScheduler()) },
                    //{ "Ebet", new Lazy<IJobScheduler>(() => new EbetScheduler()) },
                    //{ "CrystalGpi", new Lazy<IJobScheduler>(() => new CrystalGpiScheduler()) },
                    //{ "CrystalGpiDaily", new Lazy<IJobScheduler>(() => new CrystalGpiDailyScheduler()) },
                    //{ "VrLottery", new Lazy<IJobScheduler>(() => new ImOneVrLotteryScheduler()) },
                    //{ "LBKeno", new Lazy<IJobScheduler>(() => new LBKenoScheduler()) },
                    //{ "KYGaming", new Lazy<IJobScheduler>(() => new KYGamingScheduler()) },
                    //{ "BTi", new Lazy<IJobScheduler>(() => new BTiScheduler()) },
                    //{ "IMEsport", new Lazy<IJobScheduler>(() => new IMEsportScheduler()) },
                    //{ "Skywind", new Lazy<IJobScheduler>(() => new SkywindScheduler()) },
                    //{ "Sunstone", new Lazy<IJobScheduler>(() => new SunstoneScheduler()) },
                    //{ "Jasper", new Lazy<IJobScheduler>(() => new JasperScheduler()) },
                    //{ "BgFish", new Lazy<IJobScheduler>(() => new BgFishScheduler()) },
                    //{ "BgLiveCasino", new Lazy<IJobScheduler>(() => new BgLiveCasinoScheduler()) },
                    //{ "Opus", new Lazy<IJobScheduler>(() => new OpusScheduler()) },
                    //{ "ISoftbet", new Lazy<IJobScheduler>(() => new ISoftbetScheduler()) },
                    //{ "ISoftbetDaily", new Lazy<IJobScheduler>(() => new ISoftbetDailyScheduler()) },
                    //{ "BTiDaily", new Lazy<IJobScheduler>(() => new BTiDailyScheduler()) },
                    //{ "SkywindDaily", new Lazy<IJobScheduler>(() => new SkywindDailyScheduler()) },
                };
            }
        }
    }
}