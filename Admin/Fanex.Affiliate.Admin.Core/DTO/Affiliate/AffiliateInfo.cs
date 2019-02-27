using System;
using Newtonsoft.Json;

namespace Fanex.Affiliate.Admin.Core.DTO.Affiliate
{
    public class AffiliateInfo
    {
        public int AffId { get; set; }

        public string UserName { get; set; }

        public string Status { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Language { get; set; }

        public string Email { get; set; }

        [JsonIgnore]
        public DateTime RegistrationDate { get; set; }

        [JsonIgnore]
        public DateTime? ApprovalDate { get; set; }

        [JsonIgnore]
        public DateTime? CasinoDate { get; set; }

        public string CommissionGroup { get; set; }

        public decimal? BalanceBf { get; set; }

        public int? Member { get; set; }

        public bool IsFavorite { get; set; }

        public bool IsClosed { get; set; }

        public string Currency { get; set; }

        public string PlayerCurrency { get; set; }

        public string PlayerUserName { get; set; }

        public string Phone { get; set; }

        public string RegistrationDateText => RegistrationDate.ToString("MM/dd/yyyy HH:mm:ss");

        public string ApprovalDateText => ApprovalDate?.ToString("MM/dd/yyyy HH:mm:ss") ?? "N/A";

        public string CasinoDateText => CasinoDate?.ToString("MM/dd/yyyy") ?? "N/A";
    }
}
