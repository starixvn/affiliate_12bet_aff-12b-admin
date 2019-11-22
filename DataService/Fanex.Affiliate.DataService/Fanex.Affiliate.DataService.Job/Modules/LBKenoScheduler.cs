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
            var jobName = nameof(LBKenoJob);
            var job = JobBuilder.Create<LBKenoJob>()
               .WithIdentity(jobName, groupName)
               .Build();

            var trigger = TriggerBuilder.Create()
              .WithIdentity(jobName + "Trigger", groupName)
              .StartNow()
              .WithDailyTimeIntervalSchedule(builder => builder
                    .StartingDailyAt(new TimeOfDay(6,0)))
              .Build();

            return new ScheduleInfo
            {
                Job = job,
                Trigger = trigger
            };
        }
    }
}
