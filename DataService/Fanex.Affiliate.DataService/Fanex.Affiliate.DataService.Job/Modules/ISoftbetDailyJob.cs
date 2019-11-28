using System;
using System.Globalization;
using System.Text;
using Fanex.Affiliate.DataService.Importer;
using Fanex.Logging;
using Quartz;
using Sunplus.UMControllerLibrary;
using System.Threading.Tasks;
using Fanex.Affiliate.DataService.Importer.ISoftbet.MetaModels;
using Fanex.Affiliate.DataService.Importer.Core.Utilities;

namespace Fanex.Affiliate.DataService.Job.Modules
{
    [DisallowConcurrentExecution]
    public class ISoftbetDailyJob : IJob
    {
        private readonly IAppSettings _appSetting;
        private readonly UMBusiness _umBusiness;
        private readonly IISoftbetService _isoftbetService;

        public ISoftbetDailyJob()
        {
            _appSetting = new AppSettings();
            _umBusiness = new UMBusiness();
            _isoftbetService = new ISoftbetService();
        }

        public Task Execute(IJobExecutionContext context)
        {
            var logBuilder = new StringBuilder();
            try
            {
                var umUrl = _appSetting.GetValue("LBUrl");
                var isUm = !string.IsNullOrEmpty(umUrl) && _umBusiness.isUnderMaintainence(Sites.Aff12Bet, umUrl);

                if (!isUm)
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

                        logBuilder.AppendLine($"ISoftbet Daily Job is starting. Request time {param.StartTime.ToString()} - {param.EndTime.ToString()}");

                        _isoftbetService.Import(param);

                        logBuilder.AppendLine("ISoftbet Daily Job Done");
                    }
                }
            }
            catch (Exception exception)
            {
                logBuilder.AppendLine(exception.Message);
                LogService.Error(logBuilder.ToString(), exception);
            }
            return Task.CompletedTask;
        }
    }
}