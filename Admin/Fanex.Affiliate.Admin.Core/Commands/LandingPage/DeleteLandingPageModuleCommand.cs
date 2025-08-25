namespace Fanex.Affiliate.Admin.Core.Commands.LandingPage
{
    public class DeleteLandingPageModuleCommand : BaseNonQueryCommand
    {
        private const string SP_NAME = "Aff_Admin_LandingPageModule_Del";

        public override string GetSettingKey() => SP_NAME;

        public override bool IsValid() => !string.IsNullOrWhiteSpace(ModuleIds);

        public string ModuleIds { get; set; }
    }
}
