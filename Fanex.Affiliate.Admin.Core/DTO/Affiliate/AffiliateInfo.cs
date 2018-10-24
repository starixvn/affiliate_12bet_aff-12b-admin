using System;

namespace Fanex.Affiliate.Admin.Core.DTO.Affiliate
{
    public class AffiliateInfo
    {
        public string UserName { get; set; }

        public bool IsFavorite { get; set; }

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
    }
}
