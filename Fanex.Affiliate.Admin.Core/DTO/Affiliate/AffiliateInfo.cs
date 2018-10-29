using System;

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

        public DateTime RegistrationDate { get; set; }

        public DateTime? ApprovalDate { get; set; }

        public DateTime? CasinoDate { get; set; }

        public string CommissionGroup { get; set; }

        public decimal BalanceBf { get; set; }

        public int Member { get; set; }

        public bool IsFavorite { get; set; }

        public bool IsClosed { get; set; }
    }
}
