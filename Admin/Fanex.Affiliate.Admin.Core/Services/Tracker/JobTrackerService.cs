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
            DynamicRepository.Execute(command);

            var resultInfo = new ResultInfo(command.ErrCode, command.ErrMsg);

            return (command.PushTrackerId, resultInfo);
        }

        public ResultInfo UpdateDataTracker(UpdateJobTrackerCommand command)
        {
            DynamicRepository.Execute(command);

            return new ResultInfo(command.ErrCode, command.ErrMsg);
        }
    }
}
