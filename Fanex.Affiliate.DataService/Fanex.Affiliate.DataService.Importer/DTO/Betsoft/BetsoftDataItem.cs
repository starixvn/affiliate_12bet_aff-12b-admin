using System;

namespace Fanex.Affiliate.DataService.Importer.DTO.Betsoft
{
    public class BetsoftDataItem
    {
        public string UserName { get; set; }

        public decimal Amount { get; set; }

        public decimal Win { get; set; }

        public DateTime WinLossDate { get; set; }
    }
}
