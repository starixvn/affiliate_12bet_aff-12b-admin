using System;

namespace Fanex.Affiliate.DataService.Importer.Opus.MetaModels
{
    public class OpusParam
    {
        public string SecretKey { get; set; }

        public string OperatorId { get; set; }

        public string SiteCode { get; set; }

        public string ProductCode { get; set; }

        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }
    }
}
