using Fanex.Affiliate.DataService.Job.Model;

namespace Fanex.Affiliate.DataService.Job
{
    public interface IJobScheduler
    {
        ScheduleInfo BuildSchedule();
    }
}
