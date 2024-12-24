namespace Fanex.Affiliate.User.Core.Criterias
{
    using Data;
    using Data.Repository;
    using Utilities;

    public class GetStatementHistoryCriteria : CriteriaBase
    {
        private const string SpKey = "Aff_User_StatementHistory_Sel";

        public int AffId { get; set; }

        public int BaseCurrency { get; set; }

        public int? PageSize { get; set; }

        public int? PageIndex { get; set; }

        public int NRows { get; set; }

        [SpParam(Ignored = true)]
        public bool IsInternal { get; set; }

        public override string GetSettingKey() => DbSettingHelper.BuildSpKey(IsInternal, SpKey);

        public override bool IsValid() => AffId > 0;
    }
}
