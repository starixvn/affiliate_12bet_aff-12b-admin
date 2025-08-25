namespace Fanex.Affiliate.Admin.Core.Commands.LandingPage
{
    public class UpdateModuleMediaCommand : BaseModuleMediaCommand
    {
        private const string SP_NAME = "Aff_Admin_ModuleMedia_Upd";

        public override string GetSettingKey() => SP_NAME;

        public override bool IsValid() => 
            MediaId > 0 && 
            !string.IsNullOrWhiteSpace(MediaName);

        public int MediaId { get; set; }
    }
}
