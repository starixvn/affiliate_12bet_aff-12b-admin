using Fanex.Affiliate.DataService.Job.Model;
using Quartz;
using Fanex.Affiliate.DataService.Importer.Core.Utilities;
using System;
using System.Globalization;

namespace Fanex.Affiliate.DataService.Job.Modules
{
    public class LBKenoScheduler : IJobScheduler
    {

        private readonly IAppSettings _appSetting;

        public LBKenoScheduler(IAppSettings appSetting = null)
        {
            _appSetting = appSetting ?? new AppSettings();
        }

        public ScheduleInfo BuildSchedule()
        {
            const string groupName = "LBKenoJobGroup";
            const string jobName = nameof(LBKenoJob);
            var job = JobBuilder.Create<LBKenoJob>()
               .WithIdentity(jobName, groupName)
               .Build();

            var trigger = TriggerBuilder.Create()
              .WithIdentity(jobName + "Trigger", groupName)
              .StartNow()
              .WithSimpleSchedule(x => x
                  .WithIntervalInMinutes(Convert.ToInt32(_appSetting.GetValue("LBKenoIntervalTime")))
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
