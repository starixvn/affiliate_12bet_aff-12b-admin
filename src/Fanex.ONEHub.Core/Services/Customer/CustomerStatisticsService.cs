using Fanex.Affiliate.Admin.Core.Criterias.Customer;
using Fanex.Affiliate.Admin.Core.DTO.Customer;
using Fanex.Affiliate.Admin.Core.MetaModels.Customer;
using Fanex.Data.Repository;

namespace Fanex.Affiliate.Admin.Core.Services.Customer
{
    public class CustomerStatisticsService : BaseDynamicService, ICustomerStatisticsService
    {
        public CustomerStatisticsService(
            IDynamicRepository dynamicRepository) : base(dynamicRepository) { }

        public CustomerStatistics GetCustomerStatistics(CustomerStatisticsParam param)
        {
            var criteria = new CustomerStatisticsCriteria
            {
                AffId = param.AffId,
                MonthDate = param.MonthDate,
            };

            return GetTData<CustomerStatistics>(criteria);
        }
    }
}