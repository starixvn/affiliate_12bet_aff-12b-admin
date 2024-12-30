using System;

namespace Fanex.Affiliate.Admin.Core.DTO.Customer
{
    public class OnlineList
    {
        public string Ip { get; set; }

        public DateTime ReloadTime { get; set; }

        public string ReloadTimeText => ReloadTime.ToString("MM/dd/yyyy HH:mm:ss");
    }
}
