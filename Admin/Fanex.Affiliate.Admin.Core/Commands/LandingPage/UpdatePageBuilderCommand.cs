using Fanex.Data;

namespace Fanex.Affiliate.Admin.Core.Commands.LandingPage
{
    public class UpdatePageBuilderCommand : InsertPageBuilderCommand
    {
        private const string SP_NAME = "Aff_LandingPage_Builder_Upd";

        [SpParam(Ignored = true)]
        public override bool IsPreview { get; set; }

        public override string GetSettingKey() => SP_NAME;

        public override bool IsValid() => 
            PageId > 0 && 
            !string.IsNullOrWhiteSpace(PageCode);
    }
}
