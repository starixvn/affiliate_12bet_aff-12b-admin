namespace Fanex.Affiliate.User.Core.Criterias
{
    using Data;
    using Data.Repository;
    using Utilities;

    public class GetCategoryListCriteria : CriteriaBase
    {
        private const string SpKey = "Aff_Admin_MediaCategory_Sel";

        public GetCategoryListCriteria(int typeId, int targetId, bool isInternal)
        {
            TypeId = typeId;
            TargetId = targetId;
            IsInternal = isInternal;
        }

        public int TypeId { get; set; }

        public int TargetId { get; set; }

        [SpParam(Ignored = true)]
        private bool IsInternal { get; set; }

        public override string GetSettingKey() => DbSettingHelper.BuildSpKey(IsInternal, SpKey);

        public override bool IsValid() => TypeId > 0 && TargetId > 0;
    }
}
