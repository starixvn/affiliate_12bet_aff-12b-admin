using Fanex.Data;

namespace Fanex.Affiliate.User.Core.MetalModels.UserPanel
{
    public class MarketingTextParam : LinkBaseParam
    {
        public string Category { get; set; }

        public string Language { get; set; }

        public string Currency { get; set; }

        public byte? DeviceType { get; set; }

        public int? PageSize { get; set; }

        public int? PageIndex { get; set; }

        public int TotalPages { get; set; }

        public int Nrows { get; set; }

        [SpParam(Ignored = true)]
        public string ScriptUrl { get; set; }
    }
}
