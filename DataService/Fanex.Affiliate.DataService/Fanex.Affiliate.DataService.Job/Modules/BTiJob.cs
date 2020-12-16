using Fanex.Affiliate.DataService.Importer.BTi;
using Fanex.Affiliate.DataService.Importer.Core.Infrastructure;
using Fanex.Affiliate.DataService.Importer.Core.Utilities;
using Fanex.Affiliate.DataService.Job.Services;
using Quartz;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Fanex.Affiliate.DataService.Job.Modules
{
    [DisallowConcurrentExecution]
    public class BTiJob : NewApiJobBase
    {
        private readonly IAppSettings _appSetting;
        private readonly IImporterService _service;
        private readonly ITimeTracker _timeTracker;

        public BTiJob() : this(null, null, null)
        { }

        public BTiJob(IAppSettings appSettings, IImporterService importerService, ITimeTracker timeTracker)
        {
            _appSetting = appSettings ?? new AppSettings();
            _service = importerService ?? new BTiService();
            _timeTracker = timeTracker ?? new TimeTracker();
        }

        public override string ServiceName => throw new NotImplementedException();

        protected override DateTime GetEndTimeRequest()
        {
            throw new NotImplementedException();
        }

        protected override DateTime GetStartTimeRequest()
        {
            throw new NotImplementedException();
        }

        protected override IEnumerable<Tuple<DateTime, DateTime>> GetTimeRange(DateTime startTime, DateTime endTime)
        {
            throw new NotImplementedException();
        }

        protected override Task ImportDataAndTrackImportTime(DateTime startTime, DateTime endTime)
        {
            throw new NotImplementedException();
        }
    }
}
