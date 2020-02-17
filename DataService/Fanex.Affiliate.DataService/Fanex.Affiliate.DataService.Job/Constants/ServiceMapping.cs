using System.Collections.Generic;

namespace Fanex.Affiliate.DataService.Job.Constants
{
    public static class ServiceMapping
    {
        public static IDictionary<string, string> ServiceFullName => new Dictionary<string, string>
        {
            {"Topaz", "Fanex.Affiliate.DataService.Job.Modules.TopazJob" },
            {"Playtech", "Fanex.Affiliate.DataService.Job.Modules.PlaytechCasinoJob" },
            {"Betsoft", "Fanex.Affiliate.DataService.Job.Modules.BetsoftJob" },
            {"Ebet", "Fanex.Affiliate.DataService.Job.Modules.EbetJob" },
            {"CrystalGpi", "Fanex.Affiliate.DataService.Job.Modules.CrystalGpiJob" },
            {"ISoftbet", "Fanex.Affiliate.DataService.Job.Modules.ISoftbetJob" },
            {"Opus", "Fanex.Affiliate.DataService.Job.Modules.OpusJob" },
            {"VrLottery", "Fanex.Affiliate.DataService.Job.Modules.ImOneVrLotteryJob" },
            {"GpiLottery", "Fanex.Affiliate.DataService.Job.Modules.GpiLotteryJob" },
            {"LBKeno", "Fanex.Affiliate.DataService.Job.Modules.LBKenoJob" },
            {"CrystalGpiDaily", "Fanex.Affiliate.DataService.Job.Modules.CrystalGpiDailyJob" },
            {"ISoftbetDaily", "Fanex.Affiliate.DataService.Job.Modules.ISoftbetDailyJob" },
        };
    }
}
