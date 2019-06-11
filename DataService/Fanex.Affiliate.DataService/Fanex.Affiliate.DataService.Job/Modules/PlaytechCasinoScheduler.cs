using System.Collections.Generic;
using Fanex.Affiliate.DataService.Importer;
using Fanex.Affiliate.DataService.Job.Model;
using Quartz;
using Sunplus.UMControllerLibrary;

namespace Fanex.Affiliate.DataService.Job.Modules
{
    public class PlaytechCasinoScheduler : IJobScheduler
    {
        private readonly IDataImportingRunner dataImportingRunner;
        private readonly UMBusiness umBusiness;

        public PlaytechCasinoScheduler()
        {
            dataImportingRunner = new DataImportingRunner();
            umBusiness = new UMBusiness();
        }

        public ScheduleInfo BuildSchedule()
        {
            var job = JobBuilder.Create<PlaytechCasinoJob>()
                .UsingJobData(new JobDataMap(new Dictionary<string, IDataImportingRunner>
                {
                    { nameof(dataImportingRunner), dataImportingRunner}
                }))
                .UsingJobData(new JobDataMap(new Dictionary<string, UMBusiness>
                {
                    { nameof(umBusiness), umBusiness}
                })).Build();

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