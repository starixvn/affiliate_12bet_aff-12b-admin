using Fanex.Data;

namespace Fanex.Affiliate.User.Core.MetalModels.UserPanel
{
    public class SizeParam
    {
        public string Category { get; set; }

        public string SubCategory { get; set; }

        [SpParam(Ignored = true)]
        public string Language { get; set; }

        public string Languages { get; set; }

        public string Currency { get; set; }

        public string ImageType { get; set; }

        public byte DeviceType { get; set; }

        public string AffType { get; set; }
    }
}
