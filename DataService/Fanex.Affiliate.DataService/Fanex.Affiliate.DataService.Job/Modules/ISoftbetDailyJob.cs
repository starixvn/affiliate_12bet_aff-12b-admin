using System;
using System.Globalization;
using System.Text;
using Fanex.Affiliate.DataService.Importer;
using Fanex.Logging;
using Quartz;
using System.Threading.Tasks;
using Fanex.Affiliate.DataService.Importer.ISoftbet.MetaModels;
using Fanex.Affiliate.DataService.Importer.Core.Utilities;

namespace Fanex.Affiliate.DataService.Job.Modules
{
    [DisallowConcurrentExecution]
    public class ISoftbetDailyJob : JobBase
    {
        private readonly IAppSettings _appSetting;
        private readonly IISoftbetService _isoftbetService;

        public ISoftbetDailyJob()
        {
            _appSetting = new AppSettings();
            _isoftbetService = new ISoftbetService();
        }

        protected override Task ImportDataToSystem(IJobExecutionContext context = null)
        {
            var now = DateTime.Now;
            var nowByHour = new DateTime(now.Year, now.Month, now.Day, now.Hour, 0, 0, DateTimeKind.Local);
            var minTime = DateTime.ParseExact(_appSetting.GetValue("ISoftbetStartTime"), "yyyy/MM/dd HH:mm:ss", CultureInfo.InvariantCulture, DateTimeStyles.AssumeLocal);

            if (DateTime.Compare(minTime, nowByHour) < 0)
            {
                var param = new ISoftbetEntryParam
                {
                    StartTime = nowByHour.AddDays(-1).Date,
                    EndTime = nowByHour,
                    PageNumber = 1,
                    JobType = 1
                };

                LogService.Info($"ISoftbet Daily Job is starting. Request time {param.StartTime} - {param.EndTime}");

                _isoftbetService.Import(param);

                LogService.Info("ISoftbet Daily Job Done");
            }
            return Task.CompletedTask;
        }
    }
}