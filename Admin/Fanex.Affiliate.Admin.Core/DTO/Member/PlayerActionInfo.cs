using System;

namespace Fanex.Affiliate.Admin.Core.DTO.Member
{
    public class PlayerActionInfo
    {
        public string UserName { get; set; }

        public string AffUserName { get; set; }

        public bool IsRemoved { get; set; }

        public DateTime? RemovedDate { get; set; }

        public DateTime? LastAction { get; set; }
    }
}
