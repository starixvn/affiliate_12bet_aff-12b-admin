using Fanex.Affiliate.Admin.Core.Criterias;
using Fanex.Affiliate.Admin.Core.DTO;
using Fanex.Data.Repository;
using System.Collections.Generic;

namespace Fanex.Affiliate.Admin.Core.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly IDynamicRepository _dynamicRepository;

        public ProductRepository(
            IDynamicRepository dynamicRepository)
        {
            _dynamicRepository = dynamicRepository;
        }

        public IEnumerable<Product> GetProductList()
        {
            const string SpKey = "Aff_ProductList_Sel";

            return _dynamicRepository.Fetch<Product>(new BaseCriteria(SpKey));
        }
    }
}
