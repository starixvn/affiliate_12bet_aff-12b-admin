using System.Collections.Generic;
using Fanex.Affiliate.DataService.Importer.Utilities;
using Fanex.Affiliate.DataService.Job.Constants;
using Quartz;
using Quartz.Impl;

namespace Fanex.Affiliate.DataService.Job
{
    public class JobSchedulerFactory : IJobSchedulerFactory
    {
        private readonly IScheduler _scheduler;

        private readonly IList<IJobScheduler> _jobSchedulerList;

        private readonly IAppSettings _appSetting;

        public JobSchedulerFactory()
        {
            _appSetting = new AppSettings();
            var schedulerFactory = new StdSchedulerFactory();
            _scheduler = schedulerFactory.GetScheduler();

            _jobSchedulerList = new List<IJobScheduler>();
            _jobSchedulerList.Add(JobSystem.JobHandlerList["Ebet"].Value);

            //foreach (var product in _appSetting.SupportedProductList)
            //{
            //    if (JobSystem.JobHandlerList.ContainsKey(product))
            //    {
            //        _jobSchedulerList.Add(JobSystem.JobHandlerList[product].Value);
            //    }
            //}
        }

        public void SetUp()
        {
            foreach (var item in _jobSchedulerList)
            {
                var job = item.BuildSchedule();
                _scheduler.ScheduleJob(job.Job, job.Trigger);
            }

            _scheduler.Start();
        }

        public void Shutdown()
        {
            _scheduler.Shutdown(true);
        }

        public void Pause()
        {
            _scheduler.PauseAll();
        }

        public void Resume()
        {
            _scheduler.ResumeAll();
        }
    }
}