using Fanex.Affiliate.User.Core.Constants;

namespace Fanex.Affiliate.User.Core.Criterias.Marketing
{
    public class GetAvailableSpecificDomainCriteria : BaseCriteria
    {
        const string SP_NAME = "Aff_Admin_SpecificDomainControl_Sel";

        public GetAvailableSpecificDomainCriteria(string affUsername, bool isInternal)
        {
            AffUsername = affUsername;
            IsInternal = isInternal;
        }

        public override string GetSettingKey() => SP_NAME;

        public override bool IsValid() => true;

        public string AffUsername { get; set; }

        public int PageIndex { get; } = Number.One;

        public int PageSize { get; } = Number.OneHundred;
    }
}
