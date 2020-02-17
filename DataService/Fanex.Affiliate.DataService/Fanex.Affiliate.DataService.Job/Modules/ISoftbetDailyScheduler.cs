using Fanex.Affiliate.DataService.Job.Model;
using Quartz;

namespace Fanex.Affiliate.DataService.Job.Modules
{
    public class ISoftbetDailyScheduler : IJobScheduler
    {
        public ScheduleInfo BuildSchedule()
        {
            const string groupName = "ISoftbetDailyJobGroup";
            const string jobName = nameof(ISoftbetDailyJob);
            var job = JobBuilder.Create<ISoftbetDailyJob>()
               .WithIdentity(jobName, groupName)
               .Build();

            var trigger = TriggerBuilder.Create()
              .WithIdentity(jobName + "Trigger", groupName)
              .StartAt(DateBuilder.FutureDate(20, IntervalUnit.Minute))
              .WithSimpleSchedule(x => x
                  .WithIntervalInHours(12)
                  .RepeatForever())
              .Build();

            return new ScheduleInfo
            {
                Job = job,
                Trigger = trigger
            };
        }
    }
}
