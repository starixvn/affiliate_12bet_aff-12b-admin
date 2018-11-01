using System.Linq;
using Fanex.Affiliate.Admin.Core.Services.Common;
using Fanex.Affiliate.Admin.Core.Repository.Affiliate;
using System.Web.Mvc;
using Fanex.Affiliate.Admin.Core.DTO;
using Fanex.Affiliate.Admin.Core.MetaModels.Affiliate;
using Fanex.Affiliate.Admin.Core.Constants;

namespace Fanex.Affiliate.Admin.Core.Controllers
{
    public class FavoriteController : BaseController
    {
        private readonly IFavoriteRepository _favoriteRepository;

        public FavoriteController(ISessionService sessionService, IFavoriteRepository favoriteRepository) : base(sessionService)
        {
            _favoriteRepository = favoriteRepository;
        }

        [HttpPost]
        public ActionResult UpdateFavorite(InsertUpdateFavoriteParam param)
        {
            if (ModelState.IsValid)
            {
                param.UserId = SessionService.UserId;
                param.Ip = Request.UserHostAddress;

                return Json(_favoriteRepository.UpdateFavorite(param));
            }

            return Json(new ResultInfo
            {
                Code = Code.GeneralErrorCode,
                Message = string.Join("\n", ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage))
            });
        }
    }
}
