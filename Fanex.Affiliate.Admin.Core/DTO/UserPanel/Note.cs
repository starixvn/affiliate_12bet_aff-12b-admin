using Newtonsoft.Json;
using System;

namespace Fanex.Affiliate.Admin.Core.DTO.UserPanel
{
    public class Note
    {
        public int NoteId { get; set; }

        public string NoteContent { get; set; }

        public string CreatedBy { get; set; }

        [JsonIgnore]
        public DateTime? LastModify { get; set; }
    }
}
