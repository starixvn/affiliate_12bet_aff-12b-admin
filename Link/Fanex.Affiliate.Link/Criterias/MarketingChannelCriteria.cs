using Fanex.Data.Repository;

namespace Fanex.Affiliate.Link.Criterias
{
    public class MarketingChannelCriteria : CriteriaBase
    {
        public MarketingChannelCriteria(int userId)
        {
            AffId = userId;
        }

        public int AffId { get; set; }

        public override string GetSettingKey() => "Aff_User_AffMediaChannel_Sel";

        public override bool IsValid() => AffId > 0;
    }
}