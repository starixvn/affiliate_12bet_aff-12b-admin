using Fanex.Affiliate.DataService.Job.Model;
using Quartz;

namespace Fanex.Affiliate.DataService.Job.Modules
{
    public class CrystalGpiDailyScheduler : IJobScheduler
    {
        public ScheduleInfo BuildSchedule()
        {
            const string groupName = "CrystalGpiDailyJobGroup";
            const string jobName = nameof(CrystalGpiDailyJob);
            var job = JobBuilder.Create<CrystalGpiDailyJob>()
               .WithIdentity(jobName, groupName)
               .Build();

            var trigger = TriggerBuilder.Create()
              .WithIdentity(jobName + "Trigger", groupName)
              .StartAt(DateBuilder.FutureDate(15, IntervalUnit.Minute))
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
