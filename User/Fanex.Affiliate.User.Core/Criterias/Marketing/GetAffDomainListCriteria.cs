namespace Fanex.Affiliate.User.Core.Criterias
{
    using Data;
    using Data.Repository;
    using Utilities;

    public class GetAffDomainListCriteria : CriteriaBase
    {
        private const string SpKey = "Aff_User_Domain_Sel";

        public GetAffDomainListCriteria(int affId, bool isInternal)
        {
            AffId = affId;
            IsInternal = isInternal;
        }

        public int AffId { get; set; }

        [SpParam(Ignored = true)]
        private bool IsInternal { get; set; }

        public override string GetSettingKey() => DbSettingHelper.BuildSpKey(IsInternal, SpKey);

        public override bool IsValid() => AffId > 0;
    }
}
