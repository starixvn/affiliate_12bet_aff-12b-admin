namespace Fanex.Affiliate.User.Core.Criterias
{
    using Utilities;

    public class GetStatementHistoryCriteria : AffPagingCriteria
    {
        private const string SpKey = "Aff_User_StatementHistory_Sel";

        public int BaseCurrency { get; set; }

        public override string GetSettingKey() => DbSettingHelper.BuildSpKey(IsInternal, SpKey);

        public override bool IsValid() => AffId > 0;
    }
}
