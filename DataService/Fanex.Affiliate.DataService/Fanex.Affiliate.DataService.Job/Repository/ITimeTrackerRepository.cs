using System;
using Fanex.Affiliate.DataService.Job.MetaModels.TimeTracker;

namespace Fanex.Affiliate.DataService.Job.Repository
{
    public interface ITimeTrackerRepository
    {
        void SaveTime(InsertTimeTrackerParam param);

        DateTime? GetLastTimeCallSuccess(TimeTrackerBaseParam param);
    }
}
