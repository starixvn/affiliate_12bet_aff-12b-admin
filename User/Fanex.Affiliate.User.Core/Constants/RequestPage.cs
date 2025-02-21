namespace Fanex.Affiliate.User.Core.Constants
{
    using Fanex.Affiliate.User.Core.Resources;
    using Fanex.Affiliate.User.Core.ViewModels.UserPanel;
    using System.Collections.Generic;

    public static class RequestPage
    {
        public const string StatementHistory = "StatementHistory";

        public const string CommissionReport = "RevenueReport";

        public static IDictionary<string, BreadcrumbViewModel> Links => new Dictionary<string, BreadcrumbViewModel>
        {
            { StatementHistory, new BreadcrumbViewModel { Name = Lang.StatementHistory, Url = "~/UserPanel/Main/#StatementHistory" } },
            { CommissionReport, new BreadcrumbViewModel { Name = Lang.CommissionReport, Url = "~/UserPanel/Main/#RevenueReport" } }
        };
    }
}
