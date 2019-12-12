using System.Collections.Generic;
using Fanex.Affiliate.DataService.Importer;
using Fanex.Affiliate.DataService.Job.Model;
using Quartz;

namespace Fanex.Affiliate.DataService.Job.Modules
{
    public class PlaytechCasinoScheduler : IJobScheduler
    {
        private readonly IDataImportingRunner dataImportingRunner;

        public PlaytechCasinoScheduler()
        {
            dataImportingRunner = new DataImportingRunner();
        }

        public ScheduleInfo BuildSchedule()
        {
            var job = JobBuilder.Create<PlaytechCasinoJob>()
                .UsingJobData(new JobDataMap(new Dictionary<string, IDataImportingRunner>
                {
                    { nameof(dataImportingRunner), dataImportingRunner}
                }))
                .Build();

            var trigger = TriggerBuilder.Create()
                    .WithSimpleSchedule(a => a.WithIntervalInHours(1)
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