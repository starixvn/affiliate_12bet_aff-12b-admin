namespace Fanex.Affiliate.Admin.Core.MetaModels.CommissionGroup
{
    public class DeleteCommissionGroupParam
    {
        public int UserId { get; set; }

        public int GroupId { get; set; }

        public string Ip { get; set; }

        public int ErrCode { get; set; }

        public string ErrMsg { get; set; }
    }
}
