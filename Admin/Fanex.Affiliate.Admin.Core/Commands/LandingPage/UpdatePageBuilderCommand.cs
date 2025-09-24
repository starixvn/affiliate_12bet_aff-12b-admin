namespace Fanex.Affiliate.Admin.Core.Commands.LandingPage
{
    public class UpdatePageBuilderCommand : BasePageBuilderCommand
    {
        private const string SP_NAME = "Aff_LandingPage_Builder_Upd";

        public override string GetSettingKey() => SP_NAME;

        public override bool IsValid() => 
            PageId > 0;
    }
}
