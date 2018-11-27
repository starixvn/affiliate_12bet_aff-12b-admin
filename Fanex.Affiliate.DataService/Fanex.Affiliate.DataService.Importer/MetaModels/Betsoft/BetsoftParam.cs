using System;

namespace Fanex.Affiliate.DataService.Importer.MetaModels.Betsoft
{
    public class BetsoftParam
    {
        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public string BankId { get; set; }

        public string Password { get; set; }

        public string ApiUrl { get; set; }
    }
}
