namespace Fanex.Affiliate.Admin.Core.MetaModels.Operation
{
    using Fanex.Affiliate.Admin.Core.Constants;

    public class GetNormalDealParam
    {
        public int AffId { get; set; } = DefaultValue.AllId;

        public int GroupTypeId { get; set; }

        public int GroupId { get; set; }

        public bool IsOnlyRevenue { get; set; } = true;
    }
}
