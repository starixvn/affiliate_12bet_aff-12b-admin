namespace Fanex.Affiliate.Admin.Core.Commands.LandingPage
{
    public class InsertLandingPageModuleCommand : BaseLandingPageModuleCommand
    {
        private const string SP_NAME = "Aff_Admin_LandingPageModule_Ins";

        public override string GetSettingKey() => SP_NAME;

        public override bool IsValid() => 
            !string.IsNullOrWhiteSpace(ModuleName) && 
            ModuleType >= 1 && 
            ModuleType <= 4;
    }
}
