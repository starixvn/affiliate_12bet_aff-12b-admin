namespace Fanex.Affiliate.Admin.Core.MetaModels.CommissionGroup
{
    public class InsertConmissionGroupParam
    {
        public int UserId { get; set; }

        public string GroupName { get; set; }

        public string Description { get; set; }

        public string DeductionList { get; set; }

        public string TierList { get; set; }

        public decimal QmTurnover { get; set; }

        public string Ip { get; set; }

        public string Language { get; set; }

        public int ErrCode { get; set; }

        public string ErrMsg { get; set; }
    }
}
