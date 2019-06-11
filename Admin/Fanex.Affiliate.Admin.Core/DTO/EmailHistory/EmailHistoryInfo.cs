using System;

namespace Fanex.Affiliate.Admin.Core.DTO.EmailHistory
{
    public class EmailHistoryInfo
    {
        public string Sender { get; set; }

        public string AffName { get; set; }

        public string Subject { get; set; }

        public string Email { get; set; }

        public DateTime SentDate { get; set; }
    }
}
