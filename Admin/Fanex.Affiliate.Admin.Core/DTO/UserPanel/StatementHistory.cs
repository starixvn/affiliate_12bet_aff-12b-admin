using System;

namespace Fanex.Affiliate.Admin.Core.DTO.UserPanel
{
    public class StatementHistory
    {
        public DateTime Date { get; set; }

        public int TransType { get; set; }

        public decimal Amount { get; set; }

        public string Status { get; set; }

        public string Creator { get; set; }

        public string Description { get; set; }

        public string DateText => Date.ToString("dd/MMM/yyyy");
    }
}
