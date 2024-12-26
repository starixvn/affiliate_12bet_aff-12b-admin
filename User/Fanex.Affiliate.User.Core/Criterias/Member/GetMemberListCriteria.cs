namespace Fanex.Affiliate.User.Core.Criterias
{
    using Utilities;

    public class GetMemberListCriteria : AffPagingByDateCriteria
    {
        private const string SpKey = "Aff_MemberList_Sel";

        public bool IsGetAll { get; set; }

        public byte MemberType { get; set; }

        public override string GetSettingKey() => DbSettingHelper.BuildSpKey(IsInternal, SpKey);

        public override bool IsValid() => AffId > 0;
    }
}
