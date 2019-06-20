using System.Web.Mvc;
using Fanex.Affiliate.Admin.Core.Attributes;
using Fanex.Affiliate.Admin.Core.Constants;
using Fanex.Affiliate.Admin.Core.MetaModels.UserPanel;
using Fanex.Affiliate.Admin.Core.Repository.UserPanel;
using Fanex.Affiliate.Admin.Core.Services.Common;
using Fanex.Affiliate.Admin.Core.Extensions;

namespace Fanex.Affiliate.Admin.Core.Controllers.UserPanel
{
    [FunctionAuthorize(FunctionName = Function.Affiliate)]
    public class PlayerDetailController : BaseController
    {
        private readonly IPlayerDetailRepository _playerDetailRepository;

        public PlayerDetailController(
            ISessionService sessionService,
            IPlayerDetailRepository playerDetailRepository) : base(sessionService)
        {
            _playerDetailRepository = playerDetailRepository;
        }

        [HttpPost]
        [ValidateAntiForgeryHeader]
        public ActionResult GetPlayerDetailList(PlayerDetailParam param)
        {
            if (ModelState.IsValid)
            {
                return new JsonNetResult
                {
                    Data = _playerDetailRepository.GetPlayerDetailList(param)
                };
            }

            return new JsonNetResult();
        }
    }
}
