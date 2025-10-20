namespace Fanex.Affiliate.Admin.Core.Commands.LandingPage
{
    public class InsertLandingPageDealCommand : BaseNonQueryCommand
    {
        private const string SP_NAME = "Aff_LandingPage_Deal_Ins";

        public override string GetSettingKey() => SP_NAME;

        public override bool IsValid() => DealId > 0;

        public int DealId { get; set; }
    }
}
