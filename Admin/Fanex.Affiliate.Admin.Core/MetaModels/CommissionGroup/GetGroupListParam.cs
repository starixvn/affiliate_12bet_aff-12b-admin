using Fanex.Affiliate.Admin.Core.Constants;

namespace Fanex.Affiliate.Admin.Core.MetaModels.CommissionGroup
{
    public class GetGroupListParam
    {
        public int AffId { get; set; } = DefaultValue.AllId;

        public int GroupId { get; set; }

        public int GroupTypeId { get; set; } = DefaultValue.AllId;
    }
}
