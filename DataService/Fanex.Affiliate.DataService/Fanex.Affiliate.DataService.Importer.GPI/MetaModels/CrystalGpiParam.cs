using System;

namespace Fanex.Affiliate.DataService.Importer.Gpi.MetaModels
{
    public class CrystalGpiParam
    {
        public string Code { get; set; }

        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }

        public int PageNumber { get; set; }

        public int PageSize { get; set; }

        public string Password { get; set; }

        public byte Type { get; set; }

        public byte JobType { get; set; }
    }
}
