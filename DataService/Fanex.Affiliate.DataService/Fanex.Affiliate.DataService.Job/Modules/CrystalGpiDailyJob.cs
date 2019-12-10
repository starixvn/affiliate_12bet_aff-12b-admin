using System;
using System.Globalization;
using Fanex.Affiliate.DataService.Importer;
using Fanex.Logging;
using Quartz;
using Fanex.Affiliate.DataService.Importer.Gpi.MetaModels;
using System.Threading.Tasks;
using Fanex.Affiliate.DataService.Importer.Core.Utilities;

namespace Fanex.Affiliate.DataService.Job.Modules
{
    [DisallowConcurrentExecution]
    public class CrystalGpiDailyJob : JobBase
    {
        private readonly IAppSettings _appSetting;
        private readonly ICrystalGpiService _crystalGpiService;

        public CrystalGpiDailyJob()
        {
            _appSetting = new AppSettings();
            _crystalGpiService = new CrystalGpiService();
        }

        protected override Task ImportDataToSystem(IJobExecutionContext context = null)
        {
            var now = DateTime.Now;
            var nowByHour = new DateTime(now.Year, now.Month, now.Day, now.Hour, 0, 0, DateTimeKind.Local);
            var minTime = DateTime.ParseExact(_appSetting.GetValue("CrystalGpiStartTime"), "yyyy/MM/dd HH:mm:ss", CultureInfo.InvariantCulture, DateTimeStyles.AssumeLocal);

            if (DateTime.Compare(minTime, nowByHour) < 0)
            {
                var param = new CrystalGpiParam
                {
                    StartTime = nowByHour.AddDays(-1).Date,
                    EndTime = nowByHour,
                    PageNumber = 1,
                    PageSize = Convert.ToInt32(_appSetting.GetValue("CrystalGpiPageSize")),
                    JobType = 1
                };

                Logger.Info($"Crystal Gpi Daily Job is starting. Request time {param.StartTime} - {param.EndTime}");

                _crystalGpiService.Import(param);

                Logger.Info("Crystal Gpi Daily Job Done");
            }
            return Task.CompletedTask;
        }
    }
}
