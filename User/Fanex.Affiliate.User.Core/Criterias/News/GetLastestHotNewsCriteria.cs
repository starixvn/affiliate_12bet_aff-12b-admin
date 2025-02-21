namespace Fanex.Affiliate.User.Core.Criterias
{
    using Utilities;

    public class GetLastestHotNewsCriteria : BaseCriteria
    {
        private const string SpKey = "Aff_HotNewsDetail";

        public GetLastestHotNewsCriteria(bool isInternal)
        {
            IsInternal = isInternal;
        }

        public override string GetSettingKey() => DbSettingHelper.BuildSpKey(IsInternal, SpKey);

        public override bool IsValid() => true;
    }
}
