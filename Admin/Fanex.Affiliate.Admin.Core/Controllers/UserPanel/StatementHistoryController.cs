using System.Web.Mvc;
using Fanex.Affiliate.Admin.Core.Attributes;
using Fanex.Affiliate.Admin.Core.Constants;
using Fanex.Affiliate.Admin.Core.MetaModels.UserPanel;
using Fanex.Affiliate.Admin.Core.Repository;
using Fanex.Affiliate.Admin.Core.Services.UserPanel;
using Fanex.Affiliate.Admin.Core.ViewModels.UserPanel;
using Fanex.Affiliate.Admin.Core.Services.Common;
using Fanex.Affiliate.Admin.Core.Extensions;

namespace Fanex.Affiliate.Admin.Core.Controllers.UserPanel
{
    [FunctionAuthorize(FunctionName = Function.Affiliate)]
    public class StatementHistoryController : BaseController
    {
        private readonly IStatementHistoryService _statementHistoryService;

        private readonly IProductRepository _productRepository;

        public StatementHistoryController(
            ISessionService sessionService,
            IStatementHistoryService statementHistoryService,
            IProductRepository productRepository) : base(sessionService)
        {
            _statementHistoryService = statementHistoryService;
            _productRepository = productRepository;
        }

        [HttpGet]
        public ActionResult Index(AffiliateLinkParam param)
        {
            return View("~/Areas/Portal/Views/UserPanel/StatementHistory/Index.cshtml", param);
        }

        [HttpPost]
        [ValidateAntiForgeryHeader]
        [Compress]
        public ActionResult GetStatementHistoryList(StatementHistoryParam param)
        {
            param.AffId = param.AffId == 0 ? SessionService.AffId : param.AffId;

            var model = _statementHistoryService.GetStatementHistoryList(param);

            return new JsonNetResult
            {
                Data = model
            };
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
        [ValidateAntiForgeryHeader]
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

            return View("~/Areas/Portal/Views/UserPanel/StatementHistory/DailyEarningDetail.cshtml", viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryHeader]
        public ActionResult GetDailyEarningDetail(DailyEarningParam param)
        {
            return Json(_statementHistoryService.GetDailyEarningDetail(param));
        }
    }
}
