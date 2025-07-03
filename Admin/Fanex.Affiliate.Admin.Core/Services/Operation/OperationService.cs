namespace Fanex.Affiliate.Admin.Core.Services.Operation
{
    using System.Collections.Generic;
    using System.Linq;
    using Data.Repository;
    using Fanex.Affiliate.Admin.Core.Commands.Operation;
    using Fanex.Affiliate.Admin.Core.Criterias.Operation;
    using Fanex.Affiliate.Admin.Core.DTO;
    using Fanex.Affiliate.Admin.Core.DTO.CommissionGroup;
    using Fanex.Affiliate.Admin.Core.DTO.Operation;
    using Fanex.Affiliate.Admin.Core.MetaModels.Operation;
    using Fanex.Affiliate.Admin.Core.Services.Common;
    using Fanex.Affiliate.Admin.Core.Utilities.Operation;

    public class OperationService : BaseDynamicService, IOperationService
    {
        private const int OneHundred = 100;

        private readonly ISystemService _systemService;

        public OperationService(
            IDynamicRepository dynamicRepository,
            ISystemService systemService) : base(dynamicRepository)
        {
            _systemService = systemService;
        }

        public IEnumerable<DealType> GetDealTypes()
        {
            var data = FetchTData<DealType>("Aff_Admin_Deal_GroupType_Sel");
            return data.OrderBy(_ => _.GroupTypeName);
        }

        public IEnumerable<PaymentFrequency> GetPaymentFrequency()
        {
            const string spKey = "Aff_Admin_Deal_Schedule_Sel";

            return FetchTData<PaymentFrequency>(spKey);
        }

        public EntityResult<DealInfo> GetDeals(DealParam param)
        {
            var criteria = new DealCriteria
            {
                GroupId = param.GroupId,
                GroupName = param.GroupName,
                GroupTypeId = param.GroupTypeId,
                PageIndex = param.PageIndex,
                PageSize = param.PageSize
            };

            return FetchEntityResult<DealInfo>(criteria);
        }

        public IEnumerable<DeductionItem> GetDeductionList()
        {
            const string spKey = "Aff_Admin_CommGroup_Deductions_Sel";

            return FetchTData<DeductionItem>(spKey);
        }

        public DealSettingDetail GetDealSettingDetail(int groupId)
        {
            var criteria = new DealDetailCriteria
            {
                GroupId = groupId
            };

            var (dealSettingInfoList, revenueTierSettingList, revenueDeductionSettingList, cpaTierSettingList, cpaGroupProductList)
                = DynamicRepository.FetchMultiple<DealSettingDetail, RevenueTierSetting, RevenueDeductionSetting, CpaTierSetting, CpaGroupProduct>(criteria);

            var dealSettingInfo = dealSettingInfoList.FirstOrDefault();
            if (dealSettingInfo == null)
            {
                return null;
            }

            dealSettingInfo.CPATierSetting = cpaTierSettingList;
            dealSettingInfo.ProductGroup = cpaGroupProductList;
            dealSettingInfo.RevenueTierSetting = revenueTierSettingList;

            if (dealSettingInfo.DealType != Constants.DealType.CPA && revenueDeductionSettingList != null)
            {
                dealSettingInfo.PlatformFeeSetting = revenueDeductionSettingList.Where(p =>
                    p.DeductionId == Constants.Deduction.PlatformFeeId);

                dealSettingInfo.DepositFeeSetting = revenueDeductionSettingList.FirstOrDefault(p =>
                    p.DeductionId == Constants.Deduction.DepositFeeId);

                dealSettingInfo.WithdrawalFeeSetting = revenueDeductionSettingList.FirstOrDefault(p =>
                    p.DeductionId == Constants.Deduction.WithdrawalFeeId);
            }

            return dealSettingInfo;
        }

        public ResultInfo AddDeal(DealSettingParam param)
        {
            var (isValid, message) = ValidateDeal(param);
            if (!isValid)
            {
                return ResultInfo.GenerateErr(message);
            }

            var command = new InsertDealCommand();
            AssignCommonDeal(command, param);
            command.GroupTypeId = param.DealType;

            AssignDealByType(command, param, isAdd: true);

            return ExecuteReturnResultInfo(command);
        }

        public ResultInfo UpdateDeal(DealSettingParam param)
        {
            var (isValid, message) = ValidateDeal(param);
            if (!isValid)
            {
                return ResultInfo.GenerateErr(message);
            }

            var command = new UpdateDealCommand();
            AssignCommonDeal(command, param);

            AssignDealByType(command, param, isAdd: false);

            return ExecuteReturnResultInfo(command);
        }

        private static void AssignDealByType(dynamic command, DealSettingParam param, bool isAdd)
        {
            switch (param.DealType)
            {
                case Constants.DealType.CPA:
                    AssignCpaDeal(command, param);
                    if (isAdd)
                    {
                        command.GroupId = 0;
                        command.ScheduleId = param.ScheduleId;
                        command.StartDate = param.StartDate;
                    }
                    break;

                case Constants.DealType.RevenueShare:
                    AssignRevenueShareDeal(command, param);
                    break;

                default:
                    AssignCpaDeal(command, param);
                    AssignRevenueShareDeal(command, param);
                    if (isAdd)
                    {
                        command.GroupId = 0;
                        command.ScheduleId = param.ScheduleId;
                        command.StartDate = param.StartDate;
                    }
                    break;
            }
        }

        public ResultInfo DeleteDeal(DeleteDealParam param)
        {
            var command = new DeleteDealCommand
            {
                GroupId = param.DealId,
                UserId = param.UserId,
                Ip = param.Ip
            };

            return ExecuteReturnResultInfo(command);
        }

        private (bool isValid, string message) ValidateDeal(DealSettingParam dealInfo)
        {
            var messageList = new List<string>();
            if (dealInfo == null)
            {
                return (false, "Deal information cannot be null.");
            }

            var validateResult = DealInfoValidator.ValidateGroupName(dealInfo.DealName);
            if (!validateResult.isValid)
            {
                messageList.AddRange(validateResult.messageList);
            }

            if (dealInfo.DealType == Constants.DealType.CPA)
            {
                validateResult = DealInfoValidator.ValidateCPAInfo(dealInfo);
            }
            else if (dealInfo.DealType == Constants.DealType.RevenueShare)
            {
                validateResult = DealInfoValidator.ValidateRevenueShare(dealInfo, _systemService.GetProductList());
            }
            else
            {
                validateResult = DealInfoValidator.ValidateHybridDeal(dealInfo, _systemService.GetProductList());
            }

            if (!validateResult.isValid)
            {
                messageList.AddRange(validateResult.messageList);
            }

            var isValid = messageList.Count == 0;
            var message = isValid ? string.Empty : string.Join("</br>", messageList);

            return (isValid, message);
        }

        private static void AssignCommonDeal(BaseDealCommand command, DealSettingParam param)
        {
            command.GroupId = param.GroupId;
            command.GroupName = param.DealName;
            command.SubCommRate = param.SubCommRate;
            command.Description = param.Description;
            command.UserId = param.UserId;
            command.Ip = param.Ip;
        }

        private static void AssignCpaDeal(BaseDealCommand command, DealSettingParam param)
        {
            command.CountryCodes = param.CountryCodes;
            command.ActiveDays = param.ActiveDays;
            command.HoldingDays = param.HoldingDays;
            command.HoldingTickets = param.HoldingTickets;
            command.Quota = param.Quota;
            command.CpaTierList = DealInfoHelper.BuildCpaTierListXml(param.CPATierSetting, param.CheckedMinFdm, param.CheckedMinTotalDeposit, param.CheckedMinTurnover);
            command.CpaProductList = DealInfoHelper.BuildCpaProductListXml(param.ProductGroup);
            command.RedepositDays = param.RedepositDays;
            command.RedepositRate = param.RedepositRate/OneHundred;
        }

        private static void AssignRevenueShareDeal(BaseDealCommand command, DealSettingParam param)
        {
            var (tierSettingXml, tierDeductionsXml) = GetRevenueShareSettingXmls(param);

            command.QMTurnover = param.QmTurnover;
            command.IsActivePlayerSum = param.IsActivePlayerSum;
            command.TierList = tierSettingXml;
            command.DeductionList = tierDeductionsXml;
        }

        private static (string tierSettingXml, string tierDeductionsXml) GetRevenueShareSettingXmls(DealSettingParam param)
        {
            var tierSettingXml = DealInfoHelper.BuildRevenueShareTierSettingXml(param.RevenueTierSetting, param.PlatformFeeSetting);

            var tierDeductionSettingList = param.PlatformFeeSetting.Concat(new[] { param.DepositFeeSetting, param.WithdrawalFeeSetting });

            var tierDeductionsXml = DealInfoHelper.BuildRevenueShareTierDeductionSettingXml(tierDeductionSettingList);

            return (tierSettingXml, tierDeductionsXml);
        }
    }
}
