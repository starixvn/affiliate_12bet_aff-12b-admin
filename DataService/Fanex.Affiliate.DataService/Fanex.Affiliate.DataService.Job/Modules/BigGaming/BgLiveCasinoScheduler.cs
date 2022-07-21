using Quartz;

namespace Fanex.Affiliate.DataService.Job.Modules.BigGaming
{
    public class BgLiveCasinoScheduler : BigGamingScheduler
    {
        protected override string JobName => "BgLiveCasinoJob";

        protected override IJobDetail JobDetail => JobBuilder.Create<BgLiveCasinoJob>()
               .WithIdentity(JobName, $"{JobName}Group")
               .Build();
    }
}
