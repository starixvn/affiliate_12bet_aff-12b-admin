using System;
using Fanex.Data;

namespace Fanex.Affiliate.User.Core.MetalModels
{
    [Serializable]
    public class BaseParam
    {
        [SpParam(Ignored = true)]
        public bool IsInternal { get; set; }
    }
}