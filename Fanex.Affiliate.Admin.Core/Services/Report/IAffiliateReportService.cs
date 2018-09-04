using Fanex.Affiliate.Admin.Core.MetaModels.Report;
using Fanex.Affiliate.Admin.Core.ViewModels;

namespace Fanex.Affiliate.Admin.Core.Services.Report
{
    public interface IAffiliateReportService
    {
        AffiliateReportMainDataViewModel GetMainReportData(AffiliateReportMainParam param);
    }
}
