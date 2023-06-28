using Fanex.Affiliate.Admin.Core.Constants;
using Fanex.Affiliate.Admin.Core.DTO;
using Fanex.Affiliate.Admin.Core.DTO.Email;
using Fanex.Affiliate.Admin.Core.DTO.Payment;
using Fanex.Affiliate.Admin.Core.MetaModels.Payment;
using Fanex.Affiliate.Admin.Core.Repository.Affiliate;
using Fanex.Affiliate.Admin.Core.Resources;
using Fanex.Affiliate.Admin.Core.Utilities;
using Newtonsoft.Json;
using System;
using System.Text;

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
            return _paymentRequestRepository.GetPaymentRequestList(param);
        }

        public PaymentRequestDetail GetPaymentRequestDetail(long transId)
        {
            return _paymentRequestRepository.GetPaymentRequestDetail(transId);
        }

        public ResultInfo UpdatePaymentRequest(PaymentUpdateParam param, PaymentRequestUpdateEmail emailParam)
        {
            var paymentRequestDetail = _paymentRequestRepository.GetPaymentRequestDetail(param.TransId);

            if (paymentRequestDetail != null &&
                string.Equals(AffStatus.Pending, paymentRequestDetail.Status, StringComparison.InvariantCultureIgnoreCase))
            {
                param.Status = param.IsApproved ? AffStatus.Approved : AffStatus.Rejected;

                var result = _paymentRequestRepository.UpdatePaymentRequest(param);

                if (result.IsSuccessful)
                {
                    var logParam = new PaymentUpdateLogParam
                    {
                        AffId = paymentRequestDetail.AffId,
                        UserId = param.UserId,
                        Ip = param.Ip,
                        OldData = result.OldData,
                        NewData = result.NewData
                    };
                    var paymentEmailParam = GetPaymentRequestUpdateEmailParam(param.TransId, emailParam, paymentRequestDetail);

                    return param.IsApproved
                        ? HandleApprovePaymentRequest(result, logParam, paymentEmailParam, param)
                        : HandleRejectPaymentRequest(logParam, paymentEmailParam);
                }
                else
                {
                    result.Message = GetErrMsg(result.Code, result.Message);
                }

                return result;
            }
            return null;
        }

        private ResultInfo HandleRejectPaymentRequest(PaymentUpdateLogParam logParam, PaymentRequestUpdateEmail emailParam)
        {
            _emailService.SendRejectedPaymentRequestEmail(emailParam);
            _paymentRequestRepository.WriteLogWhenUpdatePaymentRequestSuccessfully(logParam);

            return new ResultInfo { Message = "Reject the Payment Request is successful!" };
        }

        private ResultInfo HandleApprovePaymentRequest(PaymentUpdateResult result, PaymentUpdateLogParam logParam, PaymentRequestUpdateEmail emailParam, PaymentUpdateParam param)
        {
            var approveParam = XmlHelper.GetPaymentApproveParam(result.DetailXml);

            if (approveParam is null)
            {
                WriteErrorLogSentry($"Update the Payment Request is failed: DetailXml is incorrect format", param, result.DetailXml);

                return ResultInfo.GenerateErr(Lang.ErrGeneral);
            }

            try
            {
                approveParam.SpKey = GetPaymentApproveSpKeyByCustId(approveParam.PlayerIbcCustId);
                var approvedResult = _paymentRequestRepository.ApprovePaymentRequest(approveParam);

                if (approvedResult.IsSuccessful)
                {
                    approvedResult.Message = "Approve the Payment Request is successful!";

                    _emailService.SendApprovedPaymentRequestEmail(emailParam);
                    _paymentRequestRepository.WriteLogWhenUpdatePaymentRequestSuccessfully(logParam);
                }
                else
                {
                    approvedResult.Message = GetErrMsg(approvedResult.Code, approvedResult.Message);

                    WriteErrorLogSentry($"Approve the Payment Request is failed: ErrorCode: {approvedResult.Code}, ErrorMessage:{approvedResult.Message}", approveParam, result.DetailXml);
                    HandleRollbackApprovePaymentRequest(result.DetailXml);
                }

                return approvedResult;
            } catch (Exception ex)
            {
                LogHelper.WriteError(ex);

                return ResultInfo.GenerateErr(Lang.ErrGeneral);
            }
        }

        private void HandleRollbackApprovePaymentRequest(string detailXml)
        {
            var result = _paymentRequestRepository.RollbackApprovePaymentRequest(detailXml);

            if (!result.IsSuccessful)
            {
                LogHelper.WriteError($"Rollback approve the Payment Request is failed: ErrorCode: {result.Code}, ErrorMessage:{result.Message}");
            }
        }

        private string GetPaymentApproveSpKeyByCustId(string playerIbcCustId)
        {
            const string ApprovePaymentRequest_SpKey = "Aff_Admin_AffiliatePaymentRequest_Approve";

            var siteDbConfig = _paymentRequestRepository.GetSiteDBConfigInfoByCustId(playerIbcCustId);

            return $"{ApprovePaymentRequest_SpKey}_{siteDbConfig.LinkedServerName}";
        }

        private PaymentRequestUpdateEmail GetPaymentRequestUpdateEmailParam(long transId, PaymentRequestUpdateEmail emailParam, PaymentRequestDetail paymentRequestDetail)
        {
            var affInfo = _affiliateService.GetAffiliateBasicInfo(paymentRequestDetail.AffId);

            emailParam.UserSiteUrl = SiteDomainHelper.GetUserSiteUrl(affInfo.AffType);
            emailParam.FirstName = affInfo.FirstName;
            emailParam.MailTo = affInfo.Email;
            emailParam.IsHtml = !affInfo.MailOption;
            emailParam.Language = affInfo.PreferredLanguage ?? LanguageSystem.DefaultLanguageCode;
            emailParam.TransId = transId;
            emailParam.TransDate = paymentRequestDetail.TransDate.ToString("MM/dd/yyyy");
            emailParam.Amount = paymentRequestDetail.Amount;

            return emailParam;
        }

        private static string GetErrMsg(int errCode, string errMessage)
        {
            var message = Lang.ResourceManager.GetString("UpdatePaymentErr" + errCode);

            return message ?? errMessage ?? "Update the Payment Request is failed!";
        }

        private static void WriteErrorLogSentry(string errMessage, object param, string detailXml)
        {
            var messageLog = new StringBuilder().AppendLine(errMessage).AppendLine();
            messageLog.AppendLine($"Parameters: {JsonConvert.SerializeObject(param)}");
            messageLog.AppendLine($"DetailXml: {detailXml}");

            LogHelper.WriteError(messageLog.ToString());
        }
    }
}
