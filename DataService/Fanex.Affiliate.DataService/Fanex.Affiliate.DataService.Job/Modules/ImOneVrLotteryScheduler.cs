using System;
using Fanex.Affiliate.DataService.Job.Model;
using Quartz;
using Fanex.Affiliate.DataService.Importer.Core.Utilities;

namespace Fanex.Affiliate.DataService.Job.Modules
{
    class ImOneVrLotteryScheduler : IJobScheduler
    {
        private readonly IAppSettings _appSetting;

        public ImOneVrLotteryScheduler(IAppSettings appSetting = null)
        {
            _appSetting = appSetting ?? new AppSettings();
        }

        public ScheduleInfo BuildSchedule()
        {
            const string groupName = "ImOneGroupJob";
            const string jobName = nameof(ImOneVrLotteryJob);
            var job = JobBuilder.Create<ImOneVrLotteryJob>()
               .WithIdentity(jobName, groupName)
               .Build();

            var trigger = TriggerBuilder.Create()
              .WithIdentity(jobName + "Trigger", groupName)
              .StartNow()
              .WithSimpleSchedule(x => x
                  .WithIntervalInMinutes(Convert.ToInt32(_appSetting.GetValue("VrLotteryIntervalTime")))
                  .RepeatForever())
              .Build();

            return new ScheduleInfo { Job = job, Trigger = trigger };
        }
    }
}
