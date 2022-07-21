using Fanex.Affiliate.DataService.Importer.Core.Utilities;
using Fanex.Affiliate.DataService.Job.Model;
using Quartz;
using System;

namespace Fanex.Affiliate.DataService.Job.Modules.BigGaming
{
    public abstract class BigGamingScheduler : IJobScheduler
    {
        private readonly IAppSettings _appSetting;

        protected abstract string JobName { get; }
        protected abstract NewApiJobBase ProductJob { get; }

        protected BigGamingScheduler(IAppSettings appSetting = null)
        {
            _appSetting = appSetting ?? new AppSettings();
        }

        public ScheduleInfo BuildSchedule()
        {
            string groupName = $"{JobName}Group";
            var job = JobBuilder.Create<BigGamingJob>()
               .WithIdentity(JobName, groupName)
               .Build();

            var trigger = TriggerBuilder.Create()
              .WithIdentity(JobName + "Trigger", groupName)
              .StartNow()
              .WithSimpleSchedule(x => x
                  .WithIntervalInMinutes(Convert.ToInt32(_appSetting.GetValue("BigGamingIntervalTime")))
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
