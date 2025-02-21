namespace Fanex.Affiliate.User.Core.Criterias
{
    using Utilities;

    public class GetUserInfoByIdCriteria : BaseCriteria
    {
        private const string SpKey = "Aff_User_GeneralInfo_Sel";

        public GetUserInfoByIdCriteria(int affId, bool isInternal)
        {
            AffId = affId;
            IsInternal = isInternal;
        }

        public int AffId { get; set; }

        public override string GetSettingKey() => DbSettingHelper.BuildSpKey(IsInternal, SpKey);

        public override bool IsValid() => AffId > 0;
    }
}
