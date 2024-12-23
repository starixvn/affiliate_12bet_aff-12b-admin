namespace Fanex.Affiliate.User.Core.Criterias
{
    using Data.Repository;

    public class GetUsingMarketingLinkCriteria : CriteriaBase
    {
        public GetUsingMarketingLinkCriteria(int userId)
        {
            AffId = userId;
        }

        public int AffId { get; set; }

        public override string GetSettingKey() => "Aff_User_AffMediaChannel_Statistics";

        public override bool IsValid() => AffId > 0;
    }
}
