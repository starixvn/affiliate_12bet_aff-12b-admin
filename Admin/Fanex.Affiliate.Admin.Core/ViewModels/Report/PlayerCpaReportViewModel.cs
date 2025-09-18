namespace Fanex.Affiliate.Admin.Core.ViewModels.Report
{
    using System.Collections.Generic;
    using Fanex.Affiliate.Admin.Core.DTO.Report;

    public class PlayerCpaReportViewModel
    {
        public IEnumerable<PlayerCpaReport> Reports { get; set; }

        public PlayerCpaReport Total { get; set; }
    }
}
