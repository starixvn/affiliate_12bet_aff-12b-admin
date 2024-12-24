namespace Fanex.Affiliate.Admin.Core.Commands.Tracker
{
    public class InsertJobTrackerCommand : BaseNonQueryCommand
    {
        private const string SP_NAME = "Task_API_PushDataTracker_Import";

        public override string GetSettingKey() => SP_NAME;

        public override bool IsValid() => true;

        public int PushDataType { get; set; }

        public string PushDataInfo { get; set; }

        public int PushTrackerId { get; set; }
    }
}