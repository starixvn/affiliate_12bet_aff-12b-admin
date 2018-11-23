using System;

namespace Fanex.Affiliate.DataService.Importer.DTO.ISoftbet
{
    public class ISoftbetResultInfo
    {
        public DateTime DateTime { get; set; }

        public int Total { get; set; }

        public int Pages { get; set; }

        public int CurrentPage { get; set; }
    }
}
