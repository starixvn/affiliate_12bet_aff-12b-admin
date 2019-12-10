using System.Web.Mvc;
using Fanex.Affiliate.Admin.Core.Services.Common;
using Fanex.Affiliate.Admin.Core.MetaModels.ChangeAdminPassword;
using Fanex.Affiliate.Admin.Core.Services.ChangeAdminPassword;
using Fanex.Affiliate.Admin.Core.Extensions;
using Fanex.Affiliate.Admin.Core.DTO;
using Fanex.Affiliate.Admin.Core.Constants;
using System.Linq;
using Fanex.Affiliate.Admin.Core.Attributes;
using Fanex.Affiliate.Admin.Core.Utilities;

namespace Fanex.Affiliate.Admin.Core.Controllers
{
    [FunctionAuthorize(FunctionName = Function.Affiliate)]
    public class ChangeAdminPasswordController: BaseController
    {
        private readonly IChangeAdminPasswordService _changeAdminPasswordService;

        public ChangeAdminPasswordController(
            ISessionService sessionService,
            IChangeAdminPasswordService changeAdminPasswordService) : base(sessionService)
        {
            _changeAdminPasswordService = changeAdminPasswordService;
        }

        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryHeader]
        [FunctionAuthorize(FunctionName = Function.Affiliate, IsHost = true)]
        public ActionResult ChangePassword(ChangeAdminPasswordParam param)
        {
            if(ModelState.IsValid)
            {
                param.OldPassword = CryptographyHelper.Encrypt(param.OldPassword.Trim());
                param.NewPassword = CryptographyHelper.Encrypt(param.NewPassword.Trim());
                param.UserId = SessionService.UserId;
                param.Ip = Request.UserHostAddress;

                return new JsonNetResult
                {
                    Data = _changeAdminPasswordService.ChangePassword(param)
                };
            }

            return new JsonNetResult
            {
                Data = new ResultInfo
                {
                    Code = Code.GeneralErrorCode,
                    Message = string.Join("<br />", ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage))
                }
            };
        }
    }
}
