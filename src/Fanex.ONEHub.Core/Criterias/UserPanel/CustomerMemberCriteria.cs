namespace Fanex.Affiliate.Admin.Core.Criterias.UserPanel
{
    public class CustomerMemberCriteria : AffPagingByDateCriteria
    {
        const string SP_NAME = "Aff_Admin_Member_Sel";

        public override string GetSettingKey() => SP_NAME;

        public override bool IsValid() => AffId > 0;

        public byte MemberType { get; set; }

        public bool IsGetAll { get; set; }
    }
}
