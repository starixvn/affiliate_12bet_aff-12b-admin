namespace Fanex.Affiliate.User.Core.Criterias
{
    using Data.Repository;

    public class GetSessionIdCriteria : CriteriaBase
    {
        public GetSessionIdCriteria(int onUserId)
        {
            OnUserId = onUserId;
        }

        public int OnUserId { get; set; }

        public override string GetSettingKey() => "Aff_SessionIdSel";

        public override bool IsValid() => OnUserId > 0;
    }
}
