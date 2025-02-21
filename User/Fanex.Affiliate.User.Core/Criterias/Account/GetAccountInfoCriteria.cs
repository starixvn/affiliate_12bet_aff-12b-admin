namespace Fanex.Affiliate.User.Core.Criterias
{
    using Data.Repository;

    public class GetAccountInfoCriteria : CriteriaBase
    {
        public GetAccountInfoCriteria(int affId)
        {
            AffId = affId;
        }

        public int AffId { get; set; }

        public override string GetSettingKey() => "Aff_User_UserInfoSel";

        public override bool IsValid() => AffId > 0;
    }
}
