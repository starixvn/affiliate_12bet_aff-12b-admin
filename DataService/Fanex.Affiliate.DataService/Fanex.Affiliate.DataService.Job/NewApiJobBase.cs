using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Quartz;

namespace Fanex.Affiliate.DataService.Job
{
    public abstract class NewApiJobBase : JobBase
    {
        protected override async Task ImportDataToSystem(IJobExecutionContext context = null)
        {
            var startTime = GetStartTimeRequest();
            var endTime = GetEndTimeRequest();
            var timeRanges = GetTimeRange(startTime, endTime);
            foreach (var range in timeRanges)
            {
                await ImportDataToSystem(range.Item1, range.Item2);
            }
        }

        protected Task ImportDataToSystem(DateTime startTime, DateTime endTime)
        {
            try
            {
                return ImportDataAndTrackImportTime(startTime, endTime);
            }
            catch (Exception ex)
            {
                Logger.Error($"{ex.Message} when import data at start time {startTime} and end time {endTime}", ex);

                return Task.FromException(ex);
            }
        }

        protected abstract DateTime GetStartTimeRequest();
        protected abstract DateTime GetEndTimeRequest();
        protected abstract IEnumerable<Tuple<DateTime, DateTime>> GetTimeRange(DateTime startTime, DateTime endTime);

        protected abstract Task ImportDataAndTrackImportTime(DateTime startTime, DateTime endTime);
    }
}
