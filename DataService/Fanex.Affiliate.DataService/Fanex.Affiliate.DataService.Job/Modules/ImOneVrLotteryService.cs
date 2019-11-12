using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Fanex.Affiliate.DataService.Job.Model;
using Fanex.Affiliate.DataService.CheckSum.Utilities;
using Quartz;

namespace Fanex.Affiliate.DataService.Job.Modules
{
    class ImOneVrLotteryService : IJobScheduler
    {
        private readonly IAppSettings _appSetting;

        public ImOneVrLotteryService(IAppSettings appSetting = null)
        {
            _appSetting = appSetting ?? new AppSettings();
        }

        public ScheduleInfo BuildSchedule()
        {
            const string groupName = "ImOneGroupJob";
            var jobName = nameof(ImOneVrLotteryJob);
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

            return new ScheduleInfo() { Job = job, Trigger = trigger };
        }
    }
}
