using Fanex.Affiliate.User.Core.ViewModels.UserPanel;
using System;

namespace Fanex.Affiliate.User.Core.BusinessService
{
    public interface ICommissionReportService
    {
        CommissionReportDataViewModel GenerateCommissionReportData(int affId, DateTime winlostDate);
    }
}
