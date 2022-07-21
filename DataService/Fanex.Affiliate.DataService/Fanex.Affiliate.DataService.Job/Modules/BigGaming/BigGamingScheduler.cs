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
        protected abstract IJobDetail JobDetail { get; }

        protected BigGamingScheduler(IAppSettings appSetting = null)
        {
            _appSetting = appSetting ?? new AppSettings();
        }

        public ScheduleInfo BuildSchedule()
        {
            var trigger = TriggerBuilder.Create()
              .WithIdentity(JobName + "Trigger", $"{JobName}Group")
              .StartNow()
              .WithSimpleSchedule(x => x
                  .WithIntervalInMinutes(Convert.ToInt32(_appSetting.GetValue("BigGamingIntervalTime")))
                  .RepeatForever())
              .Build();

            return new ScheduleInfo
            {
                Job = JobDetail,
                Trigger = trigger
            };
        }
    }
}
