using System;
using Fanex.Affiliate.DataService.Job.MetaModels.TimeTracker;
using Fanex.Affiliate.DataService.Job.Repository;

namespace Fanex.Affiliate.DataService.Job.Services
{
    public class TimeTracker : ITimeTracker
    {
        private readonly ITimeTrackerRepository _timeTrackerRepository;

        public TimeTracker(ITimeTrackerRepository timeTrackerRepository = null)
        {
            _timeTrackerRepository = timeTrackerRepository ?? new TimeTrackerRepository();
        }

        public DateTime GetLastestCallSuccess(TimeTrackerBaseParam param, DateTime defaultValue)
        {
            var latestTimeSuccess = _timeTrackerRepository.GetLastTimeCallSuccess(param);

            return latestTimeSuccess.HasValue ? latestTimeSuccess.Value : defaultValue;
        }

        public void SaveTime(InsertTimeTrackerParam param)
        {
            _timeTrackerRepository.SaveTime(param);
        }
    }
}
