using Fanex.Data.Repository;

namespace Fanex.Affiliate.Admin.Core.Criterias.LandingPage
{
    public class GetModuleCriteria : CriteriaBase
    {
        private const string SP_NAME = "Aff_LandingPage_ModuleSel";

        public override string GetSettingKey() => SP_NAME;

        public override bool IsValid() => true;

        public string ModuleTypes { get; set; }
    }
}
