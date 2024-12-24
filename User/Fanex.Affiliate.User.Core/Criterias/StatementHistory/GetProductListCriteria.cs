namespace Fanex.Affiliate.User.Core.Criterias
{
    using Utilities;

    public class GetProductListCriteria : BaseCriteria
    {
        private const string SpKey = "Aff_ProductList_Sel";

        public GetProductListCriteria(bool isInternal)
        {
            IsInternal = isInternal;
        }

        public override string GetSettingKey() => DbSettingHelper.BuildSpKey(IsInternal, SpKey);

        public override bool IsValid() => true;
    }
}
