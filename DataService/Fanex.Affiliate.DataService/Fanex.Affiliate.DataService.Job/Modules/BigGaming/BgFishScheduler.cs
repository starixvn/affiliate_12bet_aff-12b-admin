namespace Fanex.Affiliate.DataService.Job.Modules.BigGaming
{
    public class BgFishScheduler : BigGamingScheduler
    {
        protected override string JobName => "BgFishJob";

        protected override NewApiJobBase ProductJob => new BgFishJob();
    }
}
