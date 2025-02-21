using System;

namespace Fanex.Affiliate.Admin.Core.DTO.Message
{
    public class MessageInfo
    {
        public int MessageId { get; set; }

        public string Creator { get; set; }

        public string Message_en { get; set; }

        public string Message => Message_en;

        public bool Important { get; set; }

        public string UpdatedBy { get; set; }

        public DateTime? PostedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        public string PostedDateText => PostedDate?.ToString("MM/dd/yyyy HH:mm:ss");

        public string UpdatedDateText => UpdatedDate?.ToString("MM/dd/yyyy HH:mm:ss");
    }
}
