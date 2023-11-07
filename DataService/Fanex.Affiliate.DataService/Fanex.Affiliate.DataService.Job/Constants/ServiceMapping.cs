using System.Collections.Generic;

namespace Fanex.Affiliate.DataService.Job.Constants
{
    public static class ServiceMapping
    {
        public static IDictionary<string, string> ServiceFullName => new Dictionary<string, string>
        {
            {"Betsoft", "Fanex.Affiliate.DataService.Job.Modules.BetsoftJob" },
            {"Topaz", "Fanex.Affiliate.DataService.Job.Modules.TopazJob" },
            {"GpiLottery", "Fanex.Affiliate.DataService.Job.Modules.GpiLotteryJob" },
            //{"Playtech", "Fanex.Affiliate.DataService.Job.Modules.PlaytechCasinoJob" },
            //{"Ebet", "Fanex.Affiliate.DataService.Job.Modules.EbetJob" },
            //{"CrystalGpi", "Fanex.Affiliate.DataService.Job.Modules.CrystalGpiJob" },
            //{"VrLottery", "Fanex.Affiliate.DataService.Job.Modules.ImOneVrLotteryJob" },
            //{"LBKeno", "Fanex.Affiliate.DataService.Job.Modules.LBKenoJob" },
            //{"KYGaming", "Fanex.Affiliate.DataService.Job.Modules.KYGamingJob" },
            //{"BTi", "Fanex.Affiliate.DataService.Job.Modules.BTiJob" },
            //{"Skywind", "Fanex.Affiliate.DataService.Job.Modules.SkywindJob" },
            //{"Sunstone", "Fanex.Affiliate.DataService.Job.Modules.SunstoneJob" },
            //{"Jasper", "Fanex.Affiliate.DataService.Job.Modules.JasperJob" },
            //{"BgFish", "Fanex.Affiliate.DataService.Job.Modules.BigGaming.BgFishJob" },
            //{"BgLiveCasino", "Fanex.Affiliate.DataService.Job.Modules.BigGaming.BgLiveCasinoJob" },
            //{"Opus", "Fanex.Affiliate.DataService.Job.Modules.OpusJob" },
            //{"ISoftbet", "Fanex.Affiliate.DataService.Job.Modules.ISoftbetJob" },
            //{"BTiDaily", "Fanex.Affiliate.DataService.Job.Modules.BTiDailyJob" },
            //{"SkywindDaily", "Fanex.Affiliate.DataService.Job.Modules.SkywindDailyJob" },
            //{"ISoftbetDaily", "Fanex.Affiliate.DataService.Job.Modules.ISoftbetDailyJob" },
            //{"CrystalGpiDaily", "Fanex.Affiliate.DataService.Job.Modules.CrystalGpiDailyJob" },
        };
    }
}
