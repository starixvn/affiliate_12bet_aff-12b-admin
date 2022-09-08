using Fanex.Data;
using System.ComponentModel.DataAnnotations;

namespace Fanex.Affiliate.User.Core.MetalModels.UserPanel
{
    public class FeedbackParam : BaseParam
    {
        private const int None = 0;
        private const int Dissatisfaction = 1;
        private const int VeryPlease = 5;
        private const int MaxLength = 4000;

        public int AffId { get; set; }

        public string Ip { get; set; }

        [Range(None, VeryPlease)]
        public int RateId { get; set; }

        [MaxLength(MaxLength)]
        public string Message { get; set; }

        public int ErrCode { get; set; }

        public string ErrMsg { get; set; }

        [SpParam(Ignored = true)]
        public bool IsClickLogout { get; set; }

        [SpParam(Ignored = true)]
        public bool IsValid => (RateId >= Dissatisfaction && RateId <= VeryPlease) || !string.IsNullOrEmpty(Message);
    }
}
