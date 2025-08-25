using Fanex.Data.Repository;

namespace Fanex.Affiliate.Admin.Core.Criterias.LandingPage
{
    public class ModuleMediaStatisticsCriteria : CriteriaBase
    {
        private const string SP_NAME = "Aff_Admin_ModuleMedia_Statistics";

        public override string GetSettingKey() => SP_NAME;

        public override bool IsValid() => true;

        public int ModuleId { get; set; }

        public bool IncludeDeleted { get; set; }
    }
}
