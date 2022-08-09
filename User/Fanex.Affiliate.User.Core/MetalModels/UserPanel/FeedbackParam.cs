
namespace Fanex.Affiliate.User.Core.MetalModels.UserPanel
{
    public class FeedbackParam : BaseParam
    {
        public int AffId { get; set; }

        public string Ip { get; set; }

        public byte Rate { get; set; }

        public string Message { get; set; }

        public int ErrCode { get; set; }

        public string ErrMsg { get; set; }
    }
}
