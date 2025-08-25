namespace Fanex.Affiliate.Admin.Core.Commands.LandingPage
{
    public class UpdateLandingPageModuleCommand : BaseLandingPageModuleCommand
    {
        private const string SP_NAME = "Aff_Admin_LandingPageModule_Upd";

        public override string GetSettingKey() => SP_NAME;

        public override bool IsValid() => 
            ModuleId > 0 && 
            !string.IsNullOrWhiteSpace(ModuleName) && 
            ModuleType >= 1 && 
            ModuleType <= 4;

        public int ModuleId { get; set; }
    }
}
