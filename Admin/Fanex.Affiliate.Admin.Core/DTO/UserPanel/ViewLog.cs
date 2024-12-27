using System;

namespace Fanex.Affiliate.Admin.Core.DTO.UserPanel
{
    public class ViewLog
    {
        public int LogId { get; set; }

        public DateTime Date { get; set; }

        public string UpdatedName { get; set; }

        public string Description { get; set; }

        public string Action { get; set; }

        public string Ip { get; set; }

        public string DateText => Date.ToString("MM/dd/yyyy HH:mm:ss");
    }
}
