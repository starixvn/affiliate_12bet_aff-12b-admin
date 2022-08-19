using System;

namespace Fanex.Affiliate.DataService.Importer.BigGaming.MetaModels
{
    public class BigGamingParam
    {
        public string Method { get; set; }

        public string Random { get; set; }

        public string Sign { get; set; }

        public string Sn { get; set; }

        public string FromDate { get; set; }

        public string ToDate { get; set; }

        public int PageIndex { get; set; }

        public string PageSize { get; set; }

        public string GameType { get; set; }
    }
}
