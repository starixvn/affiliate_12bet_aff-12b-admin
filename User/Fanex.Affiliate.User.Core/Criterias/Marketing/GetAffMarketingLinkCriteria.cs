using Fanex.Data.Repository;

namespace Fanex.Affiliate.User.Core.Criterias
{
    public class GetAffMarketingLinkCriteria : CriteriaBase
    {
        public GetAffMarketingLinkCriteria(int userId, string channelCode)
        {
            AffId = userId;
            ChannelCode = channelCode;
        }

        public int AffId { get; set; }

        public string ChannelCode { get; set; }

        public override string GetSettingKey() => "Aff_User_AffMediaLink_Sel";

        public override bool IsValid() => AffId > 0;
    }
}
