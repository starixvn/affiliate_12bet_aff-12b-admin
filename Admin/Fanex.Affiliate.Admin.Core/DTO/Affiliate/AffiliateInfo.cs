using Fanex.Affiliate.Admin.Core.Attributes;
using Fanex.Affiliate.Admin.Core.Constants;
using Newtonsoft.Json;
using System;

namespace Fanex.Affiliate.Admin.Core.DTO.Affiliate
{
    public class AffiliateInfo
    {
        public int AffId { get; set; }

        public string UserName { get; set; }

        public string Status { get; set; }

        public string ProcessingStatus { get; set; }

        [CensoredPermissionCheck(Function.AffPersonalInfo)]
        public string FirstName { get; set; }

        [CensoredPermissionCheck(Function.AffPersonalInfo)]
        public string LastName { get; set; }

        [PermissionCheck(Function.AffTargetCountry)]
        public string TargetCountry { get; set; }

        [PermissionCheck(Function.LanguageAmendment)]
        public string Language { get; set; }

        [PermissionCheck(Function.AffEmail)]
        public string Email { get; set; }

        [JsonIgnore]
        public DateTime RegistrationDate { get; set; }

        [JsonIgnore]
        public DateTime? ApprovalDate { get; set; }

        [JsonIgnore]
        public DateTime? CasinoDate { get; set; }

        [PermissionCheck(Function.AssignCommGroup)]
        public string CommissionGroup { get; set; }

        public decimal? BalanceBf { get; set; }

        [PermissionCheck(Function.MemberDetail)]
        public int? Member { get; set; }

        public bool IsFavorite { get; set; }

        public bool IsClosed { get; set; }

        public string Currency { get; set; }

        public string PlayerCurrency { get; set; }

        [PermissionCheck(Function.AffPaymentInfo)]
        public string PlayerUserName { get; set; }

        [PermissionCheck(Function.AffPhone)]
        [CensoredPermissionCheck(Function.AffPersonalInfo)]
        public string Phone { get; set; }

        public string RegistrationDateText => RegistrationDate.ToString("MM/dd/yyyy HH:mm:ss");

        public string ApprovalDateText => ApprovalDate?.ToString("MM/dd/yyyy HH:mm:ss") ?? "N/A";

        public string CasinoDateText => CasinoDate?.ToString("MM/dd/yyyy") ?? "N/A";
    }
}
