namespace Fanex.Affiliate.User.Core.Criterias
{
    using Data.Repository;

    public class GetNewsLetterByIdCriteria : CriteriaBase
    {
        public GetNewsLetterByIdCriteria(int id)
        {
            NewsletterId = id;
        }

        public int NewsletterId { get; set; }

        public override string GetSettingKey() => "Aff_Admin_Newsletter_Sel";

        public override bool IsValid() => NewsletterId > 0;
    }
}
