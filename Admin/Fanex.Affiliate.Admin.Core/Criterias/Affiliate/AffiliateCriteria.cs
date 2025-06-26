
namespace Fanex.Affiliate.Admin.Core.Criterias.Affiliate
{
    public class AffiliateCriteria : PagingCriteria
    {
        private const string SP_NAME = "Aff_Admin_Affiliate_Sel";

        public override string GetSettingKey() => SP_NAME;

        public override bool IsValid() => true;

        public string AffCode { get; set; }

        public int UserId { get; set; }

        public string GroupIds { get; set; }

        public string TargetCountryCode { get; set; }

        public string PreferredLanguage { get; set; }

        public string Status { get; set; }

        public string ParentAffName { get; set; }

        public string UserName { get; set; }

        public string Player_UserName { get; set; }

        public string Email { get; set; }

        public byte SortType { get; set; }

        public bool SortDirectionType { get; set; }

        public bool IsExportExcel { get; set; }

        public bool IsExcludeMarketingAccount { get; set; }
    }
}
