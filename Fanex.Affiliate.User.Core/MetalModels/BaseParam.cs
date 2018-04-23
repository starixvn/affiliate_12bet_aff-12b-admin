using Fanex.Data;

namespace Fanex.Affiliate.User.Core.MetalModels
{
    public class BaseParam
    {
        [SpParam(Ignored = true)]
        public bool IsInternal { get; set; }
    }
}