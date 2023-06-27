using Fanex.Affiliate.Admin.Core.Attributes;
using Fanex.Affiliate.Admin.Core.Constants;
using Fanex.Data;
using System;

namespace Fanex.Affiliate.Admin.Core.DTO.Payment
{
    [Serializable]
    public class PaymentRequestDetail : PaymentRequest
    {
        public int AffId { get; set; }

        public string Email { get; set; }

        [CensoredPermissionCheck(Function.AffPersonalInfo)]
        public string FirstName { get; set; }

        [CensoredPermissionCheck(Function.AffPersonalInfo)]
        public string LastName { get; set; }

        public string PaymentMethod { get; set; }

        public string PlayerUsername { get; set; }

        public string AdminDesc { get; set; }

        public string AffDesc { get; set; }

        [SpParam(Ignored = true)]
        public decimal BalanceBF { get; set; }

        public new string TransDateText => TransDate.ToString("MM/dd/yyyy HH:mm:ss tt");

        public new string CompletedDateText => CompletedDate.ToString("MM/dd/yyyy HH:mm:ss tt");
    }
}
