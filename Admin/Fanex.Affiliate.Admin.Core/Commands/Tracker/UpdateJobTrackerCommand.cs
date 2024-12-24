namespace Fanex.Affiliate.Admin.Core.Commands.Tracker
{
    public class UpdateJobTrackerCommand : BaseNonQueryCommand
    {
        private const string SP_NAME = "Task_API_PushDataTracker_Upd";

        public override string GetSettingKey() => SP_NAME;

        public override bool IsValid() => true;

        public int PushTrackerId { get; set; }

        public int StatusId { get; set; }
    }
}
