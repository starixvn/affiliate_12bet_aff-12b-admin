using System.Web.Mvc;
using Fanex.Affiliate.Admin.Core.Attributes;
using Fanex.Affiliate.Admin.Core.Extensions;
using Fanex.Affiliate.Admin.Core.Repository;

namespace Fanex.Affiliate.Admin.Core.Controllers
{
    public class CommonSettingController : Controller
    {
        private readonly IProductRepository _productRepository;

        public CommonSettingController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        [HttpPost]
        [ValidateAntiForgeryHeader]
        [Compress]
        public ActionResult GetProductList()
        {
            return new JsonNetResult
            {
                Data = _productRepository.GetProductList()
            };
        }
    }
}
