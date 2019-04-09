using Fanex.Affiliate.Admin.Core.DTO;
using Fanex.Affiliate.Admin.Core.DTO.Payment;
using Fanex.Affiliate.Admin.Core.MetaModels.Payment;
using Fanex.Affiliate.Admin.Core.Repository.Payment;
using Fanex.Affiliate.Admin.Core.DTO.Email;
using Fanex.Affiliate.Admin.Core.Utilities;
using Fanex.Affiliate.Admin.Core.Constants;
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
                param.ToDate = param.ToDate.Value.AddDays(1).AddMilliseconds(-1);
            }

            return _paymentRequestRepository.GetPaymentRequestList(param);
        }

        public PaymentRequestDetail GetPaymentRequestDetail(long transId)
        {
            return _paymentRequestRepository.GetPaymentRequestDetail(transId);
        }

        public ResultInfo UpdatePaymentRequest(PaymentRequestUpdateParam param, PaymentRequestUpdateEmail emailParam)
        {
            var isApproved = string.Equals("approved", param.Status, StringComparison.InvariantCultureIgnoreCase);
            var isRejected = string.Equals("rejected", param.Status, StringComparison.InvariantCultureIgnoreCase);
            var paymentRequestDetail = GetPaymentRequestDetail(param.TransId);

            if ((isApproved || isRejected) && paymentRequestDetail != null &&
                string.Equals("pending", paymentRequestDetail.Status, StringComparison.InvariantCultureIgnoreCase))
            {
                param.AffId = paymentRequestDetail.AffId;
                param.Amount = paymentRequestDetail.Amount;
                param.TransDate = paymentRequestDetail.TransDate;

                var result = emailParam.IsApproved ? _paymentRequestRepository.ApprovePaymentRequest(param) : _paymentRequestRepository.RejectPaymentRequest(param);
                if (result != null)
                {
                    if (result.IsSuccessful)
                    {
                        var affInfo = _affiliateService.GetAffiliateBasicInfo(param.AffId);

                        emailParam.UserSiteUrl = SiteDomainHelper.GetUserSiteUrl(affInfo.AffType);
                        emailParam.FirstName = affInfo.FirstName;
                        emailParam.MailTo = affInfo.Email;
                        emailParam.IsHtml = !affInfo.MailOption;
                        emailParam.Language = affInfo.PreferredLanguage ?? LanguageSystem.DefaultLanguage;

                        emailParam.IsApproved = isApproved;
                        emailParam.TransId = param.TransId;
                        emailParam.TransDate = paymentRequestDetail.TransDate.ToString("MM/dd/yyyy");
                        emailParam.Amount = paymentRequestDetail.Amount;

                        _emailService.SendPaymentRequestUpdateEmail(emailParam);
                    }
                    else
                    {
                        result.Message = GetErrMsg(result.Code);
                    }
                }
                return result;
            }
            return null;
        }

        public static string GetErrMsg(int errCode)
        {
            var message = Lang.ResourceManager.GetString("UpdatePaymentErr" + errCode);

            return message ?? "Update transaction failed!";
        }
    }
}
