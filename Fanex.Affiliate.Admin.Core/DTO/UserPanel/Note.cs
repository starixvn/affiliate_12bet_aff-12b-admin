using System;

namespace Fanex.Affiliate.Admin.Core.DTO.UserPanel
{
    public class Note
    {
        public int NoteId { get; set; }

        public string Content { get; set; }

        public string CreatedBy { get; set; }

        public DateTime LastModify { get; set; }

        public string LastModifyText => LastModify.ToString("MM/dd/yyyy HH:mm:ss");
    }
}
