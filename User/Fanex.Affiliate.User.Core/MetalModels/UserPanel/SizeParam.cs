using Fanex.Data;

namespace Fanex.Affiliate.User.Core.MetalModels.UserPanel
{
    public class SizeParam
    {
        [SpParam(SqlParamName = "TargetProductId")]
        public string TargetProduct { get; set; }

        [SpParam(Ignored = true)]
        public string Language { get; set; }

        public string Languages { get; set; }

        public string AffType { get; set; }
    }
}
