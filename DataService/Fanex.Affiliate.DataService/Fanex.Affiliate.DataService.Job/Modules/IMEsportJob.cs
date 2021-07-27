using Fanex.Affiliate.DataService.Importer;
using Fanex.Affiliate.DataService.Importer.Core.Constants;
using Fanex.Affiliate.DataService.Importer.Core.Infrastructure;
using Fanex.Affiliate.DataService.Importer.Core.Utilities;
using Fanex.Affiliate.DataService.Job.MetaModels.TimeTracker;
using Fanex.Affiliate.DataService.Job.Services;
using Quartz;
using System;
using System.Globalization;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Fanex.Affiliate.DataService.Job.Modules
{
    [DisallowConcurrentExecution]
    public class IMEsportJob : NewApiJobBase
    {
        private readonly IAppSettings _appSetting;
        private readonly IImporterService _service;
        private readonly ITimeTracker _timeTracker;

        public override string ServiceName => "IMEsport Serivce";
        public IMEsportJob()
        {
            _appSetting = new AppSettings();
            _service = new IMEsportService();
            _timeTracker = new TimeTracker();
        }

        protected override DateTime GetStartTimeRequest()
        {
            var param = new TimeTrackerBaseParam { ProductId = Products.IMEsport };
            var defaultStartTime = DateTime.ParseExact(_appSetting.GetValue("IMEsportStartTime"), "yyyy/MM/dd HH:mm:ss", CultureInfo.InvariantCulture, DateTimeStyles.AssumeLocal);
            var lastQueryTime = _timeTracker.GetLastestCallSuccess(param, defaultStartTime);
            return lastQueryTime;
        }

        protected override DateTime GetEndTimeRequest()
        {
            var ajustDay = int.Parse(_appSetting.GetValue("IMEsportDayAdjustment"));
            var now = DateTime.Now;
            var ajustTime = now.AddDays(ajustDay);
            return ajustTime;
        }

        protected override IEnumerable<Tuple<DateTime, DateTime>> GetTimeRange(DateTime startTime, DateTime endTime)
        {
            var timeRanges = DateTimeHelper.GetProportionTimeRange(startTime, endTime, TimeSpan.FromDays(1));
            return timeRanges;
        }

        protected override async Task ImportDataAndTrackImportTime(DateTime startTime, DateTime endTime)
        {
            Logger.Info($"{ServiceName} - Get data from {startTime} to {endTime}");
            await _service.ImportAsync(startTime, endTime);

            var saveTimeTrackerParam = new InsertTimeTrackerParam
            {
                ProductId = Products.IMEsport,
                CallTime = endTime
            };
            _timeTracker.SaveTime(saveTimeTrackerParam);
            Logger.Info($"{ServiceName} - Finish get data");
        }
    }
}
