using System.Collections.Generic;
using Fanex.Affiliate.DataService.Job.Modules;
using Quartz;
using Quartz.Impl;

namespace Fanex.Affiliate.DataService.Job
{
    public class JobSchedulerFactory : IJobSchedulerFactory
    {
        private readonly IScheduler _scheduler;

        private readonly IList<IJobScheduler> _jobSchedulerList;

        public JobSchedulerFactory()
        {
            var schedulerFactory = new StdSchedulerFactory();
            _scheduler = schedulerFactory.GetScheduler();

            _jobSchedulerList = new List<IJobScheduler>();

            _jobSchedulerList.Add(new PlaytechCasinoScheduler());
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