namespace Fanex.Affiliate.Admin.Core.Criterias.Report
{
    public class TransferReportCriteria : PagingByDateCriteria
    {
        const string SP_NAME = "Aff_TransferBalanceHistory_Sel";

        public override string GetSettingKey() => SP_NAME;

        public override bool IsValid() => true;

        public string TransId { get; set; }

        public string AffName { get; set; }

        public string PlayerName { get; set; }

        public bool IsExportExcel { get; set; }

        public int BaseCurrency { get; set; }

        public decimal? TotalBasedAmount { get; set; }
    }
}
