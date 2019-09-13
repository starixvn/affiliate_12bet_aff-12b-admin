using System.Web.Mvc;
using Fanex.Affiliate.Admin.Core.Attributes;
using Fanex.Affiliate.Admin.Core.Constants;
using Fanex.Affiliate.Admin.Core.Services.Common;
using Fanex.Affiliate.Admin.Core.Extensions;
using Fanex.Affiliate.Admin.Core.MetaModels.Message;
using Fanex.Affiliate.Admin.Core.Services.Message;
using System.Linq;
using Fanex.Affiliate.Admin.Core.DTO;
using Fanex.Affiliate.Admin.Core.MetaModels;

namespace Fanex.Affiliate.Admin.Core.Controllers
{
    [FunctionAuthorize(FunctionName = Function.HotNews)]
    public class MessageController : BaseController
    {
        private readonly IMessageService _messageSevice;

        public MessageController(
            ISessionService sessionService,
            IMessageService messageSevice) : base(sessionService)
        {
            _messageSevice = messageSevice;
        }

        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Message()
        {
            return View("~/Areas/Portal/Views/Message/Message.cshtml");
        }

        [HttpPost]
        [ValidateAntiForgeryHeader]
        public ActionResult GetMessageList(MessageParam param)
        {
            return new JsonNetResult
            {
                Data = _messageSevice.GetMessageList(param)
            };
        }

        [HttpPost]
        [ValidateAntiForgeryHeader]
        public ActionResult GetMessageDetail(int selectedMessageId)
        {
            return new JsonNetResult
            {
                Data = _messageSevice.GetMessageDetail(selectedMessageId)
            };
        }

        [HttpPost]
        [ValidateAntiForgeryHeader]
        [FunctionAuthorize(FunctionName = Function.HotNews, IsHost = true)]
        public ActionResult InsertMessage(InsertUpdateMessageParam param)
        {
            if (param.IsValid())
            {
                GetUserInfoParam(param);

                return new JsonNetResult
                {
                    Data = _messageSevice.InsertMessage(param)
                };
            }

            return Json(new ResultInfo
            {
                Code = Code.GeneralErrorCode,
                Message = "Please enter the content of message, one language at least."
            });
        }

        [HttpPost]
        [ValidateAntiForgeryHeader]
        [FunctionAuthorize(FunctionName = Function.HotNews, IsHost = true)]
        public ActionResult UpdateMessage(InsertUpdateMessageParam param)
        {
            var isValidMessageId = param.MessageId >= Validation.MinId;
            if (param.IsValid() && isValidMessageId)
            {
                GetUserInfoParam(param);

                return new JsonNetResult
                {
                    Data = _messageSevice.UpdateMessage(param)
                };
            }

            return Json(new ResultInfo
            {
                Code = Code.GeneralErrorCode,
                Message = !isValidMessageId ? "Invalid MessageId" : "Please enter the content of message, one language at least."
            });
        }

        [HttpPost]
        [ValidateAntiForgeryHeader]
        [FunctionAuthorize(FunctionName = Function.HotNews, IsHost = true)]
        public ActionResult DeleteMessage(DeleteMessageParam param)
        {
            if (ModelState.IsValid)
            {
                GetUserInfoParam(param);

                return new JsonNetResult
                {
                    Data = _messageSevice.DeleteMessage(param)
                };
            }

            return Json(new ResultInfo
            {
                Code = Code.GeneralErrorCode,
                Message = string.Join("<br />", ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage))
            });
        }

        private void GetUserInfoParam(UserInfoParam param)
        {
            param.UserId = SessionService.UserId;
            param.Ip = Request.UserHostAddress;
        }
    }
}
