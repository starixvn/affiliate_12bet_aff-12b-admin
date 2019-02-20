using System;
using Fanex.Affiliate.DataService.Job.MetaModels.TimeTracker;

namespace Fanex.Affiliate.DataService.Job.Services
{
    public interface ITimeTracker
    {
        DateTime GetLastestCallSuccess(TimeTrackerBaseParam param, DateTime defaultValue);

        void SaveTime(InsertTimeTrackerParam param);
    }
}
