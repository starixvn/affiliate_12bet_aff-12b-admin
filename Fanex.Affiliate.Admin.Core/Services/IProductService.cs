using System.Collections.Generic;
using Fanex.Affiliate.Admin.Core.DTO;

namespace Fanex.Affiliate.Admin.Core.Services
{
    public interface IProductService
    {
        IEnumerable<Product> GetProductList();
    }
}
