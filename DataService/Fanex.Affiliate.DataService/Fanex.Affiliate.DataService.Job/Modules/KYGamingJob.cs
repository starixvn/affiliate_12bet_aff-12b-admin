using Fanex.Affiliate.DataService.Importer.Core.Infrastructure;
using Fanex.Affiliate.DataService.Importer.Core.Utilities;
using Fanex.Affiliate.DataService.Importer.KYGaming;
using Fanex.Affiliate.DataService.Job.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fanex.Affiliate.DataService.Job.Modules
{
    public class KYGamingJob : NewApiJobBase
    {
        private readonly IAppSettings _appSetting;
        private readonly IImporterService _service;
        private readonly ITimeTracker _timeTracker;

        public KYGamingJob(IAppSettings appSettings, IImporterService importerService, ITimeTracker timeTracker)
        {
            _appSetting = appSettings ?? new AppSettings();
            _service = importerService ?? new KYGamingService();
            _timeTracker = timeTracker ?? new TimeTracker();
        }

        public override string ServiceName => "Ky Gaming Service";

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
