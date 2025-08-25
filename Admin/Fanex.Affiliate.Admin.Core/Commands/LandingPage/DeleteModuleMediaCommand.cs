namespace Fanex.Affiliate.Admin.Core.Commands.LandingPage
{
    public class DeleteModuleMediaCommand : BaseNonQueryCommand
    {
        private const string SP_NAME = "Aff_Admin_ModuleMedia_Del";

        public override string GetSettingKey() => SP_NAME;

        public override bool IsValid() => !string.IsNullOrWhiteSpace(MediaIds);

        public string MediaIds { get; set; }

        public bool PermanentDelete { get; set; }

        public string DeleteReason { get; set; }
    }
}
