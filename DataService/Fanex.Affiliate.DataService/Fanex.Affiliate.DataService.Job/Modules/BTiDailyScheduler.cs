using Fanex.Affiliate.DataService.Job.Constants;
using Fanex.Affiliate.DataService.Job.Model;
using Quartz;

namespace Fanex.Affiliate.DataService.Job.Modules
{
    public class BTiDailyScheduler: IJobScheduler
    {
        public ScheduleInfo BuildSchedule()
        {
            const string groupName = "BTiDailyJobGroup";
            const string jobName = nameof(BTiDailyJob);
            var job = JobBuilder.Create<BTiDailyJob>()
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
