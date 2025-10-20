
namespace Fanex.Affiliate.Admin.Core.Criterias.Member
{
    public class MemberCriteria : PagingCriteria
    {
        const string SP_NAME = "Aff_Admin_MemberList_Sel";

        public override string GetSettingKey() => SP_NAME;

        public override bool IsValid() => PageIndex > 0 && PageSize > 0;

        public string AffUsername { get; set; }

        public string UserName { get; set; }

        public string IBCUserName { get; set; }

        public bool IsIncludeRemovedPlayer { get; set; }
    }
}
