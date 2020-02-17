using System;

namespace Fanex.Affiliate.DataService.Importer.ISoftbet.MetaModels
{
    public class ISoftbetEntryParam
    {
        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }

        public int PageNumber { get; set; }

        public byte JobType { get; set; }
    }
}
