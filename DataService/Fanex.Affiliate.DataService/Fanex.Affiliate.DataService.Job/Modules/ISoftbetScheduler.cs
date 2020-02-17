using System;
using Fanex.Affiliate.DataService.Importer.Core.Utilities;
using Fanex.Affiliate.DataService.Job.Model;
using Quartz;

namespace Fanex.Affiliate.DataService.Job.Modules
{
    public class ISoftbetScheduler : IJobScheduler
    {
        private readonly IAppSettings _appSetting;

        public ISoftbetScheduler(IAppSettings appSetting = null)
        {
            _appSetting = appSetting ?? new AppSettings();
        }

        public ScheduleInfo BuildSchedule()
        {
            const string groupName = "ISoftbetJobGroup";
            const string jobName = nameof(ISoftbetJob);
            var job = JobBuilder.Create<ISoftbetJob>()
               .WithIdentity(jobName, groupName)
               .Build();

            var trigger = TriggerBuilder.Create()
              .WithIdentity(jobName + "Trigger", groupName)
              .StartAt(DateBuilder.FutureDate(4, IntervalUnit.Minute))
              .WithSimpleSchedule(x => x
                  .WithIntervalInMinutes(Convert.ToInt32(_appSetting.GetValue("ISoftbetIntervalTime")))
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
