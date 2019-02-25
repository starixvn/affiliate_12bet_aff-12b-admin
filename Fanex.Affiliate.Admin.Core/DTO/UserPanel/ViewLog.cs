using System;

namespace Fanex.Affiliate.Admin.Core.DTO.UserPanel
{
    public class ViewLog
    {
        public int Id { get; set; }

        public DateTime Date { get; set; }

        public string UpdatedBy { get; set; }

        public string Action { get; set; }

        public string Description { get; set; }

        public string Ip { get; set; }
    }
}
