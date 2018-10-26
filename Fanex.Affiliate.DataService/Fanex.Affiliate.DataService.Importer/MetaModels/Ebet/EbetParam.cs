namespace Fanex.Affiliate.DataService.Importer.MetaModels.Ebet
{
    public class EbetParam
    {
        public string UserName { get; set; } = string.Empty;

        public string StartTime { get; set; }

        public string EndTime { get; set; }

        public int SubChanelId { get; set; } = 0;

        public int PageNumber { get; set; } = 1;

        public int PageSize { get; set; } = 10;

        public long TimeStamp { get; set; }

        public string Signature { get; set; }
    }
}
