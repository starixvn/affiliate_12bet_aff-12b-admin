namespace Fanex.Affiliate.User.Core.Criterias
{
    using Data.Repository;

    public class GetLastLoginInfoCriteria : CriteriaBase
    {
        public GetLastLoginInfoCriteria(int affId)
        {
            AffId = affId;
        }

        public int AffId { get; set; }

        public override string GetSettingKey() => "Aff_User_LoginInfo_Sel";

        public override bool IsValid() => AffId > 0;
    }
}
