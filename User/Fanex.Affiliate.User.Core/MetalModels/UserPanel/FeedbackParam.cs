using Fanex.Data;

namespace Fanex.Affiliate.User.Core.MetalModels.UserPanel
{
    public class FeedbackParam : BaseParam
    {
        private const int MaxRangeOfRate = 5;

        public int AffId { get; set; }

        public string Ip { get; set; }

        public int RateId { get; set; }

        public string Message { get; set; }

        public int ErrCode { get; set; }

        public string ErrMsg { get; set; }

        [SpParam(Ignored = true)]
        public bool IsValid => (RateId >= 0 && RateId <= MaxRangeOfRate) || !string.IsNullOrEmpty(Message);
    }
}
