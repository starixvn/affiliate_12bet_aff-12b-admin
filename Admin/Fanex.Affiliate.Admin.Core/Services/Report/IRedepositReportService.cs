namespace Fanex.Affiliate.Admin.Core.Services.Report
{
    using Fanex.Affiliate.Admin.Core.DTO;
    using Fanex.Affiliate.Admin.Core.DTO.Report;
    using Fanex.Affiliate.Admin.Core.MetaModels.Report;

    public interface IRedepositReportService
    {
        EntityResult<RedepositReport> GetReportData(RedepositReportParam param);
    }
}
