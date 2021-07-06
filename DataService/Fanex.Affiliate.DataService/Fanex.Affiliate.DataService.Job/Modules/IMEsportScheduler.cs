using System;
using Fanex.Affiliate.DataService.Job.Model;
using Quartz;
using Fanex.Affiliate.DataService.Importer.Core.Utilities;

namespace Fanex.Affiliate.DataService.Job.Modules
{
    class IMEsportScheduler : IJobScheduler
    {
        private readonly IAppSettings _appSetting;

        public IMEsportScheduler(IAppSettings appSetting = null)
        {
            _appSetting = appSetting ?? new AppSettings();
        }

        public ScheduleInfo BuildSchedule()
        {
            const string groupName = "IMEsportGroupJob";
            const string jobName = nameof(IMEsportJob);
            var job = JobBuilder.Create<IMEsportJob>()
               .WithIdentity(jobName, groupName)
               .Build();

            var trigger = TriggerBuilder.Create()
              .WithIdentity(jobName + "Trigger", groupName)
              .StartNow()
              .WithSimpleSchedule(x => x
                  .WithIntervalInHours(Convert.ToInt32(_appSetting.GetValue("IMEsportIntervalTime")))
                  .RepeatForever())
              .Build();

            return new ScheduleInfo { Job = job, Trigger = trigger };
        }
    }
}
