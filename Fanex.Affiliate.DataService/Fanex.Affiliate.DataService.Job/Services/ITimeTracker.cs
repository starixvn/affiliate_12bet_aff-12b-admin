using System;

namespace Fanex.Affiliate.DataService.Job.Services
{
    public interface ITimeTracker
    {
        DateTime GetLastestCallSuccess(string folderPath, DateTime defaultValue);

        void SaveTime(string folderPath, DateTime callTime);
    }
}
