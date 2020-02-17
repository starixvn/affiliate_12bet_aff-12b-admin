using System;
using Fanex.Affiliate.DataService.Job.Model;
using Quartz;
using Fanex.Affiliate.DataService.Importer.Core.Utilities;

namespace Fanex.Affiliate.DataService.Job.Modules
{
    public class CrystalGpiScheduler : IJobScheduler
    {
        private readonly IAppSettings _appSetting;

        public CrystalGpiScheduler(IAppSettings appSetting = null)
        {
            _appSetting = appSetting ?? new AppSettings();
        }

        public ScheduleInfo BuildSchedule()
        {
            const string groupName = "CrystalGpiJobGroup";
            const string jobName = nameof(CrystalGpiJob);
            var job = JobBuilder.Create<CrystalGpiJob>()
               .WithIdentity(jobName, groupName)
               .Build();

            var trigger = TriggerBuilder.Create()
              .WithIdentity(jobName + "Trigger", groupName)
              .StartAt(DateBuilder.FutureDate(2, IntervalUnit.Minute))
              .WithSimpleSchedule(x => x
                  .WithIntervalInMinutes(Convert.ToInt32(_appSetting.GetValue("CrystalGpiIntervalTime")))
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
