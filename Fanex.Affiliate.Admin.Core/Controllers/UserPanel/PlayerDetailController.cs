using System;
using System.Web.Mvc;
using Fanex.Affiliate.Admin.Core.MetaModels.UserPanel;
using Fanex.Affiliate.Admin.Core.Repository;
using Fanex.Affiliate.Admin.Core.Repository.UserPanel;
using Fanex.Affiliate.Admin.Core.Services.Common;
using Fanex.Affiliate.Admin.Core.ViewModels.UserPanel;

namespace Fanex.Affiliate.Admin.Core.Controllers.UserPanel
{
    public class PlayerDetailController : BaseController
    {
        private readonly IPlayerDetailRepository _playerDetailRepository;

        private readonly IProductRepository _productRepository;

        public PlayerDetailController(
            ISessionService sessionService,
            IPlayerDetailRepository playerDetailRepository,
            IProductRepository productRepository) : base(sessionService)
        {
            _playerDetailRepository = playerDetailRepository;
            _productRepository = productRepository;
        }

        [HttpGet]
        public ActionResult Index(PlayerDetailParam param)
        {
            if (!param.MonthDate.HasValue)
            {
                param.MonthDate = DateTime.Now;
            }

            var viewModel = new PlayerDetailViewModel
            {
                ProductList = _productRepository.GetProductList(),
                Param = param
            };

            return View("~/Areas/Portal/Views/UserPanel/PlayerDetail/Index.cshtml", viewModel);
        }

        [HttpPost]
        public ActionResult GetPlayerDetailList(PlayerDetailParam param)
        {
            param.AffId = SessionService.AffId;

            return Json(_playerDetailRepository.GetPlayerDetailList(param));
        }
    }
}
