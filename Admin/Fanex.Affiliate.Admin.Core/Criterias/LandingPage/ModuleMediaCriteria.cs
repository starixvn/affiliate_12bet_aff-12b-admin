namespace Fanex.Affiliate.Admin.Core.Criterias.LandingPage
{
    public class ModuleMediaCriteria : PagingCriteria
    {
        private const string SP_NAME = "Aff_Admin_ModuleMedia_Sel";

        public override string GetSettingKey() => SP_NAME;

        public override bool IsValid() => true;

        public int? MediaId { get; set; }

        public int? ModuleId { get; set; }

        public string SearchTerm { get; set; }

        public string FileType { get; set; }

        public string SizeFilter { get; set; }

        public bool IncludeDeleted { get; set; }
    }
}
