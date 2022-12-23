using Fanex.Affiliate.User.Core.Constants;
using Fanex.Affiliate.User.Core.Resources;
using Fanex.Data;
using System.ComponentModel.DataAnnotations;

namespace Fanex.Affiliate.User.Core.MetalModels.UserPanel
{
    public class FeedbackParam : BaseParam
    {
        private const int None = 0;
        private const int Dissatisfaction = 1;
        private const int VeryPlease = 5;

        public int AffId { get; set; }

        public string Ip { get; set; }

        [Range(None, VeryPlease, ErrorMessageResourceType = typeof(Error), ErrorMessageResourceName = "InvalidEmoji")]
        public int RateId { get; set; }

        [MaxLength(Number.FourThousand, ErrorMessageResourceType = typeof(Error), ErrorMessageResourceName = "MessageMaxLength")]
        public string Message { get; set; }

        public int ErrCode { get; set; }

        public string ErrMsg { get; set; }

        [SpParam(Ignored = true)]
        public bool IsClickLogout { get; set; }

        [SpParam(Ignored = true)]
        public bool IsValid => (RateId >= Dissatisfaction && RateId <= VeryPlease) || !string.IsNullOrEmpty(Message);
    }
}
