using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fanex.Affiliate.Admin.Core.DTO.UserPanel
{
    public class TrackingStatistics
    {
        public int TotalClick { get; set; }

        public int UniqueClick { get; set; }

        public DateTime Date { get; set; }

        public string DateText => Date.ToString("MMMM-yyyy");
    }
}
