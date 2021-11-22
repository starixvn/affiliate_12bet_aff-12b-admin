using Fanex.Affiliate.DataService.Job.Constants;
using Fanex.Affiliate.DataService.Job.Model;
using Quartz;

namespace Fanex.Affiliate.DataService.Job.Modules
{
    public class SkywindDailyScheduler : IJobScheduler
    {
        public ScheduleInfo BuildSchedule()
        {
            const string groupName = "SkywindDailyJobGroup";
            const string jobName = nameof(SkywindDailyJob);
            var job = JobBuilder.Create<SkywindDailyJob>()
               .WithIdentity(jobName, groupName)
               .Build();

            var trigger = TriggerBuilder.Create()
              .WithIdentity(jobName + "Trigger", groupName)
              .WithCronSchedule(DateTimeConfig.CronScheduleDailyAtOneAM)
              .ForJob(jobName, groupName)
              .Build();

            return new ScheduleInfo
            {
                Job = job,
                Trigger = trigger
            };
        }
    }
}
