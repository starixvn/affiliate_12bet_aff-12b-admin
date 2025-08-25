namespace Fanex.Affiliate.Admin.Core.Commands.LandingPage
{
    public class InsertModuleMediaCommand : BaseModuleMediaCommand
    {
        private const string SP_NAME = "Aff_Admin_ModuleMedia_Ins";

        public override string GetSettingKey() => SP_NAME;

        public override bool IsValid() => 
            ModuleId > 0 && 
            !string.IsNullOrWhiteSpace(MediaName) && 
            !string.IsNullOrWhiteSpace(FileName);
    }
}
