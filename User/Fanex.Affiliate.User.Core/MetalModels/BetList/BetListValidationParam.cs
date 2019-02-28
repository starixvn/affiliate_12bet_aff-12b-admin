namespace Fanex.Affiliate.User.Core.MetalModels.BetList
{
    public class BetListValidationParam : BaseParam
    {
        public int AffId { get; set; }

        public string MemberName { get; set; }

        public bool BelongLevel1 { get; set; }

        public bool BelongLevel2 { get; set; }
    }
}
