using Fanex.Affiliate.DataService.Importer.Core.Utilities;
using Fanex.Affiliate.DataService.Job.Constants;

using Quartz;
using Quartz.Impl;
using Quartz.Logging;

using System.Collections.Generic;
using System.Threading.Tasks;

namespace Fanex.Affiliate.DataService.Job
{
    public class JobSchedulerFactory : IJobSchedulerFactory
    {
        private ISchedulerFactory _schedulerFactory;

        private IList<IJobScheduler> _jobSchedulerList;
        public JobSchedulerFactory()
        {
            Init();
        }
        private void Init()
        {

            Quartz.Logging.LogProvider.IsDisabled = true;

#if DEBUG
            Quartz.Logging.LogProvider.IsDisabled = false;

            Quartz.Logging.LogProvider.SetCurrentLogProvider(new QuartzLogProvider());
#endif

            var appSetting = new AppSettings();
            _schedulerFactory = new StdSchedulerFactory();
            _jobSchedulerList = new List<IJobScheduler>();
            foreach (var product in appSetting.SupportedProductList)
            {
                if (JobSystem.JobHandlerList.ContainsKey(product))
                {
                    _jobSchedulerList.Add(JobSystem.JobHandlerList[product].Value);

                    var dailyJobKey = $"{product}Daily";
                    if (appSetting.ProductListHaveDailyJob.Contains(product) && JobSystem.JobHandlerList.ContainsKey(dailyJobKey))
                    {
                        _jobSchedulerList.Add(JobSystem.JobHandlerList[dailyJobKey].Value);
                    }
                }
            }
        }
        private async Task<IScheduler> GetSchedulerAsync() => await _schedulerFactory.GetScheduler().ConfigureAwait(true);

        public async Task SetUpAsync()
        {
            var scheduler = await GetSchedulerAsync();
            foreach (var item in _jobSchedulerList)
            {
                var job = item.BuildSchedule();
                await scheduler.ScheduleJob(job.Job, job.Trigger);
            }

            await scheduler.Start();
        }

        public async Task ShutdownAsync()
        {
            var scheduler = await _schedulerFactory.GetScheduler();

            HttpClientPooling.ClearAllHttpClient();
            await scheduler.Shutdown(true);
        }

        public async Task PauseAsync()
        {
            var scheduler = await GetSchedulerAsync();

            await scheduler.PauseAll();
        }

        public async Task ResumeAsync()
        {
            var scheduler = await GetSchedulerAsync();
            await scheduler.ResumeAll();
        }


    }
}