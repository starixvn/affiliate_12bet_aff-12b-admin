namespace Fanex.Affiliate.DataService.Job.Modules.BigGaming
{
    public class BgLiveCasinoScheduler : BigGamingScheduler
    {
        protected override string JobName => "BgLiveCasinoJob";

        protected override NewApiJobBase ProductJob => new BgLiveCasinoJob();
    }
}
