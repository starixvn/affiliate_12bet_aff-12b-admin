using Quartz;

namespace Fanex.Affiliate.DataService.Job.Model
{
    public class ScheduleInfo
    {
        public IJobDetail Job { get; set; }

        public ITrigger Trigger { get; set; }
    }
}
