using Fanex.Data;

namespace Fanex.Affiliate.Admin.Core.Commands.LandingPage
{
    public class UpdateLandingPageStatusCommand : BaseNonQueryCommand
    {
        private const string SP_NAME = "Aff_LandingPage_Builder_Upd";

        public override string GetSettingKey() => SP_NAME;

        public override bool IsValid() => PageId > 0;

        public int PageId { get; set; }

        public byte StatusId { get; set; }

        public bool IsUpdStatus { get; } = true;
    }
}
