using Fanex.Data.Repository;

namespace Fanex.Affiliate.Admin.Core.Criterias.LandingPage
{
    public class LandingPageModuleCriteria : CriteriaBase
    {
        private const string SP_NAME = "Aff_Admin_LandingPageModule_Sel";

        public override string GetSettingKey() => SP_NAME;

        public override bool IsValid() => true;

        public int? ModuleId { get; set; }

        public int? ModuleType { get; set; }

        public string ModuleName { get; set; }

        public bool? IsRequired { get; set; }

        public bool? IsUnique { get; set; }

        public bool? IsActive { get; set; }
    }
}
