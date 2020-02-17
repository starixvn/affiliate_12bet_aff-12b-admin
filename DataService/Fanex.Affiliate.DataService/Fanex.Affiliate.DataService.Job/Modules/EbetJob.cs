using System;
using System.Globalization;
using Fanex.Affiliate.DataService.Importer;
using Fanex.Affiliate.DataService.Importer.Core.Constants;
using Fanex.Affiliate.DataService.Importer.Core.Utilities;
using Fanex.Affiliate.DataService.Importer.Ebet.MetaModels;
using Fanex.Affiliate.DataService.Importer.Ebet.Utilities;
using Fanex.Affiliate.DataService.Job.MetaModels.TimeTracker;
using Fanex.Affiliate.DataService.Job.Services;
using Quartz;
using System.Threading.Tasks;

namespace Fanex.Affiliate.DataService.Job.Modules
{
    [DisallowConcurrentExecution]
    public class EbetJob : JobBase
    {
        private readonly IAppSettings _appSetting;
        private readonly IEbetService _ebetService;
        private readonly ITimeTracker _timeTracker;
        private readonly IEbetConfigHelper _ebetConfigHelper;

        public override string ServiceName => "Ebet Service";

        public EbetJob()
        {
            _appSetting = new AppSettings();
            _ebetService = new EbetService();
            _timeTracker = new TimeTracker();
            _ebetConfigHelper = new EbetConfigHelper();
        }

        protected override Task ImportDataToSystem(IJobExecutionContext context = null)
        {
            var defaultStartTime = DateTime.ParseExact(_appSetting.GetValue("EbetStartTime"), "yyyy/MM/dd HH:mm:ss", CultureInfo.InvariantCulture, DateTimeStyles.AssumeLocal);

            var hourAdjustment = Convert.ToInt32(_appSetting.GetValue("EbetHourAdjustment"));
            var now = DateTime.Now.AddHours(hourAdjustment);
            var nowByHour = new DateTime(now.Year, now.Month, now.Day, now.Hour, 0, 0, DateTimeKind.Local);

            var pageSize = Convert.ToInt32(_appSetting.GetValue("EbetPageSize"));
            var configList = _ebetConfigHelper.GetConfigList();
            foreach (var config in configList)
            {
                var currency = Convert.ToInt32(config.CurrencyID);
                var trackerParam = new TimeTrackerBaseParam
                {
                    ProductId = Products.EbetProductId,
                    Currency = currency
                };
                var lastQueryTime = _timeTracker.GetLastestCallSuccess(trackerParam, defaultStartTime);

                if (DateTime.Compare(lastQueryTime, nowByHour) < 0)
                {
                    var startTime = new DateTime(lastQueryTime.Year, lastQueryTime.Month, lastQueryTime.Day, lastQueryTime.Hour, lastQueryTime.Minute, lastQueryTime.Second);
                    var endTime = new DateTime(nowByHour.Year, nowByHour.Month, nowByHour.Day, nowByHour.Hour, nowByHour.Minute, nowByHour.Second);

                    for (var start = startTime; DateTime.Compare(start, endTime) < 0; start = start.AddHours(1))
                    {
                        var endTimeByBatch = start.AddHours(1);
                        var param = new EbetParam
                        {
                            UserName = string.Empty,
                            StartTime = start,
                            EndTime = DateTime.Compare(endTimeByBatch, endTime) > 0 ? endTime : endTimeByBatch,
                            ChannelId = config.ChannelId,
                            ApiUrl = config.ApiServerHost,
                            PrivateKey = config.PrivateKey,
                            CurrencyId = config.CurrencyID,
                            PageNumber = 1,
                            PageSize = pageSize,
                        };

                        Logger.Info($"{ServiceName} - Get data from {param.StartTime} to {param.EndTime}");
                        _ebetService.Import(param);

                        var saveTimeParam = new InsertTimeTrackerParam
                        {
                            ProductId = Products.EbetProductId,
                            Currency = currency,
                            CallTime = param.EndTime
                        };
                        _timeTracker.SaveTime(saveTimeParam);
                        Logger.Info("Finished get data");
                    }
                }
            }
            return Task.CompletedTask;
        }
    }
}