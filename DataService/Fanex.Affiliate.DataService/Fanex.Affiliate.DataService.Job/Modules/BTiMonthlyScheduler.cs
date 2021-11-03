using Fanex.Affiliate.DataService.Job.Model;
using Quartz;

namespace Fanex.Affiliate.DataService.Job.Modules
{
    public class BTiMonthlyScheduler: IJobScheduler
    {
        public ScheduleInfo BuildSchedule()
        {
            const string groupName = "BTiMonthlyJobGroup";
            const string jobName = nameof(BTiMonthlyJob);
            var job = JobBuilder.Create<BTiMonthlyJob>()
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
