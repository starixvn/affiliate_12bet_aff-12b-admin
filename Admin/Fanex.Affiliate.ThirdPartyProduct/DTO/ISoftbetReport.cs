using Fanex.Affiliate.DataService.Importer.DTO.ISoftbet;

namespace Fanex.Affiliate.ThirdPartyProduct.DTO
{
    public class ISoftbetReport : ISoftbetBetHistory
    {
        public decimal Turnover { get; set; }

        public decimal Winloss { get; set; }
    }
}
