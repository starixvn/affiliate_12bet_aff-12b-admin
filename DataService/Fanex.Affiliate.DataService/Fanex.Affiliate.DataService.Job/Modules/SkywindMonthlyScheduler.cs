using Fanex.Affiliate.DataService.Job.Model;
using Quartz;

namespace Fanex.Affiliate.DataService.Job.Modules
{
    public class SkywindMonthlyScheduler : IJobScheduler
    {
        public ScheduleInfo BuildSchedule()
        {
            const string groupName = "SkywindMonthlyJobGroup";
            const string jobName = nameof(SkywindMonthlyJob);
            var job = JobBuilder.Create<SkywindMonthlyJob>()
               .WithIdentity(jobName, groupName)
               .Build();

            var trigger = TriggerBuilder.Create()
              .WithIdentity(jobName + "Trigger", groupName)
              .StartNow()
              .WithCalendarIntervalSchedule(c => c.WithIntervalInMonths(1))
              .Build();

            return new ScheduleInfo
            {
                Job = job,
                Trigger = trigger
            };
        }
    }
}
