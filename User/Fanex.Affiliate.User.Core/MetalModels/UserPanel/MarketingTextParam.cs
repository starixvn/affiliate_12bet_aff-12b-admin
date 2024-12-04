using Fanex.Data;

namespace Fanex.Affiliate.User.Core.MetalModels.UserPanel
{
    public class MarketingTextParam : LinkBaseParam
    {
        public string Category { get; set; }

        public string Language { get; set; }

        public string Currency { get; set; }

        public string DeviceType { get; set; } = ((byte)Enum.DeviceType.PCDevice).ToString();

        public int? PageSize { get; set; }

        public int? PageIndex { get; set; }

        public byte TargetId { get; set; } = 1;

        [SpParam(Ignored = true)]
        public int TotalPages { get; set; }

        public int Nrows { get; set; }

        [SpParam(Ignored = true)]
        public string ScriptUrl { get; set; }
    }
}