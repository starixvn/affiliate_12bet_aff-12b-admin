using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Fanex.Affiliate.User.Core.DTO.UserPanel;
using Fanex.Affiliate.User.Core.ViewModels.UserPanel;

namespace Fanex.Affiliate.User.Core.BusinessService
{
    public interface ICommissionReportService
    {
        CommissionReportDataViewModel GenerateCommissionReportData(int affId, DateTime winlostDate);
    }
}
