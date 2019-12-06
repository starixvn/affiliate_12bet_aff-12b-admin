using System.Collections.Generic;
using Fanex.Affiliate.DataService.Importer.Core.Utilities;
using Fanex.Affiliate.DataService.Job.Constants;
using Quartz;
using Quartz.Impl;
using System.Threading.Tasks;
using Quartz.Logging;
using System;

namespace Fanex.Affiliate.DataService.Job
{
    public class JobSchedulerFactory : IJobSchedulerFactory
    {
        private IScheduler _scheduler;

        private IList<IJobScheduler> _jobSchedulerList;

        public static async Task<IJobSchedulerFactory> CreateAsync()
        {
            var factory = new JobSchedulerFactory();
            await factory.Init();
            return factory;
        }

        private async Task Init()
        {
            LogProvider.SetCurrentLogProvider(new FanexLogProvider());
            var appSetting = new AppSettings();
            var schedulerFactory = new StdSchedulerFactory();
            this._scheduler = await schedulerFactory.GetScheduler();
            this._jobSchedulerList = new List<IJobScheduler>();
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

        public async void SetUp()
        {
            foreach (var item in _jobSchedulerList)
            {
                var job = item.BuildSchedule();
                await _scheduler.ScheduleJob(job.Job, job.Trigger);
            }

            await _scheduler.Start();
        }

        public async void Shutdown()
        {
            HttpClientPooling.ClearAllHttpClient();
            await _scheduler.Shutdown(true);
        }

        public async void Pause()
        {
            await _scheduler.PauseAll();
        }

        public async void Resume()
        {
            await _scheduler.ResumeAll();
        }

        private class FanexLogProvider: ILogProvider
        {
            public Logger GetLogger(string name)
            {
                return (level, func, exception, parameters) =>
                {
                    if (func == null)
                        return false;

                    var msg = func();
                    switch (level)
                    {
                        case LogLevel.Debug:
                            Logging.LogService.Debug(msg, parameters);
                            break;
                        case LogLevel.Error:
                            Logging.LogService.Error(msg, exception);
                            break;
                        case LogLevel.Fatal:
                            Logging.LogService.Critical(msg, parameters);
                            Logging.LogService.Error(msg, exception);
                            break;
                        case LogLevel.Info:
                            Logging.LogService.Info(msg, parameters);
                            break;
                        case LogLevel.Warn:
                            Logging.LogService.Warn(msg, parameters);
                            break;
                        default:
                            Logging.LogService.Debug(msg, parameters);
                            break;
                    }
                    return true;
                };
            }

            public IDisposable OpenMappedContext(string key, string value)
            {
                throw new NotImplementedException();
            }

            public IDisposable OpenNestedContext(string message)
            {
                throw new NotImplementedException();
            }
        }
    }
}