using System.Web.Mvc;
using Fanex.Affiliate.Admin.Core.Attributes;
using Fanex.Affiliate.Admin.Core.Constants;
using Fanex.Affiliate.Admin.Core.Services.Common;
using Fanex.Affiliate.Admin.Core.Extensions;
using Fanex.Affiliate.Admin.Core.MetaModels.Message;
using Fanex.Affiliate.Admin.Core.Services.Message;
using Fanex.Affiliate.Admin.Core.DTO;
using Fanex.Affiliate.Admin.Core.MetaModels;
using Fanex.Affiliate.Admin.Core.Resources;
using Fanex.Affiliate.Admin.Core.DTO.Message;
using System.Collections.Generic;

namespace Fanex.Affiliate.Admin.Core.Controllers
{
    [FunctionAuthorize(FunctionName = Function.HotNews)]
    public class MessageController : BaseController
    {
        private const int threeMilliSecond = 3;
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

        [HttpPost]
        [ValidateAntiForgeryHeader]
        public ActionResult GetMessageList(MessageParam param)
        {
            if (ModelState.IsValid)
            {
                param.ToDate = param.ToDate.AddDays(1).AddMilliseconds(-threeMilliSecond);

                return new JsonNetResult
                {
                    Data = _messageSevice.GetMessageList(param)
                };
            }

            return new JsonNetResult
            {
                Data = new EntityResult<MessageInfo>
                {
                    MainData = new List<MessageInfo>(),
                    TotalItem = 0
                }
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
                var encodeParam = EncodeParam(param);
                GetUserInfoParam(encodeParam);

                return new JsonNetResult
                {
                    Data = _messageSevice.InsertMessage(encodeParam)
                };
            }

            return new JsonNetResult
            {
                Data = new ResultInfo
                {
                    Code = Code.GeneralErrorCode,
                    Message = Lang.PleaseEnterContent
                }
            };
        }

        [HttpPost]
        [ValidateAntiForgeryHeader]
        [FunctionAuthorize(FunctionName = Function.HotNews, IsHost = true)]
        public ActionResult UpdateMessage(InsertUpdateMessageParam param)
        {
            var isValidMessageId = param.MessageId >= Validation.MinId;
            if (param.IsValid() && isValidMessageId)
            {
                var encodeParam = EncodeParam(param);
                GetUserInfoParam(encodeParam);

                return new JsonNetResult
                {
                    Data = _messageSevice.UpdateMessage(encodeParam)
                };
            }

            return new JsonNetResult
            {
                Data = new ResultInfo
                {
                    Code = Code.GeneralErrorCode,
                    Message = !isValidMessageId ? Lang.MessageIdIsInvalid : Lang.PleaseEnterContent
                }
            };
        }

        [HttpPost]
        [ValidateAntiForgeryHeader]
        [FunctionAuthorize(FunctionName = Function.HotNews, IsHost = true)]
        public ActionResult DeleteMessages(DeleteMessageParam param)
        {
            if (ModelState.IsValid)
            {
                GetUserInfoParam(param);

                return new JsonNetResult
                {
                    Data = _messageSevice.DeleteMessages(param)
                };
            }

            return new JsonNetResult
            {
                Data = new ResultInfo
                {
                    Code = Code.GeneralErrorCode,
                    Message = Lang.PleaseSelectMessage
                }
            };
        }

        private InsertUpdateMessageParam EncodeParam(InsertUpdateMessageParam param)
        {
            return new InsertUpdateMessageParam
            {
                MessageId = param.MessageId,
                Important = param.Important,
                MessageContentEn = Server.HtmlEncode(param.MessageContentEn),
                MessageContentCn = Server.HtmlEncode(param.MessageContentCn),
                MessageContentTw = Server.HtmlEncode(param.MessageContentTw),
                MessageContentEs = Server.HtmlEncode(param.MessageContentEs),
                MessageContentTh = Server.HtmlEncode(param.MessageContentTh),
                MessageContentVi = Server.HtmlEncode(param.MessageContentVi),
                MessageContentId = Server.HtmlEncode(param.MessageContentId),
                MessageContentKr = Server.HtmlEncode(param.MessageContentKr),
                MessageContentJp = Server.HtmlEncode(param.MessageContentJp)
            };
        }

        private void GetUserInfoParam(UserInfoParam param)
        {
            param.UserId = SessionService.UserId;
            param.Ip = Request.UserHostAddress;
        }
    }
}
