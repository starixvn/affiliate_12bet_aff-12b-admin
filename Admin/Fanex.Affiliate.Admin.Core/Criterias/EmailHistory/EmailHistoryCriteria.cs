namespace Fanex.Affiliate.Admin.Core.Criterias.EmailHistory
{
    public class EmailHistoryCriteria : PagingByDateCriteria
    {
        const string SP_NAME = "Aff_Admin_EmailHistory_Sel";

        public override string GetSettingKey() => SP_NAME;

        public override bool IsValid() => true;

        public int SearchById { get; set; }

        public bool IsGetAll { get; set; }

        public string SearchText { get; set; }
    }
}
