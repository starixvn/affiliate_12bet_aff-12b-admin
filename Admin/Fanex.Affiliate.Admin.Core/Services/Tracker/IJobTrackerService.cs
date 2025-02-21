using Fanex.Affiliate.Admin.Core.Commands.Tracker;
using Fanex.Affiliate.Admin.Core.DTO;

namespace Fanex.Affiliate.Admin.Core.Services.Tracker
{
    public interface IJobTrackerService
    {
        (long Id, ResultInfo Result) InsertDataTracker(InsertJobTrackerCommand command);

        ResultInfo UpdateDataTracker(UpdateJobTrackerCommand command);
    }
}
