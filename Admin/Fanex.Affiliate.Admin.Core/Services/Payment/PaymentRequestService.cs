using Fanex.Affiliate.Admin.Core.DTO;
using Fanex.Affiliate.Admin.Core.DTO.Payment;
using Fanex.Affiliate.Admin.Core.MetaModels.Payment;
using Fanex.Affiliate.Admin.Core.Repository.Payment;
using Fanex.Affiliate.Admin.Core.DTO.Email;
using Fanex.Affiliate.Admin.Core.Utilities;
using System.Web;
using Fanex.Affiliate.Admin.Core.Constants;
using System.Collections.Generic;
using Fanex.Affiliate.Admin.Core.Resources;
using System;

namespace Fanex.Affiliate.Admin.Core.Services.Payment
{
    public class PaymentRequestService : IPaymentRequestService
    {
        private readonly IPaymentRequestRepository _paymentRequestRepository;
        private readonly IAffiliateService _affiliateService;
        private readonly IEmailService _emailService;

        public PaymentRequestService(
            IPaymentRequestRepository paymentRequestRepository,
            IAffiliateService affiliateService,
            IEmailService emailService)
        {
            _paymentRequestRepository = paymentRequestRepository;
            _affiliateService = affiliateService;
            _emailService = emailService;
        }

        public EntityResult<PaymentRequest> GetPaymentRequestList(PaymentRequestParam param)
        {
            if(param.ToDate != null)
            {
                var toDate = Convert.ToDateTime(param.ToDate);
                param.ToDate = toDate.AddDays(1);
            }

            return _paymentRequestRepository.GetPaymentRequestList(param);
        }

        public PaymentRequestDetail GetPaymentRequestDetail(long transId)
        {
            return _paymentRequestRepository.GetPaymentRequestDetail(transId);
        }

        public ResultInfo UpdatePaymentRequest(PaymentRequestUpdateParam param, PaymentRequestUpdateEmail emailParam)
        {
            var result = emailParam.IsApproved ? _paymentRequestRepository.ApprovePaymentRequest(param) : _paymentRequestRepository.RejectPaymentRequest(param);

            if(result != null)
            {
                if (result.IsSuccessful)
                {
                    var affInfo = _affiliateService.GetAffiliateBasicInfo(param.AffId);

                    emailParam.UserSiteUrl = SiteDomainHelper.GetUserSiteUrl(affInfo.AffType);
                    emailParam.FirstName = affInfo.FirstName;
                    emailParam.MailTo = affInfo.Email;
                    emailParam.IsHtml = !affInfo.MailOption;
                    emailParam.Language = affInfo.PreferredLanguage ?? LanguageSystem.DefaultLanguage;

                    emailParam.TransId = param.TransId;
                    emailParam.TransDate = param.TransDate.ToString("MM/dd/yyyy");
                    emailParam.Amount = param.Amount;

                    _emailService.SendPaymentRequestUpdateEmail(emailParam);
                } else
                {
                    result.Message = GetErrMsg(result.Code);
                }
            }

            return result;
        }

        public static string GetErrMsg(int intErrCode)
        {
            if (errorCodeDict.ContainsKey(intErrCode))
            {
                return errorCodeDict[intErrCode];
            }

            return GetErrorText(intErrCode.ToString());
        }

        public static string GetErrorText(string key)
        {
            var resourceKey = "Err" + key;
            object text = HttpContext.GetGlobalResourceObject("Lang", resourceKey);

            return text == null ? resourceKey : text.ToString();
        }

        private static readonly Dictionary<int, string> errorCodeDict = new Dictionary<int, string>
        {
            [1] = Lang.Err1,
            [2] = Lang.Err2,
            [3] = Lang.Err3,
            [4] = Lang.Err4,
            [5] = Lang.Err5,
            [100] = Lang.Err100,
            [101] = Lang.Err101,
            [102] = Lang.Err102,
            [103] = Lang.Err103,
            [104] = Lang.Err104,
            [105] = Lang.Err105,
            [107] = Lang.Err107,
            [111] = Lang.Err111,
            [112] = Lang.Err112,
            [113] = Lang.Err113,
            [114] = Lang.Err114,
            [115] = Lang.Err115,
            [116] = Lang.Err116,
            [500] = Lang.Err500,
            [999] = Lang.Err999,
            [1001] = Lang.Err1001,
            [1002] = Lang.Err1002,
            [1003] = Lang.Err1003,
            [1004] = Lang.Err1004,
            [1005] = Lang.Err1005,
            [1006] = Lang.Err1006,
            [1007] = Lang.Err1007,
            [3006] = Lang.Err3006,
            [4001] = Lang.Err4001,
            [4002] = Lang.Err4002,
            [4003] = Lang.Err4003,
            [4004] = Lang.Err4004,
            [5001] = Lang.Err5001,
            [30010] = Lang.Err30010,
            [30011] = Lang.Err30011,
            [30012] = Lang.Err30012,
            [30014] = Lang.Err30014,
            [30015] = Lang.Err30015
        };
    }
}
