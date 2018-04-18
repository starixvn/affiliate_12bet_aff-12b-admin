using Fanex.Data;

namespace Fanex.Affiliate.User.Core.MetalModels
{
    public class UserLoginParam
    {
        public string UserName { get; set; }

        [SpParam(Ignored = true)]
        public string Password { get; set; }

        [SpParam(Ignored = true)]
        public string ValidationKey { get; set; }

        public string LoginIP { get; set; }

        public int ServerId { get; set; }

        public string Language { get; set; }

        public int ErrorCode { get; set; }

        public string ErrorMessage { get; set; }
    }
}