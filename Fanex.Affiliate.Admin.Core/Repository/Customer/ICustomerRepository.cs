using Fanex.Affiliate.Admin.Core.DTO.Customer;
using Fanex.Affiliate.Admin.Core.MetaModels.Customer;

namespace Fanex.Affiliate.Admin.Core.Repository.Customer
{
    public interface ICustomerRepository
    {
        CustomerStatistics GetCustomerStatistics(CustomerStatisticsParam param);
    }
}
