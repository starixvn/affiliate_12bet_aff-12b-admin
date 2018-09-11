using System.Web.Mvc;
using Fanex.Affiliate.Admin.Core.MetaModels.UserPanel;
using Fanex.Affiliate.Admin.Core.Repository;
using Fanex.Affiliate.Admin.Core.Services.UserPanel;
using Fanex.Affiliate.Admin.Core.ViewModels.UserPanel;

namespace Fanex.Affiliate.Admin.Core.Controllers.UserPanel
{
    public class StatementHistoryController : Controller
    {
        private readonly IStatementHistoryService _statementHistoryService;

        private readonly IProductRepository _productRepository;

        public StatementHistoryController(IStatementHistoryService statementHistoryService, IProductRepository productRepository)
        {
            _statementHistoryService = statementHistoryService;
            _productRepository = productRepository;
        }

        [HttpGet]
        public ActionResult DailyEarning(DailyEarningParam param)
        {
            var viewModel = new DailyEarningViewModel
            {
                ProductList = _productRepository.GetProductList(),
                Param = param
            };

            return View("~/Areas/Portal/Views/UserPanel/StatementHistory/DailyEarning.cshtml", viewModel);
        }

        [HttpPost]
        public ActionResult GetDailyEarning(DailyEarningParam param)
        {
            return Json(_statementHistoryService.GetDailyEarning(param));
        }

        [HttpGet]
        public ActionResult DailyEarningDetail(DailyEarningParam param)
        {
            var viewModel = new DailyEarningViewModel
            {
                ProductList = _productRepository.GetProductList(),
                Param = param
            };

            return View(viewModel);
        }

        [HttpPost]
        public ActionResult GetDailyEarningDetail(DailyEarningParam param)
        {
            return Json(_statementHistoryService.GetDailyEarning(param));
        }
    }
}
