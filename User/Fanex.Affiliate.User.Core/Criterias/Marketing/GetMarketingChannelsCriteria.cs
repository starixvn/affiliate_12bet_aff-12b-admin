namespace Fanex.Affiliate.User.Core.Criterias
{
    using Data.Repository;

    public class GetMarketingChannelsCriteria : CriteriaBase
    {
        public GetMarketingChannelsCriteria(int userId)
        {
            AffId = userId;
        }

        public int AffId { get; set; }

        public override string GetSettingKey() => "Aff_User_AffMediaChannel_Sel";

        public override bool IsValid() => AffId > 0;
    }
}
