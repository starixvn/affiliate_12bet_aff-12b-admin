using Fanex.Data.Repository;

namespace Fanex.Affiliate.Admin.Core.Criterias.LandingPage
{
    public class ValidateModuleUniquenessCriteria : CriteriaBase
    {
        private const string SP_NAME = "Aff_Admin_LandingPageModule_ValidateUniqueness";

        public override string GetSettingKey() => SP_NAME;

        public override bool IsValid() => true;

        public string ModuleName { get; set; }

        public int ExcludeModuleId { get; set; }
    }
}
