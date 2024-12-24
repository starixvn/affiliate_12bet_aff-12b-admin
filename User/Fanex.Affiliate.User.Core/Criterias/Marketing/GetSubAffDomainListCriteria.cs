namespace Fanex.Affiliate.User.Core.Criterias
{
    using Utilities;

    public class GetSubAffDomainListCriteria : BaseCriteria
    {
        private const string SpKey = "Aff_User_SubAffiliateDomain_Sel";

        public GetSubAffDomainListCriteria(bool isInternal)
        {
            IsInternal = isInternal;
        }

        public override string GetSettingKey() => DbSettingHelper.BuildSpKey(IsInternal, SpKey);

        public override bool IsValid() => true;
    }
}
