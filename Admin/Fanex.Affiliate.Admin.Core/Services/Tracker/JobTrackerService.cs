using Fanex.Affiliate.Admin.Core.Commands.Tracker;
using Fanex.Affiliate.Admin.Core.DTO;
using Fanex.Data.Repository;

namespace Fanex.Affiliate.Admin.Core.Services.Tracker
{
    public class JobTrackerService : BaseDynamicService, IJobTrackerService
    {
        public JobTrackerService(
            IDynamicRepository dynamicRepository) : base(dynamicRepository) { }

        public (int Id, ResultInfo Result) InsertDataTracker(InsertJobTrackerCommand command)
        {
            var resultInfo = ExecuteReturnResultInfo(command);

            return (command.PushTrackerId, resultInfo);
        }

        public ResultInfo UpdateDataTracker(UpdateJobTrackerCommand command)
        {
            return ExecuteReturnResultInfo(command);
        }
    }
}
