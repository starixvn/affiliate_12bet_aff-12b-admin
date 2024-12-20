namespace Fanex.Affiliate.User.Core.Criterias
{
    using Data.Repository;

    public class GetGeneralAffInfoCriteria : CriteriaBase
    {
        public GetGeneralAffInfoCriteria(int? affId = null, string affCode = null)
        {
            AffId = affId;
            AffCode = affCode;
        }

        public int? AffId { get; set; }

        public string AffCode { get; set; }

        public override string GetSettingKey() => "Aff_Get_GeneralAffInfo";

        public override bool IsValid() => true;
    }
}
