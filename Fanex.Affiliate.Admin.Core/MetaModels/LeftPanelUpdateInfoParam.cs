namespace Fanex.Affiliate.Admin.Core.MetaModels
{
    public class LeftPanelUpdateInfoParam
    {
        public int UserId { get; set; }

        public int AffId { get; set; }

        public string PreferredLanguage { get; set; }

        public bool? Close { get; set; }

        public byte? AffType { get; set; }

        public string Ip { get; set; }

        public int ErrCode { get; set; }

        public string ErrMsg { get; set; }
    }
}
