namespace Fanex.Affiliate.User.Core.BusinessService
{
    using Commands.Payment;
    using CoreService;
    using Criterias;
    using Data.Repository;
    using DTO;
    using Enum;
    using Fanex.Affiliate.User.Core.Infrastructure;
    using MetalModels.Email;
    using MetalModels.UserPanel;
    using Resources;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Utilities;

    public class PaymentService : BaseDynamicService, IPaymentService
    {
        private const int BeginDay = 9;
        private const int EndDay = 16;

        private readonly IAccountService _accountService;
        private readonly IEmailService _emailService;
        private readonly IDateTimeProvider _dateTimeProvider;

        public PaymentService(
            IDynamicRepository dynamicRepository,
            IAccountService accountService,
            IEmailService emailService,
            IDateTimeProvider dateTimeProvider) : base(dynamicRepository)
        {
            _accountService = accountService;
            _emailService = emailService;
            _dateTimeProvider = dateTimeProvider;
        }

        public IEnumerable<Payment> GetPaymentList(PaymentParam paymentParam)
        {
            var criteria = paymentParam.MapTo<PaymentParam, GetPaymentListCriteria>();
            var result = FetchTData<Payment>(criteria);

            paymentParam.PreferredPayment = criteria.PreferredPayment;
            paymentParam.BalBroughtForward = criteria.BalBroughtForward;
            paymentParam.MinAmount = criteria.MinAmount;

            return result;
        }

        public ResultInfo CancelPaymentRequest(PaymentCancelParam cancelParam, PaymentEmailParam emailParam)
        {
            cancelParam.Status = "Cancelled";

            var resultInfo = ExecuteReturnResultInfo(cancelParam.MapTo<PaymentCancelParam, CancelPaymentRequestCommand>());

            if (resultInfo.IsOk)
            {
                var userInfo = _accountService.GetUserInfoById(cancelParam.AffId, cancelParam.IsInternal);

                if (userInfo != null)
                {
                    emailParam.User = userInfo;
                    emailParam.PaymentType = PaymentRequestType.Cancel;
                    emailParam.MailTo = userInfo.Email;
                }

                SendEmail(emailParam);
            }

            return resultInfo;
        }

        public async Task<ResultInfo> CreatePaymentRequestAsync(PaymentCreationParam paymentCreationParam, PaymentEmailParam emailParam)
        {
            var resultInfo = await ValidatePaymentRequest(paymentCreationParam);

            if (resultInfo.IsOk)
            {
                var param = new PaymentRequestInfo
                {
                    AffId = paymentCreationParam.AffId,
                    Language = paymentCreationParam.Language,
                    IpAddress = paymentCreationParam.Ip,
                    Amount = paymentCreationParam.Amount
                };

                var command = param.MapTo<PaymentRequestInfo, CreatePaymentRequestCommand>();
                resultInfo = ExecuteReturnResultInfo(command);

                if (resultInfo.IsOk)
                {
                    paymentCreationParam.TransId = command.TransId;

                    AssignEmailParam(emailParam, paymentCreationParam, PaymentRequestType.Create);
                    SendEmail(emailParam);
                }
            }

            return resultInfo;
        }
        public bool IsInDateRange()
        {
            var currentDay = _dateTimeProvider.Gmt8Now.Day;
            return (currentDay >= BeginDay && currentDay <= EndDay);
        }

        private async Task<ResultInfo> ValidatePaymentRequest(PaymentCreationParam paymentCreationParam)
        {
            var resultInfo = new ResultInfo { IsOk = false };
            if (!IsInDateRange() && !paymentCreationParam.IsLocalTest)
            {
                resultInfo.Message = Error.PaymentPayoutTimeInvalid;

                return resultInfo;
            }

            if (!IsValidAmount(paymentCreationParam))
            {
                resultInfo.Message = Error.PaymentAmountInvalid;
                return resultInfo;
            }

            var affInfo = _accountService.GetAccountInfo(paymentCreationParam.AffId);
            resultInfo = await _accountService.CheckAndUnlockUserIfLocked(affInfo.PlayerUsername);

            return resultInfo;
        }

        private static bool IsValidAmount(PaymentCreationParam paymentCreationParam)
        {
            return paymentCreationParam.Amount > 0 &&
                paymentCreationParam.Amount >= paymentCreationParam.MinAmount &&
                paymentCreationParam.Amount <= paymentCreationParam.BalBroughtForward;
        }

        private void AssignEmailParam(PaymentEmailParam emailParam, PaymentCreationParam paymentCreationParam, PaymentRequestType paymentType)
        {
            var userInfo = _accountService.GetUserInfoById(paymentCreationParam.AffId, paymentCreationParam.IsInternal);

            if (userInfo != null)
            {
                emailParam.User = userInfo;
                emailParam.PaymentType = paymentType;
                emailParam.MailTo = userInfo.Email;
                emailParam.TransId = paymentCreationParam.TransId;
                emailParam.Amount = paymentCreationParam.Amount;
                emailParam.RequestDate = paymentCreationParam.TransDate ?? _dateTimeProvider.Gmt8Now;
            }
        }

        private void SendEmail(PaymentEmailParam emailParam)
        {
            if (!string.IsNullOrEmpty(emailParam.MailTo))
            {
                _emailService.SendPaymentEmail(emailParam);
            }
        }
    }
}
