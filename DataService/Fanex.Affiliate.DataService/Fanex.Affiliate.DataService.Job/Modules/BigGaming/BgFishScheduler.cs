using Quartz;

namespace Fanex.Affiliate.DataService.Job.Modules.BigGaming
{
    public class BgFishScheduler : BigGamingScheduler
    {
        protected override string JobName => "BgFishJob";

        protected override IJobDetail JobDetail => JobBuilder.Create<BgFishJob>()
               .WithIdentity(JobName, $"{JobName}Group")
               .Build();
    }
}
