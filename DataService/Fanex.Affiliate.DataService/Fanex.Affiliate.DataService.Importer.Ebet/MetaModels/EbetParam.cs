using System;

namespace Fanex.Affiliate.DataService.Importer.Ebet.MetaModels
{
    public class EbetParam
    {
        public string CurrencyId { get; set; }

        public string UserName { get; set; } = string.Empty;

        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }

        public string ChannelId { get; set; }

        public int SubChanelId { get; set; } = 0;

        public string ApiUrl { get; set; }

        public int PageNumber { get; set; } = 1;

        public int PageSize { get; set; } = 5000;

        public long TimeStamp { get; set; }

        public string Signature { get; set; }

        public string PrivateKey { get; set; }
    }
}
