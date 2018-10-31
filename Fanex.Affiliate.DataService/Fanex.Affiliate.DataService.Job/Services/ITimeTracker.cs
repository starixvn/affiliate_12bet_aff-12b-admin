using System;

namespace Fanex.Affiliate.DataService.Job.Services
{
    public interface ITimeTracker
    {
        DateTime GetLastestCallSuccess();

        void SaveTime(DateTime callTime);
    }
}
