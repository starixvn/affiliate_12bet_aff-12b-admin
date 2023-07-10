
namespace Fanex.Affiliate.Admin.Core.DTO.Payment
{
    public class PaymentUpdateResult : ResultInfo
    {
        public string DetailXml { get; set; }

        public string OldData { get; set; }

        public string NewData { get; set; }
    }
}
